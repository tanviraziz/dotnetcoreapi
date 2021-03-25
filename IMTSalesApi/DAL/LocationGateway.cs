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
    public class LocationGateway
    {
        public string LocationManagement(Location location)
        {
            SqlTransaction transaction;
            string connString = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                   .AddJsonFile("appsettings.json", false)
                                                   .Build().GetConnectionString("DevConnection").ToString();
            SqlConnection sqlConnection = new SqlConnection(connString);

            sqlConnection.Open();
            transaction = sqlConnection.BeginTransaction();
            SqlCommand cmd = new SqlCommand("LocationManagement", sqlConnection, transaction);

            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                SqlParameter SFID = cmd.Parameters.Add("@SFID", SqlDbType.BigInt);
                SFID.Direction = ParameterDirection.Input;
                SFID.Value = (string.IsNullOrEmpty(location.ID) ? 0 : Convert.ToInt64(location.ID));

                SqlParameter Latitude = cmd.Parameters.Add("@Latitude", SqlDbType.Decimal);
                Latitude.Direction = ParameterDirection.Input;
                Latitude.Value = Convert.ToDecimal(location.Latitude);

                SqlParameter Longitude = cmd.Parameters.Add("@Longitude", SqlDbType.Decimal);
                Longitude.Direction = ParameterDirection.Input;
                Longitude.Value = Convert.ToDecimal(location.Longitude);

                SqlParameter DateAndTime = cmd.Parameters.Add("@MoveTime", SqlDbType.DateTime);
                DateAndTime.Direction = ParameterDirection.Input;
                DateAndTime.Value = location.MoveTime;

                SqlParameter Condition = cmd.Parameters.Add("@Condition", SqlDbType.VarChar, 10);
                Condition.Direction = ParameterDirection.Input;
                Condition.Value = location.Condition;

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
                location = null;
            }
            return "1";
        }

        public DataTable LocationData(string query, string choice, string condition)
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
