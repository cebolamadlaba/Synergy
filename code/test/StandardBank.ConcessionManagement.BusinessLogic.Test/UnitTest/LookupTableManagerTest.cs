using Moq;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using Xunit;
using static StandardBank.ConcessionManagement.Test.Helpers.MockedDependencies;

namespace StandardBank.ConcessionManagement.BusinessLogic.Test.UnitTest
{
    /// <summary>
    /// Look up manager tests
    /// </summary>
    public class LookupTableManagerTest
    {
        /// <summary>
        /// The lookup table manager
        /// </summary>
        private readonly ILookupTableManager _lookupTableManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="LookupTableManagerTest"/> class.
        /// </summary>
        public LookupTableManagerTest()
        {
            _lookupTableManager = new LookupTableManager(MockStatusRepository.Object, MockSubStatusRepository.Object,
                MockTypeRepository.Object, MockMarketSegmentRepository.Object, MockProvinceRepository.Object,
                MockConcessionTypeRepository.Object, MockProductRepository.Object, MockReviewFeeTypeRepository.Object,
                MockPeriodRepository.Object, MockPeriodTypeRepository.Object);
        }

        /// <summary>
        /// Tests that GetStatusId executes positive.
        /// </summary>
        [Fact]
        public void GetStatusId_Executes_Positive()
        {
            var status = new Status {Description = "Status Test", Id = 10, IsActive = true};

            MockStatusRepository.Setup(_ => _.ReadAll()).Returns(new[] {status});
            var result = _lookupTableManager.GetStatusId(status.Description);

            Assert.NotNull(result);
            Assert.Equal(result, status.Id);
        }

        /// <summary>
        /// Tests that GetSubStatusId executes positive.
        /// </summary>
        [Fact]
        public void GetSubStatusId_Executes_Positive()
        {
            var subStatus = new SubStatus { Description = "Sub Status Test", Id = 20, IsActive = true };

            MockSubStatusRepository.Setup(_ => _.ReadAll()).Returns(new[] { subStatus });
            var result = _lookupTableManager.GetSubStatusId(subStatus.Description);

            Assert.NotNull(result);
            Assert.Equal(result, subStatus.Id);
        }

        /// <summary>
        /// Tests that GetReferenceTypeName executes positive.
        /// </summary>
        [Fact]
        public void GetReferenceTypeName_Executes_Positive()
        {
            var referenceType = new ReferenceType { Description = "Reference Type Test", Id = 30, IsActive = true };

            MockTypeRepository.Setup(_ => _.ReadAll()).Returns(new[] { referenceType });
            var result = _lookupTableManager.GetReferenceTypeName(referenceType.Id);

            Assert.NotNull(result);
            Assert.Equal(result, referenceType.Description);
        }

        /// <summary>
        /// Tests that GetMarketSegmentName executes positive.
        /// </summary>
        [Fact]
        public void GetMarketSegmentName_Executes_Positive()
        {
            var marketSegment = new MarketSegment { Description = "Reference Type Test", Id = 30, IsActive = true };

            MockMarketSegmentRepository.Setup(_ => _.ReadAll()).Returns(new[] { marketSegment });
            var result = _lookupTableManager.GetMarketSegmentName(marketSegment.Id);

            Assert.NotNull(result);
            Assert.Equal(result, marketSegment.Description);
        }

        /// <summary>
        /// Tests that GetProvinceName executes positive
        /// </summary>
        [Fact]
        public void GetProvinceName_Executes_Positive()
        {
            var province = new Province {Id = 1, Description = "Unit Test Province", IsActive = true};

            MockProvinceRepository.Setup(_ => _.ReadAll()).Returns(new[] {province});

            var result = _lookupTableManager.GetProvinceName(province.Id);

            Assert.NotNull(result);
            Assert.Equal(result, province.Description);
        }

        /// <summary>
        /// Tests that GetConcessionTypeId executes positive
        /// </summary>
        [Fact]
        public void GetConcessionTypeId_Executes_Positive()
        {
            var concessionType =
                new ConcessionType {Code = "CODE", Description = "Description", Id = 1, IsActive = true};

            MockConcessionTypeRepository.Setup(_ => _.ReadAll()).Returns(new[] {concessionType});

            var result = _lookupTableManager.GetConcessionTypeId(concessionType.Code);

            Assert.NotNull(result);
            Assert.Equal(result, concessionType.Id);
        }

        /// <summary>
        /// Tests that GetProductTypesForConcessionType executes positive
        /// </summary>
        [Fact]
        public void GetProductTypesForConcessionType_Executes_Positive()
        {
            var concessionType =
                new ConcessionType { Code = "CODE", Description = "Description", Id = 1, IsActive = true };

            MockConcessionTypeRepository.Setup(_ => _.ReadAll()).Returns(new[] { concessionType });

            MockProductRepository.Setup(_ => _.ReadByConcessionTypeIdIsActive(It.IsAny<int>(), It.IsAny<bool>()))
                .Returns(new[]
                {
                    new Product
                    {
                        ConcessionTypeId = concessionType.Id,
                        Id = 1,
                        Description = "Test Product",
                        IsActive = true
                    }
                });

            MockConcessionTypeRepository.Setup(_ => _.ReadById(It.IsAny<int>())).Returns(concessionType);

            var result = _lookupTableManager.GetProductTypesForConcessionType(concessionType.Code);

            Assert.NotNull(result);
            Assert.NotEmpty(result);

            foreach (var record in result)
            {
                Assert.NotNull(record);
                Assert.NotNull(record.ConcessionType);
                Assert.Equal(record.ConcessionType.Code, concessionType.Code);
            }
        }

        /// <summary>
        /// Tests that GetReviewFeeTypes executes positive
        /// </summary>
        [Fact]
        public void GetReviewFeeTypes_Executes_Positive()
        {
            MockReviewFeeTypeRepository.Setup(_ => _.ReadAll()).Returns(new[]
                {new ReviewFeeType {Description = "Test 1", Id = 1, IsActive = true}});

            var result = _lookupTableManager.GetReviewFeeTypes();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that GetPeriods executes positive.
        /// </summary>
        [Fact]
        public void GetPeriods_Executes_Positive()
        {
            MockPeriodRepository.Setup(_ => _.ReadAll()).Returns(new[]
                {new Period {Description = "Test", Id = 1, IsActive = true}});

            var result = _lookupTableManager.GetPeriods();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that GetPeriodTypes executes positive.
        /// </summary>
        [Fact]
        public void GetPeriodTypes_Executes_Positive()
        {
            MockPeriodTypeRepository.Setup(_ => _.ReadAll()).Returns(new[]
                {new PeriodType {Description = "Test", Id = 1, IsActive = true}});

            var result = _lookupTableManager.GetPeriodTypes();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
    }
}
