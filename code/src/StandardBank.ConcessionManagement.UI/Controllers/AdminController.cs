using System.Collections.Generic;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.BusinessLogic.Features.Administration;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface;
using System.Threading.Tasks;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface.Administration;
using StandardBank.ConcessionManagement.UI.Helpers.Interface;

namespace StandardBank.ConcessionManagement.UI.Controllers
{
    /// <summary>
    /// Admin controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/[controller]")]
    public class AdminController : Controller
    {
        /// <summary>
        /// The lookup table manager
        /// </summary>
        private readonly ILookupTableManager _lookupTableManager;

        /// <summary>
        /// The mediator
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// The user manager
        /// </summary>
        private readonly IUserManager _userManager;

        /// <summary>
        /// The site helper
        /// </summary>
        private readonly ISiteHelper _siteHelper;

        /// <summary>
        /// The region manager
        /// </summary>
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminController"/> class.
        /// </summary>
        /// <param name="lookupTableManager">The lookup table manager.</param>
        /// <param name="mediator">The mediator.</param>
        /// <param name="userManager">The user manager.</param>
        /// <param name="siteHelper">The site helper.</param>
        /// <param name="regionManager">The region manager.</param>
        public AdminController(ILookupTableManager lookupTableManager, IMediator mediator, IUserManager userManager,
            ISiteHelper siteHelper, IRegionManager regionManager)
        {
            _lookupTableManager = lookupTableManager;
            _mediator = mediator;
            _userManager = userManager;
            _siteHelper = siteHelper;
            _regionManager = regionManager;
        }

        /// <summary>
        /// Gets the user admin lookup data.
        /// </summary>
        /// <returns></returns>
        [Route("user/lookup")]
        public IActionResult GetUserAdminLookupData()
        {
            var model = new UserAdminLookupModel
            {
                Centres = _lookupTableManager.GetCentres(),
                Roles = _lookupTableManager.GetRoles(),
                RoleSubRole = _lookupTableManager.GetRoleSubRole()
            };
            return Ok(model);
        }

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost("users")]
        public async Task<IActionResult> CreateUser([FromBody]User model)
        {
            var user = _siteHelper.LoggedInUser(this);

            AddUserCentre(model);

            var id = await _mediator.Send(new CreateUser(model, user));

            return Ok(id);
        }

        /// <summary>
        /// Adds the user centre.
        /// </summary>
        /// <param name="model">The model.</param>
        private void AddUserCentre(User model)
        {
            if (model.CentreId > 0)
            {
                var userCentres = new List<Centre>();

                if (model.UserCentres != null)
                {
                    userCentres.AddRange(model.UserCentres);
                }

                userCentres.Add(new Centre { Id = model.CentreId });
                model.UserCentres = userCentres;
            }
        }

        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpPost("users/{id}")]
        public async Task<IActionResult> UpdateUser([FromBody]User model, int id)
        {
            var user = _siteHelper.LoggedInUser(this);

            AddUserCentre(model);

            await _mediator.Send(new UpdateUser(model, user));

            return Ok(true);
        }

        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <returns></returns>
        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            var users = _userManager.GetUsers();
            return Ok(users);
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _mediator.Send(new GetUserById { Id = id });
            return Ok(user);
        }

        /// <summary>
        /// Deletes the user.
        /// </summary>
        /// <param name="aNumber">a number.</param>
        /// <returns></returns>
        [HttpDelete("user/{aNumber}")]
        public async Task<IActionResult> DeleteUser(string aNumber)
        {
            var id = await _mediator.Send(new DeleteUser
            {
                aNumber = aNumber,
                CurrentUser = _siteHelper.LoggedInUser(this)
            });

            return Ok(id);
        }
    }
}
