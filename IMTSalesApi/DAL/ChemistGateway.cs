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
    public class ChemistGateway
    {
        public Boolean ChemistManagement(Customer customer)
        {
            SqlTransaction transaction;
            string connString = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                   .AddJsonFile("appsettings.json", false)
                                                   .Build().GetConnectionString("DevConnection").ToString();
            SqlConnection sqlConnection = new SqlConnection(connString);

            sqlConnection.Open();
            transaction = sqlConnection.BeginTransaction();
            SqlCommand cmd = new SqlCommand("CustomerManagement", sqlConnection, transaction);

            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                SqlParameter CustomerID = cmd.Parameters.Add("@CustomerID", SqlDbType.BigInt);
                CustomerID.Direction = ParameterDirection.Input;
                CustomerID.Value = (string.IsNullOrEmpty(customer.CustomerID) ? 0 : Convert.ToInt64(customer.CustomerID));

                SqlParameter CTypeID = cmd.Parameters.Add("@CTypeID", SqlDbType.Int);
                CTypeID.Direction = ParameterDirection.Input;
                CTypeID.Value = (string.IsNullOrEmpty(customer.CTypeID) ? 0 : Convert.ToInt64(customer.CTypeID));

                SqlParameter Code = cmd.Parameters.Add("@Code", SqlDbType.VarChar, 500);
                Code.Direction = ParameterDirection.Input;
                Code.Value = customer.Code;

                SqlParameter Name = cmd.Parameters.Add("@Name", SqlDbType.VarChar, 1000);
                Name.Direction = ParameterDirection.Input;
                Name.Value = customer.Name;

                SqlParameter Address = cmd.Parameters.Add("@Address", SqlDbType.VarChar, 2000);
                Address.Direction = ParameterDirection.Input;
                Address.Value = customer.Address;

                SqlParameter ContactPerson = cmd.Parameters.Add("@ContactPerson", SqlDbType.VarChar, 500);
                ContactPerson.Direction = ParameterDirection.Input;
                ContactPerson.Value = customer.ContactPerson;

                SqlParameter PhoneNo = cmd.Parameters.Add("@PhoneNo", SqlDbType.VarChar, 100);
                PhoneNo.Direction = ParameterDirection.Input;
                PhoneNo.Value = customer.PhoneNo;

                SqlParameter AltPhoneNo = cmd.Parameters.Add("@AltPhoneNo", SqlDbType.VarChar, 100);
                AltPhoneNo.Direction = ParameterDirection.Input;
                AltPhoneNo.Value = customer.AltPhoneNo;

                SqlParameter WhatsappNo = cmd.Parameters.Add("@WhatsappNo", SqlDbType.VarChar, 100);
                WhatsappNo.Direction = ParameterDirection.Input;
                WhatsappNo.Value = customer.WhatsappNo;

                SqlParameter Email = cmd.Parameters.Add("@Email", SqlDbType.VarChar, 250);
                Email.Direction = ParameterDirection.Input;
                Email.Value = customer.Email;

                SqlParameter Qualification = cmd.Parameters.Add("@Qualification", SqlDbType.VarChar, 500);
                Qualification.Direction = ParameterDirection.Input;
                Qualification.Value = customer.Qualification;

                SqlParameter Speciality = cmd.Parameters.Add("@Speciality", SqlDbType.VarChar, 500);
                Speciality.Direction = ParameterDirection.Input;
                Speciality.Value = customer.Speciality;

                SqlParameter BaseTerrID = cmd.Parameters.Add("@BaseTerrID", SqlDbType.BigInt);
                BaseTerrID.Direction = ParameterDirection.Input;
                BaseTerrID.Value = (string.IsNullOrEmpty(customer.BaseTerrID) ? 0 : Convert.ToInt64(customer.BaseTerrID));

                SqlParameter SalePerMonth = cmd.Parameters.Add("@SalePerMonth", SqlDbType.Decimal);
                SalePerMonth.Direction = ParameterDirection.Input;
                SalePerMonth.Value = 0;

                SqlParameter PatientPerDay = cmd.Parameters.Add("@PatientPerDay", SqlDbType.SmallInt);
                PatientPerDay.Direction = ParameterDirection.Input;
                PatientPerDay.Value = 0;

                SqlParameter ProdPrescribed = cmd.Parameters.Add("@ProdPrescribed", SqlDbType.VarChar, 10);
                ProdPrescribed.Direction = ParameterDirection.Input;
                ProdPrescribed.Value = customer.ProdPrescribed;

                SqlParameter Condition = cmd.Parameters.Add("@Condition", SqlDbType.VarChar, 10);
                Condition.Direction = ParameterDirection.Input;
                Condition.Value = customer.Condition;

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
            catch (Exception ex)
            {
                transaction.Rollback();
                return false;
            }
            finally
            {
                cmd.Dispose();
                sqlConnection.Close();
                sqlConnection.Dispose();
                customer = null;
            }
            return true;
        }
        public DataTable ChemistData(string query, string choice, string condition)
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
