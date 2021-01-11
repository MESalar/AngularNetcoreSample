using System;
using System.Globalization;

namespace my_new_app.Infrastructure
{
    public static class Extentions
    {
        public static string ToPersianDateString(this DateTime date)
        {
            //string GregorianDate = "Thursday, October 24, 2013";
            //DateTime d = DateTime.Parse(GregorianDate);
            PersianCalendar pc = new PersianCalendar();
            return string.Format("{0}/{1}/{2}", pc.GetYear(date), pc.GetMonth(date), pc.GetDayOfMonth(date));
        }

        public static DateTime ToDatetime(this string date)
        {
            int year, month, day;
            char seperator = '/';
            foreach (var item in "/.,-".ToCharArray())
            {
                if (date.Contains(item))
                {
                    seperator = item;
                    break;
                }
            }


            var split = date.Split(seperator);
            year = int.Parse(split[0]);
            month = int.Parse(split[1]);
            day = int.Parse(split[2]);

            PersianCalendar pc = new PersianCalendar();
            DateTime result = pc.ToDateTime(year, month, day, 0, 0, 0, 0);
            return result;
        }
    }
 
}
