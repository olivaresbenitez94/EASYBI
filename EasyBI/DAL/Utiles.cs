using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

 namespace EasyBI
{
    public static class Utiles
    {
        public static string[] getArrayFromQuery(String strSql, String field)
        {
            using (DataTable dt = DataAccessSql.ExecuteDataTable(strSql))
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt.Rows.OfType<DataRow>().Select(dr => Convert.ToString(dr[field])).ToArray();
                }
                else
                {
                    return new string[] { };
                }
            }
        }

        public static Tuple<String, String>[] getArrayFromQuery(String strSql, String field1, String field2)
        {
            using (DataTable dt = DataAccessSql.ExecuteDataTable(strSql))
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt.Rows.OfType<DataRow>().Select(dr => 
                        new Tuple<String, String>(Convert.ToString(dr[field1]), Convert.ToString(dr[field2]))).ToArray();
                }
                else
                {
                    return new Tuple<string, string>[] { };
                }
            }
        }

        public static DataTable ToDataTable<T>(this IEnumerable<T> collection)
        {
            DataTable dt = new DataTable();
            Type type = typeof(T);
            PropertyInfo[] pia = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                            .Where(p => p.DeclaringType.Name != "DBOSql" && p.DeclaringType.Name != "DBO").ToArray();
                            

            //Inspect the properties and create the columns in the DataTable
            foreach (PropertyInfo pi in pia)
            {
                Type ColumnType = pi.PropertyType;
                if ((ColumnType.IsGenericType))
                {
                    ColumnType = ColumnType.GetGenericArguments()[0];
                }
                dt.Columns.Add(pi.Name, ColumnType);
            }

            //Populate the data table
            foreach (T item in collection)
            {
                DataRow dr = dt.NewRow();
                dr.BeginEdit();
                foreach (PropertyInfo pi in pia)
                {
                    if (pi.GetValue(item, null) != null)
                    {
                        dr[pi.Name] = pi.GetValue(item, null);
                    }
                }
                dr.EndEdit();
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public static void WriteLogFile(string logMessage)
        {
            try
            {
                string strLogMessage = string.Empty;
                string strLogFile = "C:\\Logs\\Log_LobeDAL.txt";
                StreamWriter swLog;

                strLogMessage = string.Format("{0}: {1}", DateTime.Now, logMessage);

                if (!File.Exists(strLogFile))
                {
                    swLog = new StreamWriter(strLogFile);
                }
                else
                {
                    swLog = File.AppendText(strLogFile);
                }

                swLog.WriteLine(strLogMessage);
                swLog.WriteLine();

                swLog.Close();


            }
            catch
            { }
        }

        public static DateTime GetMonday(DateTime day)
        {
            DateTime res;
            if (day.DayOfWeek != DayOfWeek.Monday)
                res = day.Subtract(new TimeSpan(((int)day.DayOfWeek - 1) % 7, 0, 0, 0));
            else
                res = day;

            return res;
        }
    }
}
