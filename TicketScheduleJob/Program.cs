﻿using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading;
using System.Timers;

namespace TicketScheduleJob
{
    class Program
    {

        static void Main(string[] args)
        {

            Program obj = new Program();
            obj.StartProcess();

            // Console.ReadLine();
        }

        public void StartProcess()
        {
            Exceptions exceptions = null;
            try
            {
                MySettingsConfigMoal mysettingsconfigmoal = new MySettingsConfigMoal();
                mysettingsconfigmoal = GetConfigDetails();
                // Console.WriteLine("Scheduler Started");
                //GetScheduleDetails();

                double intervalInMinutes = Convert.ToDouble(mysettingsconfigmoal.IntervalInMinutes);// 60 * 5000; // milliseconds to one min

                Thread _Individualprocessthread = new Thread(new ThreadStart(CallEveryMin));
                _Individualprocessthread.Start();

                //Timer checkForTime = new Timer(intervalInMinutes);
                //checkForTime.Elapsed += new ElapsedEventHandler(GetScheduleDetails);
                //checkForTime.Enabled = true;
            }
            catch (Exception ex)
            {
                exceptions.SendErrorToText(ex);
            }

        }

        public void CallEveryMin()
        {
            MySettingsConfigMoal mysettingsconfigmoal = new MySettingsConfigMoal();
            mysettingsconfigmoal = GetConfigDetails();

            int intervalInMinutes = Convert.ToInt32(mysettingsconfigmoal.IntervalInMinutes);// 60 * 5000; // milliseconds to one min

            while (true)
            {
                GetScheduleDetails();
                Thread.Sleep(intervalInMinutes);
            }
        }


        public void GetScheduleDetails()
        //public void GetScheduleDetails()
        {
            Exceptions exceptions = null;
            try
            {

                exceptions = new Exceptions();

                //  Console.WriteLine("New Process is going on... please wait...");

                exceptions.FileText("Step Start");

                BAL bALobj = new BAL();

                bALobj.GetScheduleDetails();


                bALobj.GetStoreScheduleDetails();

                exceptions.FileText("Step End");
                //  Console.WriteLine("New Process Complete...");
            }
            catch (Exception ex)
            {
                exceptions.SendErrorToText(ex);
            }

        }


        public MySettingsConfigMoal GetConfigDetails()
        {
            MySettingsConfigMoal MySettingsConfigMoal = new MySettingsConfigMoal();

            try
            {
                var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .AddUserSecrets<Program>()
              .AddEnvironmentVariables();

                IConfigurationRoot configuration = builder.Build();
                var mySettingsConfig = new MySettingsConfig();
                configuration.GetSection("MySettings").Bind(mySettingsConfig);

                MySettingsConfigMoal.Connectionstring = configuration.GetConnectionString("DefaultConnection");
                MySettingsConfigMoal.IntervalInMinutes = mySettingsConfig.IntervalInMinutes;
                MySettingsConfigMoal.IsWriteLog = mySettingsConfig.IsWriteLog;
            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error getting data from appsetting.json");
            }

            return MySettingsConfigMoal;
        }
    }
}
