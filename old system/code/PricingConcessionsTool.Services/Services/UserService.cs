using PricingConcessionsTool.Core.Business.Classes;
using PricingConcessionsTool.Core.Business.Interfaces;
using PricingConcessionsTool.DTO;
using PricingConcessionsTool.Services.AD;
using PricingConcessionsTool.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PricingConcessionsTool.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserServiceContext _userService;

        private readonly IlogContext _log;

        public UserService()
        {
            _userService = new UserServiceContext();

            _log = new LogContext();
        }

        public UserProfile GetUserAdProfile(string anumber)
        {
            var user = new UserProfile();

            try
            {

                using (ActiveDirectorySoapClient ad = new ActiveDirectorySoapClient())
                {
                    var adUser = ad.GetUser(FieldType.ANumber, anumber);

                    if (adUser != null)
                    {
                        user.ANumber = adUser.ANumber;
                        user.EmailAddress = adUser.Email;
                        user.FullName = adUser.FullyQualifiedName;
                        user.FirstName = adUser.Name;
                        user.LastName = adUser.Surname;
                        user.AdUserFound = true;
                    }
                    else
                    {
                        user.AdUserFound = false;
                    }
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                user.FullName = "Test User";
                user.AdUserFound = true;
#endif
                _log.LogException(ex);

            }

            return user;
        }

        public UserProfile GetUserProfile(string userName)
        {

            try
            {
                return _userService.GetUserProfile(userName);
            }
            catch (Exception ex)
            {
                _log.LogException(ex);

                throw new Exception("Failed while trying to get user profile");
            }
        }

        public UserProfile Login(string anumber)
        {
            UserProfile user = null;

            try
            {

                //    using (ActiveDirectorySoapClient ad = new ActiveDirectorySoapClient())
                //    {
                 user = _userService.GetUserProfile(anumber);

                //if (adUser != null)
                //{
                //    user.ANumber = adUser.ANumber;
                //    user.EmailAddress = adUser.EmailAddress;
                //    user.FullName = string.Format("{0} {1}", user.FirstName, adUser.LastName);
                //    user.FirstName = adUser.FirstName;
                //    user.LastName = adUser.LastName;
                //    user.AdUserFound = true;

                    user.UserBusinessInfo = _userService.GetUserBusinessInfo(anumber);
                    //    }
                    //    else
                    //    {
                    //        user.AdUserFound = false;
                    //    }
                    //}
                //}
            }
            catch (Exception ex)
            {
#if DEBUG
                user.ANumber = "A225347";
                user.FullName = "Test User";
                user.AdUserFound = true;
#endif
                _log.LogException(ex);

            }

            return user;
        }
    }
}
