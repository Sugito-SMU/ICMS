using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMS.Common
{
    public class Methods
    {
        public static string GetUsernameWithoutDomain(string usernameWithDomain)
        {
            if(usernameWithDomain.LastIndexOf(@"\") < 0)
            {
                return usernameWithDomain;
            }

            return usernameWithDomain.Substring(usernameWithDomain.LastIndexOf(@"\") + 1);
        }
    }
}
