using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Data;
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
               

                double intervalInMinutes = Convert.ToDouble(mysettingsconfigmoal.IntervalInMinutes);

                Thread _Individualprocessthread = new Thread(new ThreadStart(InvokeMethod));
                _Individualprocessthread.Start();

              
            }
            catch (Exception ex)
            {
                exceptions.SendErrorToText(ex);
            }

        }

        public  void InvokeMethod()
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddUserSecrets<Program>()
               .AddEnvironmentVariables();

            IConfigurationRoot configuration = builder.Build();
            var mySettingsConfig = new MySettingsConfig();
            configuration.GetSection("MySettings").Bind(mySettingsConfig);

            string interval = mySettingsConfig.IntervalInMinutes;

            int intervalInMinutes = Convert.ToInt32(interval);
           
            while (true)
            {
                GetConnectionStrings();

                Thread.Sleep(intervalInMinutes);
            }
        }

        public void CallEveryMin(string ConString)
        {
            MySettingsConfigMoal mysettingsconfigmoal = new MySettingsConfigMoal();
            mysettingsconfigmoal = GetConfigDetails();

            int intervalInMinutes = Convert.ToInt32(mysettingsconfigmoal.IntervalInMinutes);
            GetScheduleDetails(ConString);
                
        }


        public void GetScheduleDetails(string ConString)
        {
            Exceptions exceptions = null;
            try
            {

                exceptions = new Exceptions();
                exceptions.FileText("Step Start");
                BAL bALobj = new BAL();
                bALobj.GetScheduleDetails(ConString);
                bALobj.GetStoreScheduleDetails(ConString);
                exceptions.FileText("Step End");
               
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
               
            }

            return MySettingsConfigMoal;
        }


        public void GetConnectionStrings()
        {
            string ServerName = string.Empty;
            string ServerCredentailsUsername = string.Empty;
            string ServerCredentailsPassword = string.Empty;
            string DBConnection = string.Empty;


            try
            {
                DataTable dt = new DataTable();
                IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();
                var constr = config.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
                MySqlConnection con = new MySqlConnection(constr);
                MySqlCommand cmd = new MySqlCommand("SP_HSGetAllConnectionstrings", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        ServerName = Convert.ToString(dr["ServerName"]);
                        ServerCredentailsUsername = Convert.ToString(dr["ServerCredentailsUsername"]);
                        ServerCredentailsPassword = Convert.ToString(dr["ServerCredentailsPassword"]);
                        DBConnection = Convert.ToString(dr["DBConnection"]);

                        string ConString = "Data Source = " + ServerName + " ; port = " + 3306 + "; Initial Catalog = " + DBConnection + " ; User Id = " + ServerCredentailsUsername + "; password = " + ServerCredentailsPassword + "";
                        CallEveryMin(ConString);
                    }
                }
            }
            catch (Exception ex)
            {


            }
            finally
            {

                GC.Collect();
            }


        }
    }
}
