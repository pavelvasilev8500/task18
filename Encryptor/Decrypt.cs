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
    class Decrypt
    {

        UnicodeEncoding ByteConverter = new UnicodeEncoding();
        private readonly string JPATH;
        private readonly string XPATH;
        private byte[] dataToDecrypt { get; set; }
        private byte[] decryptedData { get; set; }

        public Decrypt(string jpath, string xpath)
        {
            JPATH = jpath;
            XPATH = xpath;
        }

        public void DataToDecrypt()
        {
            Console.WriteLine("From Json:");
            dataToDecrypt = LoadJson();
            DecryptData();
            Console.WriteLine("From Xml:");
            dataToDecrypt = LoadXml();
            DecryptData();
        }

        private void DecryptData()
        {
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                RSA.FromXmlString(Key.privateKeyXML);
                decryptedData = RSA.Decrypt(dataToDecrypt, false);
            }

            string decryptedString = ByteConverter.GetString(decryptedData);
            
            Console.WriteLine(decryptedString);
        }

        private byte[] LoadJson()
        {
            var fileExists = File.Exists(JPATH);
            if (!fileExists)
            {
                Console.WriteLine("No file for output data.");
                return null;
            }
            else
            {
                using (FileStream reader = new FileStream(JPATH, FileMode.Open))
                {
                    byte[] restoredData = new byte[reader.Length];
                    reader.Read(restoredData, 0, restoredData.Length);
                    var utf8Reader = new Utf8JsonReader(restoredData);
                    var deserializedData =
                        JsonSerializer.Deserialize<byte[]>(ref utf8Reader);
                    if (deserializedData == null)
                    {
                        Console.WriteLine("No data for output.");
                        return null;
                    }
                    else
                    {
                        return deserializedData;
                    }
                }
            }
        }

        private byte[] LoadXml()
        {
            var fileExists = File.Exists(XPATH);
            if (!fileExists)
            {
                Console.WriteLine("No file for input data.");
                return null;
            }
            else
            {
                using (FileStream reader = new FileStream(XPATH, FileMode.Open))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(byte[]));
                    dataToDecrypt = (byte[])xmlSerializer.Deserialize(reader);
                    if (dataToDecrypt == null)
                    {
                        Console.WriteLine("No data for output.");
                        return null;
                    }
                    else
                    {
                        return dataToDecrypt;
                    }

                }
            }
        }
    }
}