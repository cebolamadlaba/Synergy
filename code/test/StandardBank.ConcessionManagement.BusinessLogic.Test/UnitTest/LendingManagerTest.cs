using Moq;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface.Lending;
using StandardBank.ConcessionManagement.Test.Helpers;
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
            _lendingManager = new LendingManager(MockPricingManager.Object, MockConcessionManager.Object,
                MockLegalEntityRepository.Object, MockConcessionLendingRepository.Object,
                InstantiatedDependencies.Mapper, MockLegalEntityAccountRepository.Object);
        }

        /// <summary>
        /// Tests that CreateConcessionLending executes positive
        /// </summary>
        [Fact]
        public void CreateConcessionLending_Executes_Positive()
        {
            MockConcessionLendingRepository.Setup(_ => _.Create(It.IsAny<ConcessionLending>()))
                .Returns(new ConcessionLending());

            var result = _lendingManager.CreateConcessionLending(new LendingConcessionDetail(), new Model.UserInterface.Concession());

            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests that GetLendingConcession executes positive
        /// </summary>
        [Fact]
        public void GetLendingConcession_Executes_Positive()
        {
            MockConcessionManager.Setup(_ => _.GetConcessionForConcessionReferenceId(It.IsAny<string>()))
                .Returns(new Model.UserInterface.Concession());

            MockConcessionLendingRepository.Setup(_ => _.ReadByConcessionId(It.IsAny<int>()))
                .Returns(new[] {new ConcessionLending()});

            MockLegalEntityRepository.Setup(_ => _.ReadById(It.IsAny<int>())).Returns(new LegalEntity());

            MockConcessionManager.Setup(_ => _.GetConcessionConditions(It.IsAny<int>()))
                .Returns(new[] {new Model.UserInterface.ConcessionCondition()});

            var result = _lendingManager.GetLendingConcession("L001", new Model.UserInterface.User());

            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests that GetLendingConcessionsForRiskGroupNumber executes positive.
        /// </summary>
        [Fact]
        public void GetLendingConcessionsForRiskGroupNumber_Executes_Positive()
        {
            MockPricingManager.Setup(_ => _.GetRiskGroupForRiskGroupNumber(It.IsAny<int>()))
                .Returns(new Model.UserInterface.Pricing.RiskGroup { Id = 1, Name = "Test Risk Group", Number = 1000 });

            MockConcessionManager.Setup(_ => _.GetConcessionsForRiskGroup(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(new[] { new Model.UserInterface.Concession() });

            MockConcessionLendingRepository.Setup(_ => _.ReadByConcessionId(It.IsAny<int>()))
                .Returns(new[] { new ConcessionLending() });

            MockLegalEntityRepository.Setup(_ => _.ReadById(It.IsAny<int>()))
                .Returns(new LegalEntity { IsActive = true });

            MockLegalEntityAccountRepository.Setup(_ => _.ReadById(It.IsAny<int>()))
                .Returns(new LegalEntityAccount { IsActive = true });

            var result = _lendingManager.GetLendingConcessionsForRiskGroupNumber(1);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that DeleteConcessionLending executes positive.
        /// </summary>
        [Fact]
        public void DeleteConcessionLending_Executes_Positive()
        {
            MockConcessionLendingRepository.Setup(_ => _.ReadById(It.IsAny<int>())).Returns(new ConcessionLending());

            var result = _lendingManager.DeleteConcessionLending(new LendingConcessionDetail());

            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests that UpdateConcessionLending executes positive.
        /// </summary>
        [Fact]
        public void UpdateConcessionLending_Executes_Positive()
        {
            var result = _lendingManager.UpdateConcessionLending(new LendingConcessionDetail(), new Model.UserInterface.Concession());

            Assert.NotNull(result);
        }
    }
}
