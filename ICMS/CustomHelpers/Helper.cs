using ICMS.Common.Security;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ICMS.CustomHelpers
{
    public static class Helper
    {

        public static Boolean DEBUG()
        {
            // use this sequence of returns to make the snippet ReSharper friendly. 
#if DEBUG
            return true;
#else
        return false;
#endif
        }
    }
}