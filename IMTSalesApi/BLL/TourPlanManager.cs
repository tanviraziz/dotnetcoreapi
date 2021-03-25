using IMTSalesApi.DAL;
using IMTSalesApi.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace IMTSalesApi.BLL
{
    public class TourPlanManager
    {
        private TourPlanGateway tourPlanGateway = null;

        public TourPlanManager()
        {
            tourPlanGateway = new TourPlanGateway();
        }

        public string TourPlanManagement(TourPlan tourPlan)
        {
            return tourPlanGateway.TourPlanManagement(tourPlan);
        }

        public DataTable TourPlan(string queryNo, string choice, string condition)
        {
            DataTable dt = null;
            string queryString = null;
            switch (queryNo)
            {
                case "1":
                    queryString = "API_GetTourPlan";
                    break;
            }

            try
            {
                dt = new TourplandataGateway().TourplanData(queryString, choice, condition);

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
