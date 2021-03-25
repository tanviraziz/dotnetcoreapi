using IMTSalesApi.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace IMTSalesApi.BLL
{
    public class AddressManager
    {
        private AddressGateway addressGateway = null;

        public AddressManager()
        {
            addressGateway = new AddressGateway();
        }        

        public DataTable AddressData(string queryNo, string choice, string condition)
        {
            DataTable dt = null;
            string queryString = null;
            switch (queryNo)
            {
                case "1":
                    queryString = "GetRecentAddressList";
                    break;
            }

            try
            {
                dt = addressGateway.AddressData(queryString, choice, condition);
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
