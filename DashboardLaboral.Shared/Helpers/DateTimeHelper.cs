namespace System
{
    public static class DateTimeHelper
    {
        public static DateTime FechaZonaHoraria(this DateTime dateTime)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("SA Western Standard Time");
            dateTime = TimeZoneInfo.ConvertTime(dateTime, timeZoneInfo);
            return dateTime;
        }

        public static DateTime Hoy()
        {
            return DateTime.Now.FechaZonaHoraria().Date;
        }

        public static DateTime Ayer()
        {
            return DateTime.Now.AddDays(-1).FechaZonaHoraria().Date;
        }

    }
}
