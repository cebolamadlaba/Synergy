using System;
using Moq;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using Xunit;

namespace StandardBank.ConcessionManagement.BusinessLogic.Test.UnitTest
{
    /// <summary>
    /// Rule manager test
    /// </summary>
    public class RuleManagerTest
    {
        /// <summary>
        /// Tests that CalculateExpiryDate for Lending with no overdraft product executes positive.
        /// </summary>
        [Fact]
        public void CalculateExpiryDate_Lending_No_Overdraft_Executes_Positive()
        {
            var lookupTableManager = new Mock<ILookupTableManager>();
            var concessionRelationshipRepository = new Mock<IConcessionRelationshipRepository>();
            var concessionRepository = new Mock<IConcessionRepository>();
            var concessionLendingRepository = new Mock<IConcessionLendingRepository>();

            var ruleManager = new RuleManager(lookupTableManager.Object, concessionRelationshipRepository.Object,
                concessionRepository.Object, concessionLendingRepository.Object);

            lookupTableManager.Setup(_ => _.GetRelationshipId(It.IsAny<string>())).Returns(1);

            concessionLendingRepository.Setup(_ => _.ReadByConcessionId(It.IsAny<int>())).Returns(new[]
            {
                new ConcessionLending {ProductTypeId = 1, Term = 100},
                new ConcessionLending {ProductTypeId = 2, Term = 20},
                new ConcessionLending {ProductTypeId = 3, Term = 10},
                new ConcessionLending {ProductTypeId = 4, Term = 25},
                new ConcessionLending {ProductTypeId = 5, Term = 30},
                new ConcessionLending {ProductTypeId = 6, Term = 15}
            });

            lookupTableManager.Setup(_ => _.GetProductTypeName(It.IsAny<int>()))
                .Returns((int productId) => $"Test Product {productId}");

            var result = ruleManager.CalculateExpiryDate(1, "Lending");

            Assert.Equal(result.Date, DateTime.Now.AddMonths(10).Date);
        }

        /// <summary>
        /// Tests that CalculateExpiryDate for Lending with overdraft executes positive.
        /// </summary>
        [Fact]
        public void CalculateExpiryDate_Lending_With_Overdraft_Executes_Positive()
        {
            var lookupTableManager = new Mock<ILookupTableManager>();
            var concessionRelationshipRepository = new Mock<IConcessionRelationshipRepository>();
            var concessionRepository = new Mock<IConcessionRepository>();
            var concessionLendingRepository = new Mock<IConcessionLendingRepository>();

            var ruleManager = new RuleManager(lookupTableManager.Object, concessionRelationshipRepository.Object,
                concessionRepository.Object, concessionLendingRepository.Object);

            lookupTableManager.Setup(_ => _.GetRelationshipId(It.IsAny<string>())).Returns(1);

            concessionLendingRepository.Setup(_ => _.ReadByConcessionId(It.IsAny<int>())).Returns(new[]
            {
                new ConcessionLending {ProductTypeId = 1, Term = 100},
                new ConcessionLending {ProductTypeId = 2, Term = 20},
                new ConcessionLending {ProductTypeId = 3, Term = 15},
                new ConcessionLending {ProductTypeId = 4, Term = 25},
                new ConcessionLending {ProductTypeId = 5, Term = 30},
                new ConcessionLending {ProductTypeId = 6, Term = 35}
            });

            lookupTableManager.Setup(_ => _.GetProductTypeName(It.IsAny<int>()))
                .Returns((int productId) => $"Test Product {productId}");

            lookupTableManager.Setup(_ => _.GetProductTypeName(4))
                .Returns("Overdraft");

            var result = ruleManager.CalculateExpiryDate(1, "Lending");

            Assert.Equal(result.Date, DateTime.Now.AddMonths(12).Date);
        }

        /// <summary>
        /// Tests that CalculateExpiryDate for an extension executes positive.
        /// </summary>
        [Fact]
        public void CalculateExpiryDate_For_Extension_Executes_Positive()
        {
            var lookupTableManager = new Mock<ILookupTableManager>();
            var concessionRelationshipRepository = new Mock<IConcessionRelationshipRepository>();
            var concessionRepository = new Mock<IConcessionRepository>();
            var concessionLendingRepository = new Mock<IConcessionLendingRepository>();

            var ruleManager = new RuleManager(lookupTableManager.Object, concessionRelationshipRepository.Object,
                concessionRepository.Object, concessionLendingRepository.Object);

            var expiryDate = DateTime.Now.AddDays(50).Date;

            lookupTableManager.Setup(_ => _.GetRelationshipId(It.IsAny<string>())).Returns(1);

            concessionRelationshipRepository
                .Setup(_ => _.ReadByChildConcessionIdRelationshipIdRelationships(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new[] {new ConcessionRelationship {ParentConcessionId = 1}});

            concessionRepository.Setup(_ => _.ReadById(It.IsAny<int>()))
                .Returns(new Concession {IsActive = true, IsCurrent = true });

            var result = ruleManager.CalculateExpiryDate(1, "Lending");

            Assert.Equal(result.Date, expiryDate.AddMonths(3).Date);
        }
    }
}
