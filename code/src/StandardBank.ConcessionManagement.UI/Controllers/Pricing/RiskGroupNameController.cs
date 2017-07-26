using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;

namespace StandardBank.ConcessionManagement.UI.Controllers.Pricing
{
    /// <summary>
    /// Risk group name controller
    /// </summary>
    [Route("api/Pricing/[controller]")]
    public class RiskGroupNameController : Controller
    {
        /// <summary>
        /// The pricing manager
        /// </summary>
        private readonly IPricingManager _pricingManager;

        /// <summary>
        /// Initializes an instance of RiskGroupNameController
        /// </summary>
        /// <param name="pricingManager"></param>
        public RiskGroupNameController(IPricingManager pricingManager)
        {
            _pricingManager = pricingManager;
        }

        /// <summary>
        /// Gets the risk group name for the risk group number
        /// </summary>
        /// <param name="riskGroupNumber"></param>
        /// <returns></returns>
        [HttpGet("{riskGroupNumber}")]
        public string Get(int riskGroupNumber)
        {
            return _pricingManager.GetRiskGroupName(riskGroupNumber);
        }
    }
}
