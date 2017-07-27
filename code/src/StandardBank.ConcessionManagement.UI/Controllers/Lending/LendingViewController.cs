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
                        AccountNumber = 234324,
                        CustomerName = "Test Customer Alpha",
                        RiskGroupNumber = riskGroupNumber,
                        AverageBalance = 1500000,
                        Limit = 1000000,
                        LoadedMap = 1.0m,
                        ProductName = "Test Product Alpha",
                        SourceSystemIdentifier = "1",
                        SourceSystemName = "Test"
                    },
                    new SourceSystemProduct
                    {
                        AccountNumber = 4565465,
                        CustomerName = "Test Customer Beta",
                        RiskGroupNumber = riskGroupNumber,
                        AverageBalance = 3000000,
                        Limit = 1500000,
                        LoadedMap = 1.5m,
                        ProductName = "Test Product Beta",
                        SourceSystemIdentifier = "1",
                        SourceSystemName = "Test"
                    }
                },
                SourceSystemConcessions = new[]
                {
                    new SourceSystemConcession
                    {
                        SourceSystemName = "Test Source System",
                        SourceSystemIdentifier = "1",
                        ConcessionId = "T001",
                        Concessions = new[]
                        {
                            new SourceSystemCustomerConcession
                            {
                                CustomerName = "Test Customer Alpha",
                                AccountNumber = 435345,
                                LoadedMap = 1.0m,
                                AverageBalance = 6000000,
                                Limit = 5000000,
                                ApprovedMap = 1.5m,
                                ProductType = "Test Product Alpha",
                                Term = 60
                            },
                            new SourceSystemCustomerConcession
                            {
                                CustomerName = "Test Customer Beta",
                                AccountNumber = 994545,
                                LoadedMap = 3.0m,
                                AverageBalance = 4000000,
                                Limit = 3000000,
                                ApprovedMap = 4.5m,
                                ProductType = "Test Product Beta",
                                Term = 48
                            }
                        }
                    },
                    new SourceSystemConcession
                    {
                        SourceSystemName = "Test Source System",
                        SourceSystemIdentifier = "2",
                        ConcessionId = "T002",
                        Concessions = new[]
                        {
                            new SourceSystemCustomerConcession
                            {
                                CustomerName = "Test Customer Gamma",
                                AccountNumber = 435345,
                                LoadedMap = 1.0m,
                                AverageBalance = 6000000,
                                Limit = 5000000,
                                ApprovedMap = 1.5m,
                                ProductType = "Test Product Gamma",
                                Term = 60
                            },
                            new SourceSystemCustomerConcession
                            {
                                CustomerName = "Test Customer Delta",
                                AccountNumber = 994545,
                                LoadedMap = 3.0m,
                                AverageBalance = 4000000,
                                Limit = 3000000,
                                ApprovedMap = 4.5m,
                                ProductType = "Test Product Delta",
                                Term = 48
                            }
                        }
                    }
                }
            };
        }
    }
}
