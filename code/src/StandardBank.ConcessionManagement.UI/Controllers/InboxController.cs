using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.UI.Helpers.Interface;

namespace StandardBank.ConcessionManagement.UI.Controllers
{
    /// <summary>
    /// Inbox controller
    /// </summary>
    [Produces("application/json")]
    [Route("api/Inbox")]
    public class InboxController : Controller
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
        /// Initializes the controller
        /// </summary>
        /// <param name="concessionManager"></param>
        /// <param name="siteHelper"></param>
        public InboxController(IConcessionManager concessionManager, ISiteHelper siteHelper)
        {
            _concessionManager = concessionManager;
            _siteHelper = siteHelper;
        }

        /// <summary>
        /// Gets the user concessions
        /// </summary>
        /// <returns></returns>
        [Route("UserConcessions")]
        public IActionResult UserConcessions()
        {
            return Ok(_concessionManager.GetUserConcessions(_siteHelper.LoggedInUser(this)));
        }

        [Route("ActionedConcessions")]
        public IActionResult ActionedConcessions()
        {
            return Ok(_concessionManager.GetActionedConcessionsForUser(_siteHelper.LoggedInUser(this)));
        }
    }
}