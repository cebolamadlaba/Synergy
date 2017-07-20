using Moq;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;
using Role = StandardBank.ConcessionManagement.Model.UserInterface.Role;
using User = StandardBank.ConcessionManagement.Model.UserInterface.User;

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
            _concessionManager = new ConcessionManager(MockedDependencies.MockConcessionRepository.Object,
                MockedDependencies.MockLookupTableManager.Object, MockedDependencies.MockLegalEntityRepository.Object,
                MockedDependencies.MockRiskGroupRepository.Object);
        }

        /// <summary>
        /// Tests that GetPendingConcessionsForUser that's a requestor executes positive.
        /// </summary>
        [Fact]
        public void GetPendingConcessionsForUser_Requestor_Executes_Positive()
        {
            MockedDependencies.MockLookupTableManager.Setup(_ => _.GetStatusId(It.IsAny<string>())).Returns(1);
            MockedDependencies.MockLookupTableManager.Setup(_ => _.GetSubStatusId(It.IsAny<string>())).Returns(1);

            MockedDependencies.MockConcessionRepository
                .Setup(_ => _.ReadByRequestorIdStatusIdSubStatusIdIsActive(It.IsAny<int>(), It.IsAny<int>(),
                    It.IsAny<int>(), It.IsAny<bool>())).Returns(new[] {new Concession()});

            MockedDependencies.MockLegalEntityRepository.Setup(_ => _.ReadById(It.IsAny<int>()))
                .Returns(new LegalEntity());

            MockedDependencies.MockRiskGroupRepository.Setup(_ => _.ReadById(It.IsAny<int>())).Returns(new RiskGroup());

            MockedDependencies.MockLookupTableManager.Setup(_ => _.GetMarketSegmentName(It.IsAny<int>())).Returns("Market Segment Name");
            MockedDependencies.MockLookupTableManager.Setup(_ => _.GetReferenceTypeName(It.IsAny<int>())).Returns("Type Name");

            var result = _concessionManager.GetPendingConcessionsForUser(new User
            {
                Id = 1,
                UserRoles = new[] {new Role() {Name = "Requestor"}}
            });

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
    }
}
