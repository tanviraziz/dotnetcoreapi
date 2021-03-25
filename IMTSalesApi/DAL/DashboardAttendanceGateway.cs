using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IMTSalesApi.DAL
{
    public class DashboardAttendanceGateway
    {
        public DataTable DashAttendanceData(string query, string choice, string condition)
        {
            DataTable dt = new DataTable();
            string queryString = query;


            string connString = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                   .AddJsonFile("appsettings.json", false)
                                                   .Build().GetConnectionString("DevConnection").ToString();
            SqlConnection sqlConnection = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(queryString, sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {   
                SqlDataAdapter sqlDataAdapterObj = new SqlDataAdapter(cmd);
                sqlDataAdapterObj.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                cmd.Dispose();
                sqlConnection.Close();
            }

        }
    }
}
