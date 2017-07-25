using Moq;
using StandardBank.ConcessionManagement.Model.UserInterface.Pricing;
using StandardBank.ConcessionManagement.UI.Controllers.Pricing;
using Xunit;
using static StandardBank.ConcessionManagement.Test.Helpers.MockedDependencies;

namespace StandardBank.ConcessionManagement.UI.Test.UnitTest.Pricing
{
    /// <summary>
    /// Risk group legal entities controller tests
    /// </summary>
    public class RiskGroupLegalEntitiesControllerTest
    {
        /// <summary>
        /// The risk group legal entities controller
        /// </summary>
        private readonly RiskGroupLegalEntitiesController _riskGroupLegalEntitiesController;

        /// <summary>
        /// Initializes a new instance of the <see cref="RiskGroupLegalEntitiesControllerTest"/> class.
        /// </summary>
        public RiskGroupLegalEntitiesControllerTest()
        {
            _riskGroupLegalEntitiesController = new RiskGroupLegalEntitiesController(MockPricingManager.Object);
        }

        /// <summary>
        /// Tests that Get executes positive.
        /// </summary>
        [Fact]
        public void Get_Executes_Positive()
        {
            MockPricingManager.Setup(_ => _.GetLegalEntitiesForRiskGroupNumber(It.IsAny<int>()))
                .Returns(new[] {new LegalEntity()});

            var result = _riskGroupLegalEntitiesController.Get(1);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
    }
}
