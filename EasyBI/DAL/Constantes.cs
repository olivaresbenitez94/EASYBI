using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBI
{
    public class Constantes
    {

		#region SQLServer

		public static String sqlConnectionString = @"Server=DESKTOP-DEDM1FL\SQLEXPRESS01;Database=EASYBI;Integrated Security=SSPI;persist security info=True;";

		#endregion

		#region Mongo


		public static String mongoConnectionString = "mongodb://localhost:27017";

		public static String pythonExeEnvironment = @"C:\Users\oliva\Anaconda3\python.exe";

		public static String mongoDataBase = "EASIBI";

		public static String mongoExtractionCollection = "EXTRACTION";

		public static String mongoTableCollection = "TABLE";

		#endregion

	}
}
