using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.UI.Helpers.Interface;

namespace StandardBank.ConcessionManagement.UI.Controllers.Application
{
    /// <summary>
    /// User identity controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/Application/[controller]")]
    public class UserIdentityController : Controller
    {
        /// <summary>
        /// The site helper
        /// </summary>
        private readonly ISiteHelper _siteHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserIdentityController"/> class.
        /// </summary>
        /// <param name="siteHelper">The site helper.</param>
        public UserIdentityController(ISiteHelper siteHelper)
        {
            _siteHelper = siteHelper;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Get()
        {
            return $"User Identity: {_siteHelper.UserIdentity(this)}";
        }
    }
}
