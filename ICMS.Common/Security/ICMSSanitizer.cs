using Microsoft.Security.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ICMS.Common.Security
{
    public class ICMSSanitizer
    {
        public static string Sanitize(string input)
        {
            Regex _reXlsCmdInjenction = new Regex(@"^=+");

            if (!string.IsNullOrWhiteSpace(input))
            {
                if (_reXlsCmdInjenction.IsMatch(input))
                {
                    return string.Empty;
                }
                else
                {
                    if (input.Length > 255)
                    {
                        input = input.Replace("\r\n", "NLChar$@#").Replace("\n\r", "NLChar$@#").Replace("\n", "NLChar$@#").Replace("\r", "NLChar$@#");
                        return Sanitizer.GetSafeHtmlFragment(input).Replace("\r\n","").Replace("NLChar$@#", "\r\n");
                    }
                    else
                    {
                        return Sanitizer.GetSafeHtmlFragment(input);
                    }
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
