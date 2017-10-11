using System;
using Moq;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using Xunit;
using static StandardBank.ConcessionManagement.Test.Helpers.MockedDependencies;

namespace StandardBank.ConcessionManagement.BusinessLogic.Test.UnitTest
{
    /// <summary>
    /// Rule manager tests
    /// </summary>
    public class RuleManagerTest
    {
        /// <summary>
        /// The rule manager
        /// </summary>
        private readonly IRuleManager _ruleManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleManagerTest"/> class.
        /// </summary>
        public RuleManagerTest()
        {
            _ruleManager = new RuleManager(MockConcessionRelationshipRepository.Object, MockLookupTableManager.Object);
        }

        /// <summary>
        /// Tests that UpdateBaseFieldsOnApproval for Lending executes positive.
        /// </summary>
        [Fact]
        public void UpdateBaseFieldsOnApproval_Lending_Executes_Positive()
        {
            MockConcessionRelationshipRepository.Setup(_ => _.ReadByChildConcessionId(It.IsAny<int>())).Returns(new[]
            {
                new ConcessionRelationship
                {
                    RelationshipId = 1
                }
            });

            MockLookupTableManager.Setup(_ => _.GetRelationshipId("Extension")).Returns(1);

            var concessionDetail = new ConcessionLending
            {
                ExpiryDate = DateTime.Now.Date,
                DateApproved = null
            };

            _ruleManager.UpdateBaseFieldsOnApproval(concessionDetail);

            Assert.True(concessionDetail.ExpiryDate.HasValue);
            Assert.True(concessionDetail.DateApproved.HasValue);
            Assert.Equal(concessionDetail.ExpiryDate.Value.Date, DateTime.Now.AddMonths(3).Date);
        }

        /// <summary>
        /// Tests that UpdateBaseFieldsOnApproval for Cash executes positive.
        /// </summary>
        [Fact]
        public void UpdateBaseFieldsOnApproval_Cash_Executes_Positive()
        {
            MockConcessionRelationshipRepository.Setup(_ => _.ReadByChildConcessionId(It.IsAny<int>())).Returns(new[]
            {
                new ConcessionRelationship
                {
                    RelationshipId = 1
                }
            });

            MockLookupTableManager.Setup(_ => _.GetRelationshipId("Extension")).Returns(1);

            var concessionDetail = new ConcessionCash
            {
                ExpiryDate = DateTime.Now.Date,
                DateApproved = null
            };

            _ruleManager.UpdateBaseFieldsOnApproval(concessionDetail);

            Assert.True(concessionDetail.ExpiryDate.HasValue);
            Assert.True(concessionDetail.DateApproved.HasValue);
            Assert.Equal(concessionDetail.ExpiryDate.Value.Date, DateTime.Now.AddMonths(3).Date);
        }

        /// <summary>
        /// Tests that UpdateBaseFieldsOnApproval for Transactional executes positive.
        /// </summary>
        [Fact]
        public void UpdateBaseFieldsOnApproval_Transactional_Executes_Positive()
        {
            MockConcessionRelationshipRepository.Setup(_ => _.ReadByChildConcessionId(It.IsAny<int>())).Returns(new[]
            {
                new ConcessionRelationship
                {
                    RelationshipId = 1
                }
            });

            MockLookupTableManager.Setup(_ => _.GetRelationshipId("Extension")).Returns(1);

            var concessionDetail = new ConcessionTransactional
            {
                ExpiryDate = DateTime.Now.Date,
                DateApproved = null
            };

            _ruleManager.UpdateBaseFieldsOnApproval(concessionDetail);

            Assert.True(concessionDetail.ExpiryDate.HasValue);
            Assert.True(concessionDetail.DateApproved.HasValue);
            Assert.Equal(concessionDetail.ExpiryDate.Value.Date, DateTime.Now.AddMonths(3).Date);
        }
    }
}
