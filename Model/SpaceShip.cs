using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace Model
{
    public class SpaceShip
    {
        static string xmlPath = "./config.xml";
        static string baseXml = "/ConfigData";
        static string encodingXml = $"{baseXml}/EncodingConfig";
        public string IdShip { get; set; }
        public string CodeShip { get; set; }
        public int MessagePort { get; set; }
        public int FilePort { get; set; }
        public EncodingConfig Encoding { get; private set; }

        public bool loadConfig()
        {
            if (!File.Exists(xmlPath))
            {
                return false;
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(xmlPath);

            CodeShip = doc.SelectSingleNode($"{baseXml}/SpaceShipConfig/SpaceShipCode")?.InnerText;
            Encoding = new EncodingConfig();
            Encoding.OriginalFilesPath = doc.SelectSingleNode($"{encodingXml}/OriginalFilesPath")?.InnerText;
            Encoding.EncodedFilesPath = doc.SelectSingleNode($"{encodingXml}/EncodedFilesPath")?.InnerText;

            string numFilesStr = doc.SelectSingleNode($"{encodingXml}/NumFiles")?.InnerText;
            string numCharsStr = doc.SelectSingleNode($"{encodingXml}/NumChars")?.InnerText;

            if (int.TryParse(numFilesStr, out int numFiles) && int.TryParse(numCharsStr, out int numChars))
            {
                Encoding.NumFiles = numFiles;
                Encoding.NumChars = numChars;
            }
            else
            {
                return false;
            }

            return CodeShip != null && Encoding.OriginalFilesPath != null && Encoding.EncodedFilesPath != null;
        }
    }
}
