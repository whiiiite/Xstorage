using Humanizer.Localisation;
using System.Globalization;

namespace Xstorage.Extentions
{
    public static class TimeSpanExtentions
    {
        public static string TimeAgoToString(this TimeSpan time)
        {
            return Humanizer.TimeSpanHumanizeExtensions.Humanize(time, 
                culture: CultureInfo.CurrentCulture,
                maxUnit: TimeUnit.Year, toWords:false);

            // old code :..(
            //return time switch
            //{
            //    Humanizer.TimeSpanHumanizeExtensions.Humanize(time)
            //    { TotalSeconds: < 60 } => FormatPart((int)time.TotalSeconds, "one second ago", "seconds"),
            //    { TotalMinutes: < 60 } => FormatPart((int)time.TotalMinutes, "one minute ago", "minutes"),
            //    { TotalHours: < 24 } => FormatPart((int)time.TotalHours, "an hour ago", "hours"),
            //    { TotalDays: < 30 } => FormatPart((int)time.TotalDays, "a day ago", "days"),
            //    { TotalDays: < 365 } => FormatPart((int)(time.TotalDays / 30), "a month ago", "months"),
            //    _ => FormatPart((int)(time.TotalDays / 365), "a year ago", "years")
            //};
        }
    }
}
