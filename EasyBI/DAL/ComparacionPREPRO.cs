using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


namespace EasyBI.DAL
{
    public class ComparacionPREPRO
    {
        public static string PREconnection = @"Server=172.25.20.69;Database={0};User Id=DEV_RC_FBLC_COMUNFBLC_00;Password=ccCI2cfWUcSfxCqBS;";
        public static string PROconnection = @"Server=172.25.20.73;Database={0};User Id=PRD_R_FBLC01;Password=MaKwQ440l6tk4JlINwsr;";

        public class Table
        {
            public string Name;
            public string DataBase;
            public List<Column> Columns;
        }

        public class Column
        {
            public string COLUMN_NAME;
            public bool? IS_NULLABLE;
            public string DATA_TYPE;
            public int? CHARACTER_MAXIMUM_LENGTH;
            public int? NUMERIC_PRECISION;
            public int? ORDINAL_POSITION;
        }


        public class AlterTable
        {
            public string Name;
            public string DataBase;
            public List<ColumnEdit> Columns;
        }

        public class ColumnEdit
        {
            public ColumnEdit(Column col, int est)
            {
                column = col;
                estado = est;
            }

            public Column column;
            public int estado; // 0 nueva, 1 borrar, 2 editar          
        }

        public static List<Table> getComparacion(List<string> dataBases,bool tablasNuevas, bool tablasExistentes)
        {
            List<Table> diferentes = new List<Table>();
         
            foreach (string dataBase in dataBases)
            {
                List<Table> pre = getTables(DBOSql.getTablesInfo(string.Format(PREconnection,dataBase)),dataBase);
                List<Table> pro = getTables(DBOSql.getTablesInfo(string.Format(PROconnection, dataBase)),dataBase);

                if (tablasExistentes)
                {
                    foreach(Table tabla_pre in pre.Where(t => pro.Select(p => p.Name).Contains(t.Name)))
                    {
                        if(!compareTables(tabla_pre, pro.Where(tab => tab.Name == tabla_pre.Name).FirstOrDefault()))
                        {
                            diferentes.Add(tabla_pre);
                        }
                    }
                }
                
                if (tablasNuevas)
                {
                    foreach (Table tabla_pre in pre.Where(t => !pro.Select(p => p.Name).Contains(t.Name)))
                    {
                        diferentes.Add(tabla_pre);
                    }
                }
            }

            return diferentes;
        }


        // true--> iguales, false --> diferentes
        public static bool compareTables(Table pre, Table pro)
        {         
            if(compareTable(pre,pro)==false || compareTable(pro, pre) == false)
            {
                return false;
            }

            return true;
        }

        public static bool compareTable(Table tab1, Table tab2)
        {
            foreach (Column column_1 in tab1.Columns)
            {
                Column column_2 = tab2.Columns.Where(col => col.COLUMN_NAME == column_1.COLUMN_NAME).FirstOrDefault();

                if (column_2 == null)
                {
                    return false;
                }
                
                if(column_1.CHARACTER_MAXIMUM_LENGTH != column_2.CHARACTER_MAXIMUM_LENGTH ||
                    column_1.DATA_TYPE != column_2.DATA_TYPE ||
                    column_1.IS_NULLABLE != column_2.IS_NULLABLE ||
                    column_1.NUMERIC_PRECISION != column_2.NUMERIC_PRECISION)
                {
                    return false;
                }
            }

            return true;
        }

        public static List<ColumnEdit> CompareTables(List<Column> cols1, List<Column> cols2)
        {
            List<ColumnEdit> columns = new List<ColumnEdit>();

            foreach (Column column_1 in cols1)
            {
                Column column_2 = cols2.Where(col => col.COLUMN_NAME == column_1.COLUMN_NAME).FirstOrDefault();

                if (column_2 == null)
                {
                    continue;
                }

                if (column_1.CHARACTER_MAXIMUM_LENGTH != column_2.CHARACTER_MAXIMUM_LENGTH ||
                    column_1.DATA_TYPE != column_2.DATA_TYPE ||
                    column_1.IS_NULLABLE != column_2.IS_NULLABLE ||
                    column_1.NUMERIC_PRECISION != column_2.NUMERIC_PRECISION)
                {
                    ColumnEdit col = new ColumnEdit(column_1,2);
                    columns.Add(col);
                }
            }

            return columns;
        }

        public static string getColumn(Column col)
        {
            string query = "";

            string NULL = ((bool)col.IS_NULLABLE ? "NULL" : "NOT NULL");

            if(col.CHARACTER_MAXIMUM_LENGTH == 0 && col.NUMERIC_PRECISION == 0) // datetime
            {
                query = string.Format("{0} {1} {2} ", col.COLUMN_NAME, col.DATA_TYPE, NULL);
            }

            if(col.CHARACTER_MAXIMUM_LENGTH != 0 && col.NUMERIC_PRECISION == 0) // string
            {
                query = string.Format("{0} {1} ({2}) {3} ", col.COLUMN_NAME, col.DATA_TYPE,col.CHARACTER_MAXIMUM_LENGTH, NULL);
            }

            if(col.CHARACTER_MAXIMUM_LENGTH == 0 && col.NUMERIC_PRECISION != 0) // numeric
            {
                query = string.Format("{0} {1} ({2},0) {3} ", col.COLUMN_NAME, col.DATA_TYPE, col.NUMERIC_PRECISION, NULL);
            }

            return query;
        }

        public static List<Table> getTables(DataTable table,string BBDD)
        {
            List<Table> returnTables = new List<Table>();

            List<string> tables = table?.AsEnumerable()?.Select(r => r["TABLE_NAME"].ToString())?.Distinct()?.ToList()??new List<string>();

            foreach(string tab in tables)
            {
                Table tabla = new Table();
                tabla.Name = tab;
                tabla.DataBase = BBDD;

                List<Column> columns = new List<Column>();
                foreach(DataRow row in table.AsEnumerable().Where(c => c["TABLE_NAME"].ToString() == tab))
                {
                    Column column = new Column();
                    column.COLUMN_NAME = row["COLUMN_NAME"].ToString();
                    column.DATA_TYPE = row["DATA_TYPE"].ToString();
                    column.CHARACTER_MAXIMUM_LENGTH = Parser.toInt(row["CHARACTER_MAXIMUM_LENGTH"].ToString());
                    column.IS_NULLABLE = isNullable(row["IS_NULLABLE"].ToString());
                    column.NUMERIC_PRECISION = Parser.toInt(row["NUMERIC_PRECISION"].ToString());
                    column.ORDINAL_POSITION = Parser.toInt(row["ORDINAL_POSITION"].ToString());

                    columns.Add(column);
                }

                tabla.Columns = columns;
                returnTables.Add(tabla);

            }

            return returnTables;
        }

        public static bool? isNullable(string nullable)
        {
            switch (nullable)
            {
                case "YES":
                    return true;
                case "NO":
                    return false;
                default:
                    return null;
            }
        }
    }
}
