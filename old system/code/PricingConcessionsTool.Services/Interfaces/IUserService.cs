using PricingConcessionsTool.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingConcessionsTool.Services.Interfaces
{
    public interface IUserService
    {
        UserProfile GetUserProfile(string userName);
        UserProfile GetUserAdProfile(string anumber);
        UserProfile Login(string v);
    }

}