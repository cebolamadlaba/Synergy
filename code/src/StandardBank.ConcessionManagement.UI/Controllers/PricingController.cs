using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.UI.Helpers.Interface;

namespace StandardBank.ConcessionManagement.UI.Controllers
{
    /// <summary>
    /// Pricing controller
    /// </summary>
    [Produces("application/json")]
    [Route("api/Pricing")]
    public class PricingController : Controller
    {
        /// <summary>
        /// The lookup table manager
        /// </summary>
        private readonly ILookupTableManager _lookupTableManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="PricingController"/> class.
        /// </summary>
        /// <param name="lookupTableManager">The lookup table manager.</param>
        public PricingController(ILookupTableManager lookupTableManager)
        {
            _lookupTableManager = lookupTableManager;
        }

        /// <summary>
        /// Gets the risk group details for the risk group number specified
        /// </summary>
        /// <param name="riskGroupNumber"></param>
        /// <returns></returns>
        [Route("RiskGroup/{riskGroupNumber}")]
        public IActionResult RiskGroup(int riskGroupNumber)
        {
            var riskGroup = _lookupTableManager.GetRiskGroupForRiskGroupNumber(riskGroupNumber);
            
            return Ok(riskGroup);
        }
    }
}
