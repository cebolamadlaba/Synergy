using Moq;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface.Transactional;
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
                MockLegalEntityAccountRepository.Object, InstantiatedDependencies.Mapper, MockLookupTableManager.Object,
                MockFinancialTransactionalRepository.Object, MockProductTransactionalRepository.Object,
                MockLoadedPriceTransactionalRepository.Object);
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

        /// <summary>
        /// Tests that CreateConcessionTransactional executes positive.
        /// </summary>
        [Fact]
        public void CreateConcessionTransactional_Executes_Positive()
        {
            MockConcessionTransactionalRepository.Setup(_ => _.Create(It.IsAny<ConcessionTransactional>()))
                .Returns(new ConcessionTransactional());

            var result =
                _transactionalManager.CreateConcessionTransactional(new TransactionalConcessionDetail(),
                    new Model.UserInterface.Concession());

            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests that UpdateConcessionTransactional executes positive.
        /// </summary>
        [Fact]
        public void UpdateConcessionTransactional_Executes_Positive()
        {
            var result =
                _transactionalManager.UpdateConcessionTransactional(new TransactionalConcessionDetail(),
                    new Model.UserInterface.Concession());

            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests that GetTransactionalViewData executes positive.
        /// </summary>
        [Fact]
        public void GetTransactionalViewData_Executes_Positive()
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

            MockFinancialTransactionalRepository.Setup(_ => _.ReadByRiskGroupId(It.IsAny<int>()))
                .Returns(new[] {new FinancialTransactional()});

            MockProductTransactionalRepository.Setup(_ => _.ReadByRiskGroupId(It.IsAny<int>()))
                .Returns(new[] {new ProductTransactional { TableNumberId = 1}});

            MockLookupTableManager.Setup(_ => _.GetTableNumbers(It.IsAny<string>()))
                .Returns(new[] {new Model.UserInterface.TableNumber {Id = 1}});

            MockLookupTableManager.Setup(_ => _.GetTransactionTypeDescription(It.IsAny<int>()))
                .Returns("Test Transaction Description");

            var result = _transactionalManager.GetTransactionalViewData(1);

            Assert.NotNull(result);
            Assert.NotNull(result.RiskGroup);
            Assert.NotNull(result.TransactionalConcessions);
            Assert.NotEmpty(result.TransactionalConcessions);
            Assert.NotNull(result.TransactionalFinancial);
            Assert.NotNull(result.TransactionalProducts);
            Assert.NotEmpty(result.TransactionalProducts);
        }

        /// <summary>
        /// Tests that GetLatestCrsOrMrs executes positive.
        /// </summary>
        [Fact]
        public void GetLatestCrsOrMrs_Executes_Positive()
        {
            MockPricingManager.Setup(_ => _.GetRiskGroupForRiskGroupNumber(It.IsAny<int>()))
                .Returns(new Model.UserInterface.Pricing.RiskGroup { Id = 1, Name = "Test Risk Group", Number = 1000 });

            MockFinancialTransactionalRepository.Setup(_ => _.ReadByRiskGroupId(It.IsAny<int>()))
                .Returns(new[] { new FinancialTransactional { LatestCrsOrMrs = 2000 } });

            var result = _transactionalManager.GetLatestCrsOrMrs(1);

            Assert.NotNull(result);
            Assert.Equal(result, 2000);
        }

        /// <summary>
        /// Tests that GetTransactionalFinancialForRiskGroupNumber executes positive.
        /// </summary>
        [Fact]
        public void GetTransactionalFinancialForRiskGroupNumber_Executes_Positive()
        {
            MockPricingManager.Setup(_ => _.GetRiskGroupForRiskGroupNumber(It.IsAny<int>()))
                .Returns(new Model.UserInterface.Pricing.RiskGroup { Id = 1, Name = "Test Risk Group", Number = 1000 });

            MockFinancialTransactionalRepository.Setup(_ => _.ReadByRiskGroupId(It.IsAny<int>()))
                .Returns(new[] { new FinancialTransactional { LatestCrsOrMrs = 2000 } });

            var result = _transactionalManager.GetTransactionalFinancialForRiskGroupNumber(1);

            Assert.NotNull(result);
            Assert.Equal(result.LatestCrsOrMrs, 2000);
        }

        /// <summary>
        /// Tests that DeleteConcessionTransactional executes positive.
        /// </summary>
        [Fact]
        public void DeleteConcessionTransactional_Executes_Positive()
        {
            MockConcessionTransactionalRepository.Setup(_ => _.ReadById(It.IsAny<int>()))
                .Returns(new ConcessionTransactional());

            var result = _transactionalManager.DeleteConcessionTransactional(new TransactionalConcessionDetail());

            Assert.NotNull(result);
        }
    }
}

