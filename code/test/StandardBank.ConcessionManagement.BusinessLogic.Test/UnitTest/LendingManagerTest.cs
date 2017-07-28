using Moq;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface;
using Xunit;
using static StandardBank.ConcessionManagement.Test.Helpers.MockedDependencies;
using RiskGroup = StandardBank.ConcessionManagement.Model.UserInterface.Pricing.RiskGroup;

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
            _lendingManager = new LendingManager(MockPricingManager.Object, MockConcessionManager.Object,
                MockLegalEntityRepository.Object, MockConcessionLendingRepository.Object);
        }

        /// <summary>
        /// Tests that GetLendingConcessionsForRiskGroupNumber executes positive.
        /// </summary>
        [Fact]
        public void GetLendingConcessionsForRiskGroupNumber_Executes_Positive()
        {
            MockPricingManager.Setup(_ => _.GetRiskGroupForRiskGroupNumber(It.IsAny<int>())).Returns(new RiskGroup());

            MockLegalEntityRepository.Setup(_ => _.ReadByRiskGroupIdIsActive(It.IsAny<int>(), It.IsAny<bool>()))
                .Returns(new[] {new LegalEntity()});

            MockConcessionManager
                .Setup(_ => _.GetConcessionsForLegalEntityIdAndConcessionType(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(new[] {new Model.UserInterface.Concession()});

            MockConcessionLendingRepository.Setup(_ => _.ReadByConcessionId(It.IsAny<int>()))
                .Returns(new ConcessionLending());

            var result = _lendingManager.GetLendingConcessionsForRiskGroupNumber(1);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
    }
}
