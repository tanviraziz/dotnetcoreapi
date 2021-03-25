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
    public class TourPlanGateway
    {
        public string TourPlanManagement(TourPlan tourPlan)
        {
            SqlTransaction transaction;
            string connString = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                   .AddJsonFile("appsettings.json", false)
                                                   .Build().GetConnectionString("DevConnection").ToString();
            SqlConnection sqlConnection = new SqlConnection(connString);

            sqlConnection.Open();
            transaction = sqlConnection.BeginTransaction();
            SqlCommand cmd = new SqlCommand("API_TourPlanManagement", sqlConnection, transaction);

            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                SqlParameter TourPlanID = cmd.Parameters.Add("@TourPlanID", SqlDbType.BigInt);
                TourPlanID.Direction = ParameterDirection.Input;
                TourPlanID.Value = tourPlan.TourPlanID;

                SqlParameter SFID = cmd.Parameters.Add("@SFID", SqlDbType.BigInt);
                SFID.Direction = ParameterDirection.Input;
                SFID.Value = tourPlan.SFID;

                SqlParameter PlanDate = cmd.Parameters.Add("@PlanDate", SqlDbType.DateTime);
                PlanDate.Direction = ParameterDirection.Input;
                PlanDate.Value = (string.IsNullOrEmpty(tourPlan.TourDate) ? DateTime.Now : DateTime.ParseExact(tourPlan.TourDate, "MM/dd/yyyy", null));

                SqlParameter Condition = cmd.Parameters.Add("@Condition", SqlDbType.VarChar, 10);
                Condition.Direction = ParameterDirection.Input;
                Condition.Value = tourPlan.Condition;

                SqlParameter Flag = cmd.Parameters.Add("@Flag", SqlDbType.VarChar, 10);
                Flag.Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                if (cmd.Parameters["@Flag"].Value.ToString().Trim() == "Er")
                {
                    transaction.Rollback();
                    return "0";
                }

                string tourPlanid = cmd.Parameters["@Flag"].Value.ToString().Trim();
               
                foreach (var task in tourPlan.Tours)
                {
                   if (!TourTaskManagement((TourTask)task, tourPlanid, sqlConnection, transaction))
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
                tourPlan = null;
            }
            return "1";
        }

        public Boolean TourTaskManagement(TourTask task, string tourPlanid, SqlConnection connection, SqlTransaction transaction)
        {
            SqlConnection sqlConnection = connection;
            SqlCommand cmd = new SqlCommand("API_TourPlanTaskManagement", sqlConnection, transaction);

            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                SqlParameter TourTaskID = cmd.Parameters.Add("@TourTaskID", SqlDbType.BigInt);
                TourTaskID.Direction = ParameterDirection.Input;
                TourTaskID.Value = task.ID;

                SqlParameter TourPlanID = cmd.Parameters.Add("@TourPlanID", SqlDbType.BigInt);
                TourPlanID.Direction = ParameterDirection.Input;
                TourPlanID.Value = tourPlanid;

                SqlParameter CustomerID = cmd.Parameters.Add("@CustomerID", SqlDbType.BigInt);
                CustomerID.Direction = ParameterDirection.Input;
                CustomerID.Value = task.CustomerID;

                SqlParameter VisitTime = cmd.Parameters.Add("@VisitTime", SqlDbType.Time);
                VisitTime.Direction = ParameterDirection.Input;
                VisitTime.Value = task.VisitTime;

                SqlParameter IsDoctorTask = cmd.Parameters.Add("@IsDoctorTask", SqlDbType.Bit);
                IsDoctorTask.Direction = ParameterDirection.Input;
                IsDoctorTask.Value = task.DoctorTask;

                SqlParameter Condition = cmd.Parameters.Add("@Condition", SqlDbType.VarChar, 10);
                Condition.Direction = ParameterDirection.Input;
                Condition.Value = task.Condition;

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
                task = null;
            }
            return true;
        }
    }
}
