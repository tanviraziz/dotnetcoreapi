using IMTSalesApi.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace IMTSalesApi.BLL
{
    public class TourplandataManager
    {
        private TourplandataGateway tourplandataGateway = null;

        public TourplandataManager()
        {
            tourplandataGateway = new TourplandataGateway();
        }

        public DataTable TourplanData(string queryNo, string choice, string condition)
        {
            DataTable dt = null;
            string queryString = null;
            switch (queryNo)
            {
                case "1":
                    queryString = "API_GetTourPlanData";
                    break;
            }

            try
            {
                dt = tourplandataGateway.TourplanData(queryString, choice, condition);
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
