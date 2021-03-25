using IMTSalesApi.DAL;
using IMTSalesApi.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace IMTSalesApi.BLL
{
    public class LocationManager
    {
        private LocationGateway locationGateway = null;

        public LocationManager()
        {
            locationGateway = new LocationGateway();
        }

        public string LocationManagement(Location location)
        {
            try
            {
                //if (location != null) return true;
                return locationGateway.LocationManagement(location);
            }
            catch(Exception ex)
            {
                return ex.ToString();
            }
        }

        public DataTable LocationData(string queryNo, string choice, string condition)
        {
            DataTable dt = null;
            string queryString = null;
            switch (queryNo)
            {
                case "1":
                    queryString = "GetLocationList";
                    break;
            }

            try
            {
                dt = locationGateway.LocationData(queryString, choice, condition);
                if (dt != null)
                {
                    return dt;
                }
            }
            catch
            {
                return null;
            }
            return null;
        }
    }
}
