using System;
using System.Collections.Generic;
using System.Text;

namespace TicketScheduleJob
{
    class MySettingsConfig
    {
        public string IntervalInMinutes { get; set; }
        public string IsWriteLog { get; set; }
    }

    public class MySettingsConfigMoal
    {
        public string Connectionstring { get; set; }
        public string IntervalInMinutes { get; set; }
        public string IsWriteLog { get; set; }
    }
}
