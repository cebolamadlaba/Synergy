using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.BusinessLogic.Features.Administration;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface.Administration;
using StandardBank.ConcessionManagement.UI.Helpers.Interface;

namespace StandardBank.ConcessionManagement.UI.Controllers
{
    /// <summary>
    /// Business Centre controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class BusinessCentreController : Controller
    {
        /// <summary>
        /// The business centre manager
        /// </summary>
        private readonly IBusinessCentreManager _businessCentreManager;

        /// <summary>
        /// The mediator
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// The site helper
        /// </summary>
        private readonly ISiteHelper _siteHelper;

        /// <summary>
        /// The user manager
        /// </summary>
        private readonly IUserManager _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessCentreController"/> class.
        /// </summary>
        /// <param name="businessCentreManager">The business centre manager.</param>
        /// <param name="mediator">The mediator.</param>
        /// <param name="siteHelper">The site helper.</param>
        /// <param name="userManager">The user manager.</param>
        public BusinessCentreController(IBusinessCentreManager businessCentreManager, IMediator mediator,
            ISiteHelper siteHelper, IUserManager userManager)
        {
            _businessCentreManager = businessCentreManager;
            _mediator = mediator;
            _siteHelper = siteHelper;
            _userManager = userManager;
        }

        /// <summary>
        /// Gets all the Business Centre Managemen tModels
        /// </summary>
        /// <returns></returns>
        [Route("BusinessCentreManagementModels")]
        public IActionResult BusinessCentreManagementModels()
        {
            var user = _siteHelper.LoggedInUser(this);
            var businessCentres = _businessCentreManager.GetBusinessCentreManagementModels();

            if (user.IsBCM)
                businessCentres = businessCentres.Where(_ =>
                    _.BusinessCentreManagerId.HasValue && _.BusinessCentreManagerId.Value == user.Id);

            return Ok(businessCentres);
        }

        /// <summary>
        /// Validates the business centre management model.
        /// </summary>
        /// <param name="businessCentreManagementModel">The business centre management model.</param>
        /// <returns></returns>
        [Route("ValidateBusinessCentreManagementModel")]
        public IActionResult ValidateBusinessCentreManagementModel([FromBody] BusinessCentreManagementModel businessCentreManagementModel)
        {
            return Ok(_businessCentreManager.ValidateBusinessCentreManagementModel(businessCentreManagementModel));
        }

        /// <summary>
        /// Creates the business centre management model.
        /// </summary>
        /// <param name="businessCentreManagementModel">The business centre management model.</param>
        /// <returns></returns>
        [Route("CreateBusinessCentreManagementModel")]
        public async Task<IActionResult> CreateBusinessCentreManagementModel([FromBody] BusinessCentreManagementModel businessCentreManagementModel)
        {
            var user = _siteHelper.LoggedInUser(this);

            if (businessCentreManagementModel.CentreId > 0)
                await _mediator.Send(new UpdateBusinessCentreManagementModel(businessCentreManagementModel, user));
            else
                await _mediator.Send(new CreateBusinessCentreManagementModel(businessCentreManagementModel, user));

            return Ok(true);
        }

        /// <summary>
        /// Gets the businesses centre management lookup model.
        /// </summary>
        /// <returns></returns>
        [Route("BusinessCentreManagementLookupModel")]
        public IActionResult BusinessCentreManagementLookupModel()
        {
            var businessCentreManagementLookupModel = _businessCentreManager.GetBusinessCentreManagementLookupModel();
            businessCentreManagementLookupModel.CurrentUser = _siteHelper.LoggedInUser(this);
            return Ok(businessCentreManagementLookupModel);
        }

        /// <summary>
        /// Gets the business centre account executives.
        /// </summary>
        /// <param name="centreId">The centre identifier.</param>
        /// <returns></returns>
        [Route("BusinessCentreAccountExecutives/{centreId}")]
        public IActionResult BusinessCentreAccountExecutives(int centreId)
        {
            return Ok(_userManager.GetUsersByRoleCentreId(Constants.Roles.Requestor, centreId));
        }
    }
}
