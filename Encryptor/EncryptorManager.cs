using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryptor
{
    class EncryptorManager
    {
        private readonly string JPATH = $"{Environment.CurrentDirectory}\\data.json";
        private readonly string XPATH = $"{Environment.CurrentDirectory}\\data.xml";

        public void Manager()
        {
            Reload();
            while (true)
            {
                Console.WriteLine("Choose action:");
                var key = Console.ReadKey().Key;
                if(key == ConsoleKey.Q)
                {
                    Environment.Exit(0);
                }
                Console.WriteLine();
                switch (key)
                {
                    case ConsoleKey.NumPad1:
                        Encrypt();
                        break;
                    case ConsoleKey.NumPad2:
                        Decrypt();
                        break;
                    case ConsoleKey.R:
                        Reload();
                        break;
                    default:
                        break;
                }
            }
        }

        private void Encrypt()
        {
            var encrypt = new Encrypt(JPATH, XPATH);
            encrypt.EncryptData();
        }

        private void Decrypt()
        {
            var decrypt = new Decrypt(JPATH, XPATH);
            decrypt.DataToDecrypt();
        }

        private void Reload()
        {
            Console.Clear();
            Console.Write("Menu:" +
                          "\n1 - Encrypt;" +
                          "\n2 - Decrypt;" +
                          "\nr - Reload;" +
                          "\nq - exit." +
                          "\n");
        }
    }
}
