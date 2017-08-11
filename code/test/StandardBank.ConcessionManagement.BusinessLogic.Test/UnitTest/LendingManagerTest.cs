using Moq;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface.Lending;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;
using static StandardBank.ConcessionManagement.Test.Helpers.MockedDependencies;

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
            _lendingManager = new LendingManager(MockPricingManager.Object, MockConcessionManager.Object,
                MockLegalEntityRepository.Object, MockConcessionLendingRepository.Object, InstantiatedDependencies.Mapper);
        }

        /// <summary>
        /// Tests that CreateConcessionLending executes positive
        /// </summary>
        [Fact]
        public void CreateConcessionLending_Executes_Positive()
        {
            MockConcessionLendingRepository.Setup(_ => _.Create(It.IsAny<ConcessionLending>()))
                .Returns(new ConcessionLending());

            var result = _lendingManager.CreateConcessionLending(new LendingConcessionDetail(), new Model.UserInterface.Concession());

            Assert.NotNull(result);
        }
    }
}
