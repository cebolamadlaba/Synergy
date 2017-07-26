using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface.Pricing;

namespace StandardBank.ConcessionManagement.UI.Controllers.Pricing
{
    /// <summary>
    /// Risk group controller
    /// </summary>
    [Route("api/Pricing/[controller]")]
    public class RiskGroupController : Controller
    {
        /// <summary>
        /// The pricing manager
        /// </summary>
        private readonly IPricingManager _pricingManager;

        /// <summary>
        /// Initializes an instance of RiskGroupNameController
        /// </summary>
        /// <param name="pricingManager"></param>
        public RiskGroupController(IPricingManager pricingManager)
        {
            _pricingManager = pricingManager;
        }

        /// <summary>
        /// Gets the risk group name for the risk group number
        /// </summary>
        /// <param name="riskGroupNumber"></param>
        /// <returns></returns>
        [HttpGet("{riskGroupNumber}")]
        public RiskGroup Get(int riskGroupNumber)
        {
            return _pricingManager.GetRiskGroupForRiskGroupNumber(riskGroupNumber);
        }
    }
}
