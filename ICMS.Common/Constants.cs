using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMS.Common
{
    public class Constants
    {
        public const string DisplayDateFormat = "dd MMM yyyy";

        public const string NullFiller = "-";

        public static string APIEndPoint = ConfigurationManager.AppSettings["APIEndPoint"];

        public static string WebsiteEndPoint = ConfigurationManager.AppSettings["WebsiteEndPoint"];
    }
}
