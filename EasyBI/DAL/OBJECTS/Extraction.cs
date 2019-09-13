using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Helpers;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using MongoDB.Bson.Serialization;
using System.Windows.Forms;
using System.Diagnostics;
using EasyBI.EasyBI;

namespace EasyBI.DAL.OBJECTS
{
	public class Extraction
	{
		#region Properties
		//[BsonId]
		//[BsonRepresentation(BsonType.ObjectId)]
		public ObjectId _id { get; set; }
		public class Metadata
		{
			public int folder;
			public int user;
			public String name;
			public DateTime createDate;
			public DateTime modifiedDate;
			public int registers;
		}
		public class Column
		{
			public enum Type
			{
				None,
				varchar,
				integer,
				numeric,
				datetime
			}

			public string name;
			public int maxLength;
			public int minLength;
			public int position;
			public Type inferedType;

			public Column(string name, int position)
			{
				this.name = name;
				this.inferedType = Type.varchar;
				this.maxLength = 0;
				this.minLength = 100;
				this.position = position;
			}
		}

		public List<Column> Columns = new List<Column>();

		public Metadata metadata = new Metadata();
		public enum fileType
		{
			CSV,
			JSON,
			XML
		}
		
		#endregion


		public static string Run_cmd(string args, string cmd, string pythonExeEnvironment)
		{
			ProcessStartInfo start = new ProcessStartInfo();
			start.FileName = pythonExeEnvironment;
			start.Arguments = string.Format("{0} {1}", cmd, args);
			start.UseShellExecute = false;
			start.RedirectStandardOutput = true;
			using (Process process = Process.Start(start))
			{
				using (StreamReader reader = process.StandardOutput)
				{
					return  reader.ReadToEnd();
				}
			}
		}

		public static string JSONtoCSV(string pyPATH,string sourcePath, string targetPath)
		{
			List<String> argv = new List<String>();
			argv.Add(sourcePath);
			argv.Add(targetPath);

			return Run_cmd(string.Join(" ", argv), pyPATH, Constantes.pythonExeEnvironment);
		}
		
		public static void DeleteFolders(List<int> Folders, List<Extraction> extractions)
		{
			foreach (int folder in Folders)
			{
				DeleteFolder(folder, extractions);
			}
		}
		public static void DeleteFolder(int Folder, List<Extraction> extrations)
		{		
			foreach (Extraction extraction in extrations.Where(ext => ext.metadata.folder == Folder).ToList())
			{
				DAL.DBOMongo.DeleteObject(extraction._id,Constantes.mongoExtractionCollection, Constantes.mongoConnectionString, Constantes.mongoDataBase);
			}
		}
		public static List<Extraction> GetAllDocuments(int user = 1)
		{
			var conString = "mongodb://localhost:27017";
			var Client = new MongoClient(conString);
			var DB = Client.GetDatabase("EASYBI");
			var collection = DB.GetCollection<BsonDocument>("EXTRACTION");
			var builder = Builders<BsonDocument>.Filter;
			var filter = new BsonDocument("metadata.user", user);

			var list = collection.Find(filter).ToList();
			List<Extraction> extractions = new List<Extraction>();
			foreach (var doc in list)
			{
				Extraction extract = BsonSerializer.Deserialize<Extraction>(doc);
				extractions.Add(extract);

			}

			return extractions;
		}
		public static Extraction extraction(Metadata metadata, List<Column> columns)
		{
			Extraction extraction = new Extraction
			{
				metadata = metadata,
				Columns = columns
			};
			return extraction;
		}
		public static Extraction extraction(string filename, string path, String delimiter, bool lineWithHeaders, int selectedFolder, fileType fileType = fileType.CSV)
		{
			string totalPath = path + "\\" + filename;
			switch (fileType)
			{
				case fileType.CSV:

					return GetExtractionCSV(totalPath, delimiter, lineWithHeaders, selectedFolder);

				case fileType.JSON:
					string folderAppPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
					
					return GetExtractionJSON(Path.Combine(folderAppPath, "JSONtoCSV.py") ,totalPath, delimiter, lineWithHeaders, selectedFolder);

				case fileType.XML:

					return GetExtractionXML(totalPath, delimiter, lineWithHeaders);

				default:
					break;
			}


			return new Extraction();
		}
		public static bool UploadExtraction(Extraction extraction)
		{
			DAL.DBOMongo.InsertAsync(extraction, Constantes.mongoExtractionCollection, Constantes.mongoConnectionString, Constantes.mongoDataBase);

			return true;
		}
		public static Extraction GetExtractionCSV(string filename, String delimiter, bool lineWithHeaders, int folder)
		{
			Metadata metadata = new Metadata();
			metadata.createDate = DateTime.UtcNow;
			metadata.modifiedDate = DateTime.UtcNow;
			metadata.name = filename.Split('\\').LastOrDefault();
			metadata.user = Users.GetUserID(Environment.UserName);
			metadata.folder = folder;

			int count = 0;
			List<Column> columns = null;

			using (var reader = new StreamReader(filename))
			{
				string line;
				while ((line = reader.ReadLine()) != null)
				{
					if (count == 0)
					{
						columns = GetColumns(line, delimiter, lineWithHeaders);
					}
					else
					{
						int columnCount = 0;
						foreach (string field in GetFields(line, delimiter))
						{
							Column aux = null;
							try
							{
								if (columnCount >= columns.Count)
								{
									continue;
								}

								aux = columns[columnCount];
								columns[columnCount] = GetColumn(aux, field);
							}
							catch (Exception exception)
							{

								//throw;
							}

							columnCount++;
						}
					}

					count++;

				}

				metadata.registers = count;
			}


			Extraction extraction =  new Extraction()
			{
				metadata = metadata,
				Columns = columns
			};

			UploadExtraction(extraction);

			return extraction;

		}
		public static Extraction GetExtractionJSON(string pyPath, string filename, String delimiter, bool lineWithHeaders,int folder)
		{
			string targetPath = Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath), filename.Split('\\').Last());

			string result = JSONtoCSV(pyPath, filename, targetPath);

			return GetExtractionCSV(targetPath,",",true,folder);
		}
		public class Field
		{
			public string name;
			public string value;
			public int length;

			public Field(string name, string value, int length)
			{
				this.name = name;
				this.value = value;
				this.length = length;
			}
		}
		public static Extraction GetExtractionXML(string filename, String delimiter, bool lineWithHeaders)
		{



			return new Extraction();
		}
		public static Column GetColumn(Column column, string field)
		{
			field = field.Replace("\"", "");

			if (column.maxLength < field.Length)
			{
				column.maxLength = field.Length;
			}

			if (field.Length < column.minLength)
			{
				column.minLength = field.Length;
			}

			int x;

			if (Int32.TryParse(field, out x))
			{
				column.inferedType = Column.Type.integer;

				double y;
				if (double.TryParse(field, out y))
				{
					column.inferedType = Column.Type.numeric;
				}
			}

			DateTime dat;
			if (DateTime.TryParse(field, out dat))
			{
				column.inferedType = Column.Type.datetime;
			}

			return column;
		}
		public static List<string> GetFields(String line, String delimiter)
		{
			String[] del = new String[1];
			del[0] = delimiter;

			List<string> fields = new List<string>();

			foreach (string field in line.Split(del, StringSplitOptions.None))
			{
				fields.Add(field);
			}

			return fields;
		}
		// pasando la primera linea devuelve la estructura de columnas
		public static List<Column> GetColumns(String firstLine, String delimiter, bool lineWithHeaders)
		{
			String[] del = new String[1];
			del[0] = delimiter;

			List<Column> columns = new List<Column>();

			if (lineWithHeaders)
			{
				int count = 0;
				foreach (string field in firstLine.Split(del, StringSplitOptions.None))
				{
					columns.Add(new Column(field.Replace("\"",""), count));
					count++;
				}
			}
			else
			{
				int length = firstLine.Split(del, StringSplitOptions.None).Length;

				foreach (int fieldNumnber in Enumerable.Range(0, length))
				{
					columns.Add(new Column("Column" + fieldNumnber, fieldNumnber));
				}
			}

			return columns;
		}

	}
}
