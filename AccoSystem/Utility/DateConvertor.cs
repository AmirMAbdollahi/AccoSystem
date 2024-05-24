
using System.Globalization;

namespace AccoSystem.Utility;

public static class DateConvertor
{
    public static string ToShamsi(this DateTime value)
    {
        PersianCalendar pc = new PersianCalendar();
        return pc.GetYear(value) + "/" + pc.GetMonth(value).ToString("00") + "/" +
               pc.GetDayOfMonth(value).ToString("00");
    }

    public static DateTime ToMiladi(DateTime dateTime)
    {
        return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, new System.Globalization.PersianCalendar());
    }
}