using System;
using System.Collections.Generic;
using System.Text;

namespace TicketScheduleJob
{
    public class ErrorLogs
    {
        public int UserID { get; set; }
        public int TenantID { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string Exceptions { get; set; }
        public string MessageException { get; set; }
        public string IPAddress { get; set; }
    }
}
