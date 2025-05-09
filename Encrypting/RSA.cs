using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Xml;
using System.Data;
using DataAccess;

namespace Encrypting
{
    public static class RSA
    {
        private static string keyName;
        private static string xmlPublicKeyPath = "./planetPublicKey.xml";
        private static XmlDocument doc = new XmlDocument();
        static AccesADades accesADades = new AccesADades("SecureCore");
        private static string query = "Select * From PlanetKeys";

        private static string GetCodeFromXml(string codeType)
        {
            string sendCode = "";

            doc.Load("launchConfig.xml");
            XmlNodeList tcpSetingsList = doc.GetElementsByTagName("launchConfig");
            foreach (XmlNode tcpNode in tcpSetingsList)
            {
                foreach (XmlNode childNode in tcpNode.ChildNodes)
                {
                    if (childNode.Name == codeType)
                    {
                        sendCode = childNode.InnerText;
                        break;
                    }
                }
            }

            return sendCode;
        }

        private static string GetIdPlanet(string codePlanet)
        {
            string idPlanet;

            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("codePlanet", codePlanet);

            DataSet dataset = accesADades.ExecutaCerca("Planets", dict);
            idPlanet = dataset.Tables[0].Rows[0]["idPlanet"].ToString();

            return idPlanet;
        }

        public static void GenerateKeys(string codePlanet)
        {
            string idPlanet = GetIdPlanet(codePlanet);

            CspParameters cspp = new CspParameters();
            keyName = codePlanet;

            cspp.KeyContainerName = keyName;

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048, cspp);
            rsa.PersistKeyInCsp = true;

            string publicKey = rsa.ToXmlString(false);

            Dictionary<string, string> dictKeys = new Dictionary<string, string>();
            dictKeys.Add("idPlanet", idPlanet);

            DataSet datasetKeys = accesADades.ExecutaCerca("PlanetKeys", dictKeys);

            if (datasetKeys.Tables[0].Rows.Count > 0)
            {
                datasetKeys.Tables[0].Rows[0]["XMLKey"] = publicKey;
            }
            else
            {
                DataRow row = datasetKeys.Tables[0].NewRow();

                row["XMLKey"] = publicKey;
                row["idPlanet"] = idPlanet;

                datasetKeys.Tables[0].Rows.Add(row);
            }

            accesADades.Actualitzar(query, datasetKeys);

            WriteXmlPublicKey(codePlanet);
        }

        public static string GetPlanetPublickey(string codePlanet)
        {
            string idPlanet = GetIdPlanet(codePlanet);
            Dictionary<string, string> dictKeys = new Dictionary<string, string>();
            dictKeys.Add("idPlanet", idPlanet);

            DataSet datasetKeys = accesADades.ExecutaCerca("PlanetKeys", dictKeys);
            string xmlPublicKey = datasetKeys.Tables[0].Rows[0]["XMLKey"].ToString();

            return xmlPublicKey;
        }

        private static void WriteXmlPublicKey(string codePlanet)
        {
            string xmlPublicKey = GetPlanetPublickey(codePlanet);
            File.WriteAllText(xmlPublicKeyPath, xmlPublicKey);
        }

        public static string EncryptRSA(byte[] dataToEncrypt, string xmlPublicKey)
        {
            string dataEncrypted;
            RSACryptoServiceProvider rsaEnc = new RSACryptoServiceProvider(2048);
            rsaEnc.FromXmlString(xmlPublicKey);

            byte[] encryptedData = rsaEnc.Encrypt(dataToEncrypt, false);

            dataEncrypted = Convert.ToBase64String(encryptedData);

            return dataEncrypted;
        }
    }
}
