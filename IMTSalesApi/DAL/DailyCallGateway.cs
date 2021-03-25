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
    public class DailyCallGateway
    {
        public string DailyCallManagement(Dailycall dailyCall)
        {
            SqlTransaction transaction;
            string connString = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                   .AddJsonFile("appsettings.json", false)
                                                   .Build().GetConnectionString("DevConnection").ToString();
            SqlConnection sqlConnection = new SqlConnection(connString);

            sqlConnection.Open();
            transaction = sqlConnection.BeginTransaction();
            SqlCommand cmd = new SqlCommand("API_DailyCallManagement", sqlConnection, transaction);

            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                SqlParameter DailyCallID = cmd.Parameters.Add("@DailyCallID", SqlDbType.BigInt);
                DailyCallID.Direction = ParameterDirection.Input;
                DailyCallID.Value = dailyCall.DailyCallID;

                SqlParameter CustomerID = cmd.Parameters.Add("@CustomerID", SqlDbType.BigInt);
                CustomerID.Direction = ParameterDirection.Input;
                CustomerID.Value = Convert.ToInt64(dailyCall.CustomerID);

                SqlParameter VisitTime = cmd.Parameters.Add("@VisitTime", SqlDbType.Time);
                VisitTime.Direction = ParameterDirection.Input;
                VisitTime.Value = dailyCall.VisitTime;

                SqlParameter FeedBackID = cmd.Parameters.Add("FeedBackID", SqlDbType.BigInt);
                FeedBackID.Direction = ParameterDirection.Input;
                FeedBackID.Value = Convert.ToInt64(dailyCall.FeedBackID);

                SqlParameter Remarks = cmd.Parameters.Add("@Remarks", SqlDbType.VarChar,2500);
                Remarks.Direction = ParameterDirection.Input;
                Remarks.Value = dailyCall.Remarks;

                SqlParameter Condition = cmd.Parameters.Add("@Condition", SqlDbType.VarChar, 10);
                Condition.Direction = ParameterDirection.Input;
                Condition.Value = dailyCall.Condition;

                SqlParameter Flag = cmd.Parameters.Add("@Flag", SqlDbType.VarChar, 10);
                Flag.Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                if (cmd.Parameters["@Flag"].Value.ToString().Trim() == "Er")
                {
                    transaction.Rollback();
                    return "0";
                }

                string dailyCallid = cmd.Parameters["@Flag"].Value.ToString().Trim();

                foreach (var promoteProduct in dailyCall.PromotedProduct)
                {
                    if (!PromotaedProductManagement((Product)promoteProduct, dailyCallid, sqlConnection, transaction))
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
                dailyCall = null;
            }
            return "1";
        }

        public Boolean PromotaedProductManagement(Product promoteProduct, string dailyCallid, SqlConnection connection, SqlTransaction transaction)
        {
            SqlConnection sqlConnection = connection;
            SqlCommand cmd = new SqlCommand("API_DailyCallPromotedProductManagement", sqlConnection, transaction);

            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                SqlParameter DailyCallPromotionID = cmd.Parameters.Add("@DailyCallPromotionID", SqlDbType.BigInt);
                DailyCallPromotionID.Direction = ParameterDirection.Input;
                DailyCallPromotionID.Value = promoteProduct.DailyCallPromotionID;

                SqlParameter DailyCallID = cmd.Parameters.Add("@DailyCallID", SqlDbType.BigInt);
                DailyCallID.Direction = ParameterDirection.Input;
                DailyCallID.Value = dailyCallid;

                SqlParameter ProdID = cmd.Parameters.Add("@ProdID", SqlDbType.BigInt);
                ProdID.Direction = ParameterDirection.Input;
                ProdID.Value = promoteProduct.ID;

                SqlParameter Condition = cmd.Parameters.Add("@Condition", SqlDbType.VarChar, 10);
                Condition.Direction = ParameterDirection.Input;
                Condition.Value = promoteProduct.Condition;

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
                promoteProduct = null;
            }
            return true;
        }
    }
}
