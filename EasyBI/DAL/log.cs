using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace EasyBI
{
    public class log
    {
        #region Métodos

        private static String RUTA_LOG = @"C:/Log/";

        public static void insertarLog(Boolean esError, string cadena, int saltosLinea)
        {
            string nombreFichero;
            DateTime fechaActual;
            fechaActual = DateTime.Now;

            //Comprobamos si existe la carpeta donde se almacenan los logs
            if (!Directory.Exists(RUTA_LOG))
            {
                //Creamos la carpeta donde se almacenan los logs
                Directory.CreateDirectory(RUTA_LOG);
            }

            //Generamos el nombre del fichero
            nombreFichero = "log_" + getAnyo(fechaActual) + getMes(fechaActual) + getDia(fechaActual) + ".txt";

            //Abrimos un fichero (si no existe, lo crea)
            StreamWriter stream_writer = new StreamWriter(RUTA_LOG + "/" + nombreFichero, true);

            //Añadimos el mensaje de error
            if (esError)
                cadena = "[ERROR] " + cadena;

            //Añadimos la fecha y hora
            cadena = getDia(fechaActual) + "/" + getMes(fechaActual) + "/" + getAnyo(fechaActual) + " " + getHoras(fechaActual) + ":" + getMinutos(fechaActual) + ":" + getSegundos(fechaActual) + " -> " + cadena;

            //Añadimos los saltos de línea
            for (int i = 0; i < saltosLinea - 1; i++)
            {
                cadena = cadena + "\r\n";
            }

            //Añadimos la cadena
            stream_writer.WriteLine(cadena);
            stream_writer.Close();
        }

        public static void insertarLog(string cadena, Exception ex)
        {
            if (ex != null)
                insertarLog(true, cadena + ": " + ex.Message + " - " + ex.StackTrace, 0);
            else
                insertarLog(false, cadena, 0);
        }

        #endregion

        #region Fecha

        public static string getAnyo(DateTime fecha)
        {
            return Convert.ToString(fecha.Year);
        }

        public static string getMes(DateTime fecha)
        {
            string mes;
            mes = Convert.ToString(fecha.Month);

            if (mes.Length == 1)
                return "0" + mes;
            else
                return mes;
        }

        public static string getDia(DateTime fecha)
        {
            string dia;
            dia = Convert.ToString(fecha.Day);

            if (dia.Length == 1)
                return "0" + dia;
            else
                return dia;
        }

        public static string getHoras(DateTime fecha)
        {
            string horas;
            horas = Convert.ToString(fecha.Hour);

            if (horas.Length == 1)
                return "0" + horas;
            else
                return horas;
        }

        public static string getMinutos(DateTime fecha)
        {
            string minutos;
            minutos = Convert.ToString(fecha.Minute);

            if (minutos.Length == 1)
                return "0" + minutos;
            else
                return minutos;
        }

        public static string getSegundos(DateTime fecha)
        {
            string segundos;
            segundos = Convert.ToString(fecha.Second);

            if (segundos.Length == 1)
                return "0" + segundos;
            else
                return segundos;
        }

        #endregion
    }
}
