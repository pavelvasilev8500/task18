using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Encryptor
{ 
    static class Key
    {

        public static string publicKeyXML { get; private set; }
        public static string privateKeyXML { get; private set; }

        static Key()
        {
            Container("Container");
        }

        private static void Container(string containerName)
        {
            CspParameters csp = new CspParameters() { KeyContainerName = containerName };

            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(csp))
            {
                publicKeyXML = RSA.ToXmlString(false);
                privateKeyXML = RSA.ToXmlString(true);
            }

        }
    }
}
