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

		public static BsonDocument getBsonDocument (Object obj)
		{
			BsonDocument document = obj.ToBsonDocument();
			var jsonDocument = document.ToJson();
			return document;
		}
		public static void usePython()
		{ /*
			string folder = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
			ScriptEngine engine = Python.CreateEngine();
			ICollection<string> paths = engine.GetSearchPaths();
			//string dir = @"C:\Program Files (x86)\Microsoft Visual Studio\Shared\Python36_64\Lib";
			//paths.Add(dir);
			//engine.SetSearchPaths(paths);
			var theScript = "import sys\nsys.path.append(r'C:\\Users\\oliva\\Downloads\\EasyBI\\EasyBI\\packages\\IronPython.2.7.9\\lib')\nimport json\nprint('This is a message!')";


			// execute the script
			try
			{
				engine.Execute(theScript);
			}
			catch (Exception ex)
			{

			}
			*/
		}

		public static string run_cmd(string args, string cmd, string FILENAME = @"C:\Users\oliva\Anaconda3\python.exe")
		{
			bool result = true;
			ProcessStartInfo start = new ProcessStartInfo();
			start.FileName = FILENAME;
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
			/*
			ScriptRuntimeSetup setup = Python.CreateRuntimeSetup(null);
			ScriptRuntime runtime = new ScriptRuntime(setup);
			ScriptEngine engine = Python.GetEngine(runtime);
			ICollection<string> paths = engine.GetSearchPaths();
			string dir = @"C:\Program Files (x86)\Microsoft Visual Studio\Shared\Python36_64\Lib";
			paths.Add(dir);
			engine.SetSearchPaths(paths);
			var theScript = "import sys\nimport json\nprint('This is a message!')";

			ScriptSource source = engine.CreateScriptSourceFromString(theScript);//PYTHON.Python.JSONtoCSVcode()

			ScriptScope scope = engine.CreateScope();
			List<String> argv = new List<String>();
			//Do some stuff and fill argv
			argv.Add(sourcePath);
			argv.Add(targetPath);
			engine.GetSysModule().SetVariable("argv", argv);
			source.Execute(scope);
			*/
			List<String> argv = new List<String>();
			argv.Add(sourcePath);
			argv.Add(targetPath);

			return run_cmd(string.Join(" ", argv), pyPATH);
		}
		public static async void InsertAsync(Object obj, string collectionName = "EXTRACTION",string connectionString = "mongodb://localhost:27017"
			, string databaseName = "EASYBI")
		{
			// Create a MongoClient object by using the connection string
			var client = new MongoClient(connectionString);

			//Use the MongoClient to access the server
			var database = client.GetDatabase(databaseName);
			
			//get mongodb collection
			var collection = database.GetCollection<BsonDocument>(collectionName);

			await collection.InsertOneAsync(getBsonDocument(obj));
			
		}

		public static async void UpdateObject(ObjectId objectId, Object obj, string collectionName = "EXTRACTION", string connectionString = "mongodb://localhost:27017"
			, string databaseName = "EASYBI")
		{
			// Create a MongoClient object by using the connection string
			var client = new MongoClient(connectionString);

			//Use the MongoClient to access the server
			var database = client.GetDatabase(databaseName);

			//get mongodb collection
			var collection = database.GetCollection<BsonDocument>(collectionName);
			var filter = Builders<BsonDocument>.Filter.Eq("_id", objectId);
			await collection.ReplaceOneAsync(filter, getBsonDocument(obj));
		}


		public static async void DeleteObject(ObjectId objectId, string collectionName = "EXTRACTION", string connectionString = "mongodb://localhost:27017"
			, string databaseName = "EASYBI")
		{
			// Create a MongoClient object by using the connection string
			var client = new MongoClient(connectionString);

			//Use the MongoClient to access the server
			var database = client.GetDatabase(databaseName);

			//get mongodb collection
			var collection = database.GetCollection<BsonDocument>(collectionName);

			await collection.DeleteOneAsync(
							 Builders<BsonDocument>.Filter.Eq("_id", objectId));
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
				 DeleteObject(extraction._id);
			}
		}

		// falta filtrar por usuario
		public static List<Extraction> getAllDocuments(int user = 1)
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
		public static bool uploadExtraction(Extraction extraction)
		{
			InsertAsync(extraction);

			return true;
		}
		public static Extraction GetExtractionCSV(string filename, String delimiter, bool lineWithHeaders, int folder)
		{
			Metadata metadata = new Metadata();
			metadata.createDate = DateTime.UtcNow;
			metadata.modifiedDate = DateTime.UtcNow;
			metadata.name = filename.Split('\\').LastOrDefault();
			metadata.user = Users.getUserID(Environment.UserName);
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

			uploadExtraction(extraction);

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

		public static List<Field> getJSONSchema(JObject obj)
		{
			List<Field> fields = new List<Field>();


			if (obj.Type.ToString() == "Object")
			{

			}

			return fields;
		}
		private static Dictionary<string, string> getSchema()
		{

			return new Dictionary<string, string>();
		}
		public static Extraction GetExtractionXML(string filename, String delimiter, bool lineWithHeaders)
		{



			return new Extraction();
		}
		public static Column GetColumn(Column column, string field)
		{
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
					columns.Add(new Column(field, count));
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
