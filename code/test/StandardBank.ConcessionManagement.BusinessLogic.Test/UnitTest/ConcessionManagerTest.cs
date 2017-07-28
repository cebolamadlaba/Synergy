﻿using System;
using Moq;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;
using Concession = StandardBank.ConcessionManagement.Model.Repository.Concession;
using Role = StandardBank.ConcessionManagement.Model.UserInterface.Role;
using User = StandardBank.ConcessionManagement.Model.UserInterface.User;
using static StandardBank.ConcessionManagement.Test.Helpers.MockedDependencies;

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
                InstantiatedDependencies.CacheManager, MockConcessionAccountRepository.Object);
        }

        /// <summary>
        /// Tests that GetPendingConcessionsForUser that's a requestor executes positive.
        /// </summary>
        [Fact]
        public void GetPendingConcessionsForUser_Requestor_Executes_Positive()
        {
            MockLookupTableManager.Setup(_ => _.GetStatusId(It.IsAny<string>())).Returns(1);
            MockLookupTableManager.Setup(_ => _.GetSubStatusId(It.IsAny<string>())).Returns(1);

            MockConcessionRepository
                .Setup(_ => _.ReadByRequestorIdStatusIdSubStatusIdIsActive(It.IsAny<int>(), It.IsAny<int>(),
                    It.IsAny<int>(), It.IsAny<bool>())).Returns(new[] {new Concession()});

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
        /// Tests that GetConcessionsForLegalEntityIdAndConcessionType executes positive
        /// </summary>
        [Fact]
        public void GetConcessionsForLegalEntityIdAndConcessionType_Executes_Positive()
        {
            MockLookupTableManager.Setup(_ => _.GetConcessionTypeId(It.IsAny<string>())).Returns(1);

            MockConcessionRepository
                .Setup(_ => _.ReadByLegalEntityIdConcessionTypeIdIsActive(It.IsAny<int>(), It.IsAny<int>(),
                    It.IsAny<bool>())).Returns(new[] {new Concession()});

            MockLegalEntityRepository.Setup(_ => _.ReadByIdIsActive(It.IsAny<int>(), It.IsAny<bool>()))
                .Returns(new LegalEntity { IsActive = true });

            MockRiskGroupRepository.Setup(_ => _.ReadByIdIsActive(It.IsAny<int>(), It.IsAny<bool>()))
                .Returns(new RiskGroup { IsActive = true });

            MockConcessionAccountRepository.Setup(_ => _.ReadByConcessionIdIsActive(It.IsAny<int>(), It.IsAny<bool>()))
                .Returns(new ConcessionAccount());

            var result = _concessionManager.GetConcessionsForLegalEntityIdAndConcessionType(1, "Unit Test");

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
    }
}
