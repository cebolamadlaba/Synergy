using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.Common;

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

        private readonly IConfigurationData _configurationData;

        private readonly IUserManager _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="PricingController"/> class.
        /// </summary>
        /// <param name="lookupTableManager">The lookup table manager.</param>
        public PricingController(ILookupTableManager lookupTableManager, IConfigurationData configurationData, IUserManager userManager)
        {
            _lookupTableManager = lookupTableManager;
            _configurationData = configurationData;
            _userManager = userManager;
        }

        [Route("LegalEntity/{sapbpid}")]
        public IActionResult LegalEntity(int sapbpid)
        {
            var legalEntity = _lookupTableManager.GetLegalEntity(sapbpid);

            return Ok(legalEntity);
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

        [Route("RiskGroupBySAPBPID/{sapbpid}")]
        public IActionResult RiskGroupBySAPBPID(int sapbpid)
        {
            var riskGroup = _lookupTableManager.GetRiskGroupForSAPBPID(sapbpid);

            var legalEntity = _lookupTableManager.GetLegalEntity(sapbpid);


            return Ok(new Model.UserInterface.PricingView
            {
                RiskGroup = riskGroup,
                LegalEntity = legalEntity
            });
        }

        [Route("GetActivePricingProducts")]
        public IActionResult GetActivePricingProducts()
        {
            int[] activePricingProducts = _configurationData.GetVisiblePricingProducts;

            return Ok(activePricingProducts);
        }

        /// <summary>
        /// Gets the user details for the risk group number specified
        /// </summary>
        /// <param name="riskGroupNumber"></param>
        /// <returns></returns>
        [Route("PricingRiskGroupUser/{riskGroupNumber}")]
        public IActionResult PricingRiskGroupUser(int riskGroupNumber)
        {
            var user =_userManager.GetUserByRiskGroupNumber(riskGroupNumber);

            return Ok(user);
        }
    }
}
