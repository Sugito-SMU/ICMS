using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using NLog;

namespace ICMS.Common
{
    public static class Logger
    {
        private static NLog.Logger _errorLogger = LogManager.GetLogger("ErrorLogger");
        //private static NLog.Logger _debugLogger = LogManager.GetLogger("DebugLogger");
        private static NLog.Logger _accessLogger = LogManager.GetLogger("AccessLogger");

        public static void LogError(Exception ex)
        {
            _errorLogger.Error(ex);
        }

        public static void LogError(string msg)
        {
            _errorLogger.Error(msg);
        }

        public static void LogDebug(string msg)
        {
            //_debugLogger.Debug(msg);
        }

        public static void LogAccess(string msg)
        {
            string userid = Security.OAuthUtility.GetLoginId();
            string ipaddress = GetIPAddress();
            string url = GetValidUrl(HttpContext.Current.Request.Url.AbsolutePath);
            _accessLogger.Info(userid + "|" + ipaddress + "|" + url + "|" + msg);
        }

        public static string GetValidUrl(string url)
        {
            if (Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
            {
                return url;
            }
            else
            {
                return string.Empty;
            }
        }

        public static bool IsValidIP(string ipList)
        {
            Regex _ReIP = new Regex(@"^\d+\.\d+\.\d+\.\d+$");
            if (!string.IsNullOrEmpty(ipList))
            {
                string[] ips = ipList.Split(',');
                for (int i = 0; i < ips.Length; i++)
                {
                    string ip = ips[i].Trim();
                    if ((!string.IsNullOrEmpty(ip)) && (!_ReIP.IsMatch(ip)))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static string GetIPAddress()
        {
            string ip = String.Empty;
            if ((HttpContext.Current.Request != null) && (HttpContext.Current.Request.Headers != null) && !string.IsNullOrEmpty(HttpContext.Current.Request.Headers["X-Forwarded-For"]))
            {
                ip = HttpContext.Current.Request.Headers["X-Forwarded-For"];
                if (IsValidIP(ip))
                {
                    return ip;
                }
            }
            else
            {
                ip = GetIPAddressFromHttpRequest();
                if (IsValidIP(ip))
                {
                    return ip;
                }
            }
            return ip;
        }

        public static string GetIPAddressFromHttpRequest()
        {
            string ip = String.Empty;

            foreach (IPAddress IPA in Dns.GetHostAddresses(HttpContext.Current.Request.UserHostAddress))
            {
                if (IPA.AddressFamily.ToString() == "InterNetwork")
                {
                    ip = IPA.ToString();
                    break;
                }
            }

            if (ip != String.Empty)
            {
                return ip;
            }

            foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (IPA.AddressFamily.ToString() == "InterNetwork")
                {
                    ip = IPA.ToString();
                    break;
                }
            }
            return ip;
        }


    }
}
