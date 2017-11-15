using Moq;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.Model.UserInterface.Lending;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;
using static StandardBank.ConcessionManagement.Test.Helpers.MockedDependencies;
using RiskGroup = StandardBank.ConcessionManagement.Model.UserInterface.RiskGroup;

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
            _lendingManager = new LendingManager(MockConcessionManager.Object, MockConcessionLendingRepository.Object,
                InstantiatedDependencies.Mapper, MockFinancialLendingRepository.Object, MockLookupTableManager.Object,
                MockLoadedPriceLendingRepository.Object, MockRuleManager.Object, MockMiscPerformanceRepository.Object);
        }

        /// <summary>
        /// Tests that CreateConcessionLending executes positive
        /// </summary>
        [Fact]
        public void CreateConcessionLending_Executes_Positive()
        {
            MockConcessionLendingRepository.Setup(_ => _.Create(It.IsAny<ConcessionLending>()))
                .Returns(new ConcessionLending());

            var result = _lendingManager.CreateConcessionLending(new LendingConcessionDetail(),
                new Model.UserInterface.Concession());

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
        /// Tests that GetLendingViewData executes positive.
        /// </summary>
        [Fact]
        public void GetLendingViewData_Executes_Positive()
        {
            MockLookupTableManager.Setup(_ => _.GetRiskGroupForRiskGroupNumber(It.IsAny<int>()))
                .Returns(new RiskGroup { Id = 1, Name = "Test Risk Group", Number = 1000 });

            MockConcessionManager.Setup(_ => _.GetApprovedConcessionsForRiskGroup(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(new[] { new Model.UserInterface.Concession() });

            MockMiscPerformanceRepository.Setup(_ => _.GetLendingConcessionDetails(It.IsAny<int>()))
                .Returns(new[] { new LendingConcessionDetail() });

            MockMiscPerformanceRepository.Setup(_ => _.GetLendingProducts(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(new[] {new LendingProduct()});

            MockFinancialLendingRepository.Setup(_ => _.ReadByRiskGroupId(It.IsAny<int>()))
                .Returns(new[] {new FinancialLending {TotalExposure = 100}});

            var result = _lendingManager.GetLendingViewData(1);

            Assert.NotNull(result);
            Assert.NotEmpty(result.LendingConcessions);
            Assert.NotNull(result.RiskGroup);
            Assert.NotEmpty(result.LendingProducts);
            Assert.NotNull(result.LendingFinancial);
            Assert.Equal(result.LendingFinancial.TotalExposure, 100);
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

        /// <summary>
        /// Tests that GetLatestCrsOrMrs executes positive.
        /// </summary>
        [Fact]
        public void GetLatestCrsOrMrs_Executes_Positive()
        {
            MockLookupTableManager.Setup(_ => _.GetRiskGroupForRiskGroupNumber(It.IsAny<int>()))
                .Returns(new RiskGroup { Id = 1, Name = "Test Risk Group", Number = 1000 });

            MockFinancialLendingRepository.Setup(_ => _.ReadByRiskGroupId(It.IsAny<int>()))
                .Returns(new[] { new FinancialLending { LatestCrsOrMrs = 2000 } });

            var result = _lendingManager.GetLatestCrsOrMrs(1);

            Assert.NotNull(result);
            Assert.Equal(result, 2000);
        }

        /// <summary>
        /// Tests that GetLendingFinancialForRiskGroupNumber executes positive.
        /// </summary>
        [Fact]
        public void GetLendingFinancialForRiskGroupNumber_Executes_Positive()
        {
            MockLookupTableManager.Setup(_ => _.GetRiskGroupForRiskGroupNumber(It.IsAny<int>()))
                .Returns(new RiskGroup { Id = 1, Name = "Test Risk Group", Number = 1000 });

            MockFinancialLendingRepository.Setup(_ => _.ReadByRiskGroupId(It.IsAny<int>()))
                .Returns(new[] { new FinancialLending { LatestCrsOrMrs = 2000 } });

            var result = _lendingManager.GetLendingFinancialForRiskGroupNumber(1);

            Assert.NotNull(result);
            Assert.Equal(result.LatestCrsOrMrs, 2000);
        }
    }
}
