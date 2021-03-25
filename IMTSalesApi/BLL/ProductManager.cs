using IMTSalesApi.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace IMTSalesApi.BLL
{
    public class ProductManager
    {
        private ProductGateway productGateway = null;

        public ProductManager()
        {
            productGateway = new ProductGateway();
        }

        public DataTable ProductData(string queryNo, string choice, string condition)
        {
            DataTable dt = null;
            string queryString = null;
            switch (queryNo)
            {
                case "1":
                    queryString = "API_GetProductList";
                    break;
            }

            try
            {
                dt = productGateway.ProductData(queryString, choice, condition);
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
