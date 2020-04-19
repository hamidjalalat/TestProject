using System;
using System.Data;
using System.Globalization;
using System.Text;

namespace Prime
{
    public static class Shamsi
    {
        private static string[] mMonthNames = { "فروردين", "ارديبهشت", "خرداد", "تير", "مرداد", "شهريور", "مهر", "آبان", "آذر", "دي", "بهمن", "اسفند" };
        private static string[] mAbbreviatedMonthNames = { "فر", "ار", "خر", "تي", "مر", "شه", "مه", "آب", "آذ", "دي", "به", "اس" };
        private static string[] mDayNames = { "يك شنبه", "دو شنبه", "سه شنبه", "چهارشنبه", "پنج‌شنبه", "جمعه", "شنبه" };
        private static string[] mAbbreviatedDayNames = { "يك", "دو", "سه", "چهار", "پنج", "جمعه", "شنبه" };

        private static PersianCalendar pc = new PersianCalendar();


        private static string[] DayNames { get { return mDayNames; } }
        private static string[] AbbreviatedMonthNames { get { return mAbbreviatedMonthNames; } }
        private static string[] AbbreviatedDayNames { get { return mAbbreviatedDayNames; } }
        private static string[] MonthNames { get { return mMonthNames; } }

        public static int Year { get { return pc.GetYear(DateTime.Now); } }
        public static int Month { get { return pc.GetMonth(DateTime.Now); } }
        public static int Day { get { return pc.GetDayOfMonth(DateTime.Now); } }
        public static DayOfWeek DayOfWeek { get { return pc.GetDayOfWeek(DateTime.Now); } }




        public static DateTime ToDateTime(int shamsiYear, int shamsiMonth, int shamsiDay)
        {
            return pc.ToDateTime(shamsiYear, shamsiMonth, shamsiDay, 0, 0, 0, 0);
        }

        /// <summary>
        /// تبديل رشته متني تاريخ شمسي به ميلادي
        /// </summary>
        /// <param name="shamsiY_M_D">تاريخ شمسي كه با / جدا شده است. مانند 1388/4/28. سال مي تواند 2 يا 4 رقمي باشد</param>
        /// <returns></returns>
        public static DateTime ToDateTime(string shamsiY_M_D)
        {
            return ToDateTime(shamsiY_M_D, '/', ',', ':');
        }

        /// <summary>
        /// تبديل رشته متني تاريخ شمسي به ميلادي
        /// </summary>
        /// <param name="yymmdd">تاريخ شمسي 6 رقمي بدون جداكننده. مانند 880428</param>
        /// <returns></returns>
        public static DateTime ToDateTimeYYMMDD(string yymmdd)
        {
            return ToDateTimeYYMMDD(yymmdd, "");
        }

        public static DateTime ToDateTimeYYMMDD(string yymmdd, string hhmmss)
        {
            if (yymmdd == null) throw new ArgumentNullException("yymmdd");
            if (hhmmss == null) throw new ArgumentNullException("hhmmss");
            try
            {
                string sy = yymmdd.Substring(0, 2);
                string sm = yymmdd.Substring(2, 2);
                string sd = yymmdd.Substring(4, 2);

                string hh = hhmmss.Substring(0, 2);
                string mm = hhmmss.Substring(2, 2);
                string ss = hhmmss.Substring(4, 2);

                int y = int.Parse(sy) + 1300;
                int m = int.Parse(sm);
                int d = int.Parse(sd);

                int hour = int.Parse(hh);
                int min = int.Parse(mm);
                int sec = int.Parse(ss);

                if (m < 1 || m > 12 || d < 1 || d > 31)
                    throw new PrimeErrorsException("Invalid Date/Time!");
                return pc.ToDateTime(y, m, d, hour, min, sec, 0);
            }
            catch (Exception ex)
            {
                throw new PrimeErrorsException(string.Format(CultureInfo.InvariantCulture, "فرمت تاريخ نامعتبر است: {0}", yymmdd + " " + hhmmss), ex);
            }
        }



        /// <summary>
        /// تبديل رشته متني تاريخ شمسي به ميلادي
        /// </summary>
        /// <param name="shamsiYMD">رشته متني تاريخ شمسي، شامل هر نوع كاراكتر جداكننده. مانند 28-4-1388</param>
        /// <param name="dateSeparator">كاراكتر جداكننده بخش هاي سال و ماه و روز</param>
        /// <returns></returns>
        public static DateTime ToDateTime(string shamsiYMD_HMS, char dateSeparator, char dateTimeSeparator, char timeSeparator)
        {
            if (shamsiYMD_HMS == null) throw new ArgumentNullException("shamsiYMD_HMS");
            try
            {
                string[] DT = shamsiYMD_HMS.Split(dateTimeSeparator);
                string shamsiYMD = DT[0].Trim();
                string sHMS = DT.Length > 1 ? DT[1].Trim() : null;
                //------------
                string[] a = shamsiYMD.Split(dateSeparator);
                int y = int.Parse(a[0]);
                int d = int.Parse(a[2]);
                int m = -1;
                for (int i = 0; i < mMonthNames.Length; ++i)
                    if (string.Equals(mMonthNames[i], a[1], StringComparison.OrdinalIgnoreCase))
                        m = i;
                if (m == -1) m = int.Parse(a[1]);
                if (y < 0 || m < 1 || m > 12 || d < 1 || d > 31)
                    throw new PrimeErrorsException("Invalid Date!");
                if (y < 100) y += 1300;
                //------------
                int hour = 0;
                int min = 0;
                int sec = 0;
                if (!string.IsNullOrEmpty(sHMS))
                {
                    string[] HMS = sHMS.Split(timeSeparator);
                    if (HMS.Length > 0) hour = int.Parse(HMS[0]);
                    if (HMS.Length > 1) min = int.Parse(HMS[1]);
                    if (HMS.Length > 2) sec = int.Parse(HMS[2]);
                    if (hour < 0 || hour > 23 || min < 0 || min > 59 || sec < 0 || sec > 59)
                        throw new PrimeErrorsException("Invalid Time!");
                }
                //------------
                return pc.ToDateTime(y, m, d, hour, min, sec, 0);
            }
            catch (Exception ex)
            {
                throw new PrimeErrorsException(string.Format(CultureInfo.InvariantCulture, "فرمت تاريخ نامعتبر است: {0}", shamsiYMD_HMS), ex);
            }
        }


        public static int GetYear(DateTime date)
        {
            return pc.GetYear(date);
        }
        public static int GetMonth(DateTime date)
        {
            return pc.GetMonth(date);
        }
        public static string GetMonthName(DateTime date)
        {
            return MonthNames[pc.GetMonth(date) - 1];
        }
        public static string GetAbbreviatedMonthName(DateTime date)
        {
            return AbbreviatedMonthNames[pc.GetMonth(date) - 1];
        }
        public static int GetDay(DateTime date)
        {
            return pc.GetDayOfMonth(date);
        }
        public static int GetDaysInMonth(int year, int month)
        {
            return pc.GetDaysInMonth(year, month);
        }
        public static DayOfWeek GetDayOfWeek(int year, int month, int day)
        {
            string shamsi = year.ToString() + "/" + month.ToString() + "/" + day.ToString();
            DateTime date = Shamsi.ToDateTime(shamsi);
            return pc.GetDayOfWeek(date);
        }
        public static DayOfWeek GetDayOfWeek(DateTime date)
        {
            return pc.GetDayOfWeek(date);
        }
        public static string GetDayOfWeekName(DateTime date)
        {
            return DayNames[(int)GetDayOfWeek(date)];
        }
        public static string GetAbbreviatedDayOfWeekName(DateTime date)
        {
            return AbbreviatedDayNames[(int)GetDayOfWeek(date)];
        }
        public static int GetHour(DateTime date)
        {
            return pc.GetHour(date);
        }
        public static int GetMinute(DateTime date)
        {
            return pc.GetMinute(date);
        }
        public static int GetSecond(DateTime date)
        {
            return pc.GetSecond(date);
        }
        public static double GetMilliseconds(DateTime date)
        {
            return pc.GetMilliseconds(date);
        }
        public static string GetAmPm(DateTime date)
        {
            return GetHour(date) < 12 ? "ق.ظ" : "ب.ظ";
        }






        public static void Parse(DateTime date, out int shamsiYear, out int shamsiMonth, out int shamsiDay)
        {
            shamsiYear = pc.GetYear(date);
            shamsiMonth = pc.GetMonth(date);
            shamsiDay = pc.GetDayOfMonth(date);
        }


        public static string ToShortString(DateTime date)
        {
            return ToString("y/M/d", date);
        }
        public static string ToLongString(DateTime date)
        {
            return ToString("yyyy/MM/dd, HH:mm:ss", date);
        }

        public static string ToShortDateTimeString(DateTime date)
        {
            return ToString("y/M/d, HH:mm", date);
        }

        public new static string ToString()
        {
            return ToString("yy/MM/dd", DateTime.Now);
        }

        public static string ToString(string format)
        {
            return ToString(format, DateTime.Now);
        }

        public static string ToString(DateTime? date)
        {
            return !date.HasValue ? "" : ToString(date.Value);
        }

        public static string ToString(DateTime date)
        {
            return ToString("yy/MM/dd, HH:mm", date);
        }

        public static string ToString(string format, DateTime? date)
        {
            return !date.HasValue ? "" : ToString(format, date.Value);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        public static string ToString(string format, DateTime date)
        {
            if (date == DateTime.MinValue) return null;
            StringBuilder sb = new StringBuilder();
            int n;
            if (string.IsNullOrEmpty(format))
                format = DateTimeFormatInfo.CurrentInfo.FullDateTimePattern;
            if (format.Length == 1)
            {
                char c = format[0];
                if (c == 'D') format = DateTimeFormatInfo.CurrentInfo.LongDatePattern; //"yyyy/MM/dd";
                if (c == 'd') format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern; //"yyyy/MM/dd";
                if (c == 'F' || c == 'G') format = DateTimeFormatInfo.CurrentInfo.FullDateTimePattern; //"yyyy/MM/dd hh:mm:ss tt";
                if (c == 'f' || c == 'g') format = "yyyy/MM/dd hh:mm tt";
                if (c == 'M' || c == 'm') format = DateTimeFormatInfo.CurrentInfo.MonthDayPattern; //"MMMM dd";
                if (c == 'O' || c == 'o') format = "yyyy-MM-ddTHH:mm:ss.fffffffK";

                if (c == 'R' || c == 'r') format = DateTimeFormatInfo.CurrentInfo.RFC1123Pattern; //"ddd, dd MMM yyyy HH':'mm':'ss 'GMT'";
                if (c == 'S' || c == 's') format = DateTimeFormatInfo.CurrentInfo.SortableDateTimePattern; //"yyyy'-'MM'-'dd'T'HH':'mm':'ss";
                if (c == 'T') format = DateTimeFormatInfo.CurrentInfo.LongTimePattern; //"hh:mm:ss tt";
                if (c == 't') format = DateTimeFormatInfo.CurrentInfo.ShortTimePattern; //"hh:mm tt";
                if (c == 'U') format = "yyyy-MM-dd '?'";
                if (c == 'u') format = DateTimeFormatInfo.CurrentInfo.UniversalSortableDateTimePattern; //"yyyy'-'MM'-'dd HH':'mm':'ss'Z'";
                if (c == 'Y' || c == 'y') format = DateTimeFormatInfo.CurrentInfo.YearMonthPattern; //"MMMM, yyyy";
            }
            int CurrPos = 0;
            while (CurrPos < format.Length)
            {
                char c = format[CurrPos];
                CurrPos += 1;
                switch (c)
                {
                    case 'y':
                        n = GetFreq(format, ref CurrPos, c) + 1;
                        int y = GetYear(date);
                        if (n <= 2) y = y % 100;
                        else if (n == 3) y = y % 1000;
                        else if (n == 4) y = y % 10000;
                        sb.Append(y.ToString("d" + n.ToString(CultureInfo.InvariantCulture)));
                        break;

                    case 'M':
                        n = GetFreq(format, ref CurrPos, c) + 1;
                        if (n == 1) sb.Append(GetMonth(date));
                        else if (n == 2) sb.Append(GetMonth(date).ToString("00", CultureInfo.InvariantCulture));
                        else if (n == 3) sb.Append(GetAbbreviatedMonthName(date));
                        else sb.Append(GetMonthName(date));
                        break;

                    case 'd':
                        n = GetFreq(format, ref CurrPos, c) + 1;
                        if (n == 1) sb.Append(GetDay(date));
                        else if (n == 2) sb.Append(GetDay(date).ToString("00", CultureInfo.InvariantCulture));
                        else if (n == 3) sb.Append(GetAbbreviatedDayOfWeekName(date));
                        else sb.Append(GetDayOfWeekName(date));
                        break;

                    case 'f':
                    case 'F':
                        //Represents the most significant digit of the seconds fraction; that is, it represents the tenths of a second in a date and time value.
                        //"hh:mm:ss.f"    -=>  07:27:15.0
                        //"hh:mm:ss.fff"  -=>  07:27:15.018

                        n = GetFreq(format, ref CurrPos, c) + 1;
                        //double ms = GetMilliseconds(date) / 1000;
                        //string sMs = ms.ToString("f" + n.ToString());
                        //sb.Append(sMs.Substring(2));
                        string f_fmt = n == 1 ? "%" + c.ToString() : format.Substring(CurrPos - n, n);
                        sb.Append(date.ToString(f_fmt, CultureInfo.InvariantCulture));
                        break;

                    case 'g':
                        // Represents the period or era, for example, A.D. 
                        n = GetFreq(format, ref CurrPos, c) + 1;
                        break;

                    case 'h':
                        n = GetFreq(format, ref CurrPos, c) + 1;
                        int h = GetHour(date) % 12;
                        if (h == 0) h = 12;
                        if (n == 1) sb.Append(h);
                        else sb.Append(h.ToString("00", CultureInfo.InvariantCulture));
                        break;

                    case 'H':
                        n = GetFreq(format, ref CurrPos, c) + 1;
                        if (n == 1) sb.Append(GetHour(date));
                        else sb.Append(GetHour(date).ToString("00", CultureInfo.InvariantCulture));
                        break;

                    case 'K':
                        //Represents the time zone information of a date and time value. 
                        //When used with DateTime values, the result string is defined by the value of the DateTime.Kind property. 
                        sb.Append(date.ToString("%K", CultureInfo.InvariantCulture));
                        break;

                    case 'm':
                        n = GetFreq(format, ref CurrPos, c) + 1;
                        if (n == 1) sb.Append(GetMinute(date));
                        else sb.Append(GetMinute(date).ToString("00", CultureInfo.InvariantCulture));
                        break;

                    case 's':
                        n = GetFreq(format, ref CurrPos, c) + 1;
                        if (n == 1) sb.Append(GetSecond(date));
                        else sb.Append(GetSecond(date).ToString("00", CultureInfo.InvariantCulture));
                        break;

                    case 't':
                        n = GetFreq(format, ref CurrPos, c) + 1;
                        if (n == 1) sb.Append(GetAmPm(date)[0]);
                        else sb.Append(GetAmPm(date));
                        break;

                    case 'z':
                        //With DateTime values, represents the signed offset of the local operating system's time zone from Coordinated Universal Time (UTC), measured in hours.
                        n = GetFreq(format, ref CurrPos, c) + 1;
                        break;

                    case ':':
                        sb.Append(DateTimeFormatInfo.CurrentInfo.TimeSeparator);
                        break;

                    case '/':
                        sb.Append(DateTimeFormatInfo.CurrentInfo.DateSeparator);
                        break;

                    case '\\':
                        if (CurrPos < format.Length)
                        {
                            sb.Append(format[CurrPos]);
                            ++CurrPos;
                        }
                        break;

                    case '%':
                        break;

                    case '\'':
                        while (CurrPos < format.Length && format[CurrPos] != '\'')
                        {
                            sb.Append(format[CurrPos]);
                            ++CurrPos;
                        }
                        ++CurrPos;
                        break;

                    default:
                        sb.Append(c);
                        break;
                }

            }
            return sb.ToString();
        }


        private static int GetFreq(string txt, ref int i, char c)
        {
            int freq = 0;
            while (i < txt.Length && txt[i] == c)
            {
                ++i;
                ++freq;
            }
            return freq;
        }

        public static void ConvertToShamsi(DataTable tb, string miladiDateField, string shamsiDateField, string dateFormat)
        {
            if (tb == null) throw new ArgumentNullException("tb");
            int i = tb.Columns.IndexOf(shamsiDateField);
            if (i < 0) tb.Columns.Add(shamsiDateField);
            i = tb.Columns.IndexOf(shamsiDateField);
            foreach (DataRow r in tb.Rows)
            {
                if (!(r[miladiDateField] is DBNull))
                {
                    DateTime dt = (DateTime)r[miladiDateField];
                    r[i] = ToString(dateFormat, dt);
                }
            }
        }


    }
}