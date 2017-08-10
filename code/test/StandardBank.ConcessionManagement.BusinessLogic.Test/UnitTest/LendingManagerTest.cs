using Moq;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using Xunit;
using static StandardBank.ConcessionManagement.Test.Helpers.MockedDependencies;
using RiskGroup = StandardBank.ConcessionManagement.Model.UserInterface.Pricing.RiskGroup;

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
                MockLegalEntityRepository.Object, MockConcessionLendingRepository.Object);
        }
    }
}
