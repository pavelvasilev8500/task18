using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Encryptor
{
    class Encrypt
    {
        UnicodeEncoding ByteConverter = new UnicodeEncoding();
        private readonly string JPATH;
        private readonly string XPATH;
        private readonly string test = "Hello World";
        private byte[] dataToEncrypt { get; set; }
        private byte[] encryptedData { get; set; }

        public Encrypt(string jpath, string xpath)
        {
            JPATH = jpath;
            XPATH = xpath;
        }

        public void EncryptData()
        {
            dataToEncrypt = ByteConverter.GetBytes($"{test}");
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                RSA.FromXmlString(Key.publicKeyXML);
                encryptedData = RSA.Encrypt(dataToEncrypt, false);
            }
            SaveJson();
            SaveXml();
        }

        #region DATAWORK
        private void SaveJson()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            using (FileStream writer = new FileStream(JPATH, FileMode.Create))
            {
                byte[] json = JsonSerializer.SerializeToUtf8Bytes(encryptedData, options);
                writer.Write(json);
            }
            Console.WriteLine("Save Success.");
        }

        private void SaveXml()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(byte[]));
            using (FileStream writer = new FileStream(XPATH, FileMode.Create))
            {
                xmlSerializer.Serialize(writer, encryptedData);
                Console.WriteLine("Save Success.");
            }
        }
        #endregion

    }
}
