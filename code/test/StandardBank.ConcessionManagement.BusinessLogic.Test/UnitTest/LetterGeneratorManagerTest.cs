using System;
using System.Text;
using Moq;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.Common;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic.LetterGenerator;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface.Cash;
using StandardBank.ConcessionManagement.Model.UserInterface.Lending;
using StandardBank.ConcessionManagement.Model.UserInterface.Transactional;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;
using Concession = StandardBank.ConcessionManagement.Model.UserInterface.Concession;
using ConcessionCondition = StandardBank.ConcessionManagement.Model.UserInterface.ConcessionCondition;
using User = StandardBank.ConcessionManagement.Model.UserInterface.User;

namespace StandardBank.ConcessionManagement.BusinessLogic.Test.UnitTest
{
    /// <summary>
    /// Letter generator manager test
    /// </summary>
    public class LetterGeneratorManagerTest
    {
        /// <summary>
        /// Tests that GenerateLetters for lending executes positive.
        /// </summary>
        [Fact]
        public void GenerateLetters_Lending_Executes_Positive()
        {
            var fileUtiltity = new Mock<IFileUtiltity>();
            var concessionManager = new Mock<IConcessionManager>();
            var pdfUtility = new Mock<IPdfUtility>();
            var userManager = new Mock<IUserManager>();
            var lendingManager = new Mock<ILendingManager>();
            var legalEntityRepository = new Mock<ILegalEntityRepository>();
            var cashManager = new Mock<ICashManager>();
            var razorRenderer = new Mock<IRazorRenderer>();
            var transactionalManager = new Mock<ITransactionalManager>();

            var letterGeneratorManager = new LetterGeneratorManager(InstantiatedDependencies.ConfigurationData,
                fileUtiltity.Object, concessionManager.Object, pdfUtility.Object, userManager.Object,
                lendingManager.Object, legalEntityRepository.Object, cashManager.Object,
                razorRenderer.Object, transactionalManager.Object);

            var concession = new Concession
            {
                ConcessionType = Constants.ConcessionType.Lending,
                RiskGroupNumber = 123456
            };

            concessionManager.Setup(_ => _.GetConcessionForConcessionReferenceId(It.IsAny<string>()))
                .Returns(concession);

            userManager.Setup(_ => _.GetUser(It.IsAny<int?>())).Returns(new User());

            lendingManager.Setup(_ => _.GetLendingConcession(It.IsAny<string>(), It.IsAny<User>()))
                .Returns(new LendingConcession
                {
                    Concession = concession,
                    LendingConcessionDetails = new[]
                    {
                        new LendingConcessionDetail
                        {
                            ProductType = Constants.Lending.ProductType.Overdraft,
                            ApprovedMap = 123.23m,
                            MarginAgainstPrime = 12343.23m,
                            InitiationFee = 3244.23m,
                            ReviewFee = 234234.23m,
                            LegalEntityId = 1,
                            DateApproved = DateTime.Now.AddDays(-100),
                            ExpiryDate = DateTime.Now.AddDays(100)
                        },
                        new LendingConcessionDetail
                        {
                            ProductType = "MTL (Medium Term Loan)",
                            InitiationFee = 23423.34m,
                            MarginAgainstPrime = 2233.23m,
                            LegalEntityId = 1,
                            DateApproved = DateTime.Now.AddDays(-100),
                            ExpiryDate = DateTime.Now.AddDays(100)
                        }
                    },
                    ConcessionConditions = new[] {new ConcessionCondition()},
                    CurrentUser = new User()
                });

            legalEntityRepository.Setup(_ => _.ReadById(It.IsAny<int>())).Returns(new LegalEntity());

            concessionManager.Setup(_ => _.GetConcessionConditions(It.IsAny<int>()))
                .Returns(new[]
                {
                    new ConcessionCondition {ConditionValue = 123213.12m, Period = "Period", PeriodType = "PeriodType"}
                });

            fileUtiltity.Setup(_ => _.ReadFileText(It.IsAny<string>(), It.IsAny<bool>()))
                .Returns("<html><body><h1>Test</h1><p>@Model.RiskGroupNumber</p></body></html>");

            pdfUtility.Setup(_ => _.GeneratePdfFromHtml(It.IsAny<string>()))
                .Returns(Encoding.ASCII.GetBytes("Test"));

            razorRenderer.Setup(_ => _.Parse(It.IsAny<string>(), It.IsAny<ConcessionLetter>(), It.IsAny<bool>()))
                .Returns($"<html><body><h1>Test</h1><p>{concession.RiskGroupNumber}</p></body></html>");

            var result = letterGeneratorManager.GenerateLetters("L0001");

            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests that GenerateLetters for cash executes positive.
        /// </summary>
        [Fact]
        public void GenerateLetters_Cash_Executes_Positive()
        {
            var fileUtiltity = new Mock<IFileUtiltity>();
            var concessionManager = new Mock<IConcessionManager>();
            var pdfUtility = new Mock<IPdfUtility>();
            var userManager = new Mock<IUserManager>();
            var lendingManager = new Mock<ILendingManager>();
            var legalEntityRepository = new Mock<ILegalEntityRepository>();
            var cashManager = new Mock<ICashManager>();
            var razorRenderer = new Mock<IRazorRenderer>();
            var transactionalManager = new Mock<ITransactionalManager>();

            var letterGeneratorManager = new LetterGeneratorManager(InstantiatedDependencies.ConfigurationData,
                fileUtiltity.Object, concessionManager.Object, pdfUtility.Object, userManager.Object,
                lendingManager.Object, legalEntityRepository.Object, cashManager.Object,
                razorRenderer.Object, transactionalManager.Object);

            var concession = new Concession
            {
                ConcessionType = Constants.ConcessionType.Cash,
                RiskGroupNumber = 123456
            };

            concessionManager.Setup(_ => _.GetConcessionForConcessionReferenceId(It.IsAny<string>()))
                .Returns(concession);

            userManager.Setup(_ => _.GetUser(It.IsAny<int?>())).Returns(new User());

            cashManager.Setup(_ => _.GetCashConcession(It.IsAny<string>(), It.IsAny<User>())).Returns(
                new CashConcession
                {
                    Concession = concession,
                    CashConcessionDetails = new[]
                    {
                        new CashConcessionDetail
                        {
                            BaseRate = 123.23m,
                            AdValorem = 1.05m,
                            LegalEntityId = 1,
                            DateApproved = DateTime.Now.AddDays(-100),
                            ExpiryDate = DateTime.Now.AddDays(100)
                        }
                    },
                    ConcessionConditions = new[] {new ConcessionCondition()},
                    CurrentUser = new User()
                });

            legalEntityRepository.Setup(_ => _.ReadById(It.IsAny<int>())).Returns(new LegalEntity());

            concessionManager.Setup(_ => _.GetConcessionConditions(It.IsAny<int>()))
                .Returns(new[]
                {
                    new ConcessionCondition {ConditionValue = 123213.12m, Period = "Period", PeriodType = "PeriodType"}
                });

            fileUtiltity.Setup(_ => _.ReadFileText(It.IsAny<string>(), It.IsAny<bool>()))
                .Returns("<html><body><h1>Test</h1><p>@Model.RiskGroupNumber</p></body></html>");

            pdfUtility.Setup(_ => _.GeneratePdfFromHtml(It.IsAny<string>()))
                .Returns(Encoding.ASCII.GetBytes("Test"));

            razorRenderer.Setup(_ => _.Parse(It.IsAny<string>(), It.IsAny<ConcessionLetter>(), It.IsAny<bool>()))
                .Returns($"<html><body><h1>Test</h1><p>{concession.RiskGroupNumber}</p></body></html>");

            var result = letterGeneratorManager.GenerateLetters("C0001");

            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests that GenerateLetters for transactional executes positive.
        /// </summary>
        [Fact]
        public void GenerateLetters_Transactional_Executes_Positive()
        {
            var fileUtiltity = new Mock<IFileUtiltity>();
            var concessionManager = new Mock<IConcessionManager>();
            var pdfUtility = new Mock<IPdfUtility>();
            var userManager = new Mock<IUserManager>();
            var lendingManager = new Mock<ILendingManager>();
            var legalEntityRepository = new Mock<ILegalEntityRepository>();
            var cashManager = new Mock<ICashManager>();
            var razorRenderer = new Mock<IRazorRenderer>();
            var transactionalManager = new Mock<ITransactionalManager>();

            var letterGeneratorManager = new LetterGeneratorManager(InstantiatedDependencies.ConfigurationData,
                fileUtiltity.Object, concessionManager.Object, pdfUtility.Object, userManager.Object,
                lendingManager.Object, legalEntityRepository.Object, cashManager.Object,
                razorRenderer.Object, transactionalManager.Object);

            var concession = new Concession
            {
                ConcessionType = Constants.ConcessionType.Transactional,
                RiskGroupNumber = 123456
            };

            concessionManager.Setup(_ => _.GetConcessionForConcessionReferenceId(It.IsAny<string>()))
                .Returns(concession);

            userManager.Setup(_ => _.GetUser(It.IsAny<int?>())).Returns(new User());

            transactionalManager.Setup(_ => _.GetTransactionalConcession(It.IsAny<string>(), It.IsAny<User>())).Returns(
                new TransactionalConcession
                {
                    Concession = concession,
                    TransactionalConcessionDetails = new[]
                    {
                        new TransactionalConcessionDetail
                        {
                            Fee = 123.23m,
                            AdValorem = 1.05m,
                            LegalEntityId = 1,
                            DateApproved = DateTime.Now.AddDays(-100),
                            ExpiryDate = DateTime.Now.AddDays(100)
                        }
                    },
                    ConcessionConditions = new[] {new ConcessionCondition()},
                    CurrentUser = new User()
                });

            legalEntityRepository.Setup(_ => _.ReadById(It.IsAny<int>())).Returns(new LegalEntity());

            concessionManager.Setup(_ => _.GetConcessionConditions(It.IsAny<int>()))
                .Returns(new[]
                {
                    new ConcessionCondition {ConditionValue = 123213.12m, Period = "Period", PeriodType = "PeriodType"}
                });

            fileUtiltity.Setup(_ => _.ReadFileText(It.IsAny<string>(), It.IsAny<bool>()))
                .Returns("<html><body><h1>Test</h1><p>@Model.RiskGroupNumber</p></body></html>");

            pdfUtility.Setup(_ => _.GeneratePdfFromHtml(It.IsAny<string>()))
                .Returns(Encoding.ASCII.GetBytes("Test"));

            razorRenderer.Setup(_ => _.Parse(It.IsAny<string>(), It.IsAny<ConcessionLetter>(), It.IsAny<bool>()))
                .Returns($"<html><body><h1>Test</h1><p>{concession.RiskGroupNumber}</p></body></html>");

            var result = letterGeneratorManager.GenerateLetters("T0001");

            Assert.NotNull(result);
        }
    }
}