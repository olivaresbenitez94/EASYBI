using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace EasyBI
{
    public class Parser
    {
        public static bool isInt(Object value)
        {
            int aux = 0;
            if (value != null)
            {
                return Int32.TryParse(value.ToString(), out aux);
            }
            else
            {
                return false;
            }

        }

        public static int toInt(Object value)
        {
            int res = 0;
            if (value != null)
            {
                Int32.TryParse(value.ToString(), out res);
            }
            return res;
        }

        public static bool isDate(String value)
        {
            return isDate(value, "dd/MM/yyyy");
        }

        public static bool isDateWithTime(String value)
        {
            return isDate(value, "dd/MM/yyyy HH:mm");
        }

        private static bool isDate(String value, String format)
        {
            DateTime aux;
            return DateTime.TryParseExact(value, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out aux);
        }

        public static String decimalToString(Decimal value,int decimalDigits)
        {
            return value.ToString("0."+ String.Join("",Enumerable.Repeat("0",decimalDigits)));
        }
    }
}