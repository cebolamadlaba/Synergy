using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.Model.UserInterface.Integration;
using StandardBank.ConcessionManagement.Model.UserInterface.Lending;

namespace StandardBank.ConcessionManagement.UI.Controllers.Lending
{
    /// <summary>
    /// The lending view controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/Lending/[controller]")]
    public class LendingViewController : Controller
    {
        /// <summary>
        /// The pricing manager
        /// </summary>
        private readonly IPricingManager _pricingManager;

        /// <summary>
        /// The lending manager
        /// </summary>
        private readonly ILendingManager _lendingManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="LendingViewController"/> class.
        /// </summary>
        /// <param name="pricingManager">The pricing manager.</param>
        /// <param name="lendingManager">The lending manager.</param>
        public LendingViewController(IPricingManager pricingManager, ILendingManager lendingManager)
        {
            _pricingManager = pricingManager;
            _lendingManager = lendingManager;
        }

        /// <summary>
        /// Gets the specified risk group number.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <returns></returns>
        [HttpGet("{riskGroupNumber}")]
        public LendingView Get(int riskGroupNumber)
        {
            //TODO: Eventually need to get source system product data from source systems 
            return new LendingView
            {
                RiskGroup = _pricingManager.GetRiskGroupForRiskGroupNumber(riskGroupNumber),
                LendingConcessions = _lendingManager.GetLendingConcessionsForRiskGroupNumber(riskGroupNumber)
            };
        }
    }
}
