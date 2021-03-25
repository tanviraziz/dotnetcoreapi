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
    public class ProfileGateway
    {
        public Boolean ProfileManagement(Profile profile)
        {
            SqlTransaction transaction;
            string connString = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                   .AddJsonFile("appsettings.json", false)
                                                   .Build().GetConnectionString("DevConnection").ToString();
            SqlConnection sqlConnection = new SqlConnection(connString);

            sqlConnection.Open();
            transaction = sqlConnection.BeginTransaction();
            SqlCommand cmd = new SqlCommand("ProfileManagement", sqlConnection, transaction);

            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                SqlParameter Empid = cmd.Parameters.Add("@Empid", SqlDbType.BigInt);
                Empid.Direction = ParameterDirection.Input;
                Empid.Value = (string.IsNullOrEmpty(profile.Empid) ? 0 : Convert.ToInt64(profile.Empid));

                SqlParameter Name = cmd.Parameters.Add("@Name", SqlDbType.VarChar, 500);
                Name.Direction = ParameterDirection.Input;
                Name.Value = profile.Name; 

                SqlParameter Gender = cmd.Parameters.Add("@Gender", SqlDbType.VarChar, 50);
                Gender.Direction = ParameterDirection.Input;
                Gender.Value = profile.Gender;  

                SqlParameter Qualification = cmd.Parameters.Add("@Qualification", SqlDbType.VarChar, 500);
                Qualification.Direction = ParameterDirection.Input;
                Qualification.Value = profile.Qualification;

                SqlParameter PresentAddress = cmd.Parameters.Add("@PresentAddress", SqlDbType.VarChar, 500);
                PresentAddress.Direction = ParameterDirection.Input;
                PresentAddress.Value = profile.PresentAddress; 

                SqlParameter PermanentAddress = cmd.Parameters.Add("@PermanentAddress", SqlDbType.VarChar, 500);
                PermanentAddress.Direction = ParameterDirection.Input;
                PermanentAddress.Value = profile.PermanentAddress;

                SqlParameter Email = cmd.Parameters.Add("@Email", SqlDbType.VarChar, 250);
                Email.Direction = ParameterDirection.Input;
                Email.Value = profile.Email;

                SqlParameter PhoneNo = cmd.Parameters.Add("@PhoneNo", SqlDbType.VarChar, 100);
                PhoneNo.Direction = ParameterDirection.Input;
                PhoneNo.Value = profile.PhoneNo;

                SqlParameter Condition = cmd.Parameters.Add("@Condition", SqlDbType.VarChar, 10);
                Condition.Direction = ParameterDirection.Input;
                Condition.Value = profile.Condition;

                SqlParameter Flag = cmd.Parameters.Add("@Flag", SqlDbType.VarChar, 10);
                Flag.Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                if (cmd.Parameters["@Flag"].Value.ToString().Trim() == "1")
                {
                    transaction.Rollback();
                    return false;
                }                

                transaction.Commit();
            }
            catch(Exception ex)
            {
                transaction.Rollback();
                return false;
            }
            finally
            {
                cmd.Dispose();
                sqlConnection.Close();
                sqlConnection.Dispose();
                profile = null;
            }
            return true;
        }        

        public DataTable ProfileData(string query, string choice, string condition)
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
