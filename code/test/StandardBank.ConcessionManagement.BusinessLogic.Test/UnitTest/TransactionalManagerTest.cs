using Moq;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using static StandardBank.ConcessionManagement.Test.Helpers.MockedDependencies;
using Xunit;

namespace StandardBank.ConcessionManagement.BusinessLogic.Test.UnitTest
{
    /// <summary>
    /// Transactional manager tests
    /// </summary>
    public class TransactionalManagerTest
    {
        /// <summary>
        /// The transactional manager
        /// </summary>
        private readonly ITransactionalManager _transactionalManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionalManagerTest"/> class.
        /// </summary>
        public TransactionalManagerTest()
        {
            _transactionalManager = new TransactionalManager(MockPricingManager.Object, MockConcessionManager.Object,
                MockConcessionTransactionalRepository.Object, MockLegalEntityRepository.Object,
                MockLegalEntityAccountRepository.Object, InstantiatedDependencies.Mapper, MockLookupTableManager.Object);
        }

        /// <summary>
        /// Tests that GetTransactionalConcessionsForRiskGroupNumber executes positive.
        /// </summary>
        [Fact] 
        public void GetTransactionalConcessionsForRiskGroupNumber_Executes_Positive()
        {
            MockPricingManager.Setup(_ => _.GetRiskGroupForRiskGroupNumber(It.IsAny<int>()))
                .Returns(new Model.UserInterface.Pricing.RiskGroup { Id = 1, Name = "Test Risk Group", Number = 1000 });

            MockConcessionManager.Setup(_ => _.GetConcessionsForRiskGroup(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(new[] { new Model.UserInterface.Concession() });

            MockConcessionTransactionalRepository.Setup(_ => _.ReadByConcessionId(It.IsAny<int>()))
                .Returns(new[] { new ConcessionTransactional() });

            MockLegalEntityRepository.Setup(_ => _.ReadById(It.IsAny<int>()))
                .Returns(new LegalEntity { IsActive = true });

            MockLegalEntityAccountRepository.Setup(_ => _.ReadById(It.IsAny<int>()))
                .Returns(new LegalEntityAccount { IsActive = true });

            var result = _transactionalManager.GetTransactionalConcessionsForRiskGroupNumber(1);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that GetTransactionalConcession executes positive.
        /// </summary>
        [Fact]
        public void GetTransactionalConcession_Executes_Positive()
        {
            MockConcessionManager.Setup(_ => _.GetConcessionForConcessionReferenceId(It.IsAny<string>()))
                .Returns(new Model.UserInterface.Concession());

            MockConcessionCashRepository.Setup(_ => _.ReadByConcessionId(It.IsAny<int>()))
                .Returns(new[] { new ConcessionCash() });

            MockLegalEntityRepository.Setup(_ => _.ReadById(It.IsAny<int>()))
                .Returns(new LegalEntity { IsActive = true });

            MockLegalEntityAccountRepository.Setup(_ => _.ReadById(It.IsAny<int>()))
                .Returns(new LegalEntityAccount { IsActive = true });

            var result = _transactionalManager.GetTransactionalConcession("T001", new Model.UserInterface.User());

            Assert.NotNull(result);
        }
    }
}
