using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ICMS.Common.Security
{
    public class CryptoRandom
    {
        public static string GetRandomString()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buffer = new byte[10];
            rng.GetBytes(buffer);
            string rndStr = Convert.ToBase64String(buffer);
            return rndStr;
        }
    }
}
