using System;
using System.Collections.Generic;
using System.Text;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    public class UserModel
    {
        public string ANumber { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public int CentreId { get; set; }
        public int RegionId { get; set; }
        public int RoleId { get; set; }
    }
}
