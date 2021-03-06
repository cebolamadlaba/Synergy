using Moq;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface.Transactional;
using StandardBank.ConcessionManagement.Test.Helpers;
using static StandardBank.ConcessionManagement.Test.Helpers.MockedDependencies;
using Xunit;
using RiskGroup = StandardBank.ConcessionManagement.Model.UserInterface.RiskGroup;

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
            _transactionalManager = new TransactionalManager(MockConcessionManager.Object,
                MockConcessionTransactionalRepository.Object, InstantiatedDependencies.Mapper,
                MockLookupTableManager.Object, MockFinancialTransactionalRepository.Object,
                MockLoadedPriceTransactionalRepository.Object, MockRuleManager.Object,
                MockMiscPerformanceRepository.Object, null, null);
        }

        /// <summary>
        /// Tests that GetTransactionalConcession executes positive.
        /// </summary>
        [Fact]
        public void GetTransactionalConcession_Executes_Positive()
        {
            MockConcessionManager.Setup(_ => _.GetConcessionForConcessionReferenceId(It.IsAny<string>(), null))
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
            MockLookupTableManager.Setup(_ => _.GetRiskGroupForRiskGroupNumber(It.IsAny<int>()))
                .Returns(new RiskGroup { Id = 1, Name = "Test Risk Group", Number = 1000 });

            MockConcessionManager.Setup(_ => _.GetApprovedConcessionsForRiskGroup(It.IsAny<int>(), It.IsAny<string>(), null))
                .Returns(new[] { new Model.UserInterface.Concession() });

            MockMiscPerformanceRepository.Setup(_ => _.GetTransactionalConcessionDetails(It.IsAny<int>()))
                .Returns(new[] { new TransactionalConcessionDetail() });

            MockFinancialTransactionalRepository.Setup(_ => _.ReadByRiskGroupId(It.IsAny<int>()))
                .Returns(new[] { new FinancialTransactional() });

            MockMiscPerformanceRepository.Setup(_ => _.GetTransactionalProducts(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(new[] { new TransactionalProduct() });

            var result = _transactionalManager.GetTransactionalViewData(1, 0, null);

            Assert.NotNull(result);
            Assert.NotNull(result.RiskGroup);
            Assert.NotNull(result.TransactionalConcessions);
            Assert.NotEmpty(result.TransactionalConcessions);
            Assert.NotNull(result.TransactionalFinancial);
            Assert.NotNull(result.TransactionalProductGroups);

        }

        /// <summary>
        /// Tests that GetLatestCrsOrMrs executes positive.
        /// </summary>
        [Fact]
        public void GetLatestCrsOrMrs_Executes_Positive()
        {
            MockLookupTableManager.Setup(_ => _.GetRiskGroupForRiskGroupNumber(It.IsAny<int>()))
                .Returns(new RiskGroup { Id = 1, Name = "Test Risk Group", Number = 1000 });

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
            MockLookupTableManager.Setup(_ => _.GetRiskGroupForRiskGroupNumber(It.IsAny<int>()))
                .Returns(new RiskGroup { Id = 1, Name = "Test Risk Group", Number = 1000 });

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

