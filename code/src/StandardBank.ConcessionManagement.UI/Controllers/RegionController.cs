using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.BusinessLogic.Features.Administration;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.UI.Helpers.Interface;

namespace StandardBank.ConcessionManagement.UI.Controllers
{
    /// <summary>
    /// Region controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class RegionController : Controller
    {
        /// <summary>
        /// The region manager
        /// </summary>
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// The mediator
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// The site helper
        /// </summary>
        private readonly ISiteHelper _siteHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegionController"/> class.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="mediator">The mediator.</param>
        /// <param name="siteHelper">The site helper.</param>
        public RegionController(IRegionManager regionManager, IMediator mediator, ISiteHelper siteHelper)
        {
            _regionManager = regionManager;
            _mediator = mediator;
            _siteHelper = siteHelper;
        }

        /// <summary>
        /// Gets all the regions
        /// </summary>
        /// <returns></returns>
        [Route("All")]
        public IActionResult All()
        {
            return Ok(_regionManager.GetRegions());
        }

        /// <summary>
        /// Validates the specified region.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <returns></returns>
        [Route("Validate")]
        public IActionResult Validate([FromBody] Region region)
        {
            return Ok(_regionManager.ValidateRegion(region));
        }

        /// <summary>
        /// Creates the specified region.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <returns></returns>
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] Region region)
        {
            var user = _siteHelper.LoggedInUser(this);
            await _mediator.Send(new CreateRegion(region, user));

            return Ok(true);
        }
    }
}
