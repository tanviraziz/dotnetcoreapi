using IMTSalesApi.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace IMTSalesApi.BLL
{
    public class CustomerManager
    {
        private CustomerGateway customerGateway = null;

        public CustomerManager()
        {
            customerGateway = new CustomerGateway();
        }

        public DataTable CustomerData(string queryNo, string choice, string condition)
        {
            DataTable dt = null;
            string queryString = null;
            switch (queryNo)
            {
                case "1":
                    queryString = "API_GetCustomerList";
                    break;
            }

            try
            {
                dt = customerGateway.CustomerData(queryString, choice, condition);
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
