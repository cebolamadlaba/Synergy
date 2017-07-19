using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingConcessionsTool.DTO
{
    public class UserProfile
    {
        public UserProfile()
        {
            Permissions = new List<string>();
        }
        public int UserId { get; set; }
        public string ANumber { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public int RoleId { get; set; }
        public List<string> Permissions { get; set; }
        public string EmailAddress { get; set; }
        public bool AdUserFound { get; set; }

        public UserBusinessInfo UserBusinessInfo { get; set; }

        public bool IsRequestor { get; set; }

        public bool IsBCM { get; set; }

        public bool IsPCM { get; set; }

        public int CentreId { get; set; }
        public bool IsHOUser { get; set; }
        public string Role { get; set; }
    }
    public class UserBusinessInfo
    {
        public string Region { get; set; }
        public string BusinessUnit { get; set; }
        public string Branch { get; set; }
        public string Province { get; set; }

        public string Segment { get; set; }
    }
}
