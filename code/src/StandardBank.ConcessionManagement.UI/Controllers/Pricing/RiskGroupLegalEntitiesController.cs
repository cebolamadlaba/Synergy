using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface.Pricing;

namespace StandardBank.ConcessionManagement.UI.Controllers.Pricing
{
    /// <summary>
    /// Risk group legal entities controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/Pricing/[controller]")]
    public class RiskGroupLegalEntitiesController : Controller
    {
        /// <summary>
        /// The pricing manager
        /// </summary>
        private readonly IPricingManager _pricingManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="RiskGroupLegalEntitiesController"/> class.
        /// </summary>
        /// <param name="pricingManager">The pricing manager.</param>
        public RiskGroupLegalEntitiesController(IPricingManager pricingManager)
        {
            _pricingManager = pricingManager;
        }

        /// <summary>
        /// Gets the specified risk group number.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <returns></returns>
        [HttpGet("{riskGroupNumber}")]
        public IEnumerable<LegalEntity> Get(int riskGroupNumber)
        {
            return _pricingManager.GetLegalEntitiesForRiskGroupNumber(riskGroupNumber);
        }
    }
}
