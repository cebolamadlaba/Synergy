using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.BusinessLogic.Features.Administration;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.UI.Helpers.Interface;

namespace StandardBank.ConcessionManagement.UI.Controllers
{
    /// <summary>
    /// BCM Management Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/[controller]")]
    public class BCMManagementController : Controller
    {
        /// <summary>
        /// The user manager
        /// </summary>
        private readonly IUserManager _userManager;

        /// <summary>
        /// The site helper
        /// </summary>
        private readonly ISiteHelper _siteHelper;

        /// <summary>
        /// The lookup table manager
        /// </summary>
        private readonly ILookupTableManager _lookupTableManager;

        /// <summary>
        /// The mediator
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="BCMManagementController"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="siteHelper">The site helper.</param>
        /// <param name="lookupTableManager">The lookup table manager.</param>
        /// <param name="mediator">The mediator.</param>
        public BCMManagementController(IUserManager userManager, ISiteHelper siteHelper, ILookupTableManager lookupTableManager, IMediator mediator)
        {
            _userManager = userManager;
            _siteHelper = siteHelper;
            _lookupTableManager = lookupTableManager;
            _mediator = mediator;
        }

        /// <summary>
        /// Gets the BCM users.
        /// </summary>
        /// <returns></returns>
        [Route("BCMUsers")]
        public IActionResult BCMUsers()
        {
            return Ok(_userManager.GetUsersByRole(Constants.Roles.BCM));
        }

        /// <summary>
        /// Saves the BCM user.
        /// </summary>
        /// <param name="bcmUser">The BCM user.</param>
        /// <returns></returns>
        [Route("SaveBcmUser")]
        public async Task<IActionResult> SaveBcmUser([FromBody] User bcmUser)
        {
            var user = _siteHelper.LoggedInUser(this);
            var roles = _lookupTableManager.GetRoles();

            bcmUser.RoleId = roles.First(_ => _.Name == Constants.Roles.BCM).Id;

            if (bcmUser.Id > 0)
                await _mediator.Send(new UpdateUser(bcmUser, user));
            else
                await _mediator.Send(new CreateUser(bcmUser, user));

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
            return Ok(_userManager.ValidateUser(model, Constants.Roles.BCM));
        }

        /// <summary>
        /// Gets the centres.
        /// </summary>
        /// <returns></returns>
        [Route("Centres")]
        public IActionResult Centres()
        {
            return Ok(_lookupTableManager.GetCentres());
        }
    }
}
