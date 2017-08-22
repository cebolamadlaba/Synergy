using Moq;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.Model.UserInterface.Pricing;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;
using static StandardBank.ConcessionManagement.Test.Helpers.MockedDependencies;

namespace StandardBank.ConcessionManagement.BusinessLogic.Test.UnitTest
{
    /// <summary>
    /// Cash manager tests
    /// </summary>
    public class CashManagerTest
    {
        /// <summary>
        /// The cash manager
        /// </summary>
        private readonly ICashManager _cashManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CashManagerTest"/> class.
        /// </summary>
        public CashManagerTest()
        {
            _cashManager = new CashManager(MockPricingManager.Object, MockConcessionManager.Object,
                MockConcessionCashRepository.Object, MockLegalEntityRepository.Object, InstantiatedDependencies.Mapper,
                MockLegalEntityAccountRepository.Object);
        }

        /// <summary>
        /// Tests that GetCashConcessionsForRiskGroupNumber executes positive.
        /// </summary>
        //[Fact] //TODO: Fix this test
        public void GetCashConcessionsForRiskGroupNumber_Executes_Positive()
        {
            MockPricingManager.Setup(_ => _.GetRiskGroupForRiskGroupNumber(It.IsAny<int>()))
                .Returns(new RiskGroup {Id = 1, Name = "Test Risk Group", Number = 1000});

            MockConcessionManager.Setup(_ => _.GetConcessionsForRiskGroup(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(new[] {new Concession()});

            var result = _cashManager.GetCashConcessionsForRiskGroupNumber(1);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
    }
}
