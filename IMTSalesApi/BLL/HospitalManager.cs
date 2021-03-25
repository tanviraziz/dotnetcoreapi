using IMTSalesApi.DAL;
using IMTSalesApi.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace IMTSalesApi.BLL
{
    public class HospitalManager
    {
        private HospitalGateway hospitalGateway = null;

        public HospitalManager()
        {
            hospitalGateway = new HospitalGateway();
        }

        public bool HospitalManagement(Customer customer)
        {
            return hospitalGateway.HospitalManagement(customer);
        }

        public DataTable HospitalData(string queryNo, string choice, string condition)
        {
            DataTable dt = null;
            string queryString = null;
            switch (queryNo)
            {
                case "1":
                    queryString = "API_GetHospitalList";
                    break;
            }

            try
            {
                dt = hospitalGateway.HospitalData(queryString, choice, condition);
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
