using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SymmetriskKryptering
{
    class AES
    {
        //AES encrypt
        public static byte[] Encrypt(byte[] text, byte[] key, byte[] iv)
        {
            byte[] output = null;
            //AES is selected as our encryption type
            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                //key, iv, mode and padding is set
                aes.Key = key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                //memory stream for decryption is made
                using (MemoryStream ms = new MemoryStream())
                {
                    //crypto stream that does the encryption is made
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        //encryption is done and the stream is flushed
                        cs.Write(text, 0, text.Length);
                        cs.FlushFinalBlock();
                    }

                    output = ms.ToArray();
                }
            }

            return output;
        }

        //AES decrypt
        public static byte[] Decrypt(byte[] text, byte[] key, byte[] iv)
        {
            byte[] output = null;
            //AES is selected as our encryption type
            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                //key, iv, mode and padding is set
                aes.Key = key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                //memory stream for encryption is made
                using (MemoryStream ms = new MemoryStream())
                {
                    //crypto stream that does the decryption is made
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        //decryption is done and the stream is flushed
                        cs.Write(text, 0, text.Length);
                        cs.FlushFinalBlock();
                    }

                    output = ms.ToArray();
                }
            }

            return output;
        }
    }
}
