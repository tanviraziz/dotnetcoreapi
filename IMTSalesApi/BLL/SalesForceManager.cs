using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using IMTSalesApi.DAL;

namespace IMTSalesApi.BLL
{
    public class SalesForceManager
    {
        private SalesForceGateway salesForceGateway = null;

        public SalesForceManager()
        {
            salesForceGateway = new SalesForceGateway();
        }

        public DataTable SalesForceData(string queryNo, string choice, string condition)
        {
            DataTable dt = null;
            string queryString = null;
            switch (queryNo)
            {
                case "1":
                    queryString = "GetSalesForceList";
                    break;
            }

            try
            {
                dt = salesForceGateway.SalesForceData(queryString, choice, condition);
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
