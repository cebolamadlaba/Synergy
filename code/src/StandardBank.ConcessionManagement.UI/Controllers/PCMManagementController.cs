using System.Linq;
using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using System.Threading.Tasks;
using MediatR;
using StandardBank.ConcessionManagement.BusinessLogic.Features.Administration;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.UI.Helpers.Interface;

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
        /// The site helper
        /// </summary>
        private readonly ISiteHelper _siteHelper;

        /// <summary>
        /// The mediator
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// The lookup table manager
        /// </summary>
        private readonly ILookupTableManager _lookupTableManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="PCMManagementController"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="businessCentreManager">The business centre manager.</param>
        /// <param name="siteHelper">The site helper.</param>
        /// <param name="mediator">The mediator.</param>
        /// <param name="lookupTableManager">The lookup table manager.</param>
        public PCMManagementController(IUserManager userManager, IBusinessCentreManager businessCentreManager,
            ISiteHelper siteHelper, IMediator mediator, ILookupTableManager lookupTableManager)
        {
            _userManager = userManager;
            _businessCentreManager = businessCentreManager;
            _siteHelper = siteHelper;
            _mediator = mediator;
            _lookupTableManager = lookupTableManager;
        }

        /// <summary>
        /// Gets the PCM users.
        /// </summary>
        /// <returns></returns>
        [Route("PCMUsers")]
        public IActionResult PCMUsers()
        {
            var user = _siteHelper.LoggedInUser(this);
            var pcmUsers = _userManager.GetUsersByRole(Constants.Roles.PCM);

            if (user.IsPCM)
                pcmUsers = pcmUsers.Where(_ => _.Id == user.Id);
        
            return Ok(pcmUsers);
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
        
        /// <summary>
        /// Gets the region centres.
        /// </summary>
        /// <returns></returns>
        [Route("RoleSubRoles")]
        public IActionResult RoleSubRoles()
        {
            return Ok(_userManager.GetRoleSubRole());
        }

        /// <summary>
        /// Saves the PCM user.
        /// </summary>
        /// <param name="pcmUser">The PCM user.</param>
        /// <returns></returns>
        [Route("SavePcmUser")]
        public async Task<IActionResult> SavePcmUser([FromBody] User pcmUser)
        {
            var user = _siteHelper.LoggedInUser(this);
            var roles = _lookupTableManager.GetRoles();

            pcmUser.RoleId = roles.First(_ => _.Name == Constants.Roles.PCM).Id;
            pcmUser.CentreId = 0;
            
            if (pcmUser.Id > 0)
                await _mediator.Send(new UpdateUser(pcmUser, user));
            else
                await _mediator.Send(new CreateUser(pcmUser, user));

            return Ok(true);
        }

        /// <summary>
        /// Validates the user.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [Route("ValidateUser")]
        public IActionResult ValidateUser([FromBody] User model)
        {
            return Ok(_userManager.ValidateUser(model, Constants.Roles.PCM));
        }
    }
}
