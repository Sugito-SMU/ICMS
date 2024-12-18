using System.Text.RegularExpressions;
using Microsoft.Security.Application;

namespace TLMS.Common
{
    public class Security
    {
        private static Regex _reXlsCmdInjenction = new Regex(@"^=+");
        public static string Sanitize(string input)
        {

            if (!string.IsNullOrWhiteSpace(input))
            {
                if (_reXlsCmdInjenction.IsMatch(input))
                {
                    return string.Empty;
                }
                else
                {
                    return Sanitizer.GetSafeHtmlFragment(input);
                }
            }
            else
            {
                return input;
            }
        }

        public static bool IsValidAlphaNumericSort(string input)
        {
            Regex re = new Regex(@"^[a-zA-Z0-9]+$");
            if (string.IsNullOrEmpty(input))
            {
                return true;
            }
            else
            {
                if (re.IsMatch(input))
                {
                    return true;
                }
                else
                {
                    if (re.IsMatch(input.Replace(" asc", "").Replace(" desc", "")))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
    }

}
