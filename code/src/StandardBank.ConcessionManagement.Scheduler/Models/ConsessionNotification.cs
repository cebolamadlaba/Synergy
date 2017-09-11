using System;
using System.Collections.Generic;
using System.Text;

namespace StandardBank.ConcessionManagement.Scheduler.Models
{
    public class ConsessionNotification
    {
        public DateTime ExpiryDate { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string ConcessionRef { get; set; }

    }
}
