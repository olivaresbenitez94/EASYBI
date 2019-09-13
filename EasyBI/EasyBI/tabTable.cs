using EasyBI.DAL.OBJECTS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBI.EasyBI
{
	public class tabTable
	{
		#region Clases


		#endregion

		#region Métodos
		public static DataTable ExtractionToTable(Extraction extraction)
		{
			DataTable table = new DataTable();

			table.Columns.Add(new DataColumn("Index", typeof(int)));
			table.Columns.Add(new DataColumn("Name", typeof(string)));
			table.Columns.Add(new DataColumn("MinLenght", typeof(int)));
			table.Columns.Add(new DataColumn("MaxLenght", typeof(int)));
			table.Columns.Add(new DataColumn("Type", typeof(string)));

			foreach (Extraction.Column col in extraction.Columns)
			{
				DataRow row = table.NewRow();
				row["Index"] = Parser.toInt(col.position);
				row["Name"] = col.name;
				row["MinLenght"] = Parser.toInt(col.minLength);
				row["MaxLenght"] = Parser.toInt(col.maxLength);
				row["Type"] = col.inferedType.ToString();

				table.Rows.Add(row);
			}

			return table;
		}

		#endregion
	}
}
