﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace TicketScheduleJob
{
    public class Exceptions
    {


        public void SendErrorToText(Exception ex)
        {
            String ErrorlineNo, Errormsg, extype, ErrorLocation;

            var line = Environment.NewLine + Environment.NewLine;

            ErrorlineNo = ex.StackTrace.Substring(ex.StackTrace.Length - 7, 7);
            Errormsg = ex.GetType().Name.ToString();
            extype = ex.GetType().ToString();
            ErrorLocation = ex.Message.ToString();

            try
            {


                ErrorLogs errorLogs = new ErrorLogs
                {
                    ActionName = "Ticketing Job",
                    ControllerName = "Ticketing Job",
                    TenantID = 0,
                    UserID = 0,
                    Exceptions = ex.StackTrace,
                    MessageException = ex.Message,
                    IPAddress = ""
                };

                DAL dal = new DAL();
                dal.InsertErrorLog(errorLogs);
                //string filepath = @"E:\Devesh\Ticketing\TicketScheduleJob\TicketScheduleJob\ExceptionDetailsFile";  //Text File Path

                //string filepath = Directory.GetCurrentDirectory() + "\\ExceptionDetailsFile";

                //var filepath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
                //Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
                //var appRoot = appPathMatcher.Match(filepath).Value;

                //filepath = Path.Combine(appRoot, "ExceptionDetailsFile");

                //if (!Directory.Exists(filepath))
                //{
                //    Directory.CreateDirectory(filepath);
                //}
                //filepath = Path.Combine(filepath , "Errormsg"+DateTime.Today.ToString("dd-MM-yy") + ".txt");   //Text File Name
                //if (!File.Exists(filepath))
                //{
                //    File.Create(filepath).Dispose();
                //}
                //using (StreamWriter sw = File.AppendText(filepath))
                //{
                //    string error = "Log Written Date:" + " " + DateTime.Now.ToString() + line + "Error Line No :" + " " + ErrorlineNo + line + "Error Message:" + " " + Errormsg + line + "Exception Type:" + " " + extype + line + "Error Location :" + " " + ErrorLocation + line;
                //    sw.WriteLine("-----------Exception Details on " + " " + DateTime.Now.ToString() + "-----------------");
                //    sw.WriteLine("-------------------------------------------------------------------------------------");
                //    sw.WriteLine(line);
                //    sw.WriteLine(error);
                //    sw.WriteLine("--------------------------------*End*------------------------------------------");
                //    sw.WriteLine(line);
                //    sw.Flush();
                //    sw.Close();
                //}
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        public void FileText(string Text)
        {

            var line = Environment.NewLine + Environment.NewLine;

            try
            {
                ErrorLogs errorLogs = new ErrorLogs
                {
                    ActionName = "Ticketing Job",
                    ControllerName = "Ticketing Job Steps",
                    TenantID = 0,
                    UserID = 0,
                    Exceptions = Text,
                    MessageException = "",
                    IPAddress = ""
                };

                DAL dal = new DAL();

                MySettingsConfigMoal mysettingsconfigmoal = new MySettingsConfigMoal();
                Program obj = new Program();
                mysettingsconfigmoal = obj.GetConfigDetails();

                if (mysettingsconfigmoal.IsWriteLog == "1")
                {
                    dal.InsertErrorLog(errorLogs);
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }
    }
}
