using Microsoft.AspNetCore.Mvc;
using Moq;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.UI.Controllers;
using Xunit;
using static StandardBank.ConcessionManagement.Test.Helpers.MockedDependencies;

namespace StandardBank.ConcessionManagement.UI.Test.UnitTest
{
    /// <summary>
    /// Pricing controller tests
    /// </summary>
    public class PricingControllerTest
    {
        /// <summary>
        /// The pricing controller
        /// </summary>
        private readonly PricingController _pricingController;

        /// <summary>
        /// Initializes the class
        /// </summary>
        public PricingControllerTest()
        {
            _pricingController = new PricingController(MockLookupTableManager.Object, MockSiteHelper.Object);
        }

        /// <summary>
        /// Tests that RiskGroup executes positive
        /// </summary>
        [Fact]
        public void RiskGroup_Executes_Positive()
        {
            var riskGroup = new RiskGroup
            {
                Id = 1,
                Name = "Unit Test Risk Group",
                Number = 1,
                MarketSegmentId = 1,
                MarketSegment = "Unit Test Market Segment",
                RegionId = 1,
                Region = "Unit Test Region"
            };

            MockLookupTableManager.Setup(_ => _.GetRiskGroupForRiskGroupNumber(It.IsAny<int>())).Returns(riskGroup);

            MockSiteHelper.Setup(_ => _.LoggedInUser(It.IsAny<Controller>())).Returns(new User()
            {
                SelectedRegion = new Region {Id = 1}
            });

            var result = _pricingController.RiskGroup(1);
            var apiResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(apiResult.Value);

            var resultRiskGroup = apiResult.Value as RiskGroup;

            Assert.NotNull(resultRiskGroup);
            Assert.Equal(riskGroup.Id, resultRiskGroup.Id);
            Assert.Equal(riskGroup.Name, resultRiskGroup.Name);
            Assert.Equal(riskGroup.Number, resultRiskGroup.Number);
            Assert.Equal(riskGroup.MarketSegmentId, resultRiskGroup.MarketSegmentId);
            Assert.Equal(riskGroup.MarketSegment, resultRiskGroup.MarketSegment);
            Assert.Equal(riskGroup.RegionId, resultRiskGroup.RegionId);
            Assert.Equal(riskGroup.Region, resultRiskGroup.Region);
        }
    }
}