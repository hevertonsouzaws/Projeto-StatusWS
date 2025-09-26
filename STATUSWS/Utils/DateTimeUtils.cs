namespace StatusWS.Utils
{

    public static class DateTimeUtils
    {
        public static DateTimeOffset GetBrazilTime()
        {
            try
            {
                var timeZoneId = OperatingSystem.IsWindows() ? "E. South America Standard Time" : "America/Sao_Paulo";
                var brazilianTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
                var utcNow = DateTimeOffset.UtcNow;

                return TimeZoneInfo.ConvertTime(utcNow, brazilianTimeZone);
            }
            catch (TimeZoneNotFoundException)
            {
                return DateTimeOffset.UtcNow;
            }
        }
    }

}
