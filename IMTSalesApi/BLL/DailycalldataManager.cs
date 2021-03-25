using IMTSalesApi.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace IMTSalesApi.BLL
{
    public class DailycalldataManager
    {
        private DailycalldataGateway dailycalldataGateway = null;
        public DailycalldataManager()
        {
            dailycalldataGateway = new DailycalldataGateway();
        }

        public DataTable DailycallData(string queryNo, string choice, string condition)
        {
            DataTable dt = null;
            string queryString = null;
            switch (queryNo)
            {
                case "1":
                    queryString = "API_GetDailycallData";
                    break;
            }

            try
            {
                dt = dailycalldataGateway.DailycallData(queryString, choice, condition);
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
