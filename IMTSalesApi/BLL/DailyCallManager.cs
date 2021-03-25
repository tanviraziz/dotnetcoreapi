using IMTSalesApi.DAL;
using IMTSalesApi.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace IMTSalesApi.BLL
{
    public class DailyCallManager
    {
        private DailyCallGateway dailyCallGateway = null;

        public DailyCallManager()
        {
            dailyCallGateway = new DailyCallGateway();
        }

        public string DailyCallManagement(Dailycall dailyCall)
        {
            return dailyCallGateway.DailyCallManagement(dailyCall);
        }

        public DataTable Dailycall(string queryNo, string choice, string condition)
        {
            DataTable dt = null;
            string queryString = null;
            switch (queryNo)
            {
                case "1":
                    queryString = "API_GetDailycall";
                    break;
            }

            try
            {
                dt = new DailycalldataGateway().DailycallData(queryString, choice, condition);

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
