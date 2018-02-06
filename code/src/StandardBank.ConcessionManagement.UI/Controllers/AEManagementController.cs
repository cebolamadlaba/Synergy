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
    /// AE Management Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/[controller]")]
    public class AEManagementController : Controller
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
        /// Initializes a new instance of the <see cref="AEManagementController"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="siteHelper">The site helper.</param>
        /// <param name="lookupTableManager">The lookup table manager.</param>
        /// <param name="mediator">The mediator.</param>
        public AEManagementController(IUserManager userManager, ISiteHelper siteHelper, ILookupTableManager lookupTableManager, IMediator mediator)
        {
            _userManager = userManager;
            _siteHelper = siteHelper;
            _lookupTableManager = lookupTableManager;
            _mediator = mediator;
        }

        /// <summary>
        /// Gets the AE users.
        /// </summary>
        /// <returns></returns>
        [Route("AEUsers")]
        public IActionResult AEUsers()
        {
            return Ok(_userManager.GetUsersByRole(Constants.Roles.Requestor));
        }

        /// <summary>
        /// Saves the AE user.
        /// </summary>
        /// <param name="aeUser">The AE user.</param>
        /// <returns></returns>
        [Route("SaveAeUser")]
        public async Task<IActionResult> SaveAeUser([FromBody] User aeUser)
        {
            var user = _siteHelper.LoggedInUser(this);
            var roles = _lookupTableManager.GetRoles();

            aeUser.RoleId = roles.First(_ => _.Name == Constants.Roles.Requestor).Id;

            if (aeUser.Id > 0)
                await _mediator.Send(new UpdateUser(aeUser, user));
            else
                await _mediator.Send(new CreateUser(aeUser, user));

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
            return Ok(_userManager.ValidateUser(model, Constants.Roles.Requestor));
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
