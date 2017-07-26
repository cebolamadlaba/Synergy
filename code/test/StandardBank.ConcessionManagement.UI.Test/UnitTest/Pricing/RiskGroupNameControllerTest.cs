using Moq;
using StandardBank.ConcessionManagement.UI.Controllers.Pricing;
using Xunit;
using static StandardBank.ConcessionManagement.Test.Helpers.MockedDependencies;

namespace StandardBank.ConcessionManagement.UI.Test.UnitTest.Pricing
{
    /// <summary>
    /// Risk group name controller tests
    /// </summary>
    public class RiskGroupNameControllerTest
    {
        /// <summary>
        /// The risk group name controller
        /// </summary>
        private readonly RiskGroupNameController _riskGroupNameController;

        /// <summary>
        /// Initializes a new instance of the <see cref="RiskGroupNameControllerTest"/> class.
        /// </summary>
        public RiskGroupNameControllerTest()
        {
            _riskGroupNameController = new RiskGroupNameController(MockPricingManager.Object);
        }

        /// <summary>
        /// Tests that Get executes positive.
        /// </summary>
        [Fact]
        public void Get_Executes_Positive()
        {
            var riskGroupName = "Unit Test Risk Group";
            MockPricingManager.Setup(_ => _.GetRiskGroupName(It.IsAny<int>())).Returns(riskGroupName);

            var result =  _riskGroupNameController.Get(1);

            Assert.NotNull(result);
            Assert.Equal(riskGroupName, result);
        }
    }
}
