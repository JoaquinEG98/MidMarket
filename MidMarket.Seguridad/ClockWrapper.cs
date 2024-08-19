using System;

namespace MidMarket.Seguridad
{
    public static class ClockWrapper
    {
        private static readonly TimeZoneInfo ArgentinaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Argentina Standard Time");

        public static DateTime Now()
        {
            return TimeZoneInfo.ConvertTime(DateTime.Now, ArgentinaTimeZone);
        }
    }
}
