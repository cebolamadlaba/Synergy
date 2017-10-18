using Moq;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.Model.UserInterface.Cash;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;
using static StandardBank.ConcessionManagement.Test.Helpers.MockedDependencies;
using Concession = StandardBank.ConcessionManagement.Model.UserInterface.Concession;
using RiskGroup = StandardBank.ConcessionManagement.Model.UserInterface.RiskGroup;

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
            _cashManager = new CashManager(MockConcessionManager.Object,
                MockConcessionCashRepository.Object, MockLegalEntityRepository.Object, InstantiatedDependencies.Mapper,
                MockLegalEntityAccountRepository.Object, MockFinancialCashRepository.Object,
                MockProductCashRepository.Object, MockLookupTableManager.Object, MockLoadedPriceCashRepository.Object,
                MockRuleManager.Object);
        }

        /// <summary>
        /// Tests that GetCashConcessionsForRiskGroupNumber executes positive.
        /// </summary>
        [Fact]
        public void GetCashConcessionsForRiskGroupNumber_Executes_Positive()
        {
            MockLookupTableManager.Setup(_ => _.GetRiskGroupForRiskGroupNumber(It.IsAny<int>()))
                .Returns(new RiskGroup {Id = 1, Name = "Test Risk Group", Number = 1000});

            MockConcessionManager.Setup(_ => _.GetConcessionsForRiskGroup(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(new[] {new Concession()});

            MockConcessionCashRepository.Setup(_ => _.ReadByConcessionId(It.IsAny<int>()))
                .Returns(new[] {new ConcessionCash()});

            MockLegalEntityRepository.Setup(_ => _.ReadById(It.IsAny<int>()))
                .Returns(new LegalEntity {IsActive = true});

            MockLegalEntityAccountRepository.Setup(_ => _.ReadById(It.IsAny<int>()))
                .Returns(new LegalEntityAccount {IsActive = true});

            var result = _cashManager.GetCashConcessionsForRiskGroupNumber(1);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that CreateConcessionCash executes positive.
        /// </summary>
        [Fact]
        public void CreateConcessionCash_Executes_Positive()
        {
            MockConcessionCashRepository.Setup(_ => _.Create(It.IsAny<ConcessionCash>())).Returns(new ConcessionCash());

            var result = _cashManager.CreateConcessionCash(new CashConcessionDetail(), new Concession());

            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests that GetCashConcession executes positive.
        /// </summary>
        [Fact]
        public void GetCashConcession_Executes_Positive()
        {
            MockConcessionManager.Setup(_ => _.GetConcessionForConcessionReferenceId(It.IsAny<string>()))
                .Returns(new Concession());

            MockConcessionCashRepository.Setup(_ => _.ReadByConcessionId(It.IsAny<int>()))
                .Returns(new[] {new ConcessionCash()});

            MockLegalEntityRepository.Setup(_ => _.ReadById(It.IsAny<int>()))
                .Returns(new LegalEntity { IsActive = true });

            MockLegalEntityAccountRepository.Setup(_ => _.ReadById(It.IsAny<int>()))
                .Returns(new LegalEntityAccount { IsActive = true });

            var result = _cashManager.GetCashConcession("C001", new Model.UserInterface.User());

            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests that DeleteConcessionCash executes positive.
        /// </summary>
        [Fact]
        public void DeleteConcessionCash_Executes_Positive()
        {
            MockConcessionCashRepository.Setup(_ => _.ReadById(It.IsAny<int>())).Returns(new ConcessionCash());

            var result = _cashManager.DeleteConcessionCash(new CashConcessionDetail());

            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests that UpdateConcessionCash executes positive.
        /// </summary>
        [Fact]
        public void UpdateConcessionCash_Executes_Positive()
        {
            var result = _cashManager.UpdateConcessionCash(new CashConcessionDetail(), new Concession());

            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests that GetCashViewData executes positive.
        /// </summary>
        [Fact]
        public void GetCashViewData_Executes_Positive()
        {
            MockLookupTableManager.Setup(_ => _.GetRiskGroupForRiskGroupNumber(It.IsAny<int>()))
                .Returns(new RiskGroup { Id = 1, Name = "Test Risk Group", Number = 1000 });

            MockConcessionManager.Setup(_ => _.GetConcessionsForRiskGroup(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(new[] { new Concession() });

            MockConcessionCashRepository.Setup(_ => _.ReadByConcessionId(It.IsAny<int>()))
                .Returns(new[] { new ConcessionCash() });

            MockLegalEntityRepository.Setup(_ => _.ReadById(It.IsAny<int>()))
                .Returns(new LegalEntity { IsActive = true });

            MockLegalEntityAccountRepository.Setup(_ => _.ReadById(It.IsAny<int>()))
                .Returns(new LegalEntityAccount { IsActive = true });

            MockFinancialCashRepository.Setup(_ => _.ReadByRiskGroupId(It.IsAny<int>()))
                .Returns(new[] {new FinancialCash()});

            MockProductCashRepository.Setup(_ => _.ReadByRiskGroupId(It.IsAny<int>()))
                .Returns(new[] {new ProductCash { TableNumberId = 1}});

            MockLookupTableManager.Setup(_ => _.GetTableNumbers(It.IsAny<string>()))
                .Returns(new[] {new Model.UserInterface.TableNumber {Id = 1}});

            var result = _cashManager.GetCashViewData(1);

            Assert.NotNull(result);
            Assert.NotNull(result.RiskGroup);
            Assert.NotNull(result.CashConcessions);
        }

        /// <summary>
        /// Tests that GetCashFinancialForRiskGroupNumber executes positive.
        /// </summary>
        [Fact]
        public void GetCashFinancialForRiskGroupNumber_Executes_Positive()
        {
            MockLookupTableManager.Setup(_ => _.GetRiskGroupForRiskGroupNumber(It.IsAny<int>()))
                .Returns(new RiskGroup {Id = 1, Name = "Test Risk Group", Number = 1000});

            MockFinancialCashRepository.Setup(_ => _.ReadByRiskGroupId(It.IsAny<int>()))
                .Returns(new[] {new FinancialCash {LatestCrsOrMrs = 123321}});

            var result = _cashManager.GetCashFinancialForRiskGroupNumber(1);

            Assert.NotNull(result);
            Assert.Equal(123321, result.LatestCrsOrMrs);
        }

        /// <summary>
        /// Tests that GetLatestCrsOrMrs executes positive.
        /// </summary>
        [Fact]
        public void GetLatestCrsOrMrs_Executes_Positive()
        {
            MockLookupTableManager.Setup(_ => _.GetRiskGroupForRiskGroupNumber(It.IsAny<int>()))
                .Returns(new RiskGroup { Id = 1, Name = "Test Risk Group", Number = 1000 });

            MockFinancialCashRepository.Setup(_ => _.ReadByRiskGroupId(It.IsAny<int>()))
                .Returns(new[] { new FinancialCash { LatestCrsOrMrs = 2000 } });

            var result = _cashManager.GetLatestCrsOrMrs(1);

            Assert.NotNull(result);
            Assert.Equal(result, 2000);
        }
    }
}
