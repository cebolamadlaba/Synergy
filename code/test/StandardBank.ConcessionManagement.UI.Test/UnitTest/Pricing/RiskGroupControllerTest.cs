using Moq;
using StandardBank.ConcessionManagement.Model.UserInterface.Pricing;
using StandardBank.ConcessionManagement.UI.Controllers.Pricing;
using Xunit;
using static StandardBank.ConcessionManagement.Test.Helpers.MockedDependencies;

namespace StandardBank.ConcessionManagement.UI.Test.UnitTest.Pricing
{
    /// <summary>
    /// Risk group name controller tests
    /// </summary>
    public class RiskGroupControllerTest
    {
        /// <summary>
        /// The risk group name controller
        /// </summary>
        private readonly RiskGroupController _riskGroupNameController;

        /// <summary>
        /// Initializes a new instance of the <see cref="RiskGroupControllerTest"/> class.
        /// </summary>
        public RiskGroupControllerTest()
        {
            _riskGroupNameController = new RiskGroupController(MockPricingManager.Object);
        }

        /// <summary>
        /// Tests that Get executes positive.
        /// </summary>
        [Fact]
        public void Get_Executes_Positive()
        {
            var riskGroup = new RiskGroup {Id = 1, Name = "Unit Test Risk Group", Number = 1};
            MockPricingManager.Setup(_ => _.GetRiskGroupForRiskGroupNumber(It.IsAny<int>())).Returns(riskGroup);

            var result =  _riskGroupNameController.Get(1);

            Assert.NotNull(result);
            Assert.Equal(riskGroup.Id, result.Id);
            Assert.Equal(riskGroup.Name, result.Name);
            Assert.Equal(riskGroup.Number, result.Number);
        }
    }
}
