using PricingConcessionsTool.API.Utils;
using PricingConcessionsTool.DTO;
using PricingConcessionsTool.Services.Interfaces;
using System.Web.Http;

namespace PricingConcessionsTool.API.Controllers
{
    public class UserController : ApiController
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public UserProfile GetUserAdUser()
        {
            return _userService.GetUserAdProfile(Util.GetUserName(this));
        }

        [HttpGet]
        public UserProfile Login()
        {
            return _userService.Login(Util.GetUserName(this));
        }


        public UserProfile GetUserProfile(string anumber)
        {
            return _userService.GetUserProfile(anumber);

        }

        //public UserProfile GetLoggedOnUserProfile()
        //{
        //    return _userService.GetUserAdProfile(Util.GetUserName(this));
        //}

    }
}