using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Data;

namespace Encrypting
{
    public static class AES
    {
        public static Aes GenerateAES()
        {
            Aes myAes = Aes.Create();
            myAes.KeySize = 256;

            myAes.GenerateKey();
            myAes.GenerateIV();

            return myAes;
        }

        public static byte[] EncryptAES(byte[] data, byte[] key, byte[] iv)
        {
            using (Aes myAes = Aes.Create())
            {
                myAes.Key = key;
                myAes.IV = iv;

                using (MemoryStream ms = new MemoryStream())
                using (CryptoStream cryptoStream = new CryptoStream(ms, myAes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(data, 0, data.Length);
                    cryptoStream.FlushFinalBlock();
                    return ms.ToArray();
                }
            }
        }

    }
}