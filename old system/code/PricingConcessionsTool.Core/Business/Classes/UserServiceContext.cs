using PricingConcessionsTool.Core.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PricingConcessionsTool.DTO;
using PricingConcessionsTool.Core.Data;
using PricingConcessionsTool.DTO.Enums;

namespace PricingConcessionsTool.Core.Business.Classes
{
    public class UserServiceContext : DataContextBase, IUserServiceContext
    {
        public UserBusinessInfo GetUserBusinessInfo(string anumber)
        {
            return new UserBusinessInfo
            {
                 Branch ="Head Office",
                 BusinessUnit="Retail",
                 Province="KZN",
                 Region="KZN",
                 Segment="Test"
            };
        }

        public UserProfile GetUserProfile(string userName)
        {
            using (var _context = new PricingEntities())
            {
                var tblUser = _context.tblUsers.FirstOrDefault(c => c.ANumber == userName && c.IsActive);

                if (tblUser != null)
                {
                    var userpofile = new UserProfile()
                    {
                        ANumber = tblUser.ANumber,
                        UserId = tblUser.pkUserId,
                        EmailAddress = tblUser.EmailAddress,
                        FullName = tblUser.Surname + " " + tblUser.FirstName,
                        FirstName = tblUser.FirstName,
                        LastName=tblUser.Surname,
                        RoleId = tblUser.tblUserRoles.FirstOrDefault(u=>u.IsActive).fkRoleId,
                        IsRequestor = tblUser.tblUserRoles.Any(ur=>ur.fkRoleId==(int)Roles.Requestor),
                        IsBCM = tblUser.tblUserRoles.Any(ur => ur.fkRoleId == (int)Roles.BCM),
                        IsPCM = tblUser.tblUserRoles.Any(ur => ur.fkRoleId == (int)Roles.PCM),                      
                                           
                    };

                    var centre = tblUser.tblCentreUsers.FirstOrDefault(c => c.IsActive);

                    if (centre != null)
                    {
                        userpofile.CentreId = centre.fkCentreId;
                    }
                                      
                    return userpofile;
                }
                return null;
            }
        }

        public UserProfile GetUserProfileById(int userId)
        {
            using (var _context = new PricingEntities())
            {
                var tblUser = _context.tblUsers.FirstOrDefault(c => c.pkUserId == userId && c.IsActive);

                if (tblUser != null)
                {
                    var userpofile = new UserProfile()
                    {
                        ANumber = tblUser.ANumber,
                        UserId = tblUser.pkUserId,
                        EmailAddress = tblUser.EmailAddress,
                        FullName = tblUser.Surname + " " + tblUser.FirstName,
                        FirstName = tblUser.FirstName,
                        LastName = tblUser.Surname,
                        RoleId = tblUser.tblUserRoles.FirstOrDefault(u => u.IsActive).fkRoleId,
                        Role = tblUser.tblUserRoles.FirstOrDefault(u => u.IsActive).rtblRole.RoleDescription,
                        IsRequestor = tblUser.tblUserRoles.Any(ur => ur.fkRoleId == (int)Roles.Requestor),
                        IsBCM = tblUser.tblUserRoles.Any(ur => ur.fkRoleId == (int)Roles.BCM),
                        IsPCM = tblUser.tblUserRoles.Any(ur => ur.fkRoleId == (int)Roles.PCM),

                    };

                    var centre = tblUser.tblCentreUsers.FirstOrDefault(c => c.IsActive);

                    if (centre != null)
                    {
                        userpofile.CentreId = centre.fkCentreId;
                    }

                    return userpofile;
                }
                return null;
            }
        }
    }

}
