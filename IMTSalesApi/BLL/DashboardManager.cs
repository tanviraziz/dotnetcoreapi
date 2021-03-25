using IMTSalesApi.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace IMTSalesApi.BLL
{
    public class DashboardManager
    {
        private DashboardAttendanceGateway dashboardattendanceGateway = null;

        public DashboardManager()
        {
            dashboardattendanceGateway = new DashboardAttendanceGateway();
        }       

        public DataTable AttendanceData(string choice)
        {
            DataTable dt = null;
            string queryString = null;
            switch (choice)
            {
                case "1":
                    queryString = "GetDashboardAttendanceCount";
                    break;
                case "2":
                    queryString = "GetDashboardAttendanceList";
                    break;
                case "3":
                    queryString = "GetDashboardAttendanceDatewiseTotal";
                    break;
                case "4":
                    queryString = "GetDashboardAttendanceMPwiseTotal";
                    break;
            }

            try
            {
                dt = dashboardattendanceGateway.DashAttendanceData(queryString, null, null);
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
