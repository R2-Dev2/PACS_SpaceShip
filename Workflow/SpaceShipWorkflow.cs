using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Encrypting;
using static Workflow.PACSMessage;
using System.Security.Cryptography;

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
        public int planetPortL { get; private set; }
        private Aes myAes;
        private string planetId;
        private ValidationResult status;
        private int step = 0;
        public string KeyEncripted = "no";
        public string IVEncrypted = "no";
        public string pdfEncrypted = "no";
        public int encryptedKeyCount = 0;

        public string GetEntryMessage()
        {
            if (string.IsNullOrEmpty(planetIp) || planetPortL == 0) return null;
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

        public string EncrypKey()
        {
            string xmlPublicKey = Encrypting.RSA.GetPlanetPublickey(planetCode);
            string credentialEncrypted = Encrypting.RSA.EncryptRSA(this.myAes.Key, xmlPublicKey);

            return credentialEncrypted;
        }

        public string EncrypIV()
        {
            string xmlPublicKey = Encrypting.RSA.GetPlanetPublickey(planetCode);
            string credentialEncrypted = Encrypting.RSA.EncryptRSA(this.myAes.IV, xmlPublicKey);

            return credentialEncrypted;
        }

        public string EncryptPDF()
        {
            byte[] deliveryDataPdf = DatabaseHelper.GetDeliveryDataPdf(DeliveryCode);

            byte[] encryptedData = AES.EncryptAES(myAes, deliveryDataPdf);

            return Encoding.UTF8.GetString(encryptedData);
        }

        private void LoadDeliveryInfo()
        {
            DataRow row = DatabaseHelper.DeliveryInfo(deliveryCode, spaceShipId);
            if(row != null)
            {
                this.planetId = row["idPlanet"].ToString();
            }
        }

        private void LoadPlanetInfo()
        {
            DataRow row = DatabaseHelper.PlanetInfoById(this.planetId);
            this.planetId = row["idPlanet"].ToString();
            this.planetIp = row["IPPlanet"].ToString();
            this.planetCode = row["CodePlanet"].ToString();
            string listenPort = row["PortPlanetL"].ToString();
            if (int.TryParse(listenPort, out int listenPortInt)) this.planetPortL = listenPortInt;
            else this.planetPortL = 0;
        }
    }
}
