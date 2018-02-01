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
        /// Initializes a new instance of the <see cref="PCMManagementController"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        public PCMManagementController(IUserManager userManager)
        {
            _userManager = userManager;
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
    }
}
