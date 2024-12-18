using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ICMS.Common
{
    public static class GlobalVariables
    {
        public static string StudentId
        {
            get
            {
                return HttpContext.Current.Application["StudentId"] as string;
            }
            set
            {
                HttpContext.Current.Application["StudentId"] = value;
            }
        }

        public static string StudentUsername
        {
            get
            {
                return HttpContext.Current.Application["StudentUsername"] as string;
            }
            set
            {
                HttpContext.Current.Application["StudentUsername"] = value;
            }
        }
    }
}
