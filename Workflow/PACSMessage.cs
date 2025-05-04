using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Workflow
{
    class PACSMessage
    {
        public enum MessageType
        {
            [Description("Entry Requirement")]
            ER,
            [Description("Validation Result")]
            VR
        }
        public enum ValidationResult
        {
            [Description("In progress")]
            VP,
            [Description("Access Denied")]
            AD,
            [Description("Acess Granted")]
            AG
        }

        public MessageType type { get; set; }
        public string shipCode { get; set; }
        public class EntryMessage : PACSMessage
        {
            public string deliveryCode { get; set; }
            public EntryMessage(string shipCode, string deliveryCode)
            {
                type = MessageType.ER;
                this.shipCode = shipCode;
                this.deliveryCode = deliveryCode;
            }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(type).Append(shipCode).Append(deliveryCode);
                return sb.ToString();
            }
        }

        public class ValidationMessage : PACSMessage
        {
            public int resNumber { get; set; }
            public ValidationResult result { get; set; }

            public ValidationMessage(string shipCode, int resNumber, ValidationResult result)
            {
                type = MessageType.VR;
                this.shipCode = shipCode;
                this.resNumber = resNumber;
                this.result = result;
            }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(type).Append(resNumber).Append(shipCode).Append(result);
                return sb.ToString();
            }
        }

        public static string ValidationResultMsg(string shipCode, int resNumber, ValidationResult result)
        {
            return new ValidationMessage(shipCode, resNumber, result).ToString();
        }

        public static string EntryRequirementMsg(string shipCode, string deliveryCode)
        {
            return new EntryMessage(shipCode, deliveryCode).ToString();
        }

        public static PACSMessage ParseMessage(string message)
        {
            string msgType = message.Substring(0, 2);
            string shipCode;
            switch (msgType)
            {
                case "ER":
                    shipCode = message.Substring(2, 12);
                    string deliveryCode = message.Substring(12);
                    return new EntryMessage(shipCode, deliveryCode);
                case "VR":
                    int resNumber;
                    if (!int.TryParse(message.Substring(2, 1), out resNumber))
                    {
                        return null;
                    }
                    shipCode = message.Substring(3, 12);
                    string result = message.Substring(14);
                    ValidationResult valResult;
                    if (!Enum.TryParse(result, out valResult))
                    {
                        return null;
                    }
                    return new ValidationMessage(shipCode, resNumber, valResult);

                default:
                    return null;
            }
        }
    }
}
