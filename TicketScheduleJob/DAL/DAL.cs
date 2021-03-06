﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using TicketScheduleJob.Model;

namespace TicketScheduleJob
{
    public class DAL
    {
       
        public static MySqlConnection con = null;

        static Exceptions exceptions;
        static DAL()
        {
            MySettingsConfigMoal mysettingsconfigmoal = new MySettingsConfigMoal(); 
            Program obj = new Program();
            mysettingsconfigmoal = obj.GetConfigDetails();

            con = new MySqlConnection(mysettingsconfigmoal.Connectionstring);
            exceptions = new Exceptions();
        }


        public List<TicketScheduleModal> getScheduleDetails(string ConString)
        {
            DataSet ds = new DataSet();
            List<TicketScheduleModal> ticketschedulemodal = new List<TicketScheduleModal>();

            try
            {
                exceptions.FileText("Step DAL 2 Start");
                //conn.Open();
                MySqlConnection con = new MySqlConnection(ConString);
                MySqlCommand cmd = new MySqlCommand("get_ScheduleSearchDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                cmd.Connection.Close();
                if (ds != null && ds.Tables[0] != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            TicketScheduleModal obj = new TicketScheduleModal()
                            {
                                ScheduleID = dr["ScheduleID"] == System.DBNull.Value ? 0 : Convert.ToInt32(dr["ScheduleID"]),
                                TenantID = dr["TenantID"] == System.DBNull.Value ? 0 : Convert.ToInt32(dr["TenantID"]),
                                ScheduleFor = dr["ScheduleFor"] == System.DBNull.Value ? string.Empty : Convert.ToString(dr["ScheduleFor"]),
                                ScheduleType = dr["ScheduleType"] == System.DBNull.Value ? 0 : Convert.ToInt32(dr["ScheduleType"]),
                                ScheduleTime = Convert.ToString(dr["ScheduleTime"]),
                                IsDaily = Convert.ToBoolean(dr["IsDaily"]),
                                NoOfDay = dr["NoOfDay"] == System.DBNull.Value ? 0 : Convert.ToInt32(dr["NoOfDay"]),
                                IsWeekly = Convert.ToBoolean(dr["IsWeekly"]),
                                NoOfWeek = dr["NoOfWeek"] == System.DBNull.Value ? 0 : Convert.ToInt32(dr["NoOfWeek"]),
                                DayIds = dr["DayIds"] == System.DBNull.Value ? string.Empty : Convert.ToString(dr["DayIds"]),
                                IsDailyForMonth = Convert.ToBoolean(dr["IsDailyForMonth"]),
                                NoOfDaysForMonth = dr["NoOfDaysForMonth"] == System.DBNull.Value ? 0 : Convert.ToInt32(dr["NoOfDaysForMonth"]),
                                NoOfMonthForMonth = dr["NoOfMonthForMonth"] == System.DBNull.Value ? 0 : Convert.ToInt32(dr["NoOfMonthForMonth"]),
                                IsWeeklyForMonth = Convert.ToBoolean(dr["IsWeeklyForMonth"]),
                                NoOfMonthForWeek = dr["NoOfMonthForWeek"] == System.DBNull.Value ? 0 : Convert.ToInt32(dr["NoOfMonthForWeek"]),
                                NoOfWeekForWeek = dr["NoOfWeekForWeek"] == System.DBNull.Value ? 0 : Convert.ToInt32(dr["NoOfWeekForWeek"]),
                                NameOfDayForWeek = dr["NameOfDayForWeek"] == System.DBNull.Value ? string.Empty : Convert.ToString(dr["NameOfDayForWeek"]),
                                IsWeeklyForYear = Convert.ToBoolean(dr["IsWeeklyForYear"]),
                                NoOfWeekForYear = dr["NoOfWeekForYear"] == System.DBNull.Value ? 0 : Convert.ToInt32(dr["NoOfWeekForYear"]),
                                NameOfDayForYear = dr["NameOfDayForYear"] == System.DBNull.Value ? string.Empty : Convert.ToString(dr["NameOfDayForYear"]),
                                NameOfMonthForYear = dr["NameOfMonthForYear"] == System.DBNull.Value ? string.Empty : Convert.ToString(dr["NameOfMonthForYear"]),
                                IsDailyForYear = Convert.ToBoolean(dr["IsDailyForYear"]),
                                NameOfMonthForDailyYear = dr["NameOfMonthForDailyYear"] == System.DBNull.Value ? string.Empty : Convert.ToString(dr["NameOfMonthForDailyYear"]),
                                NoOfDayForDailyYear = dr["NoOfDayForDailyYear"] == System.DBNull.Value ? 0 : Convert.ToInt32(dr["NoOfDayForDailyYear"]),
                                SearchInputParams = dr["SearchInputParams"] == System.DBNull.Value ? string.Empty : Convert.ToString(dr["SearchInputParams"]),
                                IsActive = Convert.ToBoolean(dr["IsActive"]),
                                CreatedBy = dr["CreatedBy"] == System.DBNull.Value ? 0 : Convert.ToInt32(dr["CreatedBy"]),
                                CreatedDate = Convert.ToDateTime(dr["CreatedDate"]),
                                ModifyBy = dr["ModifyBy"] == System.DBNull.Value ? 0 : Convert.ToInt32(dr["ModifyBy"]),
                                ModifyDate = Convert.ToDateTime(dr["ModifyDate"]),
                                CreatedByEmailId = dr["CreatedByEmailId"] == System.DBNull.Value ? string.Empty : Convert.ToString(dr["CreatedByEmailId"]),
                                CreatedByFirstName = dr["CreatedByFirstName"] == System.DBNull.Value ? string.Empty : Convert.ToString(dr["CreatedByFirstName"]),
                                CreatedByLastName = dr["CreatedByLastName"] == System.DBNull.Value ? string.Empty : Convert.ToString(dr["CreatedByLastName"]),
                                SendToEmailID = dr["SendToEmailID"] == System.DBNull.Value ? string.Empty : Convert.ToString(dr["SendToEmailID"]),
                                ScheduleFrom = dr["ScheduleFrom"] == System.DBNull.Value ? 0 : Convert.ToInt32(dr["ScheduleFrom"])
                            };

                            ticketschedulemodal.Add(obj);
                        }

                               
                    }
                }

                exceptions.FileText("Step DAL 2 End");
            }
            catch (Exception ex)
            {
                exceptions.SendErrorToText(ex);
            }
            finally
            {
                if (ds != null)
                    ds.Dispose();
                con.Close();
            }

            return ticketschedulemodal;
        }


        #region Store Reports

        public List<TicketScheduleModal> getStoreScheduleDetails(string ConString)
        {
            DataSet ds = new DataSet();
            List<TicketScheduleModal> ticketschedulemodal = new List<TicketScheduleModal>();

            try
            {
                exceptions.FileText("Step DAL 2 Start");
                //conn.Open();
                MySqlConnection con = new MySqlConnection(ConString);
                MySqlCommand cmd = new MySqlCommand("get_StoreScheduleSearchDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                cmd.Connection.Close();
                if (ds != null && ds.Tables[0] != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            TicketScheduleModal obj = new TicketScheduleModal()
                            {
                                ScheduleID = dr["ScheduleID"] == System.DBNull.Value ? 0 : Convert.ToInt32(dr["ScheduleID"]),
                                TenantID = dr["TenantID"] == System.DBNull.Value ? 0 : Convert.ToInt32(dr["TenantID"]),
                                ScheduleFor = dr["ScheduleFor"] == System.DBNull.Value ? string.Empty : Convert.ToString(dr["ScheduleFor"]),
                                ScheduleType = dr["ScheduleType"] == System.DBNull.Value ? 0 : Convert.ToInt32(dr["ScheduleType"]),
                                ScheduleTime = Convert.ToString(dr["ScheduleTime"]),
                                IsDaily = Convert.ToBoolean(dr["IsDaily"]),
                                NoOfDay = dr["NoOfDay"] == System.DBNull.Value ? 0 : Convert.ToInt32(dr["NoOfDay"]),
                                IsWeekly = Convert.ToBoolean(dr["IsWeekly"]),
                                NoOfWeek = dr["NoOfWeek"] == System.DBNull.Value ? 0 : Convert.ToInt32(dr["NoOfWeek"]),
                                DayIds = dr["DayIds"] == System.DBNull.Value ? string.Empty : Convert.ToString(dr["DayIds"]),
                                IsDailyForMonth = Convert.ToBoolean(dr["IsDailyForMonth"]),
                                NoOfDaysForMonth = dr["NoOfDaysForMonth"] == System.DBNull.Value ? 0 : Convert.ToInt32(dr["NoOfDaysForMonth"]),
                                NoOfMonthForMonth = dr["NoOfMonthForMonth"] == System.DBNull.Value ? 0 : Convert.ToInt32(dr["NoOfMonthForMonth"]),
                                IsWeeklyForMonth = Convert.ToBoolean(dr["IsWeeklyForMonth"]),
                                NoOfMonthForWeek = dr["NoOfMonthForWeek"] == System.DBNull.Value ? 0 : Convert.ToInt32(dr["NoOfMonthForWeek"]),
                                NoOfWeekForWeek = dr["NoOfWeekForWeek"] == System.DBNull.Value ? 0 : Convert.ToInt32(dr["NoOfWeekForWeek"]),
                                NameOfDayForWeek = dr["NameOfDayForWeek"] == System.DBNull.Value ? string.Empty : Convert.ToString(dr["NameOfDayForWeek"]),
                                IsWeeklyForYear = Convert.ToBoolean(dr["IsWeeklyForYear"]),
                                NoOfWeekForYear = dr["NoOfWeekForYear"] == System.DBNull.Value ? 0 : Convert.ToInt32(dr["NoOfWeekForYear"]),
                                NameOfDayForYear = dr["NameOfDayForYear"] == System.DBNull.Value ? string.Empty : Convert.ToString(dr["NameOfDayForYear"]),
                                NameOfMonthForYear = dr["NameOfMonthForYear"] == System.DBNull.Value ? string.Empty : Convert.ToString(dr["NameOfMonthForYear"]),
                                IsDailyForYear = Convert.ToBoolean(dr["IsDailyForYear"]),
                                NameOfMonthForDailyYear = dr["NameOfMonthForDailyYear"] == System.DBNull.Value ? string.Empty : Convert.ToString(dr["NameOfMonthForDailyYear"]),
                                NoOfDayForDailyYear = dr["NoOfDayForDailyYear"] == System.DBNull.Value ? 0 : Convert.ToInt32(dr["NoOfDayForDailyYear"]),
                                SearchInputParams = dr["SearchInputParams"] == System.DBNull.Value ? string.Empty : Convert.ToString(dr["SearchInputParams"]),
                                IsActive = Convert.ToBoolean(dr["IsActive"]),
                                CreatedBy = dr["CreatedBy"] == System.DBNull.Value ? 0 : Convert.ToInt32(dr["CreatedBy"]),
                                CreatedDate = Convert.ToDateTime(dr["CreatedDate"]),
                                ModifyBy = dr["ModifyBy"] == System.DBNull.Value ? 0 : Convert.ToInt32(dr["ModifyBy"]),
                                ModifyDate = Convert.ToDateTime(dr["ModifyDate"]),
                                CreatedByEmailId = dr["CreatedByEmailId"] == System.DBNull.Value ? string.Empty : Convert.ToString(dr["CreatedByEmailId"]),
                                CreatedByFirstName = dr["CreatedByFirstName"] == System.DBNull.Value ? string.Empty : Convert.ToString(dr["CreatedByFirstName"]),
                                CreatedByLastName = dr["CreatedByLastName"] == System.DBNull.Value ? string.Empty : Convert.ToString(dr["CreatedByLastName"]),
                                SendToEmailID = dr["SendToEmailID"] == System.DBNull.Value ? string.Empty : Convert.ToString(dr["SendToEmailID"]),
                                ScheduleFrom = dr["ScheduleFrom"] == System.DBNull.Value ? 0 : Convert.ToInt32(dr["ScheduleFrom"])
                            };

                            ticketschedulemodal.Add(obj);
                        }


                    }
                }

                exceptions.FileText("Step DAL 2 End");
            }
            catch (Exception ex)
            {
                exceptions.SendErrorToText(ex);
            }
            finally
            {
                if (ds != null)
                    ds.Dispose();
                con.Close();
            }

            return ticketschedulemodal;
        }

        #endregion



        #region  DashboardTickets

        public List<SearchOutputDashBoard> GetDashboardTicketsOnSearch(SearchInputModel searchModel,string ConString)
        {
            DataSet ds = new DataSet();
            MySqlConnection con = new MySqlConnection(ConString);
            MySqlCommand cmd = new MySqlCommand();

            List<SearchOutputDashBoard> objSearchResult = new List<SearchOutputDashBoard>();

            List<string> CountList = new List<string>();

            //int rowStart = 0; // searchparams.pageNo - 1) * searchparams.pageSize;
            try
            {

                exceptions.FileText("Step DAL 4 Start");

                if (con != null && con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.Connection = con;

                /*Based on active tab stored procedure will call
                    1. SP_SearchTicketData_ByDate
                    2. SP_SearchTicketData_ByCustomerType
                    3. SP_SearchTicketData_ByTicketType
                    4. SP_SearchTicketData_ByCategoryType
                    5. SP_SearchTicketData_ByAll                 
                 */
                MySqlCommand sqlcmd = new MySqlCommand("", con);

                // sqlcmd.Parameters.AddWithValue("HeaderStatus_Id", searchModel.HeaderStatusId);

                if (searchModel.ActiveTabId == 1)//ByDate
                {
                    sqlcmd.CommandText = "SP_SearchTicketData_ByDate_ForDashboard";

                    sqlcmd.Parameters.AddWithValue("Ticket_CreatedOn", string.IsNullOrEmpty(searchModel.searchDataByDate.Ticket_CreatedOn) ? "" : searchModel.searchDataByDate.Ticket_CreatedOn);
                    sqlcmd.Parameters.AddWithValue("Ticket_ModifiedOn", string.IsNullOrEmpty(searchModel.searchDataByDate.Ticket_ModifiedOn) ? "" : searchModel.searchDataByDate.Ticket_ModifiedOn);
                    sqlcmd.Parameters.AddWithValue("SLA_DueON", searchModel.searchDataByDate.SLA_DueON);
                    sqlcmd.Parameters.AddWithValue("Ticket_StatusID", searchModel.searchDataByDate.Ticket_StatusID);
                }
                else if (searchModel.ActiveTabId == 2)//ByCustomerType
                {
                    sqlcmd.CommandText = "SP_SearchTicketData_ByCustomerType_ForDashBoard";

                    sqlcmd.Parameters.AddWithValue("CustomerMobileNo", string.IsNullOrEmpty(searchModel.searchDataByCustomerType.CustomerMobileNo) ? "" : searchModel.searchDataByCustomerType.CustomerMobileNo);
                    sqlcmd.Parameters.AddWithValue("customerEmail", string.IsNullOrEmpty(searchModel.searchDataByCustomerType.CustomerEmailID) ? "" : searchModel.searchDataByCustomerType.CustomerEmailID);

                    if (string.IsNullOrEmpty(Convert.ToString(searchModel.searchDataByCustomerType.TicketID)) || Convert.ToString(searchModel.searchDataByCustomerType.TicketID) == "")
                        sqlcmd.Parameters.AddWithValue("TicketID", 0);
                    else
                        sqlcmd.Parameters.AddWithValue("TicketID", Convert.ToInt32(searchModel.searchDataByCustomerType.TicketID));

                    sqlcmd.Parameters.AddWithValue("TicketStatusID", searchModel.searchDataByCustomerType.TicketStatusID);
                }
                else if (searchModel.ActiveTabId == 3)//ByTicketType
                {
                    sqlcmd.CommandText = "SP_SearchTicketData_ByTicketType_ForDashBoard";

                    sqlcmd.Parameters.AddWithValue("Priority_Id", searchModel.searchDataByTicketType.TicketPriorityID);
                    sqlcmd.Parameters.AddWithValue("TicketStatusID", searchModel.searchDataByTicketType.TicketStatusID);
                    sqlcmd.Parameters.AddWithValue("channelOfPurchaseIDs", string.IsNullOrEmpty(searchModel.searchDataByTicketType.ChannelOfPurchaseIds) ? "" : searchModel.searchDataByTicketType.ChannelOfPurchaseIds);
                    sqlcmd.Parameters.AddWithValue("ActionTypeIds", searchModel.searchDataByTicketType.ActionTypes);
                }
                else if (searchModel.ActiveTabId == 4) //ByCategory
                {
                    sqlcmd.CommandText = "SP_SearchTicketData_ByCategory_Dashboard";

                    sqlcmd.Parameters.AddWithValue("Category_Id", searchModel.searchDataByCategoryType.CategoryId);
                    sqlcmd.Parameters.AddWithValue("SubCategory_Id", searchModel.searchDataByCategoryType.SubCategoryId);
                    sqlcmd.Parameters.AddWithValue("IssueType_Id", searchModel.searchDataByCategoryType.IssueTypeId);
                    sqlcmd.Parameters.AddWithValue("Ticket_StatusID", searchModel.searchDataByCategoryType.TicketStatusID);
                }
                else if (searchModel.ActiveTabId == 5)
                {
                    sqlcmd.CommandText = "SP_SearchTicketData_ByAll_ForDashBoard";

                    /*Column 1 (5)*/
                    sqlcmd.Parameters.AddWithValue("Ticket_CreatedOn", string.IsNullOrEmpty(searchModel.searchDataByAll.CreatedDate) ? "" : searchModel.searchDataByAll.CreatedDate);
                    sqlcmd.Parameters.AddWithValue("Ticket_ModifiedOn", string.IsNullOrEmpty(searchModel.searchDataByAll.ModifiedDate) ? "" : searchModel.searchDataByAll.ModifiedDate);
                    sqlcmd.Parameters.AddWithValue("Category_Id", searchModel.searchDataByAll.CategoryId);
                    sqlcmd.Parameters.AddWithValue("SubCategory_Id", searchModel.searchDataByAll.SubCategoryId);
                    sqlcmd.Parameters.AddWithValue("IssueType_Id", searchModel.searchDataByAll.IssueTypeId);

                    /*Column 2 (5) */
                    sqlcmd.Parameters.AddWithValue("TicketSourceType_ID", searchModel.searchDataByAll.TicketSourceTypeID);
                    sqlcmd.Parameters.AddWithValue("TicketIdORTitle", string.IsNullOrEmpty(searchModel.searchDataByAll.TicketIdORTitle) ? "" : searchModel.searchDataByAll.TicketIdORTitle);
                    sqlcmd.Parameters.AddWithValue("Priority_Id", searchModel.searchDataByAll.PriorityId);
                    sqlcmd.Parameters.AddWithValue("Ticket_StatusID", searchModel.searchDataByAll.TicketSatutsID);
                    sqlcmd.Parameters.AddWithValue("SLAStatus", string.IsNullOrEmpty(searchModel.searchDataByAll.SLAStatus) ? "" : searchModel.searchDataByAll.SLAStatus);

                    /*Column 3 (5)*/
                    sqlcmd.Parameters.AddWithValue("TicketClaim_ID", Convert.ToInt32(searchModel.searchDataByAll.ClaimId));
                    sqlcmd.Parameters.AddWithValue("InvoiceNumberORSubOrderNo", string.IsNullOrEmpty(searchModel.searchDataByAll.InvoiceNumberORSubOrderNo) ? "" : searchModel.searchDataByAll.InvoiceNumberORSubOrderNo);
                    sqlcmd.Parameters.AddWithValue("OrderItemId", string.IsNullOrEmpty(Convert.ToString(searchModel.searchDataByAll.OrderItemId)) ? 0 : Convert.ToInt32(searchModel.searchDataByAll.OrderItemId));
                    sqlcmd.Parameters.AddWithValue("IsVisitedStore", searchModel.searchDataByAll.IsVisitStore == "yes" ? 1 : 0);
                    sqlcmd.Parameters.AddWithValue("IsWantToVisitStore", searchModel.searchDataByAll.IsWantVistingStore == "yes" ? 1 : 0);

                    /*Column 4 (5)*/
                    sqlcmd.Parameters.AddWithValue("Customer_EmailID", searchModel.searchDataByAll.CustomerEmailID);
                    sqlcmd.Parameters.AddWithValue("CustomerMobileNo", string.IsNullOrEmpty(searchModel.searchDataByAll.CustomerMobileNo) ? "" : searchModel.searchDataByAll.CustomerMobileNo);
                    sqlcmd.Parameters.AddWithValue("AssignTo", searchModel.searchDataByAll.AssignTo);
                    sqlcmd.Parameters.AddWithValue("StoreCodeORAddress", searchModel.searchDataByAll.StoreCodeORAddress);
                    sqlcmd.Parameters.AddWithValue("WantToStoreCodeORAddress", searchModel.searchDataByAll.WantToStoreCodeORAddress);

                    //Row - 2 and Column - 1  (5)
                    sqlcmd.Parameters.AddWithValue("HaveClaim", searchModel.searchDataByAll.HaveClaim);
                    sqlcmd.Parameters.AddWithValue("ClaimStatusId", searchModel.searchDataByAll.ClaimStatusId);
                    sqlcmd.Parameters.AddWithValue("ClaimCategoryId", searchModel.searchDataByAll.ClaimCategoryId);
                    sqlcmd.Parameters.AddWithValue("ClaimSubCategoryId", searchModel.searchDataByAll.ClaimSubCategoryId);
                    sqlcmd.Parameters.AddWithValue("ClaimIssueTypeId", searchModel.searchDataByAll.ClaimIssueTypeId);

                    //Row - 2 and Column - 2  (4)
                    sqlcmd.Parameters.AddWithValue("HaveTask", searchModel.searchDataByAll.HaveTask);
                    sqlcmd.Parameters.AddWithValue("TaskStatus_Id", searchModel.searchDataByAll.TaskStatusId);
                    sqlcmd.Parameters.AddWithValue("TaskDepartment_Id", searchModel.searchDataByAll.TaskDepartment_Id);
                    sqlcmd.Parameters.AddWithValue("TaskFunction_Id", searchModel.searchDataByAll.TaskFunction_Id);
                }



                sqlcmd.Parameters.AddWithValue("CurrentUserId", searchModel.curentUserId);
                sqlcmd.Parameters.AddWithValue("Tenant_ID", searchModel.TenantID);
                sqlcmd.Parameters.AddWithValue("Assignto_IDs", searchModel.AssigntoId.TrimEnd(','));
                sqlcmd.Parameters.AddWithValue("Brand_IDs", searchModel.BrandId.TrimEnd(','));

                sqlcmd.CommandType = CommandType.StoredProcedure;

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = sqlcmd;
                da.Fill(ds);

                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        //SearchOutputDashBoard obj = new SearchOutputDashBoard();

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            SearchOutputDashBoard obj = new SearchOutputDashBoard()
                            {
                                ticketID = Convert.ToInt32(dr["TicketID"] == DBNull.Value ? 0 : dr["TicketID"]),
                                ticketStatus = Convert.ToString((EnumMaster.TicketStatus)Convert.ToInt32(dr["StatusID"] == DBNull.Value ? 0 : dr["StatusID"])),
                                Message = dr["TicketDescription"] == DBNull.Value ? string.Empty : Convert.ToString(dr["TicketDescription"]),
                                Category = dr["CategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CategoryName"]),
                                subCategory = dr["SubCategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["SubCategoryName"]),
                                IssueType = dr["IssueTypeName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["IssueTypeName"]),
                                Priority = dr["PriortyName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["PriortyName"]),
                                Assignee = dr["AssignedName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["AssignedName"]),
                                CreatedOn = dr["CreatedOn"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CreatedOn"]),
                                createdBy = dr["CreatedByName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CreatedByName"]),
                                createdago = dr["CreatedDate"] == DBNull.Value ? string.Empty : SetCreationdetails(Convert.ToString(dr["CreatedDate"]), "CreatedSpan"),
                                assignedTo = dr["AssignedName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["AssignedName"]),
                                assignedago = dr["AssignedDate"] == DBNull.Value ? string.Empty : SetCreationdetails(Convert.ToString(dr["AssignedDate"]), "AssignedSpan"),
                                updatedBy = dr["ModifyByName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ModifyByName"]),
                                updatedago = dr["ModifiedDate"] == DBNull.Value ? string.Empty : SetCreationdetails(Convert.ToString(dr["ModifiedDate"]), "ModifiedSpan"),

                                responseTimeRemainingBy = (dr["AssignedDate"] == DBNull.Value || dr["PriorityRespond"] == DBNull.Value) ?
                            string.Empty : SetCreationdetails(Convert.ToString(dr["PriorityRespond"]) + "|" + Convert.ToString(dr["AssignedDate"]), "RespondTimeRemainingSpan"),
                                responseOverdueBy = (dr["AssignedDate"] == DBNull.Value || dr["PriorityRespond"] == DBNull.Value) ?
                            string.Empty : SetCreationdetails(Convert.ToString(dr["PriorityRespond"]) + "|" + Convert.ToString(dr["AssignedDate"]), "ResponseOverDueSpan"),

                                resolutionOverdueBy = (dr["AssignedDate"] == DBNull.Value || dr["PriorityResolve"] == DBNull.Value) ?
                            string.Empty : SetCreationdetails(Convert.ToString(dr["PriorityResolve"]) + "|" + Convert.ToString(dr["AssignedDate"]), "ResolutionOverDueSpan"),

                                TaskStatus = dr["TaskDetails"] == DBNull.Value ? string.Empty : Convert.ToString(dr["TaskDetails"]),
                                ClaimStatus = dr["ClaimDetails"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ClaimDetails"]),
                                TicketCommentCount = dr["ClaimDetails"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TicketComments"]),
                                isEscalation = dr["IsEscalated"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IsEscalated"])

                            };
                            objSearchResult.Add(obj);
                        }
                    }
                }

                //paging here
                //if (searchparams.pageSize > 0 && objSearchResult.Count > 0)
                //    objSearchResult[0].totalpages = objSearchResult.Count > searchparams.pageSize ? Math.Round(Convert.ToDouble(objSearchResult.Count / searchparams.pageSize)) : 1;

                //objSearchResult = objSearchResult.Skip(rowStart).Take(searchparams.pageSize).ToList();
                exceptions.FileText("Step DAL 4 End");
            }
            catch (Exception ex)
            {
                exceptions.SendErrorToText(ex);
            }
            finally
            {
                if (ds != null) ds.Dispose(); con.Close();
            }
            return objSearchResult;
        }

        public string SetCreationdetails(string time, string ColName)
        {
            string timespan = string.Empty;
            DateTime now = DateTime.Now;
            TimeSpan diff = new TimeSpan();
            string[] PriorityArr = null;

            try
            {
                //timespan = "";
                //if (String.IsNullOrEmpty(timespan))
                //{
                //    return timespan;
                //}
                exceptions.FileText("Step DAL 3 Start");


                if (ColName == "CreatedSpan" || ColName == "ModifiedSpan" || ColName == "AssignedSpan")
                {
                    exceptions.FileText("Step DAL 3.1");
                    diff = now - Convert.ToDateTime(time);
                    exceptions.FileText("Step DAL 3.2");
                    timespan = CalculateSpan(diff) + " ago";

                }
                else if (ColName == "RespondTimeRemainingSpan")
                {
                    exceptions.FileText("Step DAL 3.3");
                    PriorityArr = time.Split(new char[] { '|' })[0].Split(new char[] { '-' });
                    exceptions.FileText("Step DAL 3.4");
                    switch (PriorityArr[1])
                    {
                        case "D":
                            exceptions.FileText("Step DAL D 3.4");
                            diff = (Convert.ToDateTime(time.Split(new char[] { '|' })[1]).AddDays(Convert.ToDouble(PriorityArr[0]))) - now;

                            break;

                        case "H":
                            exceptions.FileText("Step DAL H 3.4");
                            diff = (Convert.ToDateTime(time.Split(new char[] { '|' })[1]).AddHours(Convert.ToDouble(PriorityArr[0]))) - now;

                            break;

                        case "M":
                            exceptions.FileText("Step DAL M 3.4");
                            diff = (Convert.ToDateTime(time.Split(new char[] { '|' })[1]).AddMinutes(Convert.ToDouble(PriorityArr[0]))) - now;

                            break;

                    }
                    exceptions.FileText("Step DAL 3.5");
                    timespan = CalculateSpan(diff);
                }
                else if (ColName == "ResponseOverDueSpan" || ColName == "ResolutionOverDueSpan")
                {
                    exceptions.FileText("Step DAL 3.6");
                    PriorityArr = time.Split(new char[] { '|' })[0].Split(new char[] { '-' });
                    exceptions.FileText("Step DAL 3.7");
                    switch (PriorityArr[1])
                    {
                        case "D":
                            exceptions.FileText("Step DAL D 3.7");
                            diff = now - (Convert.ToDateTime(time.Split(new char[] { '|' })[1]).AddDays(Convert.ToDouble(PriorityArr[0])));

                            break;

                        case "H":
                            exceptions.FileText("Step DAL H 3.7");
                            diff = now - (Convert.ToDateTime(time.Split(new char[] { '|' })[1]).AddHours(Convert.ToDouble(PriorityArr[0])));

                            break;

                        case "M":
                            exceptions.FileText("Step DAL M 3.7");
                            diff = now - (Convert.ToDateTime(time.Split(new char[] { '|' })[1]).AddMinutes(Convert.ToDouble(PriorityArr[0])));

                            break;

                    }
                    exceptions.FileText("Step DAL 3.8");
                    timespan = CalculateSpan(diff);
                }
                exceptions.FileText("Step DAL 3 End");
            }
            catch (Exception ex)
            {
                exceptions.SendErrorToText(ex);
            }
            finally
            {
                if (PriorityArr != null && PriorityArr.Length > 0)
                    Array.Clear(PriorityArr, 0, PriorityArr.Length);
            }
            return timespan;
        }

        public string CalculateSpan(TimeSpan ts)
        {
            string span = string.Empty;
            try
            {
                exceptions.FileText("Step DAL CalculateSpan Start");
                if (Math.Abs(ts.Days) > 0)
                {
                    exceptions.FileText("Step DAL CalculateSpan Days Start");
                    span = Convert.ToString(Math.Abs(ts.Days)) + " Days";
                }
                else if (Math.Abs(ts.Hours) > 0)
                {
                    exceptions.FileText("Step DAL CalculateSpan Hours Start");
                    span = Convert.ToString(Math.Abs(ts.Hours)) + " Hours";
                }
                else if (Math.Abs(ts.Minutes) > 0)
                {
                    exceptions.FileText("Step DAL CalculateSpan Minutes Start");
                    span = Convert.ToString(Math.Abs(ts.Minutes)) + " Minutes";
                }
                else if (Math.Abs(ts.Seconds) > 0)
                {
                    exceptions.FileText("Step DAL CalculateSpan Seconds Start");
                    span = Convert.ToString(Math.Abs(ts.Seconds)) + " Seconds";
                }
                exceptions.FileText("Step DAL CalculateSpan End");
            }
            catch (Exception ex)
            {
                exceptions.SendErrorToText(ex);
            }
            return span;
        }

        #endregion

        #region  Tickets

        public List<SearchResponse> GetTicketsOnSearch(SearchTicketModel searchModel,string ConString)
        {
            DataSet ds = new DataSet();
            MySqlConnection con = new MySqlConnection(ConString);
            MySqlCommand cmd = new MySqlCommand();
            List<SearchResponse> objSearchResult = new List<SearchResponse>();
            List<SearchResponse> temp = new List<SearchResponse>(); //delete later
            List<string> CountList = new List<string>();

            //int rowStart = 0; // searchparams.pageNo - 1) * searchparams.pageSize;
            try
            {
                exceptions.FileText("Step DAL 7 Start");

                if (con != null && con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                cmd.Connection = con;

                /*Based on active tab stored procedure will call
                    1. SP_SearchTicketData_ByDate
                    2. SP_SearchTicketData_ByCustomerType
                    3. SP_SearchTicketData_ByTicketType
                    4. SP_SearchTicketData_ByCategoryType
                    5. SP_SearchTicketData_ByAll                 
                 */
                MySqlCommand sqlcmd = new MySqlCommand("", con);

                sqlcmd.Parameters.AddWithValue("HeaderStatus_Id", searchModel.HeaderStatusId);

                if (searchModel.ActiveTabId == 1)
                {
                    sqlcmd.CommandText = "SP_SearchTicketData_ByDate";

                    sqlcmd.Parameters.AddWithValue("Ticket_CreatedOn", string.IsNullOrEmpty(searchModel.searchDataByDate.Ticket_CreatedOn) ? "" : searchModel.searchDataByDate.Ticket_CreatedOn);
                    sqlcmd.Parameters.AddWithValue("Ticket_ModifiedOn", string.IsNullOrEmpty(searchModel.searchDataByDate.Ticket_ModifiedOn) ? "" : searchModel.searchDataByDate.Ticket_ModifiedOn);
                    sqlcmd.Parameters.AddWithValue("SLA_DueON", searchModel.searchDataByDate.SLA_DueON);
                    sqlcmd.Parameters.AddWithValue("Ticket_StatusID", searchModel.searchDataByDate.Ticket_StatusID);
                }
                else if (searchModel.ActiveTabId == 2)
                {
                    sqlcmd.CommandText = "SP_SearchTicketData_ByCustomerType";

                    sqlcmd.Parameters.AddWithValue("CustomerMobileNo", string.IsNullOrEmpty(searchModel.searchDataByCustomerType.CustomerMobileNo) ? "" : searchModel.searchDataByCustomerType.CustomerMobileNo);
                    sqlcmd.Parameters.AddWithValue("CustomerEmailID", string.IsNullOrEmpty(searchModel.searchDataByCustomerType.CustomerEmailID) ? "" : searchModel.searchDataByCustomerType.CustomerEmailID);
                    sqlcmd.Parameters.AddWithValue("Ticket_ID", searchModel.searchDataByCustomerType.TicketID == null ? 0 : searchModel.searchDataByCustomerType.TicketID);
                    sqlcmd.Parameters.AddWithValue("TicketStatusID", searchModel.searchDataByCustomerType.TicketStatusID);
                }
                else if (searchModel.ActiveTabId == 3)
                {
                    sqlcmd.CommandText = "SP_SearchTicketData_ByTicketType";

                    sqlcmd.Parameters.AddWithValue("Priority_Id", searchModel.searchDataByTicketType.TicketPriorityID);
                    sqlcmd.Parameters.AddWithValue("TicketStatusID", searchModel.searchDataByTicketType.TicketStatusID);
                    sqlcmd.Parameters.AddWithValue("ChannelOfPurchaseIDs", string.IsNullOrEmpty(searchModel.searchDataByTicketType.ChannelOfPurchaseIds) ? "" : searchModel.searchDataByTicketType.ChannelOfPurchaseIds);
                    sqlcmd.Parameters.AddWithValue("ActionTypeIds", searchModel.searchDataByTicketType.ActionTypes);
                }
                else if (searchModel.ActiveTabId == 4)
                {
                    sqlcmd.CommandText = "SP_SearchTicketData_ByCategory";

                    sqlcmd.Parameters.AddWithValue("Category_Id", searchModel.searchDataByCategoryType.CategoryId);
                    sqlcmd.Parameters.AddWithValue("SubCategory_Id", searchModel.searchDataByCategoryType.SubCategoryId);
                    sqlcmd.Parameters.AddWithValue("IssueType_Id", searchModel.searchDataByCategoryType.IssueTypeId);
                    sqlcmd.Parameters.AddWithValue("Ticket_StatusID", searchModel.searchDataByCategoryType.TicketStatusID);
                }
                else if (searchModel.ActiveTabId == 5)
                {
                    sqlcmd.CommandText = "SP_SearchTicketData_ByAll";

                    /*Column 1 (5)*/
                    sqlcmd.Parameters.AddWithValue("Ticket_CreatedOn", string.IsNullOrEmpty(searchModel.searchDataByAll.CreatedDate) ? "" : searchModel.searchDataByAll.CreatedDate);
                    sqlcmd.Parameters.AddWithValue("Ticket_ModifiedOn", string.IsNullOrEmpty(searchModel.searchDataByAll.ModifiedDate) ? "" : searchModel.searchDataByAll.ModifiedDate);
                    sqlcmd.Parameters.AddWithValue("Category_Id", searchModel.searchDataByAll.CategoryId);
                    sqlcmd.Parameters.AddWithValue("SubCategory_Id", searchModel.searchDataByAll.SubCategoryId);
                    sqlcmd.Parameters.AddWithValue("IssueType_Id", searchModel.searchDataByAll.IssueTypeId);

                    /*Column 2 (5) */
                    sqlcmd.Parameters.AddWithValue("TicketSourceType_ID", searchModel.searchDataByAll.TicketSourceTypeID);
                    sqlcmd.Parameters.AddWithValue("TicketIdORTitle", string.IsNullOrEmpty(searchModel.searchDataByAll.TicketIdORTitle) ? "" : searchModel.searchDataByAll.TicketIdORTitle);
                    sqlcmd.Parameters.AddWithValue("Priority_Id", searchModel.searchDataByAll.PriorityId);
                    sqlcmd.Parameters.AddWithValue("Ticket_StatusID", searchModel.searchDataByAll.TicketSatutsID);
                    sqlcmd.Parameters.AddWithValue("SLAStatus", string.IsNullOrEmpty(searchModel.searchDataByAll.SLAStatus) ? "" : searchModel.searchDataByAll.SLAStatus);

                    /*Column 3 (5)*/
                    sqlcmd.Parameters.AddWithValue("TicketClaim_ID", searchModel.searchDataByAll.ClaimId);
                    sqlcmd.Parameters.AddWithValue("InvoiceNumberORSubOrderNo", string.IsNullOrEmpty(searchModel.searchDataByAll.InvoiceNumberORSubOrderNo) ? "" : searchModel.searchDataByAll.InvoiceNumberORSubOrderNo);
                    sqlcmd.Parameters.AddWithValue("OrderItemId", searchModel.searchDataByAll.OrderItemId);

                    /*All for to load all the data*/
                    if (searchModel.searchDataByAll.IsVisitStore.ToLower() != "all")
                        sqlcmd.Parameters.AddWithValue("IsVisitedStore", searchModel.searchDataByAll.IsVisitStore == "yes" ? 1 : 0);
                    else
                        sqlcmd.Parameters.AddWithValue("IsVisitedStore", -1);

                    if (searchModel.searchDataByAll.IsWantVistingStore.ToLower() != "all")
                        sqlcmd.Parameters.AddWithValue("IsWantToVisitStore", searchModel.searchDataByAll.IsWantVistingStore == "yes" ? 1 : 0);
                    else
                        sqlcmd.Parameters.AddWithValue("IsWantToVisitStore", -1);

                    /*Column 4 (5)*/
                    sqlcmd.Parameters.AddWithValue("Customer_EmailID", searchModel.searchDataByAll.CustomerEmailID);
                    sqlcmd.Parameters.AddWithValue("CustomerMobileNo", string.IsNullOrEmpty(searchModel.searchDataByAll.CustomerMobileNo) ? "" : searchModel.searchDataByAll.CustomerMobileNo);
                    sqlcmd.Parameters.AddWithValue("OtherAgentAssignTo", string.IsNullOrEmpty(Convert.ToString(searchModel.searchDataByAll.AssignTo)) ? 0 : Convert.ToInt32(searchModel.searchDataByAll.AssignTo));
                    sqlcmd.Parameters.AddWithValue("StoreCodeORAddress", searchModel.searchDataByAll.StoreCodeORAddress);
                    sqlcmd.Parameters.AddWithValue("WantToStoreCodeORAddress", string.IsNullOrEmpty(searchModel.searchDataByAll.WantToStoreCodeORAddress) ? "" : searchModel.searchDataByAll.WantToStoreCodeORAddress);

                    //Row - 2 and Column - 1  (5)
                    sqlcmd.Parameters.AddWithValue("HaveClaim", searchModel.searchDataByAll.HaveClaim);
                    sqlcmd.Parameters.AddWithValue("ClaimStatusId", searchModel.searchDataByAll.ClaimStatusId);
                    sqlcmd.Parameters.AddWithValue("ClaimCategoryId", searchModel.searchDataByAll.ClaimCategoryId);
                    sqlcmd.Parameters.AddWithValue("ClaimSubCategoryId", searchModel.searchDataByAll.ClaimSubCategoryId);
                    sqlcmd.Parameters.AddWithValue("ClaimIssueTypeId", searchModel.searchDataByAll.ClaimIssueTypeId);

                    //Row - 2 and Column - 2  (4)
                    sqlcmd.Parameters.AddWithValue("HaveTask", searchModel.searchDataByAll.HaveTask);
                    sqlcmd.Parameters.AddWithValue("TaskStatus_Id", searchModel.searchDataByAll.TaskStatusId);
                    sqlcmd.Parameters.AddWithValue("TaskDepartment_Id", searchModel.searchDataByAll.TaskDepartment_Id);
                    sqlcmd.Parameters.AddWithValue("TaskFunction_Id", searchModel.searchDataByAll.TaskFunction_Id);
                }

                sqlcmd.Parameters.AddWithValue("Tenant_ID", searchModel.TenantID);
                sqlcmd.Parameters.AddWithValue("Assignto_Id", searchModel.AssigntoId);

                sqlcmd.CommandType = CommandType.StoredProcedure;

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = sqlcmd;
                da.Fill(ds);

                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            SearchResponse obj = new SearchResponse()
                            {
                                ticketID = dr["TicketID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TicketID"]),
                                ticketStatus = dr["StatusID"] == DBNull.Value ? String.Empty : Convert.ToString((EnumMaster.TicketStatus)Convert.ToInt32(dr["StatusID"])),
                                Message = dr["TicketDescription"] == DBNull.Value ? String.Empty : Convert.ToString(dr["TicketDescription"]),
                                Category = dr["CategoryName"] == DBNull.Value ? String.Empty : Convert.ToString(dr["CategoryName"]),
                                subCategory = dr["SubCategoryName"] == DBNull.Value ? String.Empty : Convert.ToString(dr["SubCategoryName"]),
                                IssueType = dr["IssueTypeName"] == DBNull.Value ? String.Empty : Convert.ToString(dr["IssueTypeName"]),
                                Priority = dr["PriortyName"] == DBNull.Value ? String.Empty : Convert.ToString(dr["PriortyName"]),
                                Assignee = dr["AssignedName"] == DBNull.Value ? String.Empty : Convert.ToString(dr["AssignedName"]),
                                CreatedOn = dr["CreatedOn"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CreatedOn"]),
                                createdBy = dr["CreatedByName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CreatedByName"]),
                                createdago = dr["CreatedDate"] == DBNull.Value ? string.Empty : SetCreationdetails(Convert.ToString(dr["CreatedDate"]), "CreatedSpan"),
                                assignedTo = dr["AssignedName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["AssignedName"]),
                                assignedago = dr["AssignedDate"] == DBNull.Value ? string.Empty : SetCreationdetails(Convert.ToString(dr["AssignedDate"]), "AssignedSpan"),
                                updatedBy = dr["ModifyByName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ModifyByName"]),
                                updatedago = dr["ModifiedDate"] == DBNull.Value ? string.Empty : SetCreationdetails(Convert.ToString(dr["ModifiedDate"]), "ModifiedSpan"),

                                responseTimeRemainingBy = (dr["AssignedDate"] == DBNull.Value || string.IsNullOrEmpty(Convert.ToString(dr["PriorityRespond"]))) ?
                            string.Empty : SetCreationdetails(Convert.ToString(dr["PriorityRespond"]) + "|" + Convert.ToString(dr["AssignedDate"]), "RespondTimeRemainingSpan"),
                                responseOverdueBy = (dr["AssignedDate"] == DBNull.Value || string.IsNullOrEmpty(Convert.ToString(dr["PriorityRespond"]))) ?
                            string.Empty : SetCreationdetails(Convert.ToString(dr["PriorityRespond"]) + "|" + Convert.ToString(dr["AssignedDate"]), "ResponseOverDueSpan"),

                                resolutionOverdueBy = (dr["AssignedDate"] == DBNull.Value || string.IsNullOrEmpty(Convert.ToString(dr["PriorityResolve"]))) ?
                            string.Empty : SetCreationdetails(Convert.ToString(dr["PriorityResolve"]) + "|" + Convert.ToString(dr["AssignedDate"]), "ResolutionOverDueSpan"),

                                TaskStatus = dr["TaskDetails"] == DBNull.Value ? string.Empty : Convert.ToString(dr["TaskDetails"]),
                                ClaimStatus = dr["ClaimDetails"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ClaimDetails"]),
                                TicketCommentCount = dr["TicketComments"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TicketComments"]),
                                isEscalation = dr["IsEscalated"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IsEscalated"]),
                                ticketSourceType = dr["TicketSourceType"] == DBNull.Value ? string.Empty : Convert.ToString(dr["TicketSourceType"]),
                                ticketSourceTypeID = dr["TicketSourceTypeID"] == DBNull.Value ? 0 : Convert.ToInt16(dr["TicketSourceTypeID"]),
                                IsReassigned = dr["IsReassigned"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsReassigned"]),
                                IsSLANearBreach = dr["IsSLANearBreach"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsSLANearBreach"])
                            };

                            objSearchResult.Add(obj);
                        }
                    }
                }
                exceptions.FileText("Step DAL 7 End");
                //paging here
                //if (searchparams.pageSize > 0 && objSearchResult.Count > 0)
                //    objSearchResult[0].totalpages = objSearchResult.Count > searchparams.pageSize ? Math.Round(Convert.ToDouble(objSearchResult.Count / searchparams.pageSize)) : 1;

                //objSearchResult = objSearchResult.Skip(rowStart).Take(searchparams.pageSize).ToList();
            }
            catch (Exception ex)
            {
                exceptions.SendErrorToText(ex);
                //throw ex;
            }
            finally
            {
                if (ds != null) ds.Dispose(); con.Close();
            }
            return objSearchResult;
        }

        #endregion

        #region  ReportService

        public List<SearchResponseReport> GetReportSearch(ReportSearchModel searchModel,string ConString)
        {
            DataSet ds = new DataSet();
            MySqlConnection con = new MySqlConnection(ConString);
            MySqlCommand cmd = new MySqlCommand();
            List<SearchResponseReport> objSearchResult = new List<SearchResponseReport>();

            List<string> CountList = new List<string>();

            //int resultCount = 0; // searchparams.pageNo - 1) * searchparams.pageSize;
            try
            {
                exceptions.FileText("Step DAL 10 Start");
                if (con != null && con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.Connection = con;

                /*Based on active tab stored procedure will call
                    1. SP_SearchTicketData_ByDate
                    2. SP_SearchTicketData_ByCustomerType
                    3. SP_SearchTicketData_ByTicketType
                    4. SP_SearchTicketData_ByCategoryType
                    5. SP_SearchTicketData_ByAll                 
                 */
                MySqlCommand sqlcmd = new MySqlCommand("", con);

                // sqlcmd.Parameters.AddWithValue("HeaderStatus_Id", searchModel.HeaderStatusId);
                // sqlcmd.CommandText = "SP_SearchReportData";

                sqlcmd.CommandText = "SP_Report_SchedulerSearch";

                /*Column 1 (5)*/
                sqlcmd.Parameters.AddWithValue("Ticket_CreatedOn", string.IsNullOrEmpty(searchModel.reportSearch.CreatedDate) ? "" : searchModel.reportSearch.CreatedDate);
                sqlcmd.Parameters.AddWithValue("Ticket_ModifiedOn", string.IsNullOrEmpty(searchModel.reportSearch.ModifiedDate) ? "" : searchModel.reportSearch.ModifiedDate);
                sqlcmd.Parameters.AddWithValue("Category_Id", searchModel.reportSearch.CategoryId);
                sqlcmd.Parameters.AddWithValue("SubCategory_Id", searchModel.reportSearch.SubCategoryId);
                sqlcmd.Parameters.AddWithValue("IssueType_Id", searchModel.reportSearch.IssueTypeId);

                /*Column 2 (5) */
                sqlcmd.Parameters.AddWithValue("TicketSourceType_ID", searchModel.reportSearch.TicketSourceTypeID);
                sqlcmd.Parameters.AddWithValue("TicketIdORTitle", string.IsNullOrEmpty(searchModel.reportSearch.TicketIdORTitle) ? "" : searchModel.reportSearch.TicketIdORTitle);
                sqlcmd.Parameters.AddWithValue("Priority_Id", searchModel.reportSearch.PriorityId);
                sqlcmd.Parameters.AddWithValue("Ticket_StatusID", searchModel.reportSearch.TicketSatutsID);
                sqlcmd.Parameters.AddWithValue("SLAStatus", string.IsNullOrEmpty(searchModel.reportSearch.SLAStatus) ? "" : searchModel.reportSearch.SLAStatus);

                /*Column 3 (5)*/
                sqlcmd.Parameters.AddWithValue("TicketClaim_ID", Convert.ToInt32(searchModel.reportSearch.ClaimId == "" ? "0" : searchModel.reportSearch.ClaimId));
                sqlcmd.Parameters.AddWithValue("InvoiceNumberORSubOrderNo", string.IsNullOrEmpty(searchModel.reportSearch.InvoiceNumberORSubOrderNo) ? "" : searchModel.reportSearch.InvoiceNumberORSubOrderNo);
                sqlcmd.Parameters.AddWithValue("OrderItemId", string.IsNullOrEmpty(Convert.ToString(searchModel.reportSearch.OrderItemId)) ? 0 : Convert.ToInt32(searchModel.reportSearch.OrderItemId));
                sqlcmd.Parameters.AddWithValue("IsVisitedStore", searchModel.reportSearch.IsVisitStore == "yes" ? 1 : 0);
                sqlcmd.Parameters.AddWithValue("IsWantToVisitStore", searchModel.reportSearch.IsWantVistingStore == "yes" ? 1 : 0);

                /*Column 4 (5)*/
                sqlcmd.Parameters.AddWithValue("Customer_EmailID", searchModel.reportSearch.CustomerEmailID);
                sqlcmd.Parameters.AddWithValue("CustomerMobileNo", string.IsNullOrEmpty(searchModel.reportSearch.CustomerMobileNo) ? "" : searchModel.reportSearch.CustomerMobileNo);
                sqlcmd.Parameters.AddWithValue("AssignTo", searchModel.reportSearch.AssignTo);
                sqlcmd.Parameters.AddWithValue("StoreCodeORAddress", searchModel.reportSearch.StoreCodeORAddress);
                sqlcmd.Parameters.AddWithValue("WantToStoreCodeORAddress", searchModel.reportSearch.WantToStoreCodeORAddress);

                //Row - 2 and Column - 1  (5)
                sqlcmd.Parameters.AddWithValue("HaveClaim", searchModel.reportSearch.HaveClaim);
                sqlcmd.Parameters.AddWithValue("ClaimStatusId", searchModel.reportSearch.ClaimStatusId);
                sqlcmd.Parameters.AddWithValue("ClaimCategoryId", searchModel.reportSearch.ClaimCategoryId);
                sqlcmd.Parameters.AddWithValue("ClaimSubCategoryId", searchModel.reportSearch.ClaimSubCategoryId);
                sqlcmd.Parameters.AddWithValue("ClaimIssueTypeId", searchModel.reportSearch.ClaimIssueTypeId);

                //Row - 2 and Column - 2  (4)
                sqlcmd.Parameters.AddWithValue("HaveTask", searchModel.reportSearch.HaveTask);
                sqlcmd.Parameters.AddWithValue("TaskStatus_Id", searchModel.reportSearch.TaskStatusId);
                sqlcmd.Parameters.AddWithValue("TaskDepartment_Id", searchModel.reportSearch.TaskDepartment_Id);
                sqlcmd.Parameters.AddWithValue("TaskFunction_Id", searchModel.reportSearch.TaskFunction_Id);
                //     sqlcmd.Parameters.AddWithValue("Task_Priority", searchModel.reportSearch.TaskPriority);

                sqlcmd.Parameters.AddWithValue("CurrentUserId", searchModel.curentUserId);
                sqlcmd.Parameters.AddWithValue("Tenant_ID", searchModel.TenantID);
                sqlcmd.Parameters.AddWithValue("Assignto_IDs", searchModel.reportSearch.AssignTo.ToString());
                sqlcmd.Parameters.AddWithValue("Brand_IDs", searchModel.reportSearch.BrandID.ToString());

                sqlcmd.CommandType = CommandType.StoredProcedure;

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = sqlcmd;
                da.Fill(ds);

                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        // resultCount = Convert.ToInt32(ds.Tables[0].Rows[0]["RowCount"]);
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            SearchResponseReport obj = new SearchResponseReport()
                            {
                                ticketID = Convert.ToInt32(dr["TicketID"]),
                                ticketStatus = Convert.ToString((EnumMaster.TicketStatus)Convert.ToInt32(dr["StatusID"])),
                                Message = dr["TicketDescription"] == DBNull.Value ? string.Empty : Convert.ToString(dr["TicketDescription"]),
                                Category = dr["CategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CategoryName"]),
                                subCategory = dr["SubCategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["SubCategoryName"]),
                                IssueType = dr["IssueTypeName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["IssueTypeName"]),
                                Priority = dr["PriortyName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["PriortyName"]),
                                Assignee = dr["AssignedName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["AssignedName"]),
                                CreatedOn = dr["CreatedOn"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CreatedOn"]),
                                createdBy = dr["CreatedByName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CreatedByName"]),
                                createdago = dr["CreatedDate"] == DBNull.Value ? string.Empty : SetCreationdetails(Convert.ToString(dr["CreatedDate"]), "CreatedSpan"),
                                assignedTo = dr["AssignedName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["AssignedName"]),
                                assignedago = dr["AssignedDate"] == DBNull.Value ? string.Empty : SetCreationdetails(Convert.ToString(dr["AssignedDate"]), "AssignedSpan"),
                                updatedBy = dr["ModifyByName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ModifyByName"]),
                                updatedago = dr["ModifiedDate"] == DBNull.Value ? string.Empty : SetCreationdetails(Convert.ToString(dr["ModifiedDate"]), "ModifiedSpan"),

                                responseTimeRemainingBy = (dr["AssignedDate"] == DBNull.Value || dr["PriorityRespond"] == DBNull.Value) ?
                            string.Empty : SetCreationdetails(Convert.ToString(dr["PriorityRespond"]) + "|" + Convert.ToString(dr["AssignedDate"]), "RespondTimeRemainingSpan"),
                                responseOverdueBy = (dr["AssignedDate"] == DBNull.Value || dr["PriorityRespond"] == DBNull.Value) ?
                            string.Empty : SetCreationdetails(Convert.ToString(dr["PriorityRespond"]) + "|" + Convert.ToString(dr["AssignedDate"]), "ResponseOverDueSpan"),

                                resolutionOverdueBy = (dr["AssignedDate"] == DBNull.Value || dr["PriorityResolve"] == DBNull.Value) ?
                            string.Empty : SetCreationdetails(Convert.ToString(dr["PriorityResolve"]) + "|" + Convert.ToString(dr["AssignedDate"]), "ResolutionOverDueSpan"),

                                TaskStatus = dr["TaskDetails"] == DBNull.Value ? string.Empty : Convert.ToString(dr["TaskDetails"]),
                                ClaimStatus = dr["ClaimDetails"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ClaimDetails"]),
                                TicketCommentCount = dr["ClaimDetails"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TicketComments"]),
                                isEscalation = dr["IsEscalated"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IsEscalated"]),
                                ticketSourceType = Convert.ToString(dr["TicketSourceType"]),
                                IsReassigned = Convert.ToBoolean(dr["IsReassigned"]),
                                ticketSourceTypeID = Convert.ToInt16(dr["TicketSourceTypeID"])
                            };
                            objSearchResult.Add(obj);
                        }
                    }
                }
                // return resultCount;
                //paging here
                //if (searchparams.pageSize > 0 && objSearchResult.Count > 0)
                //    objSearchResult[0].totalpages = objSearchResult.Count > searchparams.pageSize ? Math.Round(Convert.ToDouble(objSearchResult.Count / searchparams.pageSize)) : 1;

                //objSearchResult = objSearchResult.Skip(rowStart).Take(searchparams.pageSize).ToList();

                exceptions.FileText("Step DAL 10 End");
            }
            catch (Exception ex)
            {
                exceptions.SendErrorToText(ex);
            }
            finally
            {
                if (ds != null) ds.Dispose(); con.Close();
            }
            return objSearchResult;
        }

        #endregion


        #region  StoreReportService

        public SearchStoreResponseReport GetStoreReportSearch(StoreReportModel searchModel,string ConString)
        {
            DataSet ds = new DataSet();
            MySqlConnection con = new MySqlConnection(ConString);
            MySqlCommand cmd = new MySqlCommand();


            List<string> CountList = new List<string>();

            SearchStoreResponseReport objSearchResult = new SearchStoreResponseReport();
            List<SearchStoreTaskReportResponse> TaskReport = new List<SearchStoreTaskReportResponse>();
            List<SearchStoreClaimReportResponse> ClaimReport = new List<SearchStoreClaimReportResponse>();
            List<SearchStoreCampaignReportResponse> CampaignReport = new List<SearchStoreCampaignReportResponse>();

            try
            {
                exceptions.FileText("Step DAL 10 Start");
                if (con != null && con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.Connection = con;

                cmd = new MySqlCommand("SP_ScheduleStoreReportForDownload", con);
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@_TenantID", searchModel.TenantID);
                cmd.Parameters.AddWithValue("@_ActiveTabID", searchModel.ActiveTabId);

                /*------------------ TASK PARAMETERS ------------------------------*/

                cmd.Parameters.AddWithValue("@_TaskTitle", string.IsNullOrEmpty(searchModel.TaskTitle) ? "" : searchModel.TaskTitle);
                cmd.Parameters.AddWithValue("@_TaskStatus", string.IsNullOrEmpty(searchModel.TaskStatus) ? "" : searchModel.TaskStatus.TrimEnd(','));
                cmd.Parameters.AddWithValue("@_IsTaskWithTicket", Convert.ToInt16(searchModel.IsTaskWithTicket));
                cmd.Parameters.AddWithValue("@_TaskTicketID", searchModel.TaskTicketID);
                cmd.Parameters.AddWithValue("@_DepartmentIds", string.IsNullOrEmpty(searchModel.DepartmentIds) ? "" : searchModel.DepartmentIds.TrimEnd(','));
                cmd.Parameters.AddWithValue("@_FunctionIds", string.IsNullOrEmpty(searchModel.FunctionIds) ? "" : searchModel.FunctionIds.TrimEnd(','));
                cmd.Parameters.AddWithValue("@_PriorityIds", string.IsNullOrEmpty(searchModel.PriorityIds) ? "" : searchModel.PriorityIds.TrimEnd(','));
                cmd.Parameters.AddWithValue("@_IsTaskWithClaim", Convert.ToInt16(searchModel.IsTaskWithClaim));
                cmd.Parameters.AddWithValue("@_TaskClaimID", searchModel.TaskClaimID);
                cmd.Parameters.AddWithValue("@_TaskCreatedDate", string.IsNullOrEmpty(searchModel.TaskCreatedDate) ? "" : searchModel.TaskCreatedDate);
                cmd.Parameters.AddWithValue("@_TaskCreatedBy",  searchModel.TaskCreatedBy);
                cmd.Parameters.AddWithValue("@_TaskAssignedId", searchModel.TaskAssignedId);

                /*------------------ ENDS HERE-------------------------------*/

                /*------------------ CLAIM  PARAMETERS------------------------------*/

                cmd.Parameters.AddWithValue("@_ClaimID", searchModel.ClaimID);
                cmd.Parameters.AddWithValue("@_ClaimStatus", string.IsNullOrEmpty(searchModel.ClaimStatus) ? "" : searchModel.ClaimStatus.TrimEnd(','));
                cmd.Parameters.AddWithValue("@_IsClaimWithTicket", Convert.ToInt16(searchModel.IsClaimWithTicket));
                cmd.Parameters.AddWithValue("@_ClaimTicketID", searchModel.ClaimTicketID);
                cmd.Parameters.AddWithValue("@_ClaimCategoryIds", string.IsNullOrEmpty(searchModel.ClaimCategoryIds) ? "" : searchModel.ClaimCategoryIds.TrimEnd(','));
                cmd.Parameters.AddWithValue("@_ClaimSubCategoryIds", string.IsNullOrEmpty(searchModel.ClaimSubCategoryIds) ? "" : searchModel.ClaimSubCategoryIds.TrimEnd(','));
                cmd.Parameters.AddWithValue("@_ClaimIssuetypeIds", string.IsNullOrEmpty(searchModel.ClaimIssuetypeIds) ? "" : searchModel.ClaimIssuetypeIds);
                cmd.Parameters.AddWithValue("@_IsClaimWithTask", Convert.ToInt16(searchModel.IsClaimWithTask));
                cmd.Parameters.AddWithValue("@_ClaimTaskID", searchModel.ClaimTaskID);
                cmd.Parameters.AddWithValue("@_ClaimCreatedDate", string.IsNullOrEmpty(searchModel.ClaimCreatedDate) ? "" : searchModel.ClaimCreatedDate);
                cmd.Parameters.AddWithValue("@_ClaimCreatedBy", searchModel.ClaimCreatedBy);
                cmd.Parameters.AddWithValue("@_ClaimAssignedId", searchModel.ClaimAssignedId);



                /*------------------ CAMPAIGN  PARAMETERS------------------------------*/

                cmd.Parameters.AddWithValue("@_CampaignName", string.IsNullOrEmpty(searchModel.CampaignName) ? "" : searchModel.CampaignName);
                cmd.Parameters.AddWithValue("@_CampaignAssignedId", searchModel.CampaignAssignedIds);
                cmd.Parameters.AddWithValue("@_CampaignStartDate", string.IsNullOrEmpty(searchModel.CampaignStartDate) ? "" : searchModel.CampaignStartDate);
                cmd.Parameters.AddWithValue("@_CampaignEndDate", string.IsNullOrEmpty(searchModel.CampaignEndDate) ? "" : searchModel.CampaignEndDate);
                cmd.Parameters.AddWithValue("@_CampaignStatusids", string.IsNullOrEmpty(searchModel.CampaignStatusids) ? "" : searchModel.CampaignStatusids.TrimEnd(','));

                /*------------------ ENDS HERE-------------------------------*/


                cmd.CommandType = CommandType.StoredProcedure;

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);

                if (ds != null && ds.Tables != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (searchModel.ActiveTabId.Equals(1))// task mapping
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                SearchStoreTaskReportResponse obj = new SearchStoreTaskReportResponse()
                                {
                                    TaskID = Convert.ToInt32(dr["TaskID"]),
                                    TicketID = dr["TicketID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TicketID"]),
                                    TicketDescription = dr["TicketDescription"] == DBNull.Value ? string.Empty : Convert.ToString(dr["TicketDescription"]),
                                    TaskTitle = dr["TaskTitle"] == DBNull.Value ? string.Empty : Convert.ToString(dr["TaskTitle"]),
                                    TaskDescription = dr["TaskDescription"] == DBNull.Value ? string.Empty : Convert.ToString(dr["TaskDescription"]),
                                    DepartmentId = dr["DepartmentId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["DepartmentId"]),
                                    DepartmentName = dr["DepartmentName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["DepartmentName"]),
                                    FunctionID = dr["FunctionID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["FunctionID"]),
                                    FunctionName = dr["FunctionName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["FunctionName"]),
                                    PriorityID = dr["PriorityID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["PriorityID"]),
                                    PriorityName = dr["PriorityName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["PriorityName"]),
                                    TaskEndTime = dr["TaskEndTime"] == DBNull.Value ? string.Empty : Convert.ToString(dr["TaskEndTime"]),
                                    TaskStatus = dr["TaskStatus"] == DBNull.Value ? string.Empty : Convert.ToString(dr["TaskStatus"]),
                                    CreatedBy = dr["CreatedBy"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CreatedBy"]),
                                    CreatedByName = dr["CreatedByName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CreatedByName"]),
                                    CreatedDate = dr["CreatedDate"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CreatedDate"]),
                                    ModifiedBy = dr["ModifiedBy"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ModifiedBy"]),
                                    ModifiedByName = dr["ModifiedByName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ModifiedByName"]),
                                    ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ModifiedDate"]),
                                    IsActive = dr["IsActive"] == DBNull.Value ? string.Empty : Convert.ToString(dr["IsActive"]),

                                };

                                TaskReport.Add(obj);
                            }

                            objSearchResult.TaskReport = TaskReport;
                        }
                        else if (searchModel.ActiveTabId.Equals(2))// claim mapping
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                SearchStoreClaimReportResponse obj = new SearchStoreClaimReportResponse()
                                {
                                    ClaimID = Convert.ToInt32(dr["ClaimID"]),
                                    ClaimTitle = dr["ClaimTitle"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ClaimTitle"]),
                                    ClaimDescription = dr["ClaimDescription"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ClaimDescription"]),
                                    BrandID = dr["BrandID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["BrandID"]),
                                    BrandName = dr["BrandName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["BrandName"]),
                                    CategoryID = dr["CategoryID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CategoryID"]),
                                    CategoryName = dr["CategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CategoryName"]),
                                    SubCategoryID = dr["SubCategoryID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SubCategoryID"]),
                                    SubCategoryName = dr["SubCategoryName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["SubCategoryName"]),

                                    IssueTypeID = dr["IssueTypeID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IssueTypeID"]),
                                    IssueTypeName = dr["IssueTypeName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["IssueTypeName"]),
                                    PriorityID = dr["PriorityID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["PriorityID"]),
                                    PriorityName = dr["PriorityName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["PriorityName"]),
                                    CustomerID = dr["CustomerID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CustomerID"]),
                                    CustomerName = dr["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CustomerName"]),
                                    OrderMasterID = dr["OrderMasterID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["OrderMasterID"]),
                                    OrderNo = dr["OrderNumber"] == DBNull.Value ? string.Empty : Convert.ToString(dr["OrderNumber"]),
                                    ClaimPercent = dr["ClaimPercent"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ClaimPercent"]),
                                    ClaimAssignedID = dr["AssignedID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["AssignedID"]),
                                    AssignedToName = dr["AssignedToName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["AssignedToName"]),
                                    ClaimStatus = dr["ClaimStatus"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ClaimStatus"]),

                                    IsActive = dr["IsActive"] == DBNull.Value ? string.Empty : Convert.ToString(dr["IsActive"]),
                                    ClaimApproved = dr["ClaimApproved"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ClaimApproved"]),
                                    ClaimRejected = dr["ClaimRejected"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ClaimRejected"]),
                                    CreatedBy = dr["CreatedBy"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CreatedBy"]),
                                    CreatedByName = dr["CreatedByName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CreatedByName"]),
                                    CreatedDate = dr["CreatedDate"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CreatedDate"]),
                                    ModifiedBy = dr["ModifiedBy"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ModifiedBy"]),
                                    ModifiedByName = dr["ModifiedByName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ModifiedByName"]),
                                    ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ModifiedDate"]),
                                    IsClaimEscalated = dr["IsClaimEscalated"] == DBNull.Value ? string.Empty : Convert.ToString(dr["IsClaimEscalated"]),
                                    IsCustomerResponseDone = dr["IsCustomerResponseDone"] == DBNull.Value ? string.Empty : Convert.ToString(dr["IsCustomerResponseDone"]),
                                    CustomerResponsedOn = dr["CustomerResponsedOn"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CustomerResponsedOn"]),
                                    FinalClaimPercent = dr["FinalClaimPercent"] == DBNull.Value ? string.Empty : Convert.ToString(dr["FinalClaimPercent"]),
                                    TicketDescription = dr["TicketDescription"] == DBNull.Value ? string.Empty : Convert.ToString(dr["TicketDescription"]),
                                    TaskDescription = dr["TaskDescription"] == DBNull.Value ? string.Empty : Convert.ToString(dr["TaskDescription"]),


                                };
                                ClaimReport.Add(obj);
                            }
                            objSearchResult.ClaimReport = ClaimReport;
                        }
                        else// campaign mapping
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                SearchStoreCampaignReportResponse obj = new SearchStoreCampaignReportResponse()
                                {
                                    CampaignCustomerID = dr["CampaignCustomerID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CampaignCustomerID"]),
                                    CustomerID = dr["CustomerID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CustomerID"]),
                                    CustomerName = dr["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CustomerName"]),
                                    CampaignTypeID = dr["CampaignTypeID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CampaignTypeID"]),
                                    CampaignName = dr["CampaignName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CampaignName"]),
                                    CampaignTypeDate = dr["CampaignTypeDate"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CampaignTypeDate"]),
                                    CallReScheduledTo = dr["CallReScheduledTo"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CallReScheduledTo"]),
                                    CreatedBy = dr["CreatedBy"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CreatedBy"]),
                                    CreatedByName = dr["CreatedByName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CreatedByName"]),

                                    CampaignStatus = dr["CampaignStatus"] == DBNull.Value ? string.Empty : Convert.ToString(dr["CampaignStatus"]),
                                    AssignedTo = dr["AssignedTo"] == DBNull.Value ? 0 : Convert.ToInt32(dr["AssignedTo"]),
                                    AssignedToName = dr["AssignedToName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["AssignedToName"]),
                                    Response = dr["Response"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Response"]),
                                    NoOfTimesNotContacted = dr["Response"] == DBNull.Value ? 0 : Convert.ToInt32(dr["NoOfTimesNotContacted"]),

                                };
                                CampaignReport.Add(obj);
                            }

                            objSearchResult.CampaignReport = CampaignReport;
                        }
                    }
                }

                exceptions.FileText("Step DAL 10 End");
            }
            catch (Exception ex)
            {
                exceptions.SendErrorToText(ex);
            }
            finally
            {
                if (ds != null) ds.Dispose(); con.Close();
            }
            return objSearchResult;
        }

        #endregion


        public SMTPDetails GetSMTPDetails(int TenantID,string ConString)
        {
            DataSet ds = new DataSet();
            SMTPDetails sMTPDetails = new SMTPDetails();

            try
            {
                exceptions.FileText("Step DAL 12 Start");
                MySqlConnection con = new MySqlConnection(ConString);
                MySqlCommand cmd = new MySqlCommand();

                con.Open();
                cmd.Connection = con;
                MySqlCommand cmd1 = new MySqlCommand("SP_getSMTPDetails", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Tenant_ID", TenantID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    sMTPDetails.EnableSsl = Convert.ToBoolean(ds.Tables[0].Rows[0]["EnabledSSL"]);
                    sMTPDetails.SMTPPort = Convert.ToString(ds.Tables[0].Rows[0]["SMTPPort"]);
                    sMTPDetails.FromEmailId = Convert.ToString(ds.Tables[0].Rows[0]["EmailUserID"]);
                    sMTPDetails.EmailSenderName = Convert.ToString(ds.Tables[0].Rows[0]["EmailSenderName"]);
                    sMTPDetails.IsBodyHtml = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsBodyHtml"]);
                    sMTPDetails.Password = Convert.ToString(ds.Tables[0].Rows[0]["EmailPassword"]);
                    sMTPDetails.SMTPHost = Convert.ToString(ds.Tables[0].Rows[0]["SMTPHost"]);
                    sMTPDetails.SMTPServer = Convert.ToString(ds.Tables[0].Rows[0]["SMTPHost"]);
                }

                exceptions.FileText("Step DAL 12 End");
            }
            catch (Exception ex)
            {
                exceptions.SendErrorToText(ex);
            }
            finally
            {
                if (ds != null)
                    ds.Dispose(); con.Close();
            }

            return sMTPDetails;
        }

        public void GetMailContent(TicketScheduleModal ticketschedulemodal)
        {
            DataSet ds = new DataSet();
            try
            {
                exceptions.FileText("Step DAL 13 Start");

                MySqlCommand cmd = new MySqlCommand();

                con.Open();
                cmd.Connection = con;
                MySqlCommand cmd1 = new MySqlCommand("SP_getMailContent", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Alert_TypeID", ticketschedulemodal.Alert_TypeID);
                cmd1.Parameters.AddWithValue("@_ScheduleID", ticketschedulemodal.ScheduleID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    ticketschedulemodal.Emailbody =  Convert.ToString(ds.Tables[0].Rows[0]["Content"]);
                   // ticketschedulemodal.Emailbody = ticketschedulemodal.Emailbody.Replace("@ScheduledBy", ticketschedulemodal.CreatedByFirstName + " " + ticketschedulemodal.CreatedByLastName);
                   // ticketschedulemodal.Emailbody = ticketschedulemodal.Emailbody.Replace("@ScheduledTime", ticketschedulemodal.ScheduleTime);

                    ticketschedulemodal.Emailsubject = Convert.ToString(ds.Tables[0].Rows[0]["Subject"]);
                }

                exceptions.FileText("Step DAL 13 End");
            }
            catch (Exception ex)
            {
                exceptions.SendErrorToText(ex);
            }
            finally
            {

                if (ds != null)
                    ds.Dispose(); con.Close();
            }

        }

        public void GetSoreMailContent(TicketScheduleModal ticketschedulemodal)
        {
            DataSet ds = new DataSet();
            try
            {
                exceptions.FileText("Step DAL 13 Start");

                MySqlCommand cmd = new MySqlCommand();

                con.Open();
                cmd.Connection = con;
                MySqlCommand cmd1 = new MySqlCommand("SP_getStoreMailContent", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Alert_TypeID", ticketschedulemodal.Alert_TypeID);
                cmd1.Parameters.AddWithValue("@_ScheduleID", ticketschedulemodal.ScheduleID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    ticketschedulemodal.Emailbody = Convert.ToString(ds.Tables[0].Rows[0]["Content"]);
                    // ticketschedulemodal.Emailbody = ticketschedulemodal.Emailbody.Replace("@ScheduledBy", ticketschedulemodal.CreatedByFirstName + " " + ticketschedulemodal.CreatedByLastName);
                    // ticketschedulemodal.Emailbody = ticketschedulemodal.Emailbody.Replace("@ScheduledTime", ticketschedulemodal.ScheduleTime);

                    ticketschedulemodal.Emailsubject = Convert.ToString(ds.Tables[0].Rows[0]["Subject"]);
                }

                exceptions.FileText("Step DAL 13 End");
            }
            catch (Exception ex)
            {
                exceptions.SendErrorToText(ex);
            }
            finally
            {

                if (ds != null)
                    ds.Dispose(); con.Close();
            }

        }

        public void SchedulerMailResult(TicketScheduleModal ticketschedulemodal, bool isSend, string SchedulerType, string InnerExceptions, string Message, string StackTrace, string StatusCode = "")
        {
            try
            {
                exceptions.FileText("Step DAL 16 Start");

                string exceptionmsg = "InnerExceptions: " + InnerExceptions + ", Message: " + Message + ", StackTrace: " + StackTrace + ", StatusCode: " + StatusCode;
                con.Open();
                MySqlCommand cmd = new MySqlCommand("SP_InsertSchedulerMailResult", con);
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@_EMailID", ticketschedulemodal.CreatedByEmailId);
                cmd.Parameters.AddWithValue("@_SchedulerID", ticketschedulemodal.ScheduleID);
                cmd.Parameters.AddWithValue("@_SchedulerType", SchedulerType);
                cmd.Parameters.AddWithValue("@_IsSend", isSend);
                cmd.Parameters.AddWithValue("@_Message", exceptionmsg);
                cmd.CommandType = CommandType.StoredProcedure;
                int updatecount = cmd.ExecuteNonQuery();

                exceptions.FileText("Step DAL 16 End");
            }
            catch (Exception ex)
            {
                exceptions.SendErrorToText(ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        public int InsertErrorLog(ErrorLogs errorLog)
        {
            int Success = 0;
            try
            {

                con.Open();
                MySqlCommand cmd = new MySqlCommand("SP_ErrorLog", con);
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@User_ID", errorLog.UserID);
                cmd.Parameters.AddWithValue("@Tenant_ID", errorLog.TenantID);
                cmd.Parameters.AddWithValue("@Controller_Name", errorLog.ControllerName);
                cmd.Parameters.AddWithValue("@Action_Name", errorLog.ActionName);
                cmd.Parameters.AddWithValue("@_Exceptions", errorLog.Exceptions);
                cmd.Parameters.AddWithValue("@_MessageException", errorLog.MessageException);
                cmd.Parameters.AddWithValue("@_IPAddress", errorLog.IPAddress);
                cmd.CommandType = CommandType.StoredProcedure;
                Success = Convert.ToInt32(cmd.ExecuteNonQuery());
                con.Close();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return Success;
        }
    }
}
