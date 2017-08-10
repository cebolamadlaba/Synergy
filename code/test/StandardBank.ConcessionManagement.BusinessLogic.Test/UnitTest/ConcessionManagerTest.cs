using System;
using Moq;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;
using Concession = StandardBank.ConcessionManagement.Model.Repository.Concession;
using Role = StandardBank.ConcessionManagement.Model.UserInterface.Role;
using User = StandardBank.ConcessionManagement.Model.UserInterface.User;
using static StandardBank.ConcessionManagement.Test.Helpers.MockedDependencies;
using System.Collections.Generic;

namespace StandardBank.ConcessionManagement.BusinessLogic.Test.UnitTest
{
    /// <summary>
    /// Concession manager test
    /// </summary>
    public class ConcessionManagerTest
    {
        /// <summary>
        /// The concession manager
        /// </summary>
        private readonly IConcessionManager _concessionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcessionManagerTest"/> class.
        /// </summary>
        public ConcessionManagerTest()
        {
            _concessionManager = new ConcessionManager(MockConcessionRepository.Object, MockLookupTableManager.Object,
                MockLegalEntityRepository.Object, MockRiskGroupRepository.Object,
                InstantiatedDependencies.CacheManager, MockConcessionAccountRepository.Object,
                InstantiatedDependencies.Mapper, MockConcessionConditionRepository.Object, MockLegalEntityAccountRepository.Object);
        }

        /// <summary>
        /// Tests that GetPendingConcessionsForUser that's a requestor executes positive.
        /// </summary>
        [Fact]
        public void GetPendingConcessionsForUser_Requestor_Executes_Positive()
        {
            MockLookupTableManager.Setup(_ => _.GetStatusId(It.IsAny<string>())).Returns(1);

            MockConcessionRepository
                .Setup(_ => _.ReadByRequestorIdStatusIdIsActive(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>()))
                .Returns(new[] {new Concession()});

            MockLegalEntityRepository.Setup(_ => _.ReadByIdIsActive(It.IsAny<int>(), It.IsAny<bool>()))
                .Returns(new LegalEntity { IsActive = true });

            MockRiskGroupRepository.Setup(_ => _.ReadByIdIsActive(It.IsAny<int>(), It.IsAny<bool>()))
                .Returns(new RiskGroup { IsActive = true });

            MockLookupTableManager.Setup(_ => _.GetMarketSegmentName(It.IsAny<int>())).Returns("Market Segment Name");
            MockLookupTableManager.Setup(_ => _.GetReferenceTypeName(It.IsAny<int>())).Returns("Type Name");

            var result = _concessionManager.GetPendingConcessionsForUser(new User
            {
                Id = 1,
                UserRoles = new[] {new Role() {Name = "Requestor"}}
            });

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that GetDueForExpiryConcessionsForUser that's a requestor executes positive
        /// </summary>
        [Fact]
        public void GetDueForExpiryConcessionsForUser_Requestor_Executes_Positive()
        {
            MockConcessionRepository
                .Setup(_ => _.ReadByRequestorIdBetweenStartExpiryDateEndExpiryDateIsActive(It.IsAny<int>(), It.IsAny<DateTime>(),
                    It.IsAny<DateTime>(), It.IsAny<bool>())).Returns(new[] { new Concession() });

            MockLegalEntityRepository.Setup(_ => _.ReadByIdIsActive(It.IsAny<int>(), It.IsAny<bool>()))
                .Returns(new LegalEntity { IsActive = true });

            MockRiskGroupRepository.Setup(_ => _.ReadByIdIsActive(It.IsAny<int>(), It.IsAny<bool>()))
                .Returns(new RiskGroup { IsActive = true });

            MockLookupTableManager.Setup(_ => _.GetMarketSegmentName(It.IsAny<int>())).Returns("Market Segment Name");
            MockLookupTableManager.Setup(_ => _.GetReferenceTypeName(It.IsAny<int>())).Returns("Type Name");

            var result = _concessionManager.GetDueForExpiryConcessionsForUser(new User
            {
                Id = 1,
                UserRoles = new[] { new Role() { Name = "Requestor" } }
            });

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that GetExpiredConcessionsForUser that's a requestor executes positive
        /// </summary>
        [Fact]
        public void GetExpiredConcessionsForUser_Requestor_Executes_Positive()
        {
            MockConcessionRepository
                .Setup(_ => _.ReadByRequestorIdBetweenStartExpiryDateEndExpiryDateIsActive(It.IsAny<int>(), It.IsAny<DateTime>(),
                    It.IsAny<DateTime>(), It.IsAny<bool>())).Returns(new[] { new Concession() });

            MockLegalEntityRepository.Setup(_ => _.ReadByIdIsActive(It.IsAny<int>(), It.IsAny<bool>()))
                .Returns(new LegalEntity { IsActive = true });

            MockRiskGroupRepository.Setup(_ => _.ReadByIdIsActive(It.IsAny<int>(), It.IsAny<bool>()))
                .Returns(new RiskGroup { IsActive = true });

            MockLookupTableManager.Setup(_ => _.GetMarketSegmentName(It.IsAny<int>())).Returns("Market Segment Name");
            MockLookupTableManager.Setup(_ => _.GetReferenceTypeName(It.IsAny<int>())).Returns("Type Name");

            var result = _concessionManager.GetExpiredConcessionsForUser(new User
            {
                Id = 1,
                UserRoles = new[] { new Role() { Name = "Requestor" } }
            });

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that GetMismatchedConcessionsForUser that's a requestor executes positive
        /// </summary>
        [Fact]
        public void GetMismatchedConcessionsForUser_Requestor_Executes_Positive()
        {
            MockLookupTableManager.Setup(_ => _.GetStatusId(It.IsAny<string>())).Returns(1);

            MockConcessionRepository
                .Setup(_ => _.ReadByRequestorIdStatusIdIsActive(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>()))
                .Returns(new[] {new Concession()});

            MockLegalEntityRepository.Setup(_ => _.ReadByIdIsActive(It.IsAny<int>(), It.IsAny<bool>()))
                .Returns(new LegalEntity { IsActive = true });

            MockRiskGroupRepository.Setup(_ => _.ReadByIdIsActive(It.IsAny<int>(), It.IsAny<bool>()))
                .Returns(new RiskGroup { IsActive = true });

            MockLookupTableManager.Setup(_ => _.GetMarketSegmentName(It.IsAny<int>())).Returns("Market Segment Name");
            MockLookupTableManager.Setup(_ => _.GetReferenceTypeName(It.IsAny<int>())).Returns("Type Name");

            var result = _concessionManager.GetMismatchedConcessionsForUser(new User
            {
                Id = 1,
                UserRoles = new[] { new Role() { Name = "Requestor" } }
            });

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that GetDeclinedConcessionsForUser that's a requestor executes positive
        /// </summary>
        [Fact]
        public void GetDeclinedConcessionsForUser_Requestor_Executes_Positive()
        {
            MockLookupTableManager.Setup(_ => _.GetStatusId(It.IsAny<string>())).Returns(1);

            MockConcessionRepository
                .Setup(_ => _.ReadByRequestorIdStatusIdIsActive(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>()))
                .Returns(new[] { new Concession() });

            MockLegalEntityRepository.Setup(_ => _.ReadByIdIsActive(It.IsAny<int>(), It.IsAny<bool>()))
                .Returns(new LegalEntity { IsActive = true});

            MockRiskGroupRepository.Setup(_ => _.ReadByIdIsActive(It.IsAny<int>(), It.IsAny<bool>()))
                .Returns(new RiskGroup {IsActive = true});

            MockLookupTableManager.Setup(_ => _.GetMarketSegmentName(It.IsAny<int>())).Returns("Market Segment Name");
            MockLookupTableManager.Setup(_ => _.GetReferenceTypeName(It.IsAny<int>())).Returns("Type Name");

            var result = _concessionManager.GetDeclinedConcessionsForUser(new User
            {
                Id = 1,
                UserRoles = new[] { new Role() { Name = "Requestor" } }
            });

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that GetUserConcessions that's a requestor executes positive
        /// </summary>
        [Fact]
        public void GetUserConcessions_Requestor_Executes_Positive()
        {
            MockLookupTableManager.Setup(_ => _.GetStatusId(It.IsAny<string>())).Returns(1);
            MockLookupTableManager.Setup(_ => _.GetSubStatusId(It.IsAny<string>())).Returns(1);

            MockConcessionRepository
                .Setup(_ => _.ReadByRequestorIdStatusIdSubStatusIdIsActive(It.IsAny<int>(), It.IsAny<int>(),
                    It.IsAny<int>(), It.IsAny<bool>())).Returns(new[] { new Concession() });

            MockConcessionRepository
                .Setup(_ => _.ReadByRequestorIdStatusIdIsActive(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>()))
                .Returns(new[] { new Concession() });

            MockConcessionRepository
                .Setup(_ => _.ReadByRequestorIdBetweenStartExpiryDateEndExpiryDateIsActive(It.IsAny<int>(), It.IsAny<DateTime>(),
                    It.IsAny<DateTime>(), It.IsAny<bool>())).Returns(new[] { new Concession() });

            MockLegalEntityRepository.Setup(_ => _.ReadByIdIsActive(It.IsAny<int>(), It.IsAny<bool>()))
                .Returns(new LegalEntity { IsActive = true });

            MockRiskGroupRepository.Setup(_ => _.ReadByIdIsActive(It.IsAny<int>(), It.IsAny<bool>()))
                .Returns(new RiskGroup { IsActive = true });

            MockLookupTableManager.Setup(_ => _.GetMarketSegmentName(It.IsAny<int>())).Returns("Market Segment Name");
            MockLookupTableManager.Setup(_ => _.GetReferenceTypeName(It.IsAny<int>())).Returns("Type Name");

            var result = _concessionManager.GetUserConcessions(new User
            {
                Id = 1,
                UserRoles = new[] { new Role() { Name = "Requestor" } }
            });

            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests that GetConcessionConditions executes positive
        /// </summary>
        [Fact]
        public void GetConcessionConditions_Executes_Positive()
        {
            MockConcessionConditionRepository.Setup(_ => _.ReadByConcessionId(It.IsAny<int>())).Returns(new[]
            {
                new ConcessionCondition
                {
                    PeriodId = 1,
                    PeriodTypeId = 1,
                    Id = 1,
                    IsActive = true,
                    Value = 1,
                    ConcessionId = 1,
                    Volume = 1,
                    ConditionTypeId = 1,
                    ConditionProductId = 1,
                    InterestRate = 1
                }
            });

            var conditionTypeName = "Test Condition Type Name";

            MockLookupTableManager.Setup(_ => _.GetConditionTypeName(It.IsAny<int>()))
                .Returns(conditionTypeName);

            var productTypeName = "Test Product Type Name";

            MockLookupTableManager.Setup(_ => _.GetProductTypeName(It.IsAny<int>())).Returns(productTypeName);

            var periodTypeName = "Test Period Type Name";

            MockLookupTableManager.Setup(_ => _.GetPeriodTypeName(It.IsAny<int>())).Returns(periodTypeName);

            var periodName = "Test Period Name";

            MockLookupTableManager.Setup(_ => _.GetPeriodName(It.IsAny<int>())).Returns(periodName);

            var result = _concessionManager.GetConcessionConditions(1);

            Assert.NotNull(result);
            Assert.NotEmpty(result);

            foreach (var record in result)
            {
                Assert.Equal(record.ConditionType, conditionTypeName);
                Assert.Equal(record.ProductType, productTypeName);
                Assert.Equal(record.PeriodType, periodTypeName);
                Assert.Equal(record.Period, periodName);
            }
        }

        /// <summary>
        /// Tests that GetClientAccounts executes positive
        /// </summary>
        [Fact]
        public void GetClientAccounts_Executes_Positive()
        {
            MockRiskGroupRepository.Setup(_ => _.ReadByRiskGroupNumberIsActive(It.IsAny<int>(), It.IsAny<bool>()))
                .Returns(new RiskGroup
                {
                    Id = 1, IsActive = true, RiskGroupNumber = 1, RiskGroupName = "Test Risk Group"
                });

            MockLegalEntityRepository.Setup(_ => _.ReadByRiskGroupIdIsActive(It.IsAny<int>(), It.IsAny<bool>()))
                .Returns(new[]
                {
                    new LegalEntity
                    {
                        Id = 1,
                        IsActive = true,
                        CustomerName = "Test Customer Name",
                        RiskGroupId = 1,
                        CustomerNumber = "Test Customer Number",
                        MarketSegmentId = 1
                    }
                });

            MockLegalEntityAccountRepository
                .Setup(_ => _.ReadByLegalEntityIdIsActive(It.IsAny<int>(), It.IsAny<bool>())).Returns(new[]
                {
                    new LegalEntityAccount
                    {
                        AccountNumber = "Test Account Number",
                        Id = 1,
                        IsActive = true,
                        LegalEntityId = 1
                    }
                });

            var result = _concessionManager.GetClientAccounts(1);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void GetActionedConcessionsForBCMUser_Executes_Positive()
        {
            MockConcessionRepository
            .Setup(_ => _.GetActionedByBCMUser(It.IsAny<int>())).Returns(new[] { new Concession() });
            var result = _concessionManager.GetActionedConcessionsForUser(new User {UserRoles = new List<Role> { new Role { Name = "BCM" } } });

          
            Assert.NotEmpty(result);

        }

        [Fact]
        public void GetActionedConcessionsForPCMUser_Executes_Positive()
        {
            MockConcessionRepository
            .Setup(_ => _.GetActionedByPCMUser(It.IsAny<int>())).Returns(new[] { new Concession() });
            var result = _concessionManager.GetActionedConcessionsForUser(new User { UserRoles = new List<Role> { new Role { Name = "PCM" } } });


            Assert.NotEmpty(result);

        }
        [Fact]
        public void GetActionedConcessionsForHOUser_Executes_Positive()
        {
            MockConcessionRepository
            .Setup(_ => _.GetActionedByHOUser(It.IsAny<int>())).Returns(new[] { new Concession() });
            var result = _concessionManager.GetActionedConcessionsForUser(new User { UserRoles = new List<Role> { new Role { Name = "Head Office" } } });


            Assert.NotEmpty(result);

        }
    }
}
