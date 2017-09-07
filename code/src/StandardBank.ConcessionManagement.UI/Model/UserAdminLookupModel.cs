using StandardBank.ConcessionManagement.Model.UserInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StandardBank.ConcessionManagement.UI.Model
{
    public class UserAdminLookupModel
    {
        public IEnumerable<Role> Roles { get; set; }
        public IEnumerable<Region> Regions { get; set; }
        public IEnumerable<Centre> Centres { get; set; }
    }
}
