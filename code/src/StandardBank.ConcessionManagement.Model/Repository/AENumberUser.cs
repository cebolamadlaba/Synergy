using System;
using System.Collections.Generic;
using System.Text;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    public class AENumberUser
    {
        public AENumberUser() { }

        public int AENumberUserId { get; set; }
        public string AENumber { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; }
    }
}
