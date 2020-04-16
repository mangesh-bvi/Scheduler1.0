using ClosedXML.Excel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TicketScheduleJob
{
    public class BAL
    {
        DAL getdata;
        Exceptions exceptions;
        public BAL()
        {
            getdata = new DAL();
            exceptions = new Exceptions();
        }

        public void GetScheduleDetails()
        {
            try
            {
                exceptions.FileText("Step BAL 1 Start");

                List<TicketScheduleModal> ListTicketScheduleModal = new List<TicketScheduleModal>();
                ListTicketScheduleModal = getdata.getScheduleDetails();

                if (ListTicketScheduleModal.Count > 0)
                {
                    for (int i = 0; i < ListTicketScheduleModal.Count; i++)
                    {
                        if (!String.IsNullOrEmpty(ListTicketScheduleModal[i].SearchInputParams))
                        {
                            if (ListTicketScheduleModal[i].ScheduleFrom == 0)
                            {
                                SearchInputModel searchparams = JsonConvert.DeserializeObject<SearchInputModel>(ListTicketScheduleModal[i].SearchInputParams);
                                searchparams.curentUserId = ListTicketScheduleModal[i].CreatedBy;
                                searchparams.TenantID = ListTicketScheduleModal[i].TenantID;
                                ListTicketScheduleModal[i].SearchOutputFileName = DashBoardSearchTicket(searchparams, ListTicketScheduleModal[i].CreatedBy, ListTicketScheduleModal[i].TenantID);
                                ListTicketScheduleModal[i].Alert_TypeID = (int)EnumMaster.Alert_TypeID.Dashboard;
                            }
                            if (ListTicketScheduleModal[i].ScheduleFrom == 2)
                            {
                                SearchTicketModel searchparams = JsonConvert.DeserializeObject<SearchTicketModel>(ListTicketScheduleModal[i].SearchInputParams);
                                searchparams.AssigntoId = ListTicketScheduleModal[i].CreatedBy;
                                searchparams.TenantID = ListTicketScheduleModal[i].TenantID;
                                ListTicketScheduleModal[i].SearchOutputFileName = GetTicketsOnSearch(searchparams, ListTicketScheduleModal[i].CreatedBy, ListTicketScheduleModal[i].TenantID);
                                ListTicketScheduleModal[i].Alert_TypeID = (int)EnumMaster.Alert_TypeID.Ticket;
                            }
                            if (ListTicketScheduleModal[i].ScheduleFrom == 3)
                            {
                                ReportSearchModel searchparams = new ReportSearchModel();
                                searchparams.reportSearch = JsonConvert.DeserializeObject<ReportSearchData>(ListTicketScheduleModal[i].SearchInputParams);
                                searchparams.curentUserId = ListTicketScheduleModal[i].CreatedBy;
                                searchparams.TenantID = ListTicketScheduleModal[i].TenantID;
                                ListTicketScheduleModal[i].SearchOutputFileName = GetReportSearch(searchparams, ListTicketScheduleModal[i].CreatedBy, ListTicketScheduleModal[i].TenantID);
                                ListTicketScheduleModal[i].Alert_TypeID = (int)EnumMaster.Alert_TypeID.Report;
                            }


                            ListTicketScheduleModal[i].SMTPDetails = getdata.GetSMTPDetails(ListTicketScheduleModal[i].TenantID);

                            getdata.GetMailContent(ListTicketScheduleModal[i]);
                        }
                    }
                }

                if (ListTicketScheduleModal.Count > 0)
                {
                    for (int i = 0; i < ListTicketScheduleModal.Count; i++)
                    {
                        if (!String.IsNullOrEmpty(ListTicketScheduleModal[i].SearchInputParams))
                        {
                            Task t = ProcessToSendMail(ListTicketScheduleModal[i]);
                            t.Wait();
                        }
                    }
                }

                exceptions.FileText("Step BAL 1 End");
            }
            catch (Exception ex)
            {
                exceptions.SendErrorToText(ex);
            }

        }

        public async Task ProcessToSendMail(TicketScheduleModal ticketschedulemodal)
        {
            try
            {
                exceptions.FileText("Step BAL 14 Start");
                await Task.Run(() => SendEmail(ticketschedulemodal));
                exceptions.FileText("Step BAL 14 End");
            }
            catch(Exception ex)
            {
                exceptions.SendErrorToText(ex);
            }
        }

        #region  DashboardTickets

        private string DashBoardSearchTicket(SearchInputModel searchparams, int CreatedBy, int TenantID)
        {
            List<SearchOutputDashBoard> _searchResult = null;
            string SearchOutputFileName = null;
            try
            {
                exceptions.FileText("Step BAL 3 Start");
                _searchResult = getdata.GetDashboardTicketsOnSearch(searchparams);

                SearchOutputFileName = DashboardCreateExcel(_searchResult, CreatedBy, TenantID);
                exceptions.FileText("Step BAL 3 End");
            }
            catch (Exception ex)
            {
                exceptions.SendErrorToText(ex);
            }
            return SearchOutputFileName;
        }

        private string DashboardCreateExcel(List<SearchOutputDashBoard> searchResult, int CreatedBy, int TenantID)
        {
            string SearchOutputFileName = null;
            try
            {
                exceptions.FileText("Step BAL 5 Start");

                var wb = new XLWorkbook();
                var ws = wb.Worksheets.Add("Games");

                ws.Cell("A1").Value = "ID";
                ws.Cell("B1").Value = "Status";
                ws.Cell("C1").Value = "Subject/Latest Message";
                ws.Cell("D1").Value = "Category";
                ws.Cell("E1").Value = "Priority";
                ws.Cell("F1").Value = "Assignee";
                ws.Cell("G1").Value = "Creation On";

                for (int i = 0; i < searchResult.Count; i++)
                {

                    ws.Cell("A" + (i + 2)).Value = searchResult[i].ticketID;
                    ws.Cell("B" + (i + 2)).Value = searchResult[i].ticketStatus;
                    ws.Cell("C" + (i + 2)).Value = searchResult[i].Message;
                    ws.Cell("D" + (i + 2)).Value = searchResult[i].Category;
                    ws.Cell("E" + (i + 2)).Value = searchResult[i].Priority;
                    ws.Cell("F" + (i + 2)).Value = searchResult[i].assignedTo;
                    ws.Cell("G" + (i + 2)).Value = searchResult[i].CreatedOn;
                }

                // Beautify
                ws.Range("A1:G1").Style.Font.Bold = true;
                ws.Columns().AdjustToContents();

                SearchOutputFileName = GetNameOfExcel(CreatedBy, TenantID, "DashBoard");

                wb.SaveAs(SearchOutputFileName);

                exceptions.FileText("Step BAL 5 Start");
            }
            catch (Exception ex)
            {
                exceptions.SendErrorToText(ex);
            }
            return SearchOutputFileName;
        }

        #endregion

        #region  Tickets

        private string GetTicketsOnSearch(SearchTicketModel searchModel, int CreatedBy, int TenantID)
        {
            List<SearchResponse> _searchResult = null;
            string SearchOutputFileName = null;
            try
            {
                exceptions.FileText("Step BAL 6 Start");

                _searchResult = getdata.GetTicketsOnSearch(searchModel);

                SearchOutputFileName = TicketsCreateExcel(_searchResult, CreatedBy, TenantID);

                exceptions.FileText("Step BAL 6 End");
            }
            catch (Exception ex)
            {
                exceptions.SendErrorToText(ex);
            }
            return SearchOutputFileName;
        }

        private string TicketsCreateExcel(List<SearchResponse> searchResult, int CreatedBy, int TenantID)
        {
            string SearchOutputFileName = null;
            try
            {
                exceptions.FileText("Step BAL 8 Start");

                var wb = new XLWorkbook();
                var ws = wb.Worksheets.Add("Games");

                ws.Cell("A1").Value = "ID";
                ws.Cell("B1").Value = "Status";
                ws.Cell("C1").Value = "Subject/Latest Message";
                ws.Cell("D1").Value = "Category";
                ws.Cell("E1").Value = "Priority";
                ws.Cell("F1").Value = "Assignee";
                ws.Cell("G1").Value = "Creation On";

                for (int i = 0; i < searchResult.Count; i++)
                {

                    ws.Cell("A" + (i + 2)).Value = searchResult[i].ticketID;
                    ws.Cell("B" + (i + 2)).Value = searchResult[i].ticketStatus;
                    ws.Cell("C" + (i + 2)).Value = searchResult[i].Message;
                    ws.Cell("D" + (i + 2)).Value = searchResult[i].Category;
                    ws.Cell("E" + (i + 2)).Value = searchResult[i].Priority;
                    ws.Cell("F" + (i + 2)).Value = searchResult[i].assignedTo;
                    ws.Cell("G" + (i + 2)).Value = searchResult[i].CreatedOn;
                }

                // Beautify
                ws.Range("A1:G1").Style.Font.Bold = true;
                ws.Columns().AdjustToContents();

                SearchOutputFileName = GetNameOfExcel(CreatedBy, TenantID, "Tickets");

                wb.SaveAs(SearchOutputFileName);

                exceptions.FileText("Step BAL 8 End");
            }
            catch (Exception ex)
            {
                exceptions.SendErrorToText(ex);
            }
            return SearchOutputFileName;
        }

        #endregion

        #region  ReportService

        private string GetReportSearch(ReportSearchModel searchModel, int CreatedBy, int TenantID)
        {
            List<SearchResponseReport> _searchResult = null;
            string SearchOutputFileName = null;
            try
            {
                exceptions.FileText("Step BAL 9 Start");

                _searchResult = getdata.GetReportSearch(searchModel);

                SearchOutputFileName = ReportCreateExcel(_searchResult, CreatedBy, TenantID);

                exceptions.FileText("Step BAL 9 End");
            }
            catch (Exception ex)
            {
                exceptions.SendErrorToText(ex);
            }
            return SearchOutputFileName;
        }

        private string ReportCreateExcel(List<SearchResponseReport> searchResult, int CreatedBy, int TenantID)
        {
            string SearchOutputFileName = null;
            try
            {
                exceptions.FileText("Step BAL 11 Start");

                var wb = new XLWorkbook();
                var ws = wb.Worksheets.Add("Report");
                Thread.Sleep(5000);
                exceptions.FileText("Step BAL 11.1 Start");

                ws.Cell("A1").Value = "ID";
                exceptions.FileText("Step BAL 11.2 Start");
                ws.Cell("B1").Value = "Status";
                ws.Cell("C1").Value = "Subject/Latest Message";
                ws.Cell("D1").Value = "Category";
                ws.Cell("E1").Value = "Priority";
                ws.Cell("F1").Value = "Assignee";
                ws.Cell("G1").Value = "Creation On";
                exceptions.FileText("Step BAL 11.3 Start");
                Thread.Sleep(5000);
                for (int i = 0; i < searchResult.Count; i++)
                {
                    exceptions.FileText("Step BAL 11.4 Start="+i.ToString());
                    try
                    {
                        ws.Cell("A" + (i + 2)).Value = searchResult[i].ticketID;
                        ws.Cell("B" + (i + 2)).Value = searchResult[i].ticketStatus;
                        ws.Cell("C" + (i + 2)).Value = searchResult[i].Message;
                        ws.Cell("D" + (i + 2)).Value = searchResult[i].Category;
                        ws.Cell("E" + (i + 2)).Value = searchResult[i].Priority;
                        ws.Cell("F" + (i + 2)).Value = searchResult[i].assignedTo;
                        ws.Cell("G" + (i + 2)).Value = searchResult[i].CreatedOn;
                    }
                    catch (Exception ex)
                    {

                        exceptions.SendErrorToText(ex);
                    }
                   
                }
                exceptions.FileText("Step BAL 11.5 Start");
                Thread.Sleep(5000);

                // Beautify
                exceptions.FileText("Step BAL 11.6");
                ws.Range("A1:G1").Style.Font.Bold = true;
                exceptions.FileText("Step BAL 11.7");
               // ws.Columns().AdjustToContents();
              //  exceptions.FileText("Step BAL 11.8");
                exceptions.FileText("Search output filename start");
                SearchOutputFileName = GetNameOfExcel(CreatedBy, TenantID, "Report");
                exceptions.FileText("Search output filename end");
                Thread.Sleep(5000);
                wb.SaveAs(SearchOutputFileName);
                exceptions.FileText("WS Save");
                Thread.Sleep(5000);
                exceptions.FileText("Step BAL 11 End");
            }
            catch (Exception ex)
            {
                exceptions.SendErrorToText(ex);
            }
            return SearchOutputFileName;
        }

        #endregion

        public string GetNameOfExcel(int CreatedBy, int TenantID, string Schedulefrom)
        {
            string dateformat = "";
            string subPath = "";
            try
            {

                exceptions.FileText("GetNameOfExcel 1");

                string startupPath = Environment.CurrentDirectory;
                //string projectDirectory = Directory.GetParent(startupPath).Parent.FullName;
                exceptions.FileText("GetNameOfExcel 2");
                subPath = Path.Combine(startupPath, "ExcelFile", CreatedBy + "_" + TenantID);

                if (!Directory.Exists(subPath))
                {
                    Directory.CreateDirectory(subPath);
                }
                exceptions.FileText("GetNameOfExcel 3");
                DateTime currentdate = DateTime.Now;

                Random generator = new Random();
                String r = generator.Next(0, 999999).ToString("D6");
                exceptions.FileText("GetNameOfExcel 4");
                dateformat = currentdate.Year + "" + currentdate.Month + "" + currentdate.Day + "_" + currentdate.Hour + "" + currentdate.Minute + "" + currentdate.Second + "_" + r;
                exceptions.FileText("GetNameOfExcel 5");

                //exceptions.FileText("Step BAL 6 End");
            }
            catch (Exception ex)
            {
                exceptions.SendErrorToText(ex);
            }

            return Path.Combine(subPath, "Ticket_" + Schedulefrom + "_Schedule_" + dateformat + ".xlsx");
        }

        public void SendEmail(TicketScheduleModal ticketschedulemodal, string[] cc = null, string[] bcc = null, int tenantId = 0)
        {
            try
            {
                exceptions.FileText("Step BAL 15 Start");

                SMTPDetails smtpDetails = ticketschedulemodal.SMTPDetails;
                string emailToAddress = ticketschedulemodal.SendToEmailID;
                string CCToAddress = ticketschedulemodal.CreatedByEmailId;
                string subject = ticketschedulemodal.Emailsubject;
                string body = ticketschedulemodal.Emailbody;
                string Attachmentfile = ticketschedulemodal.SearchOutputFileName;

                cc = new string[] { CCToAddress };

                string[] emailToList = emailToAddress.Split(',');

                if (emailToList.Length > 0)
                {

                    SmtpClient smtpClient = new SmtpClient(smtpDetails.SMTPServer, Convert.ToInt32(smtpDetails.SMTPPort));
                    smtpClient.EnableSsl = smtpDetails.EnableSsl;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Credentials = new NetworkCredential(smtpDetails.FromEmailId, smtpDetails.Password);
                    {
                        using (MailMessage message = new MailMessage())
                        {
                            message.From = new MailAddress(smtpDetails.FromEmailId, smtpDetails.EmailSenderName);

                            if (cc != null)
                            {
                                if (cc.Length > 0)
                                {
                                    for (int i = 0; i < cc.Length; i++)
                                    {
                                        message.CC.Add(cc[i]);
                                    }
                                }
                            }
                            if (bcc != null)
                            {
                                if (bcc.Length > 0)
                                {
                                    for (int k = 0; k < bcc.Length; k++)
                                    {
                                        message.CC.Add(bcc[k]);
                                    }
                                }
                            }
                            message.Subject = subject == null ? "" : subject;
                            message.Body = body == null ? "" : body;
                            message.IsBodyHtml = smtpDetails.IsBodyHtml;
                            message.Attachments.Add(new Attachment(Attachmentfile));

                            if (emailToList.Length > 0)
                            {
                                foreach (string emailid in emailToList)
                                {
                                    message.To.Add(emailid);
                                }
                                smtpClient.Send(message);
                            }
                            else
                            {
                                getdata.SchedulerMailResult(ticketschedulemodal, false, "TS", "No Email ID Present to send", "SendEmail", "");
                            }

                            //message.To.Add(emailToAddress);
                        }
                    }
                }
                else
                {
                    getdata.SchedulerMailResult(ticketschedulemodal, false, "TS", "No Email ID Present to send", "SendEmail", "");
                }


                exceptions.FileText("Step BAL 15 End");
            }
            catch (SmtpFailedRecipientsException ex)
            {
                getdata.SchedulerMailResult(ticketschedulemodal, false, "TS", ex.InnerExceptions.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), ex.StatusCode.ToString());
            }
            catch (Exception ex)
            {
                getdata.SchedulerMailResult(ticketschedulemodal, false, "TS", ex.InnerException.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), "NoStatusCode");
                exceptions.SendErrorToText(ex);
            }
        }

        
    }
}
