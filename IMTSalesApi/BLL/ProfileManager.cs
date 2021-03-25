using IMTSalesApi.DAL;
using IMTSalesApi.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace IMTSalesApi.BLL
{
    public class ProfileManager
    {
        private ProfileGateway profileGateway = null;

        public ProfileManager()
        {
            profileGateway = new ProfileGateway();
        }

        public bool ProfileManagement(Profile profile)
        {
            return profileGateway.ProfileManagement(profile);
        }

        public DataTable ProfileData(string queryNo, string choice, string condition)
        {
            DataTable dt = null;
            string queryString = null;
            switch (queryNo)
            {
                case "1":
                    queryString = "GetProfileList";
                    break;
            }

            try
            {
                dt = profileGateway.ProfileData(queryString, choice, condition);
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
