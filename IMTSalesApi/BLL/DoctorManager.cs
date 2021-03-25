using IMTSalesApi.DAL;
using IMTSalesApi.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace IMTSalesApi.BLL
{
    public class DoctorManager
    {
        private DoctorGateway doctorGateway = null;

        public DoctorManager()
        {
            doctorGateway = new DoctorGateway();
        }

        public bool DoctorManagement(Customer customer)
        {
            return doctorGateway.DoctorManagement(customer);
        }

        public DataTable DoctorData(string queryNo, string choice,string condition)
        {
            DataTable dt = null;
            string queryString = null;
            switch (queryNo)
            {
                case "1":
                    queryString = "API_GetDoctorList";
                    break;                
            }

            try
            {
                dt = doctorGateway.DoctorData(queryString, choice, condition);
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
