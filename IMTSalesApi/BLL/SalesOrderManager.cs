using IMTCMS.DAL.DAO;
using IMTSalesApi.DAL;
using IMTSalesApi.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace IMTSalesApi.BLL
{
    public class SalesOrderManager
    {
        private SalesOrderGateway salesOrderGateway = null;

        public SalesOrderManager()
        {
            salesOrderGateway = new SalesOrderGateway();
        }

        public string SalesOrderManagement(SalesOrder sale)
        {
            try
            {
                return salesOrderGateway.SalesOrderManagement(sale);
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public DataTable Orders(string queryNo, string choice, string condition)
        {
            DataTable dt = null;
            string queryString = null;
            switch (queryNo)
            {
                case "1":
                    queryString = "API_GetSaleOrders";
                    break;
            }

            try
            {
                dt = new SalesOrderGateway().OrderData(queryString, choice, condition);

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
