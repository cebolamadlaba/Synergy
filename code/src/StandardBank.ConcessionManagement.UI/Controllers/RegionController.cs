using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface;

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
        /// Initializes a new instance of the <see cref="RegionController"/> class.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        public RegionController(IRegionManager regionManager)
        {
            _regionManager = regionManager;
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
    }
}
