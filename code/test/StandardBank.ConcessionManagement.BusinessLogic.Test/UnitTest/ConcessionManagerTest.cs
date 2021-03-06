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
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface;
using ConcessionComment = StandardBank.ConcessionManagement.Model.Repository.ConcessionComment;
using ConcessionCondition = StandardBank.ConcessionManagement.Model.Repository.ConcessionCondition;
using ConcessionRelationship = StandardBank.ConcessionManagement.Model.Repository.ConcessionRelationship;

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
                MockRiskGroupRepository.Object, InstantiatedDependencies.Mapper,
                MockConcessionConditionRepository.Object, MockConcessionCommentRepository.Object,
                MockConcessionRelationshipRepository.Object, MockAuditRepository.Object, MockUserManager.Object,
                MockConcessionInboxViewRepository.Object, MockConcessionDetailRepository.Object,
                MockConcessionConditionViewRepository.Object, MockMiscPerformanceRepository.Object, MockCentreRepository.Object, null, null,
                MockAENumberUserManager.Object, MockSUbRoleRepository.Object);
        }

        /// <summary>
        /// Tests that GetPendingConcessionsForUser that's a requestor executes positive.
        /// </summary>
        [Fact]
        public void GetPendingConcessionsForUser_Requestor_Executes_Positive()
        {
            MockLookupTableManager.Setup(_ => _.GetStatusId(It.IsAny<string>())).Returns(1);

            MockLegalEntityRepository.Setup(_ => _.ReadByIdIsActive(It.IsAny<int>(), It.IsAny<bool>()))
                .Returns(new Model.Repository.LegalEntity { IsActive = true });

            MockRiskGroupRepository.Setup(_ => _.ReadByIdIsActive(It.IsAny<int>(), It.IsAny<bool>()))
                .Returns(new Model.Repository.RiskGroup { IsActive = true });

            MockLookupTableManager.Setup(_ => _.GetMarketSegmentName(It.IsAny<int>())).Returns("Market Segment Name");
            MockLookupTableManager.Setup(_ => _.GetReferenceTypeName(It.IsAny<int>())).Returns("Type Name");

            var result = _concessionManager.GetPendingConcessionsForUser(new User
            {
                Id = 1,
                UserRoles = new[] { new Role() { Name = Constants.Roles.Requestor } }
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
            MockConcessionInboxViewRepository
                .Setup(_ => _.ReadByRequestorIdBetweenStartExpiryDateEndExpiryDateStatusIdsIsActive(It.IsAny<int>(),
                    It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<IEnumerable<int>>(), It.IsAny<bool>()))
                .Returns(new[] { new ConcessionInboxView() });

            var result = _concessionManager.GetDueForExpiryConcessionsForUser(new User
            {
                Id = 1,
                UserRoles = new[] { new Role() { Name = Constants.Roles.Requestor } }
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
            MockConcessionInboxViewRepository
                .Setup(_ => _.ReadByRequestorIdBetweenStartExpiryDateEndExpiryDateStatusIdsIsActive(It.IsAny<int>(),
                    It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<IEnumerable<int>>(), It.IsAny<bool>()))
                .Returns(new[] { new ConcessionInboxView() });

            var result = _concessionManager.GetExpiredConcessionsForUser(new User
            {
                Id = 1,
                UserRoles = new[] { new Role() { Name = Constants.Roles.Requestor } }
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
            MockConcessionInboxViewRepository
                .Setup(_ => _.ReadByRequestorIdStatusIdsIsMismatchedIsActive(It.IsAny<int>(),
                    It.IsAny<IEnumerable<int>>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .Returns(new[] { new ConcessionInboxView() });

            var result = _concessionManager.GetMismatchedConcessionsForUser(new User
            {
                Id = 1,
                UserRoles = new[] { new Role() { Name = Constants.Roles.Requestor } }
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
            MockConcessionInboxViewRepository
                .Setup(_ => _.ReadByRequestorIdStatusIdsIsActive(It.IsAny<int>(), It.IsAny<IEnumerable<int>>(),
                    It.IsAny<bool>())).Returns(new[] { new ConcessionInboxView() });

            var result = _concessionManager.GetDeclinedConcessionsForUser(new User
            {
                Id = 1,
                UserRoles = new[] { new Role() { Name = Constants.Roles.Requestor } }
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
            MockLegalEntityRepository.Setup(_ => _.ReadByIdIsActive(It.IsAny<int>(), It.IsAny<bool>()))
                .Returns(new Model.Repository.LegalEntity { IsActive = true });

            MockRiskGroupRepository.Setup(_ => _.ReadByIdIsActive(It.IsAny<int>(), It.IsAny<bool>()))
                .Returns(new Model.Repository.RiskGroup { IsActive = true });

            MockLookupTableManager.Setup(_ => _.GetMarketSegmentName(It.IsAny<int>())).Returns("Market Segment Name");
            MockLookupTableManager.Setup(_ => _.GetReferenceTypeName(It.IsAny<int>())).Returns("Type Name");

            var result = _concessionManager.GetUserConcessions(new User
            {
                Id = 1,
                UserRoles = new[] { new Role() { Name = Constants.Roles.Requestor } }
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

            var conditionProductName = "Test Product Type Name";

            MockLookupTableManager.Setup(_ => _.GetConditionProductName(It.IsAny<int>())).Returns(conditionProductName);

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
                Assert.Equal(record.ProductType, conditionProductName);
                Assert.Equal(record.PeriodType, periodTypeName);
                Assert.Equal(record.Period, periodName);
            }
        }

        /// <summary>
        /// Tests that GetClientAccounts when CanRequest is true executes positive
        /// </summary>
        [Fact]
        public void GetClientAccounts_CanRequest_True_Executes_Positive()
        {
            MockMiscPerformanceRepository.Setup(_ => _.GetClientAccounts(It.IsAny<int>(), It.IsAny<int?>(), ""))
                .Returns(new[] { new ClientAccount() });

            var result = _concessionManager.GetClientAccounts(1, new User { CanRequest = true }, "");

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that GetClientAccounts when CanRequest is false executes positive
        /// </summary>
        [Fact]
        public void GetClientAccounts_CanRequest_False_Executes_Positive()
        {
            MockMiscPerformanceRepository.Setup(_ => _.GetClientAccounts(It.IsAny<int>(), It.IsAny<int?>(), ""))
                .Returns(new[] { new ClientAccount() });

            var result = _concessionManager.GetClientAccounts(1, new User { CanRequest = false }, "");

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void GetActionedConcessionsForBCMUser_Executes_Positive()
        {
            MockConcessionInboxViewRepository.Setup(_ => _.ReadByBcmUserIdIsActive(It.IsAny<int>(), It.IsAny<bool>()))
                .Returns(new[] { new ConcessionInboxView() });

            var result = _concessionManager.GetActionedConcessionsForUser(new User
            {
                UserRoles = new List<Role> { new Role { Name = Constants.Roles.BCM } }
            });

            Assert.NotEmpty(result);
        }

        [Fact]
        public void GetActionedConcessionsForPCMUser_Executes_Positive()
        {
            MockConcessionInboxViewRepository.Setup(_ => _.ReadByPcmUserIdIsActive(It.IsAny<int>(), It.IsAny<bool>()))
                .Returns(new[] { new ConcessionInboxView() });

            var result = _concessionManager.GetActionedConcessionsForUser(new User
            {
                UserRoles = new List<Role> { new Role { Name = Constants.Roles.PCM } }
            });

            Assert.NotEmpty(result);
        }

        [Fact]
        public void GetActionedConcessionsForHOUser_Executes_Positive()
        {
            MockConcessionInboxViewRepository.Setup(_ => _.ReadByHoUserIdIsActive(It.IsAny<int>(), It.IsAny<bool>()))
                .Returns(new[] { new ConcessionInboxView() });

            var result =
                _concessionManager.GetActionedConcessionsForUser(new User
                {
                    UserRoles = new List<Role> { new Role { Name = Constants.Roles.HeadOffice } }
                });

            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that GetConcessionsForRiskGroup executes positive
        /// </summary>
        [Fact]
        public void GetConcessionsForRiskGroup_Executes_Positive()
        {
            MockLookupTableManager.Setup(_ => _.GetConcessionTypeId(It.IsAny<string>())).Returns(1);

            MockConcessionAccountRepository.Setup(_ => _.ReadByConcessionIdIsActive(It.IsAny<int>(), It.IsAny<bool>()))
                .Returns(new ConcessionAccount());

            MockRiskGroupRepository.Setup(_ => _.ReadById(It.IsAny<int>())).Returns(new Model.Repository.RiskGroup
            {
                Id = 1,
                IsActive = true,
                RiskGroupNumber = 10,
                RiskGroupName = "Test Risk Group"
            });

            MockUserManager.Setup(_ => _.GetUser(It.IsAny<int>())).Returns(new User
            {
                FirstName = "Test",
                Surname = "User",
                ANumber = "A123",
                SelectedCentre = new Model.UserInterface.Centre { Name = "Test Centre" }
            });

            MockConcessionRepository
                .Setup(_ => _.ReadByRiskGroupIdConcessionTypeIdIsActive(It.IsAny<int>(), It.IsAny<int>(),
                    It.IsAny<bool>())).Returns(new[] { new Concession() });

            var result = _concessionManager.GetConcessionsForRiskGroup(1, "Test", null);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that CreateConcessionCondition executes positive
        /// </summary>
        [Fact]
        public void CreateConcessionCondition_Executes_Positive()
        {
            MockConcessionConditionRepository.Setup(_ => _.Create(It.IsAny<ConcessionCondition>()))
                .Returns(new ConcessionCondition());

            var result = _concessionManager.CreateConcessionCondition(new Model.UserInterface.ConcessionCondition(),
                new Model.UserInterface.Concession());

            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests that GetConcessionForConcessionReferenceId executes positive
        /// </summary>
        [Fact]
        public void GetConcessionForConcessionReferenceId_Executes_Positive()
        {
            MockConcessionRepository.Setup(_ => _.ReadByConcessionRefIsActive(It.IsAny<string>(), It.IsAny<bool>()))
                .Returns(new[] { new Concession { IsActive = true } });

            MockRiskGroupRepository.Setup(_ => _.ReadById(It.IsAny<int>())).Returns(new Model.Repository.RiskGroup
            {
                Id = 1,
                IsActive = true,
                RiskGroupNumber = 10,
                RiskGroupName = "Test Risk Group"
            });

            MockUserManager.Setup(_ => _.GetUser(It.IsAny<int>())).Returns(new User
            {
                FirstName = "Test",
                Surname = "User",
                ANumber = "A123",
                SelectedCentre = new Model.UserInterface.Centre { Name = "Test Centre" }
            });

            var result = _concessionManager.GetConcessionForConcessionReferenceId("L001", null);

            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests that CreateConcession executes positive.
        /// </summary>
        [Fact]
        public void CreateConcession_Executes_Positive()
        {
            MockLookupTableManager.Setup(_ => _.GetReferenceTypeId(It.IsAny<string>())).Returns(1);
            MockLookupTableManager.Setup(_ => _.GetConcessionTypeId(It.IsAny<string>())).Returns(2);
            MockLookupTableManager.Setup(_ => _.GetStatusId(It.IsAny<string>())).Returns(3);
            MockLookupTableManager.Setup(_ => _.GetSubStatusId(It.IsAny<string>())).Returns(4);
            MockCentreRepository.Setup(_ => _.ReadById(It.IsAny<int>())).Returns(
                new Model.Repository.Centre() { Id = 1, IsActive = true, RegionId = 1, CentreName = "Test Centre" });

            MockConcessionRepository.Setup(_ => _.Create(It.IsAny<Concession>())).Returns(new Concession() { Id = 100 });

            var result =
                _concessionManager.CreateConcession(new Model.UserInterface.Concession { ConcessionType = "UnitTest" },
                    (new FakeSiteHelper()).LoggedInUser(null));

            Assert.NotNull(result);
            Assert.True(!string.IsNullOrWhiteSpace(result.ConcessionRef));
            Assert.Equal(result.ConcessionRef, "U000000000100");
        }

        /// <summary>
        /// Tests that DeactivateConcession executes positive.
        /// </summary>
        [Fact]
        public void DeactivateConcession_Executes_Positive()
        {
            MockConcessionRepository.Setup(_ => _.ReadByConcessionRefIsActive(It.IsAny<string>(), It.IsAny<bool>()))
                .Returns(new[] { new Concession() });

            var result = _concessionManager.DeactivateConcession("U100", true, new User());

            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests that UpdateConcession executes positive.
        /// </summary>
        [Fact]
        public void UpdateConcession_Executes_Positive()
        {
            MockConcessionRepository.Setup(_ => _.ReadByConcessionRefIsActive(It.IsAny<string>(), It.IsAny<bool>()))
                .Returns(new[] { new Concession() });

            MockLookupTableManager.Setup(_ => _.GetReferenceTypeId(It.IsAny<string>())).Returns(1);
            MockLookupTableManager.Setup(_ => _.GetConcessionTypeId(It.IsAny<string>())).Returns(2);
            MockLookupTableManager.Setup(_ => _.GetStatusId(It.IsAny<string>())).Returns(3);
            MockLookupTableManager.Setup(_ => _.GetSubStatusId(It.IsAny<string>())).Returns(4);

            var result = _concessionManager.UpdateConcession(new Model.UserInterface.Concession(), new User());

            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests that DeleteConcessionCondition executes positive.
        /// </summary>
        [Fact]
        public void DeleteConcessionCondition_Executes_Positive()
        {
            MockConcessionConditionRepository.Setup(_ => _.ReadById(It.IsAny<int>()))
                .Returns(new ConcessionCondition());

            var result = _concessionManager.DeleteConcessionCondition(new Model.UserInterface.ConcessionCondition());

            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests that CreateConcessionComment executes positive.
        /// </summary>
        [Fact]
        public void CreateConcessionComment_Executes_Positive()
        {
            MockConcessionCommentRepository.Setup(_ => _.Create(It.IsAny<ConcessionComment>()))
                .Returns(new ConcessionComment());

            var result = _concessionManager.CreateConcessionComment(new ConcessionComment());

            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests that GetApprovedConcessionsForUser executes positive.
        /// </summary>
        [Fact]
        public void GetApprovedConcessionsForUser_Executes_Positive()
        {
            MockLookupTableManager.Setup(_ => _.GetStatusId(It.IsAny<string>())).Returns(1);

            MockConcessionInboxViewRepository
                .Setup(_ => _.ReadByRequestorIdStatusIdsIsActive(It.IsAny<int>(), It.IsAny<IEnumerable<int>>(),
                    It.IsAny<bool>())).Returns(new[] { new ConcessionInboxView() });

            var result = _concessionManager.GetApprovedConcessionsForUser(1, null);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that UpdateConcessionCondition executes positive.
        /// </summary>
        [Fact]
        public void UpdateConcessionCondition_Executes_Positive()
        {
            var result = _concessionManager.UpdateConcessionCondition(new Model.UserInterface.ConcessionCondition(),
                new Model.UserInterface.Concession());

            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests that CreateConcessionRelationship executes positive.
        /// </summary>
        [Fact]
        public void CreateConcessionRelationship_Executes_Positive()
        {
            MockLookupTableManager.Setup(_ => _.GetRelationshipId(It.IsAny<string>())).Returns(1);

            MockConcessionRelationshipRepository.Setup(_ => _.Create(It.IsAny<ConcessionRelationship>()))
                .Returns(new ConcessionRelationship());

            var result =
                _concessionManager.CreateConcessionRelationship(new Model.UserInterface.ConcessionRelationship());

            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests that ActivateConcession executes positive.
        /// </summary>
        [Fact]
        public void ActivateConcession_Executes_Positive()
        {
            MockConcessionRepository.Setup(_ => _.ReadByConcessionRefIsActive(It.IsAny<string>(), It.IsAny<bool>()))
                .Returns(new[] { new Concession() });

            var result = _concessionManager.ActivateConcession("U100", new User());

            Assert.NotNull(result);
        }

        [Fact]
        public void GetRagStatus_3Months_Withing21DaysReturnsGreen()
        {
            var status = _concessionManager.GetRagStatus(Constants.Period.ThreeMonths, AddWorkingDays(21));
            Assert.True(status == Constants.RagStatusResult.Green);
        }
        [Fact]
        public void GetRagStatus_3Months_Withing41DaysReturnsAmber()
        {
            var status = _concessionManager.GetRagStatus(Constants.Period.ThreeMonths, AddWorkingDays(41));
            Assert.True(status == Constants.RagStatusResult.Yellow);
        }
        [Fact]
        public void GetRagStatus_3Months_Withing42DaysReturnsRed()
        {
            var status = _concessionManager.GetRagStatus(Constants.Period.ThreeMonths, AddWorkingDays(42));
            Assert.True(status == Constants.RagStatusResult.Red);
        }
        [Fact]
        public void GetRagStatus_6Months_Withing42DaysReturnsGreen()
        {
            var status = _concessionManager.GetRagStatus(Constants.Period.SixMonths, AddWorkingDays(42));
            Assert.True(status == Constants.RagStatusResult.Green);
        }
        [Fact]
        public void GetRagStatus_6Months_Withing83DaysReturnsAmber()
        {
            var status = _concessionManager.GetRagStatus(Constants.Period.SixMonths, AddWorkingDays(83));
            Assert.True(status == Constants.RagStatusResult.Yellow);
        }
        [Fact]
        public void GetRagStatus_6Months_Withing84DaysReturnsRed()
        {
            var status = _concessionManager.GetRagStatus(Constants.Period.SixMonths, AddWorkingDays(84));
            Assert.True(status == Constants.RagStatusResult.Red);
        }
        [Fact]
        public void GetRagStatus_9Months_Withing63DaysReturnsGreen()
        {
            var status = _concessionManager.GetRagStatus(Constants.Period.NineMonths, AddWorkingDays(63));
            Assert.True(status == Constants.RagStatusResult.Green);
        }
        [Fact]
        public void GetRagStatus_9Months_Withing64DaysReturnsAmber()
        {
            var status = _concessionManager.GetRagStatus(Constants.Period.NineMonths, AddWorkingDays(64));
            Assert.True(status == Constants.RagStatusResult.Yellow);
        }
        [Fact]
        public void GetRagStatus_9Months_Withing126DaysReturnsRed()
        {
            var status = _concessionManager.GetRagStatus(Constants.Period.NineMonths, AddWorkingDays(126));
            Assert.True(status == Constants.RagStatusResult.Red);
        }
        [Fact]
        public void GetRagStatus_12Months_Withing84DaysReturnsGreen()
        {
            var status = _concessionManager.GetRagStatus(Constants.Period.TwelveMonths, AddWorkingDays(84));
            Assert.True(status == Constants.RagStatusResult.Green);
        }
        [Fact]
        public void GetRagStatus_12Months_Withing88DaysReturnsAmber()
        {
            var status = _concessionManager.GetRagStatus(Constants.Period.TwelveMonths, AddWorkingDays(88));
            Assert.True(status == Constants.RagStatusResult.Yellow);
        }
        [Fact]
        public void GetRagStatus_12Months_Withing168DaysReturnsRed()
        {
            var status = _concessionManager.GetRagStatus(Constants.Period.TwelveMonths, AddWorkingDays(168));
            Assert.True(status == Constants.RagStatusResult.Red);
        }
        private DateTime AddWorkingDays(int days)
        {
            DateTime returnDate = DateTime.Today;
            while (days > 0)
            {
                returnDate = returnDate.AddDays(-1);
                if (returnDate.DayOfWeek != DayOfWeek.Saturday && returnDate.DayOfWeek != DayOfWeek.Sunday)
                    days--;
            }

            return returnDate;
        }
    }
}

