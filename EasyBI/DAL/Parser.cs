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

        public static DateTime serverStringtoDate(String value)
        {
            return toDate(value, "yyyyMMdd");
        }

        public static DateTime readableStringtoDate(String value)
        {
            return toDate(value, "dd/MM/yyyy");
        }

        public static DateTime readableStringtoDateWithTime(String value)
        {
            return toDate(value, "dd/MM/yyyy HH:mm");
        }

        public static DateTime serverStringtoDateWithTime(String value)
        {
            return toDate(value, "yyyyMMdd HH:mm");
        }

        public static DateTime serverStringToTime(String value)
        {
            return toDate(value, "HH:mm:ss");
        }
        
        public static DateTime timestampToDate(String value)
        {
            return toDate(value, "yyyyMMddHHmmss");
        }
        
        public static DateTime iPadStringToDate(String value)
        {
            return toDate(value, "dd/MM/yyyy");
        }

        public static DateTime iPadStringToDateTime(String value)
        {
            return toDate(value, "dd/MM/yyyy H:mm:ss");
        }

        private static DateTime toDate(String value, String format)
        {
            DateTime res;
            if (DateTime.TryParseExact(value, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out res))
            {
                return res;
            }
            else
            {
                return new DateTime(1900, 1, 1);
            }
        }

        public static String dateToiPadString(DateTime date)
        {
            return date.ToString("dd/MM/yyyy");
        }

        public static String dateTimeToiPadString(DateTime date)
        {
            return date.ToString("dd/MM/yyyy H:mm:ss");
        }

        public static bool isDecimal(String value)
        {
            Decimal res;
            return Decimal.TryParse(value.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out res);
        }

        public static Decimal toDecimal(String value)
        {
            Decimal res;
            if (Decimal.TryParse(value.Replace(",","."), NumberStyles.Any, CultureInfo.InvariantCulture, out res))
            {
                return res;
            }
            else
            {
                return 0;
            }
        }

        public static String decimalToString(Decimal value)
        {
            return decimalToString(value, 2);
        }

        public static String decimalToString(Decimal value,int decimalDigits)
        {
            return value.ToString("0."+ String.Join("",Enumerable.Repeat("0",decimalDigits)));
        }
    }
}