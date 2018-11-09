using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace MiniApps.Stats.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTimeOffset ToChinaStandardTime(this DateTimeOffset dateTimeOffset)
        {
            TimeZoneInfo targetTimeZone = null;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Shanghai");
            }
            else
            {
                targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("China Standard Time");
            }
            var targetTime = TimeZoneInfo.ConvertTime(dateTimeOffset, targetTimeZone);

            return targetTime;
        }
                
        public static long ToLocalTimeSeconds(this DateTimeOffset dateTimeOffset)
        {
            // 东八区
            return dateTimeOffset.ToUnixTimeSeconds() - 3600 * 8;
        }
                
    }
}
