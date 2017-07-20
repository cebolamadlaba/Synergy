using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.UI.Helpers.Interface;

namespace StandardBank.ConcessionManagement.UI.Controllers.Application
{
    /// <summary>
    /// Logged in user controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/Application/[controller]")]
    public class LoggedInUserController : Controller
    {
        /// <summary>
        /// The site helper
        /// </summary>
        private readonly ISiteHelper _siteHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggedInUserController"/> class.
        /// </summary>
        /// <param name="siteHelper">The site helper.</param>
        public LoggedInUserController(ISiteHelper siteHelper)
        {
            _siteHelper = siteHelper;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public User Get()
        {
            return _siteHelper.LoggedInUser(this);
        }
    }
}
