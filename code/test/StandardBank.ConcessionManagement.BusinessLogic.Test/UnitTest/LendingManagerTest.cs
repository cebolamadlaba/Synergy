using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using Xunit;
using static StandardBank.ConcessionManagement.Test.Helpers.MockedDependencies;

namespace StandardBank.ConcessionManagement.BusinessLogic.Test.UnitTest
{
    /// <summary>
    /// Lending manager tests
    /// </summary>
    public class LendingManagerTest
    {
        /// <summary>
        /// The lending manager
        /// </summary>
        private readonly ILendingManager _lendingManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="LendingManagerTest"/> class.
        /// </summary>
        public LendingManagerTest()
        {
            _lendingManager = new LendingManager(MockPricingManager.Object, MockLegalEntityRepository.Object, MockLookupTableManager.Object);
        }

        /// <summary>
        /// Tests that GetLendingConcessionsForRiskGroupNumber executes positive.
        /// </summary>
        //[Fact] //TODO: Get this working once the method is done
        public void GetLendingConcessionsForRiskGroupNumber_Executes_Positive()
        {
            var result = _lendingManager.GetLendingConcessionsForRiskGroupNumber(1);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
    }
}
