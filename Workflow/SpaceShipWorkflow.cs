using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Workflow.PACSMessage;
using System.Security.Cryptography;
using Encrypting;

namespace Workflow
{
    public class SpaceShipWorkflow
    {
        private string spaceShipCode;
        public string SpaceShipCode
        {
            get { return spaceShipCode; }
            set { spaceShipCode = value; }

        }
        private string spaceShipId;
        public string SpaceShipId
        {
            get { return spaceShipId; }
            set { spaceShipId = value; }

        }
        private string deliveryCode;
        public string DeliveryCode
        {
            get { return deliveryCode; }
            set { 
                deliveryCode = value;
                LoadDeliveryInfo();
                LoadPlanetInfo();
            }

        }
        public string planetCode { get; private set; }
        public string planetIp { get; private set; }
        public int planetMsgPort { get; private set; }
        public int planetFilePort { get; private set; }
        private Aes myAes;
        public string planetId { get; private set; }
        private ValidationResult status;
        private int step = 0;
        public string KeyEncripted;
        public string IVEncrypted;
        public string PDFEncrypted;
        public byte[] pdfEncrypted;
        public int encryptedKeyCount = 0;

        public string GetEntryMessage()
        {
            if (string.IsNullOrEmpty(planetIp) || planetMsgPort == 0 || planetFilePort == 0) return null;
            return new EntryMessage(spaceShipCode, deliveryCode).ToString();
        }

        public string GetValidationMessage()
        {
            step++;
            return new ValidationMessage(spaceShipCode, step, status).ToString();
        }

        public void GenerateAesCredentials()
        {
            this.myAes = AES.GenerateAES();
        }

        public void EncrypKey()
        {
            string xmlPublicKey = Encrypting.RSA.GetPlanetPublickey(planetCode);
            string credentialEncrypted = Encrypting.RSA.EncryptRSA(this.myAes.Key, xmlPublicKey);
            this.KeyEncripted = credentialEncrypted;
        }

        public void EncrypIV()
        {
            string xmlPublicKey = Encrypting.RSA.GetPlanetPublickey(planetCode);
            string credentialEncrypted = Encrypting.RSA.EncryptRSA(this.myAes.IV, xmlPublicKey);
            this.IVEncrypted = credentialEncrypted;
        }

        public void EncryptPDF()
        {
            byte[] deliveryDataPdf = DatabaseHelper.GetDeliveryDataPdf(DeliveryCode);

            byte[] encryptedData = AES.EncryptAES(deliveryDataPdf, myAes.Key, myAes.IV);

            this.PDFEncrypted = Convert.ToBase64String(encryptedData);
        }

        private void LoadDeliveryInfo()
        {
            try
            {
                DataRow row = DatabaseHelper.DeliveryInfo(deliveryCode, spaceShipId);
                if (row != null)
                {
                    this.planetId = row["idPlanet"].ToString();
                }
            }
            catch{ }
        }

        private void LoadPlanetInfo()
        {
            DataRow row;
            try
            {
                row = DatabaseHelper.PlanetInfoById(this.planetId);
                if (row is null) return;
                this.planetId = row["idPlanet"].ToString();
                this.planetIp = row["IPPlanet"].ToString();
                this.planetCode = row["CodePlanet"].ToString();
                string msgPort = row["PortPlanetL"].ToString();
                string filePort = row["PortPlanetS"].ToString();
                if (int.TryParse(msgPort, out int msgPortInt)) this.planetMsgPort = msgPortInt;
                else this.planetMsgPort = 0;
                if (int.TryParse(filePort, out int filePortInt)) this.planetFilePort = filePortInt;
                else this.planetFilePort = 0;
            }
            catch { }
        }
    }
}
