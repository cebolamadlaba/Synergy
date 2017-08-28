using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Test.Helpers;
using static StandardBank.ConcessionManagement.Test.Helpers.MockedDependencies;
using Xunit;

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
            _transactionalManager = new TransactionalManager(MockPricingManager.Object, MockConcessionManager.Object,
                MockConcessionTransactionalRepository.Object, MockLegalEntityRepository.Object,
                MockLegalEntityAccountRepository.Object, InstantiatedDependencies.Mapper, MockLookupTableManager.Object);
        }

        /// <summary>
        /// Tests that GetCashConcessionsForRiskGroupNumber executes positive.
        /// </summary>
        //[Fact] //TODO: Create and make this test work
        public void GetCashConcessionsForRiskGroupNumber_Executes_Positive()
        {
            
        }
    }
}
