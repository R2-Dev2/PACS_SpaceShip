using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace Workflow
{
    public static class DatabaseHelper
    {
        static AccesADades accesADades = new AccesADades("SecureCore");

        public static DataRow PlanetInfoById(string id)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("idPlanet", id);
            DataSet dts = accesADades.ExecutaCerca("Planets", dict);
            if (dts.Tables[0].Rows.Count == 1)
            {
                return dts.Tables[0].Rows[0];
            }
            return null;
        }

        public static DataRow SpaceShipInfo(string code)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("codeSpaceShip", code);
            DataSet dts = accesADades.ExecutaCerca("SpaceShips", dict);
            if (dts.Tables[0].Rows.Count == 1)
            {
                return dts.Tables[0].Rows[0];
            }
            return null;
        }

        public static DataRow DeliveryInfo(string deliveryCode, string spaceShipId)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("CodeDelivery", deliveryCode);
            dict.Add("idSpaceShip", spaceShipId);
            DataSet dts = accesADades.ExecutaCerca("DeliveryDataPdf", dict);
            if (dts.Tables[0].Rows.Count == 1)
            {
                return dts.Tables[0].Rows[0];
            }
            return null;
        }

    }
}
