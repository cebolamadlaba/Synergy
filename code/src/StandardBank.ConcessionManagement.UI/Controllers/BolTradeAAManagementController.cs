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
    public class BolTradeAAManagementController : Controller
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
        /// Initializes a new instance of the <see cref="BolTradeAAManagementController"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="siteHelper">The site helper.</param>
        /// <param name="lookupTableManager">The lookup table manager.</param>
        /// <param name="mediator">The mediator.</param>
        public BolTradeAAManagementController(IUserManager userManager
                , ISiteHelper siteHelper
                , ILookupTableManager lookupTableManager
                , IMediator mediator)
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
                return Ok(new[] { user });

            return Ok(_userManager.GetUsersByRole(Constants.Roles.Requestor));
        }

        /// <summary>
        /// Gets the bol or trade users.
        /// </summary>
        /// <returns></returns>
        [Route("BolOrTradeAAUsers")]
        public IActionResult BolOrTradeAAUsers()
        {
            var user = _siteHelper.LoggedInUser(this);

            if (user.IsRequestor)
                return Ok(_userManager.GetAccountAssistantsForAccountExecutive(user.Id));//.Where(x=>x.SubRoleId != null));

            return Ok(_userManager.GetUsersByRole(Constants.Roles.AA));

            //return Ok(_userManager.GetUsersByRole(Constants.Roles.AA).Where(x=>x.SubRoleId != null));
        }

        [Route("UpdateAccountAssistantSubRole")]
        public async Task<IActionResult> UpdateAccountAssistantSubRole([FromBody] User bolTradeuser)
        {
            return this.SaveBolOrTradeAccountExecutive(bolTradeuser, true, false).Result;
            //var user = _siteHelper.LoggedInUser(this);
            //var roles = _lookupTableManager.GetRoles();

            //bolTradeuser.RoleId = roles.First(_ => _.Name == Constants.Roles.AA).Id;

            //if (bolTradeuser.Id > 0)
            //    await _mediator.Send(new UpdateUser(bolTradeuser, user));
            //else
            //    bolTradeuser.Id = await _mediator.Send(new CreateUser(bolTradeuser, user));

            //return Ok(true);
        }

        /// <summary>
        /// Saves the BolOrTrade Account Executive.
        /// </summary>
        /// <param name="accountExecutive">The BolOrTrade Account Executive.</param>
        /// <returns></returns>
        [Route("SaveBolOrTradeAccountExecutive")]
        public async Task<IActionResult> SaveBolOrTradeAccountExecutive([FromBody] User bolTradeuser)
        {
            return this.SaveBolOrTradeAccountExecutive(bolTradeuser, true, true).Result;
            //var user = _siteHelper.LoggedInUser(this);
            //var roles = _lookupTableManager.GetRoles();

            //bolTradeuser.RoleId = roles.First(_ => _.Name == Constants.Roles.AA).Id;

            //if (bolTradeuser.Id > 0)
            //    await _mediator.Send(new UpdateUser(bolTradeuser, user));
            //else
            //    bolTradeuser.Id = await _mediator.Send(new CreateUser(bolTradeuser, user));

            //if (bolTradeuser.AccountExecutiveUserId.HasValue && bolTradeuser.AccountExecutiveUserId.Value > 0)
            //{
            //    var accountExecutive = new AccountExecutive
            //    {
            //        User = _userManager.GetUser(bolTradeuser.AccountExecutiveUserId.Value),
            //        AccountAssistants = new[] { bolTradeuser }
            //    };

            //    await _mediator.Send(new CreateOrUpdateAccountExecutives(accountExecutive, user));
            //}

            //return Ok(true);
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

        /// <summary>
        /// Gets the roleSubRoles.
        /// </summary>
        /// <returns></returns>
        [Route("RoleSubRoles")]
        public IActionResult RoleSubRoles()
        {
            return Ok(_userManager.GetRoleSubRole());
        }


        private async Task<IActionResult> SaveBolOrTradeAccountExecutive(User bolTradeuser, bool updateUser, bool updateAaAeLink)
        {
            var user = _siteHelper.LoggedInUser(this);

            if (updateUser)
            {
                var roles = _lookupTableManager.GetRoles();

                bolTradeuser.RoleId = roles.First(_ => _.Name == Constants.Roles.AA).Id;

                if (bolTradeuser.Id > 0)
                    await _mediator.Send(new UpdateUser(bolTradeuser, user));
                else
                    bolTradeuser.Id = await _mediator.Send(new CreateUser(bolTradeuser, user));
            }

            if (updateAaAeLink)
            {
                if (bolTradeuser.AccountExecutiveUserId.HasValue && bolTradeuser.AccountExecutiveUserId.Value > 0)
                {
                    var accountExecutive = new AccountExecutive
                    {
                        User = _userManager.GetUser(bolTradeuser.AccountExecutiveUserId.Value),
                        AccountAssistants = new[] { bolTradeuser }
                    };

                    await _mediator.Send(new CreateOrUpdateAccountExecutives(accountExecutive, user));
                }
            }

            return Ok(true);
        }
    }
}
