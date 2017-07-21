using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface.Inbox;
using StandardBank.ConcessionManagement.UI.Helpers.Interface;

namespace StandardBank.ConcessionManagement.UI.Controllers.Inbox
{
    /// <summary>
    /// User concessions controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/inbox/[controller]")]
    public class UserConcessionsController : Controller
    {
        /// <summary>
        /// The concession manager
        /// </summary>
        private readonly IConcessionManager _concessionManager;

        /// <summary>
        /// The site helper
        /// </summary>
        private readonly ISiteHelper _siteHelper;

        /// <summary>
        /// User concessions controller
        /// </summary>
        /// <param name="concessionManager"></param>
        /// <param name="siteHelper"></param>
        public UserConcessionsController(IConcessionManager concessionManager, ISiteHelper siteHelper)
        {
            _concessionManager = concessionManager;
            _siteHelper = siteHelper;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public UserConcessions Get()
        {
            return _concessionManager.GetUserConcessions(_siteHelper.LoggedInUser(this));
        }
    }
}
