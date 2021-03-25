using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using IMTSalesApi.DAL;

namespace IMTSalesApi.BLL
{
    public class TerritoryManager
    {
        private TerritoryGateway territoryGateway = null;

        public TerritoryManager()
        {
            territoryGateway = new TerritoryGateway();
        }

        public DataTable TerritoryData(string queryNo, string choice, string condition)
        {
            DataTable dt = null;
            string queryString = null;
            switch (queryNo)
            {
                case "1":
                    queryString = "GetTerritoryList";
                    break;
            }

            try
            {
                dt = territoryGateway.TerritoryData(queryString, choice, condition);
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
