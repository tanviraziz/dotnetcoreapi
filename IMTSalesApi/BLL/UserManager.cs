using IMTSalesApi.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace IMTSalesApi.BLL
{
    public class UserManager
    {
        private UserGateway userGateway = null;

        public UserManager()
        {
            userGateway = new UserGateway();
        }

        public DataTable UserData(string queryNo, string choice, string condition1,string condition2)
        {
            DataTable dt = null;
            string queryString = null;
            switch (queryNo)
            {
                case "1":
                    queryString = "API_GetUserInfo";
                    break;
            }

            try
            {
                dt = userGateway.UserData(queryString, choice, condition1,condition2);
                if (dt != null && dt.Rows.Count > 0)
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
