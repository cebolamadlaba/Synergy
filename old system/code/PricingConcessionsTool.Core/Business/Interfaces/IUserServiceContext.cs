using PricingConcessionsTool.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingConcessionsTool.Core.Business.Interfaces
{
    public interface IUserServiceContext
    {
        UserProfile GetUserProfile(string userName);
        UserBusinessInfo GetUserBusinessInfo(string anumber);
        UserProfile GetUserProfileById(int userId);
    }
}
