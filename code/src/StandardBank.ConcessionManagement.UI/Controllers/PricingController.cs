using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;

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
        /// The pricing manager
        /// </summary>
        private readonly IPricingManager _pricingManager;
        
        /// <summary>
        /// Initializes the controller
        /// </summary>
        /// <param name="pricingManager"></param>
        public PricingController(IPricingManager pricingManager)
        {
            _pricingManager = pricingManager;
        }

        /// <summary>
        /// Gets the risk group details for the risk group number specified
        /// </summary>
        /// <param name="riskGroupNumber"></param>
        /// <returns></returns>
        [Route("RiskGroup/{riskGroupNumber}")]
        public IActionResult RiskGroup(int riskGroupNumber)
        {
            return Ok(_pricingManager.GetRiskGroupForRiskGroupNumber(riskGroupNumber));
        }
    }
}