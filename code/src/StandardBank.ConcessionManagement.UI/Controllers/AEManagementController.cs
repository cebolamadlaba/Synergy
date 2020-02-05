using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.BusinessLogic.Features.Administration;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.Model.UserInterface.Administration;
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
            var user = _siteHelper.LoggedInUser(this);

            if (user.IsRequestor)
            {
                return Ok(new[] { user });
            }

            return Ok(_userManager.GetUsersByRole(Constants.Roles.Requestor));
        }

        /// <summary>
        /// Gets the aa users.
        /// </summary>
        /// <returns></returns>
        [Route("AAUsers")]
        public IActionResult AAUsers()
        {
            return Ok(_userManager.GetUsersByRole(Constants.Roles.AA));
        }

        /// <summary>
        /// Saves the account executive.
        /// </summary>
        /// <param name="accountExecutive">The account executive.</param>
        /// <returns></returns>
        [Route("SaveAccountExecutive")]
        public async Task<IActionResult> SaveAccountExecutive([FromBody] AccountExecutive accountExecutive)
        {
            var user = _siteHelper.LoggedInUser(this);
            var roles = _lookupTableManager.GetRoles();

            accountExecutive.User.RoleId = roles.First(_ => _.Name == Constants.Roles.Requestor).Id;

            if (accountExecutive.User.Id > 0)
            {
                await _mediator.Send(new UpdateUser(accountExecutive.User, user));
            }
            else
            {
                var userId = await _mediator.Send(new CreateUser(accountExecutive.User, user));
                accountExecutive.User.Id = userId;
            }

            await _mediator.Send(new CreateOrUpdateAccountExecutives(accountExecutive, user));

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

        /// <summary>
        /// Gets the ae aa users.
        /// </summary>
        /// <param name="aeUserId">The ae user identifier.</param>
        /// <returns></returns>
        [Route("AEAAUsers/{aeUserId}")]
        public IActionResult AEAAUsers(int aeUserId)
        {
            return Ok(_userManager.GetAccountAssistantsForAccountExecutive(aeUserId));
        }
    }
}
