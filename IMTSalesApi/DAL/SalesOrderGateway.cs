using IMTCMS.DAL.DAO;
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
    public class SalesOrderGateway
    {
        public string SalesOrderManagement(SalesOrder sale)
        {
            SqlTransaction transaction;
            string connString = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                   .AddJsonFile("appsettings.json", false)
                                                   .Build().GetConnectionString("DevConnection").ToString();
            SqlConnection sqlConnection = new SqlConnection(connString);

            sqlConnection.Open();
            transaction = sqlConnection.BeginTransaction();
            SqlCommand cmd = new SqlCommand("API_SaleManagement", sqlConnection, transaction);

            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                SqlParameter SaleOrdID = cmd.Parameters.Add("@SaleOrdID", SqlDbType.BigInt);
                SaleOrdID.Direction = ParameterDirection.Input;
                SaleOrdID.Value = sale.OrderID;

                SqlParameter CustomerID = cmd.Parameters.Add("@CustomerID", SqlDbType.BigInt);
                CustomerID.Direction = ParameterDirection.Input;
                CustomerID.Value = sale.CustomerID;

                SqlParameter MPOID = cmd.Parameters.Add("@UID", SqlDbType.BigInt);
                MPOID.Direction = ParameterDirection.Input;
                MPOID.Value = sale.MPOID;

                SqlParameter PCTypeID = cmd.Parameters.Add("@PCTypeID", SqlDbType.Int);
                PCTypeID.Direction = ParameterDirection.Input;
                PCTypeID.Value = (sale.Type.Trim().ToUpper() == "LPO" ? "2":"1") ;

                SqlParameter OrdDate = cmd.Parameters.Add("@OrdDate", SqlDbType.DateTime);
                OrdDate.Direction = ParameterDirection.Input;
                //OrdDate.Value = (string.IsNullOrEmpty(sale.SaleDate) ? DateTime.Now : DateTime.ParseExact(sale.SaleDate, "dd/MM/yyyy", null));
                OrdDate.Value = DateTime.Now;

                SqlParameter EntryBy = cmd.Parameters.Add("@EntryBy", SqlDbType.VarChar, 10);
                EntryBy.Direction = ParameterDirection.Input;
                EntryBy.Value = "";

                SqlParameter Condition = cmd.Parameters.Add("@Condition", SqlDbType.VarChar, 10);
                Condition.Direction = ParameterDirection.Input;
                Condition.Value = sale.Condition;

                SqlParameter Flag = cmd.Parameters.Add("@Flag", SqlDbType.VarChar, 10);
                Flag.Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                if (cmd.Parameters["@Flag"].Value.ToString().Trim() == "Er")
                {
                    transaction.Rollback();
                    return "0";
                }

                string saleOrdid = cmd.Parameters["@Flag"].Value.ToString().Trim();

                foreach (var entry in sale.Products)
                {
                    if (!SaleDetailManagement((Product)entry, saleOrdid, sqlConnection, transaction))
                    {
                        transaction.Rollback();
                        return "0";
                    }
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
                sale = null;
            }
            return "1";
        }

        public Boolean SaleDetailManagement(Product item, string saleOrdid, SqlConnection connection, SqlTransaction transaction)
        {
            SqlConnection sqlConnection = connection;
            SqlCommand cmd = new SqlCommand("API_SaleDetailManagement", sqlConnection, transaction);

            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                SqlParameter SaleOrdID = cmd.Parameters.Add("@SaleOrdID", SqlDbType.BigInt);
                SaleOrdID.Direction = ParameterDirection.Input;
                SaleOrdID.Value = saleOrdid;

                SqlParameter ProdID = cmd.Parameters.Add("@ProdID", SqlDbType.BigInt);
                ProdID.Direction = ParameterDirection.Input;
                ProdID.Value = item.ID;                

                SqlParameter Required = cmd.Parameters.Add("@Required", SqlDbType.Decimal);
                Required.Direction = ParameterDirection.Input;
                Required.Value = Convert.ToDecimal(item.Quantity);

                SqlParameter EntryBy = cmd.Parameters.Add("@EntryBy", SqlDbType.VarChar, 10);
                EntryBy.Direction = ParameterDirection.Input;
                //EntryBy.Value = LoginUser.UserID;

                SqlParameter Condition = cmd.Parameters.Add("@Condition", SqlDbType.VarChar, 10);
                Condition.Direction = ParameterDirection.Input;
                Condition.Value = item.Condition;

                SqlParameter Flag = cmd.Parameters.Add("@Flag", SqlDbType.VarChar, 10);
                Flag.Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                if (cmd.Parameters["@Flag"].Value.ToString().Trim() == "Er")
                {
                    return false;
                }                
            }
            catch
            {
                return false;
            }
            finally
            {
                cmd.Dispose();
                item = null;
            }
            return true;
        }

        public DataTable OrderData(string query, string choice, string condition)
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
