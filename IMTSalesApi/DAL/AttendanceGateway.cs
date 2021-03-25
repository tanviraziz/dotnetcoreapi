using IMTSalesApi.Model;
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
    public class AttendanceGateway
    {
        public string AttendanceManagement(Attendance attendance)
        {
            SqlTransaction transaction;
            string connString = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                   .AddJsonFile("appsettings.json", false)
                                                   .Build().GetConnectionString("DevConnection").ToString();
            SqlConnection sqlConnection = new SqlConnection(connString);

            sqlConnection.Open();
            transaction = sqlConnection.BeginTransaction();
            SqlCommand cmd = new SqlCommand("AttendanceManagement", sqlConnection, transaction);

            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                SqlParameter SFID = cmd.Parameters.Add("@UID", SqlDbType.BigInt);
                SFID.Direction = ParameterDirection.Input;
                SFID.Value = (string.IsNullOrEmpty(attendance.UID) ? 0 : Convert.ToInt64(attendance.UID));

                SqlParameter Latitude = cmd.Parameters.Add("@Latitude", SqlDbType.Decimal);
                Latitude.Direction = ParameterDirection.Input;
                Latitude.Value = Convert.ToDecimal(attendance.Latitude);

                SqlParameter Longitude = cmd.Parameters.Add("@Longitude", SqlDbType.Decimal);
                Longitude.Direction = ParameterDirection.Input;
                Longitude.Value = Convert.ToDecimal(attendance.Longitude);

                SqlParameter StartEndTime = cmd.Parameters.Add("@StartEndTime", SqlDbType.DateTime);
                StartEndTime.Direction = ParameterDirection.Input;
                StartEndTime.Value = (attendance.Condition == "1") ?  attendance.StartTime : attendance.EndTime; 
                

                SqlParameter Condition = cmd.Parameters.Add("@Condition", SqlDbType.VarChar, 10);
                Condition.Direction = ParameterDirection.Input;
                Condition.Value = attendance.Condition;

                SqlParameter Flag = cmd.Parameters.Add("@Flag", SqlDbType.VarChar, 10);
                Flag.Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                if (cmd.Parameters["@Flag"].Value.ToString().Trim() == "1")
                {
                    transaction.Rollback();
                    return null;
                }

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return ex.ToString();
            }
            finally
            {
                cmd.Dispose();
                sqlConnection.Close();
                sqlConnection.Dispose();
                attendance = null;
            }
            return "1";
        }

        public DataTable AttendanceData(string query, string choice, string condition)
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
                SqlParameter Choice = cmd.Parameters.Add("@Choice", SqlDbType.VarChar, 10);
                Choice.Direction = ParameterDirection.Input;
                Choice.Value = choice;

                SqlParameter Condition = cmd.Parameters.Add("@Condition", SqlDbType.VarChar, 100);
                Condition.Direction = ParameterDirection.Input;
                Condition.Value = condition;

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
