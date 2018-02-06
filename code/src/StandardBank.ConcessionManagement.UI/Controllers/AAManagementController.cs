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
    /// AA Management Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/[controller]")]
    public class AAManagementController : Controller
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
        /// Initializes a new instance of the <see cref="AAManagementController"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="siteHelper">The site helper.</param>
        /// <param name="lookupTableManager">The lookup table manager.</param>
        /// <param name="mediator">The mediator.</param>
        public AAManagementController(IUserManager userManager, ISiteHelper siteHelper, ILookupTableManager lookupTableManager, IMediator mediator)
        {
            _userManager = userManager;
            _siteHelper = siteHelper;
            _lookupTableManager = lookupTableManager;
            _mediator = mediator;
        }

        /// <summary>
        /// Gets the AA users.
        /// </summary>
        /// <returns></returns>
        [Route("AAUsers")]
        public IActionResult AAUsers()
        {
            var user = _siteHelper.LoggedInUser(this);

            if (user.IsRequestor)
                return Ok(_userManager.GetAccountAssistantsForAccountExecutive(user.Id));

            return Ok(_userManager.GetUsersByRole(Constants.Roles.AA));
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
                return Ok(new[] { user });

            return Ok(_userManager.GetUsersByRole(Constants.Roles.Requestor));
        }

        /// <summary>
        /// Saves the AA user.
        /// </summary>
        /// <param name="aaUser">The AA user.</param>
        /// <returns></returns>
        [Route("SaveAaUser")]
        public async Task<IActionResult> SaveAaUser([FromBody] User aaUser)
        {
            var user = _siteHelper.LoggedInUser(this);
            var roles = _lookupTableManager.GetRoles();

            aaUser.RoleId = roles.First(_ => _.Name == Constants.Roles.AA).Id;

            if (aaUser.Id > 0)
                await _mediator.Send(new UpdateUser(aaUser, user));
            else
                aaUser.Id = await _mediator.Send(new CreateUser(aaUser, user));

            if (aaUser.AccountExecutiveUserId.HasValue && aaUser.AccountExecutiveUserId.Value > 0)
            {
                var accountExecutive = new AccountExecutive
                {
                    User = _userManager.GetUser(aaUser.AccountExecutiveUserId.Value),
                    AccountAssistants = new [] { aaUser }
                };

                await _mediator.Send(new CreateOrUpdateAccountExecutives(accountExecutive, user));
            }

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
            return Ok(_userManager.ValidateUser(model, Constants.Roles.AA));
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
