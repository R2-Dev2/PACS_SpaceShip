using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Data;
using DataAccess;

namespace PACS_SpaceShip
{
    class AES
    {
        AccesADades accesADades = new AccesADades("SecureCore");

        private void GenerateIVAES()
        {
            string xmlPublicKey;

            using (Aes myAes = Aes.Create())
            {
                string keyEncripted;
                string IVEncripted;
                myAes.GenerateKey();
                myAes.GenerateIV();

                xmlPublicKey = RSA.RSA.GetPlanetPublickey("RAKA");

                keyEncripted = RSA.RSA.EncryptRSA(myAes.Key, xmlPublicKey);
                IVEncripted = RSA.RSA.EncryptRSA(myAes.IV, xmlPublicKey);

                //TODO: ENVIAR LES DADES AL PLANETA
            }
        }

        private void EncryptAES(Aes myAes, byte[] deliveryDataPdf)
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

            //TODO: ENVIAR LES DADES AL PLANETA
        }

        private void GetDeliveryDataPdf(string codeDelivery)
        {
            Dictionary<string, string> dictPdf = new Dictionary<string, string>();
            dictPdf.Add("codeDeliveryData", codeDelivery);

            DataSet dts = accesADades.ExecutaCerca("DeliveryDataPdf", dictPdf);

            byte[] deliveryDataPdf = File.ReadAllBytes(dts.Tables[0].Rows[0]["DocumentPdf"].ToString());
        }
    }
}
