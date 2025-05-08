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

            myAes.GenerateKey();
            myAes.GenerateIV();

            return myAes;
        }

        public static byte[] EncryptAES(Aes myAes, byte[] deliveryDataPdf)
        {
            byte[] encryptedData;

            using (MemoryStream msInput = new MemoryStream(deliveryDataPdf))
            using (MemoryStream msOutput = new MemoryStream())
            using (CryptoStream cryptoStream = new CryptoStream(msOutput, myAes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                msInput.CopyTo(cryptoStream);
                cryptoStream.FlushFinalBlock();
                encryptedData = msInput.ToArray();
            }

            return encryptedData;
        }
    }
}