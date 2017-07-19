using PricingConcessionsTool.DTO;
using PricingConcessionsTool.Services.Interfaces;
using PricingConcessionsTool.Services.Services;
using System.Web;
using System.Web.Mvc;

namespace PricingConcessionsTool.Security
{
    public class CustomAuthorize : AuthorizeAttribute
    {
        private IUserService _userService = new UserService();

        public string AccessLevel { get; set; }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("~/Unauthorized/Unauthorized");
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = base.AuthorizeCore(httpContext);

            if (!isAuthorized)
            {
                return false;
            }

            var index = httpContext.User.Identity.Name.ToString().IndexOf("\\") + 1;

            var user = GetUser(httpContext.User.Identity.Name.ToString().Substring(index)); 

            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private UserProfile GetUser(string userName)
        {
            return _userService.GetUserProfile(userName);
        }

    }
}