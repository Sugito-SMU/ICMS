using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ICMS.Common.Security
{
    public class Encryption
    {
        public static string Encrypt(string clearText, string certThumbprint)
        {
            string output = string.Empty;
            X509Certificate2 cert = GetCertificate(certThumbprint);
            using (RSACryptoServiceProvider csp = (RSACryptoServiceProvider)cert.PublicKey.Key)
            {
                byte[] bytesData = Encoding.UTF8.GetBytes(clearText);
                byte[] bytesEncrypted = csp.Encrypt(bytesData, true);
                output = Convert.ToBase64String(bytesEncrypted);
            }
            return output;
        }

        public static string Decrypt(string encrypted, string certThumbprint)
        {
            string text = string.Empty;

            X509Certificate2 cert = GetCertificate(certThumbprint);
            using (RSACryptoServiceProvider csp = (RSACryptoServiceProvider)cert.PrivateKey)
            {

                byte[] bytesEncrypted = Convert.FromBase64String(encrypted);
                byte[] bytesDecrypted = csp.Decrypt(bytesEncrypted, true);
                text = Encoding.UTF8.GetString(bytesDecrypted);
            }
            return text;

        }

        public static X509Certificate2 GetCertificate(string certThumbprint)
        {
            string text = string.Empty;
            X509Store store = new X509Store("My", StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);
            return store.Certificates.Find(X509FindType.FindByThumbprint, certThumbprint, false)[0];
        }
    }
}
