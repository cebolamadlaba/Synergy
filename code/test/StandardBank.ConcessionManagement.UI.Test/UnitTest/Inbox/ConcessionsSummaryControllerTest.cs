using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using StandardBank.ConcessionManagement.UI.Controllers.Inbox;
using Xunit;

namespace StandardBank.ConcessionManagement.UI.Test.UnitTest.Inbox
{
    /// <summary>
    /// Concessions summary controller tests
    /// </summary>
    public class ConcessionsSummaryControllerTest
    {
        /// <summary>
        /// The concessions summary controller
        /// </summary>
        private readonly ConcessionsSummaryController _concessionsSummaryController;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcessionsSummaryControllerTest"/> class.
        /// </summary>
        public ConcessionsSummaryControllerTest()
        {
            _concessionsSummaryController =
                new ConcessionsSummaryController(MockedDependencies.MockConcessionCountRepository.Object);
        }

        /// <summary>
        /// Tests that Get executes positive.
        /// </summary>
        [Fact]
        public void Get_Executes_Positive()
        {
            //setup the repository to return specific values
            var concessionCounts = new[]
            {
                new ConcessionCount {Status = "Pending", Count = 1},
                new ConcessionCount {Status = "DueForExpiry", Count = 2},
                new ConcessionCount {Status = "Expired", Count = 3},
                new ConcessionCount {Status = "Declined", Count = 4}
            };

            MockedDependencies.MockConcessionCountRepository.Setup(_ => _.ReadAll()).Returns(concessionCounts);

            var result = _concessionsSummaryController.Get();

            Assert.NotNull(result);

            //check that all the values returned match the repository values
            Assert.Equal(concessionCounts.First(_ => _.Status == "Pending").Count, result.PendingConcessions);
            Assert.Equal(concessionCounts.First(_ => _.Status == "DueForExpiry").Count, result.DueForExpiryConcessions);
            Assert.Equal(concessionCounts.First(_ => _.Status == "Expired").Count, result.ExpiredConcessions);
            Assert.Equal(concessionCounts.First(_ => _.Status == "Declined").Count, result.DeclinedConcessions);
        }
    }
}
