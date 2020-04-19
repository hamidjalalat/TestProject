using System.Globalization;

namespace System
{
    public static class ShamsiExtension
    {
        public static string ToHtmlDate(this DateTime? dateTime)
        {
            if (dateTime == null) return "";
            return dateTime.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        public static string ToHtmlDateTime(this DateTime? dateTime)
        {
            if (dateTime == null) return "";
            return dateTime.Value.ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
        }

        public static string ToHtmlDate(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        public static string ToHtmlDateTime(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
        }


        public static string ToHHMM(this DateTime dateTime)
        {
            return dateTime.ToString("HH:mm", CultureInfo.InvariantCulture);
        }

        public static int ShamsiYear(this DateTime dateTime)
        {
            var pc = new PersianCalendar();
            return pc.GetYear(dateTime);
        }
        public static int ShamsiMonth(this DateTime dateTime)
        {
            var pc = new PersianCalendar();
            return pc.GetMonth(dateTime);
        }



        public static string ToShamsi(this DateTime dateTime, string format)
        {
            return Prime.Shamsi.ToString(format, dateTime);
        }

        public static string ToShamsi(this DateTime dateTime)
        {
            return Prime.Shamsi.ToString("yy/MM/dd", dateTime);
        }

        public static string ToShamsiHHMM(this DateTime dateTime)
        {
            return Prime.Shamsi.ToString("yy/MM/dd, HH:mm", dateTime);
        }

        public static string ToShamsiHHMMSS(this DateTime dateTime)
        {
            return Prime.Shamsi.ToString("yy/MM/dd, HH:mm:ss", dateTime);
        }


        
        public static string ToShamsi(this DateTime? dateTime, string format)
        {
            return Prime.Shamsi.ToString(format, dateTime);
        }

        public static string ToShamsi(this DateTime? dateTime)
        {
            return Prime.Shamsi.ToString("yy/MM/dd", dateTime);
        }

        public static string ToShamsiHHMM(this DateTime? dateTime)
        {
            return Prime.Shamsi.ToString("yy/MM/dd, HH:mm", dateTime);
        }

        public static string ToShamsiHHMMSS(this DateTime? dateTime)
        {
            return Prime.Shamsi.ToString("yy/MM/dd, HH:mm:ss", dateTime);
        }
        
    }
}