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
                LendingConcessions = new[]
                {
                    new LendingConcession
                    {
                        Concession = new Concession { ReferenceNumber = "L00001" },
                        LendingConcessionDetails = new[]
                        {
                            new LendingConcessionDetail
                            {
                                AccountNumber = 1,
                                ApprovedMap = 2,
                                AverageBalance = 3,
                                CustomerName = "Test Customer Alpha",
                                Limit = 4,
                                LoadedMap = 5,
                                ProductType = "Test Product Alpha",
                                Term = 6
                            },
                            new LendingConcessionDetail
                            {
                                AccountNumber = 8,
                                ApprovedMap = 9,
                                AverageBalance = 10,
                                CustomerName = "Test Customer Beta",
                                Limit = 11,
                                LoadedMap = 12,
                                ProductType = "Test Product Beta",
                                Term = 13
                            }
                        }
                    },
                    new LendingConcession
                    {
                        Concession = new Concession { ReferenceNumber = "L00002" },
                        LendingConcessionDetails = new[]
                        {
                            new LendingConcessionDetail
                            {
                                AccountNumber = 14,
                                ApprovedMap = 15,
                                AverageBalance = 16,
                                CustomerName = "Test Customer Gamma",
                                Limit = 17,
                                LoadedMap = 18,
                                ProductType = "Test Product Gamma",
                                Term = 19
                            },
                            new LendingConcessionDetail
                            {
                                AccountNumber = 20,
                                ApprovedMap = 21,
                                AverageBalance = 22,
                                CustomerName = "Test Customer Delta",
                                Limit = 23,
                                LoadedMap = 24,
                                ProductType = "Test Product Delta",
                                Term = 25
                            }
                        }
                    }
                }
            };
        }
    }
}
