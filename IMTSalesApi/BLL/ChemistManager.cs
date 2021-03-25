using IMTSalesApi.DAL;
using IMTSalesApi.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace IMTSalesApi.BLL
{
    public class ChemistManager
    {
        private ChemistGateway chemistGateway = null;

        public ChemistManager()
        {
            chemistGateway = new ChemistGateway();
        }

        public bool ChemistManagement(Customer customer)
        {
            return chemistGateway.ChemistManagement(customer);
        }

        public DataTable ChemistData(string queryNo, string choice, string condition)
        {
            DataTable dt = null;
            string queryString = null;
            switch (queryNo)
            {
                case "1":
                    queryString = "API_GetChemistList";
                    break;
            }

            try
            {
                dt = chemistGateway.ChemistData(queryString, choice, condition);
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
