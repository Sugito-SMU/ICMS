using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static ICMS.Common.ICMSConstants;

namespace ICMS.Common
{
    public static class ICMSMethods
    {
        public static bool ShowPreActivity(string overallStatusCode, string statusCode, DateTime startDate, string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                return false;
            }
            else
            {
                return (overallStatusCode == OverallStatusCodes.COM && (statusCode == ActivityReflectionCodes.Optional && startDate <= DateTime.Now.Date))
                    || (IsOverallStatusPendingOrComplete(overallStatusCode) && statusCode != ActivityReflectionCodes.Optional);
            }
        }

        public static bool ShowMidActivity(string overallStatusCode, string statusCode, DateTime startDate, DateTime endDate, string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                return false;
            }
            else
            {
                return IsOverallStatusPendingOrComplete(overallStatusCode) && statusCode != ActivityReflectionCodes.Optional && ShowPreActivity(overallStatusCode, statusCode, startDate, url) &&
                (statusCode == ActivityReflectionCodes.MidActivityNotStarted || statusCode == ActivityReflectionCodes.MidActivityDraft
                || statusCode == ActivityReflectionCodes.PostActivityNotStarted || statusCode == ActivityReflectionCodes.PostActivityDraft
                || statusCode == ActivityReflectionCodes.MidActivitySubmitted || statusCode == ActivityReflectionCodes.Completed
                || (statusCode == ActivityReflectionCodes.PreActivitySubmitted && (startDate.AddDays((endDate - startDate).TotalDays / 2).Date <= DateTime.Now.Date)));
            }

        }
        public static bool ShowPostActivity(string overallStatusCode, string statusCode, DateTime startDate, DateTime endDate, string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                return false;
            }
            else
            {
                return IsOverallStatusPendingOrComplete(overallStatusCode) && ShowMidActivity(overallStatusCode, statusCode, startDate, endDate, url) &&
                (statusCode == ActivityReflectionCodes.PostActivityNotStarted || statusCode == ActivityReflectionCodes.PostActivityDraft
                || statusCode == ActivityReflectionCodes.Completed || (statusCode == ActivityReflectionCodes.MidActivitySubmitted && endDate.Date <= DateTime.Now.Date));
            }
        }

        public static int GetWordCount(string text)
        {
            var regex = new Regex("[\\w\\d\\’\\'-]+", RegexOptions.IgnoreCase);
            return text == null ? 0 : regex.Matches(text).Count;
        }

        public static bool EnablePreActivity(string overallStatusCode, string statusCode, DateTime startDate, Boolean proBono, string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                return false;
            }
            else
            {
                return statusCode == ActivityReflectionCodes.Optional || (statusCode == ActivityReflectionCodes.NotApplicable && proBono)
                || statusCode == ActivityReflectionCodes.PreActivityNotStarted || statusCode == ActivityReflectionCodes.PreActivityDraft;
            }
        }
        public static bool EnableMidActivity(string overallStatusCode, string statusCode, DateTime startDate, DateTime endDate, string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                return false;
            }
            else
            {
                return IsOverallStatusPendingOrComplete(overallStatusCode) && ((statusCode == ActivityReflectionCodes.PreActivitySubmitted && (startDate.AddDays((endDate - startDate).TotalDays / 2).Date <= DateTime.Now.Date))
                || statusCode == ActivityReflectionCodes.MidActivityNotStarted
                || statusCode == ActivityReflectionCodes.MidActivityDraft);
            }
        }
        public static bool EnablePostActivity(string overallStatusCode, string statusCode, DateTime startDate, DateTime endDate, string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                return false;
            }
            else
            {
                return IsOverallStatusPendingOrComplete(overallStatusCode) && ((statusCode == ActivityReflectionCodes.MidActivitySubmitted && (endDate.Date <= DateTime.Now.Date))
                || statusCode == ActivityReflectionCodes.PostActivityNotStarted
                || statusCode == ActivityReflectionCodes.PostActivityDraft);
            }
        }

        public static bool IsOverallStatusPendingOrComplete(string overallStatusCode)
        {
            return overallStatusCode == OverallStatusCodes.COM || overallStatusCode == OverallStatusCodes.ASM
                || overallStatusCode == OverallStatusCodes.SAS || overallStatusCode == OverallStatusCodes.PGF
                || overallStatusCode == OverallStatusCodes.SLF || overallStatusCode == OverallStatusCodes.GRD;
        }
    }
}
