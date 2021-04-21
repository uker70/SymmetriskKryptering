using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SymmetriskKryptering
{
    class Program
    {
        static void Main(string[] args)
        {
            //loop that runs the program
            int runProgramChoice = 1;
            while (runProgramChoice == 1)
            {
                //variables
                Stopwatch timer = new Stopwatch();
                TimeSpan encryptTime = TimeSpan.Zero;
                string text = "";
                byte[][] key = new byte[1][];
                byte[][] iv = new byte[1][];
                byte[] encrypted = null;
                byte[] decrypted = null;
                int encryptionChoice;

                //input from user to select encryption method and what text to encrypt
                Console.WriteLine("1. AES\n2. DES\n3. Triple DES");
                encryptionChoice = MenuChoose(1, 3);

                Console.WriteLine("What do you want to encrypt?");
                text = Console.ReadLine();

                //timer to get how long the encryption took
                timer.Start();
                switch (encryptionChoice)
                {
                    //AES encryption
                    case 1:
                        //key and iv is generated
                        key[0] = NumberGenerator.Generate(32);
                        iv[0] = NumberGenerator.Generate(16);

                        //text is encrypted
                        encrypted = AES.Encrypt(Encoding.UTF8.GetBytes(text), key[0], iv[0]);

                        //time is saved and reset, so we can get decryption time
                        timer.Stop();
                        encryptTime = timer.Elapsed;
                        timer.Reset();
                        timer.Start();

                        //text is decryption
                        decrypted = AES.Decrypt(encrypted, key[0], iv[0]);
                        break;

                    //DES encryption
                    case 2:
                        //key and iv is generated
                        key[0] = NumberGenerator.Generate(8);
                        iv[0] = NumberGenerator.Generate(8);

                        //text is encrypted
                        encrypted = DES.Encrypt(Encoding.UTF8.GetBytes(text), key[0], iv[0]);

                        //time is saved and reset, so we can get decryption time
                        encryptTime = timer.Elapsed;
                        timer.Reset();
                        timer.Start();

                        //text is decryption
                        decrypted = DES.Decrypt(encrypted, key[0], iv[0]);
                        break;

                    //Triple DES encryption
                    case 3:
                        //key and iv is generated
                        key = new byte[3][];
                        iv = new byte[3][];
                        for (int i = 0; i < 3; i++)
                        {
                            key[i] = NumberGenerator.Generate(8);
                            iv[i] = NumberGenerator.Generate(8);
                        }

                        //text is encrypted
                        encrypted = Encoding.UTF8.GetBytes(text);
                        for (int i = 0; i < 3; i++)
                        {
                            encrypted = DES.Encrypt(encrypted, key[i], iv[i]);
                        }

                        //time is saved and reset, so we can get decryption time
                        encryptTime = timer.Elapsed;
                        timer.Reset();
                        timer.Start();

                        //text is decryption
                        decrypted = encrypted;
                        for (int i = 2; i > -1; i--)
                        {
                            decrypted = DES.Decrypt(decrypted, key[i], iv[i]);
                        }
                        break;
                }
                //decryption timer is stopped
                timer.Stop();

                //writes the key, iv encrypted tex and decrypted text
                Console.WriteLine();
                foreach (byte[] b in key)
                {
                    Console.WriteLine("Key: "+Convert.ToBase64String(b));
                }

                foreach (byte[] b in iv)
                {
                    Console.WriteLine("IV: "+ Convert.ToBase64String(b));
                }

                Console.WriteLine("\nEncrypted");
                Console.WriteLine(Convert.ToBase64String(encrypted));
                Console.WriteLine("Decrypted");
                Console.WriteLine(Encoding.UTF8.GetString(decrypted));
                Console.WriteLine("\nEncrypt time: "+encryptTime+"\nDecrypt time: "+timer.Elapsed);

                //choose to try again or exit program
                Console.WriteLine("\n1. Try again\n2. Exit");
                runProgramChoice = MenuChoose(1, 2);
                Console.Clear();
            }
        }

        //menu to select login or create user
        private static int MenuChoose(int numbOne, int numbTwo)
        {
            int input = 0;
            while (input < numbOne || input > numbTwo)
            {
                try
                {
                    Console.Write("\nChoose: ");
                    input = Convert.ToInt32(Console.ReadLine());
                }
                catch
                { }
            }

            return input;
        }
    }
}
