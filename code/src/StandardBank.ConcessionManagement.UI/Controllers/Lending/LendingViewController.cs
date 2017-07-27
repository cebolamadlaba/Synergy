using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
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
        /// Initializes a new instance of the <see cref="LendingViewController"/> class.
        /// </summary>
        /// <param name="pricingManager">The pricing manager.</param>
        public LendingViewController(IPricingManager pricingManager)
        {
            _pricingManager = pricingManager;
        }

        /// <summary>
        /// Gets the specified risk group number.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <returns></returns>
        [HttpGet("{riskGroupNumber}")]
        public LendingView Get(int riskGroupNumber)
        {
            //TODO: This object needs to be populated from "source systems"
            //TODO: where that data comes from and how it gets populated needs to be worked out at some time
            return new LendingView
            {
                RiskGroup = _pricingManager.GetRiskGroupForRiskGroupNumber(riskGroupNumber),
                SourceSystemProducts = new[]
                {
                    new SourceSystemProduct
                    {
                        AccountNumber = 1,
                        CustomerName = "Test Customer",
                        RiskGroupNumber = 1,
                        AverageBalance = 1,
                        Limit = 1,
                        LoadedMap = 1,
                        ProductName = "Test Product",
                        SourceSystemIdentifier = "1",
                        SourceSystemName = "Test"
                    }
                },
                SourceSystemConcessions = new[]
                {
                    new SourceSystemConcession
                    {
                        CustomerName = "Test Customer",
                        AccountNumber = 1,
                        SourceSystemName = "Test Source System",
                        LoadedMap = 1,
                        AverageBalance = 1,
                        Limit = 1,
                        SourceSystemIdentifier = "1",
                        ApprovedMap = 1,
                        ConcessionId = 1,
                        ProductType = "Test Product",
                        Term = 1
                    }
                }
            };
        }
    }
}
