using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic;

namespace StandardBank.ConcessionManagement.UI.Controllers
{
    /// <summary>
    /// PCM Management Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/[controller]")]
    public class PCMManagementController : Controller
    {
        /// <summary>
        /// The user manager
        /// </summary>
        private readonly IUserManager _userManager;

        /// <summary>
        /// The business centre manager
        /// </summary>
        private readonly IBusinessCentreManager _businessCentreManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="PCMManagementController"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="businessCentreManager">The business centre manager.</param>
        public PCMManagementController(IUserManager userManager, IBusinessCentreManager businessCentreManager)
        {
            _userManager = userManager;
            _businessCentreManager = businessCentreManager;
        }

        /// <summary>
        /// Gets the PCM users.
        /// </summary>
        /// <returns></returns>
        [Route("PCMUsers")]
        public IActionResult PCMUsers()
        {
            return Ok(_userManager.GetUsersByRole(Constants.Roles.PCM));
        }

        /// <summary>
        /// Gets the region centres.
        /// </summary>
        /// <returns></returns>
        [Route("RegionCentres")]
        public IActionResult RegionCentres()
        {
            return Ok(_businessCentreManager.GetRegionCentres());
        }
    }
}
