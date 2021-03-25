using IMTSalesApi.DAL;
using IMTSalesApi.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace IMTSalesApi.BLL
{
    public class AttendanceManager
    {
        private AttendanceGateway attendanceGateway = null;

        public AttendanceManager()
        {
            attendanceGateway = new AttendanceGateway();
        }

        public string AttendanceManagement(Attendance attendance)
        {
            try
            {
                return attendanceGateway.AttendanceManagement(attendance);
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public DataTable AttendanceData(string queryNo, string choice, string condition)
        {
            DataTable dt = null;
            string queryString = null;
            switch (queryNo)
            {
                case "1":
                    queryString = "GetAttendanceList";
                    break;
            }

            try
            {
                dt = attendanceGateway.AttendanceData(queryString, choice, condition);
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
