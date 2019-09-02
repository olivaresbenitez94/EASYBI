using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static EasyBI.DAL.OBJECTS.Extraction;

namespace EasyBI.DAL.OBJECTS
{
	public class Table
	{
		#region Properties
		//[BsonId]
		//[BsonRepresentation(BsonType.ObjectId)]
		public ObjectId _id { get; set; }
		public enum TableType
		{
			Landing,
			Dimension_fact
		}

		public Metadata metadata = new Metadata();
		public List<TableColumn> columns = new List<TableColumn>();
		public class Metadata
		{
			public int folder;
			public int user;
			public ObjectId extraction;	
			public String name;
			public DateTime createDate;
			public DateTime modifiedDate;
		}
		public class TableColumn
		{	public enum ColumnType
			{
				varchar,
				integer,
				numeric,
				datetime,
				datetime2,
				text,
				nvarchar,
				xml,
				ntext
			}

			public int position;
			public bool Nullable;
			public ColumnType type;
			public int? length;
			public int? precision;
			public string name;

			public TableColumn(string name, int position, int length = 20)
			{
				this.name = name;
				this.position = position;
				this.Nullable = true;
				this.type = ColumnType.varchar;
				this.length = length;
				this.precision = null;
			}

			public TableColumn(Column column)
			{
				this.name = column.name;
				this.position = column.position;
				this.Nullable = true;
				this.type = ColumnType.varchar;
				this.length = column.maxLength;

				switch (column.inferedType)
				{
					case Column.Type.integer:
						this.precision = null;
						this.length = null;
						break;
					case Column.Type.numeric:
						this.precision = 2;
						this.length = 10;
						break;
					case Column.Type.datetime:
						this.type = ColumnType.datetime;
						this.precision = null;
						this.length = null;
						break;
					default:
						break;
				}
			}

			public TableColumn()
			{
			}
		}
		#endregion	

		#region métodos
		public static List<Table> getAllDocuments(int user)
		{
			var conString = "mongodb://localhost:27017";
			var Client = new MongoClient(conString);
			var DB = Client.GetDatabase("EASYBI");
			var collection = DB.GetCollection<BsonDocument>("TABLE");
			var builder = Builders<BsonDocument>.Filter;
			var filter = new BsonDocument("metadata.user", user);
			var list = collection.Find(filter).ToList();

			List<Table> tables = new List<Table>();
			foreach (var doc in list)
			{
				Table table = BsonSerializer.Deserialize<Table>(doc);
				tables.Add(table);

			}

			return tables;
		}
		public static List<Table> getDocuments(ObjectId extractionId)
		{
			var conString = "mongodb://localhost:27017";
			var Client = new MongoClient(conString);
			var DB = Client.GetDatabase("EASYBI");
			var collection = DB.GetCollection<BsonDocument>("TABLE");
			var builder = Builders<BsonDocument>.Filter;
			var filter = new BsonDocument("metadata.extraction", extractionId);
			var list = collection.Find(filter).ToList();

			List<Table> tables = new List<Table>();
			foreach (var doc in list)
			{
				Table table = BsonSerializer.Deserialize<Table>(doc);
				tables.Add(table);

			}

			return tables;
		}

		public static List<Table> getTables(ObjectId idExtraction, List<Table> allTables)
		{
			return allTables.Where(tab => tab.metadata.extraction == idExtraction).ToList();
		}

		public static DataTable TableToDatatable(Table table, bool landing = false)
		{
			DataTable dataTable = new DataTable();
			
			dataTable.Columns.Add("Position", typeof(int));
			dataTable.Columns.Add("Name", typeof(string));
			dataTable.Columns.Add("Type", typeof(string));
			dataTable.Columns.Add("Length", typeof(int));
			if (!landing)
				dataTable.Columns.Add("Precision", typeof(int));
			dataTable.Columns.Add("Nullable", typeof(bool));

			foreach (TableColumn column in table.columns)
			{
				DataRow dataRow = dataTable.NewRow();
				dataRow["Position"] = column.position;
				dataRow["Name"] = column.name;			
				dataRow["Type"] = landing ? TableColumn.ColumnType.varchar.ToString() : column.type.ToString();
				dataRow["Length"] = (column.length == null) ? 0 : column.length;
				if(!landing)
					dataRow["Precision"] = (column.precision == null) ? 0 : column.precision;
				dataRow["Nullable"] = column.Nullable;

				dataTable.Rows.Add(dataRow);
			}

			return dataTable;
		}
	
		public static Table ExtractionToTable(Extraction ext,bool landing = false)
		{
			Table table = new Table();
			
			Metadata metadata = new Metadata();
			metadata.createDate = DateTime.Now;
			metadata.modifiedDate = DateTime.Now;
			metadata.folder = ext.metadata.folder;
			metadata.user = ext.metadata.user;
			metadata.name = ext.metadata.name.Split('.').FirstOrDefault() ?? "name";	
			metadata.extraction = ext._id;

			List<TableColumn> columns = new List<TableColumn>();

			foreach (Column column in ext.Columns)
			{
				if(!landing)
					columns.Add(new TableColumn(column));
				else
					columns.Add(new TableColumn(column.name, column.position, column.maxLength));
			}

			table.columns = columns;
			table.metadata = metadata;

			return table;
		}

		public static void InsertTable(Table table)
		{
			Extraction.InsertAsync(table, "TABLE");
		}

		public static void UpdateTable(Table table)
		{
			Extraction.UpdateObject(table._id, table, "TABLE");
		}

		public static void DeleteTable(Table table)
		{
			Extraction.DeleteObject(table._id, "TABLE");
		}

		public static DataGridView DataTableToGrid (TableType tableType = TableType.Dimension_fact)
		{
			DataGridView grid = new DataGridView();

			DataGridViewTextBoxColumn position = new DataGridViewTextBoxColumn();
			position.Name = "Position";
			position.HeaderText = "Position";
			position.DataPropertyName = "Position";
			position.ValueType = typeof(int);
			position.ReadOnly = true;
			position.SortMode = DataGridViewColumnSortMode.NotSortable;

			grid.Columns.Add(position);

			DataGridViewTextBoxColumn name = new DataGridViewTextBoxColumn();
			name.Name = "Name";
			name.HeaderText = "Name";
			name.DataPropertyName = "Name";
			name.ValueType = typeof(string);
			name.SortMode = DataGridViewColumnSortMode.NotSortable;

			grid.Columns.Add(name);

			// Type comboboxcolumn
			DataGridViewComboBoxColumn typeColumn = new DataGridViewComboBoxColumn();
			List<string> types = Enum.GetValues(typeof(DAL.OBJECTS.Table.TableColumn.ColumnType)).
				Cast<DAL.OBJECTS.Table.TableColumn.ColumnType>().
				Select(type => type.ToString()).ToList();

			typeColumn.Name = "Type";
			typeColumn.DataSource = types;
			typeColumn.HeaderText = "Type";
			typeColumn.DataPropertyName = "Type";
			typeColumn.ValueType = typeof(string);
			typeColumn.SortMode = DataGridViewColumnSortMode.NotSortable;

			grid.Columns.Add(typeColumn);

			DataGridViewTextBoxColumn length = new DataGridViewTextBoxColumn();
			length.Name = "Length";
			length.HeaderText = "Length";
			length.DataPropertyName = "Length";
			length.ValueType = typeof(int);
			length.SortMode = DataGridViewColumnSortMode.NotSortable;

			grid.Columns.Add(length);

			DataGridViewTextBoxColumn precision = new DataGridViewTextBoxColumn();
			precision.Name = "Precision";
			precision.HeaderText = "Precision";
			precision.DataPropertyName = "Precision";
			precision.ValueType = typeof(int);
			precision.SortMode = DataGridViewColumnSortMode.NotSortable;

			grid.Columns.Add(precision);

			DataGridViewCheckBoxColumn nullable = new DataGridViewCheckBoxColumn();
			nullable.Name = "Nullable";
			nullable.HeaderText = "Nullable";
			nullable.DataPropertyName = "Nullable";
			nullable.ValueType = typeof(bool);
			nullable.SortMode = DataGridViewColumnSortMode.NotSortable;

			grid.Columns.Add(nullable);

			grid.AllowUserToAddRows = false;
			grid.AllowUserToDeleteRows = false;
			grid.AllowUserToResizeRows = false;
			grid.AllowUserToOrderColumns = false;

			return grid;	

		}

		public static string TableToSQL(Table table)
		{
			List<string> columns = new List<string>();

			foreach (TableColumn column in table.columns)
			{
				columns.Add(TableColumnToString(column));
			}

			return string.Format(" CREATE TABLE {0} ( {1} ); ", table.metadata.name.Replace(".", "").Replace("/","").Replace("\\","").Replace("-", "_"), string.Join(" ,\n", columns));
		}

		public static string GetNullable(bool nullable)
		{
			if (nullable)
			{
				return "NULL";
			}
			else{
				return "NOT NULL";
			}
		}

		public static string TableColumnToString(TableColumn col)
		{
			string result = "";
			string name = col.name.Replace(".", "").Replace("/", "").Replace("\\", "").Replace("-", "_");
			switch (col.type)
			{
				case TableColumn.ColumnType.varchar:
					result = string.Format("[{0}] [varchar]({1}) {2}", name, (col.length > 2000? "max":col.length.ToString()), GetNullable(col.Nullable));
					break;
				case TableColumn.ColumnType.integer:
					result = string.Format("[{0}] [int] {1}", name, GetNullable(col.Nullable));
					break;
				case TableColumn.ColumnType.numeric:
					result = string.Format("[{0}] [numeric]({1},{2}) {3}", name, (col.length > 38 ? 38 : col.length), (col.precision > 38 ? 38 : col.precision), GetNullable(col.Nullable));
					break;
				case TableColumn.ColumnType.datetime:
					result = string.Format("[{0}] [datetime] {1}", name, GetNullable(col.Nullable));
					break;
				case TableColumn.ColumnType.datetime2:
					result = string.Format("[{0}] [datetime2](7) {1}", name, GetNullable(col.Nullable));
					break;
				case TableColumn.ColumnType.text:
					result = string.Format("[{0}] [text] {1}", name, GetNullable(col.Nullable));
					break;
				case TableColumn.ColumnType.nvarchar:
					result = string.Format("[{0}] [nvarchar]({1}) {2}", name, (col.length > 2000 ? "max" : col.length.ToString()), GetNullable(col.Nullable));
					break;
				case TableColumn.ColumnType.xml:
					result = string.Format("[{0}] [xml] {1}", name, GetNullable(col.Nullable));
					break;
				case TableColumn.ColumnType.ntext:
					result = string.Format("[{0}] [ntext] {1}", name, GetNullable(col.Nullable));
					break;
				default:
					break;
			}
			return result;
		}

		public static Table GridToTable(DataGridView grid, string name, int folder, ObjectId extractionId, int user, Table tab =  null)
		{
			Table table = new Table();

			foreach (DataGridViewRow row in grid.Rows)
			{
				string ColumnName = row.Cells["Name"].Value.ToString();
				int Position = Parser.toInt(row.Cells["Position"].Value.ToString());
				int Precision = 0;
				Parser.toInt(row.Cells["Precision"].Value.ToString());
				int Length = Parser.toInt(row.Cells["Length"].Value.ToString());
				bool Nullable = Convert.ToBoolean(row.Cells["Nullable"].Value.ToString());
				string type = row.Cells["Type"].Value.ToString();

				TableColumn tableColumn = new TableColumn();
				tableColumn.name = ColumnName;
				tableColumn.position = Position;
				tableColumn.precision = Precision;
				tableColumn.length = Length;
				tableColumn.Nullable = Nullable;
				tableColumn.type = (TableColumn.ColumnType)Enum.Parse(typeof(TableColumn.ColumnType), type);

				table.columns.Add(tableColumn);
			}

			Metadata metadata = new Metadata();
			metadata.name = name;
			metadata.folder = folder;
			metadata.modifiedDate = DateTime.Now;
			metadata.extraction = extractionId;
			metadata.user = user;

			if(tab != null)
			{
				table._id = tab._id;
			}
			else
			{
				metadata.createDate = DateTime.Now;
			}

			table.metadata = metadata;

			return table;
		}

		#endregion

	}
}
