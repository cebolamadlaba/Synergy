using System;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using Xunit;

namespace StandardBank.ConcessionManagement.Repository.Test.Integration
{
    /// <summary>
    /// FinancialTransactional repository tests
    /// </summary>
    public class FinancialTransactionalRepositoryTest
    {
        /// <summary>
        /// Tests that Create executes positive.
        /// </summary>
        [Fact]
        public void Create_Executes_Positive()
        {
            var model = new FinancialTransactional
            {
                RiskGroupId = DataHelper.GetRiskGroupId(),
                TotalNumberOfAccounts = 6797,
                AverageAccountManagementFee = 1226,
                AverageMinimumMonthlyFee = 2378,
                TotalChequeIssuingVolumes = 3243,
                TotalChequeDepositVolumes = 4441,
                TotalChequeEncashmentVolumes = 4095,
                TotalChequeEncashmentValues = 478,
                TotalCashWithdrawalVolumes = 5462,
                TotalCashWithdrawalValues = 2749,
                AverageChequeIssuingValue = 6623,
                AverageChequeIssuingPrice = 5114,
                AverageChequeDepositValue = 8129,
                AverageChequeDepositPrice = 1770,
                AverageChequeEncashmentPrice = 7428,
                AverageCashWithdrawalPrice = 5713
            };

            var result = InstantiatedDependencies.FinancialTransactionalRepository.Create(model);

            Assert.NotNull(result);
            Assert.NotEqual(result.Id, 0);
        }

        /// <summary>
        /// Tests that ReadById executes positive.
        /// </summary>
        [Fact]
        public void ReadById_Executes_Positive()
        {
            var results = InstantiatedDependencies.FinancialTransactionalRepository.ReadAll();
            var id = results.First().Id;
            var result = InstantiatedDependencies.FinancialTransactionalRepository.ReadById(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        /// <summary>
        /// Tests that ReadByRiskGroupId executes positive
        /// </summary>
        [Fact]
        public void ReadByRiskGroupId_Executes_Positive()
        {
            var results = InstantiatedDependencies.FinancialTransactionalRepository.ReadAll();
            var riskGroupId = results.First().RiskGroupId;
            var result = InstantiatedDependencies.FinancialTransactionalRepository.ReadByRiskGroupId(riskGroupId);

            Assert.NotNull(result);
            Assert.NotEmpty(result);

            foreach (var record in result)
                Assert.Equal(record.RiskGroupId, riskGroupId);
        }

        /// <summary>
        /// Tests that ReadAll executes positive.
        /// </summary>
        [Fact]
        public void ReadAll_Executes_Positive()
        {
            var result = InstantiatedDependencies.FinancialTransactionalRepository.ReadAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Tests that Update executes positive.
        /// </summary>
        [Fact]
        public void Update_Executes_Positive()
        {
            var results = InstantiatedDependencies.FinancialTransactionalRepository.ReadAll();
            var id = results.First().Id;
            var model = InstantiatedDependencies.FinancialTransactionalRepository.ReadById(id);

            model.RiskGroupId = DataHelper.GetAlternateRiskGroupId(model.RiskGroupId);
            model.TotalNumberOfAccounts = model.TotalNumberOfAccounts + 100;
            model.AverageAccountManagementFee = model.AverageAccountManagementFee + 100;
            model.AverageMinimumMonthlyFee = model.AverageMinimumMonthlyFee + 100;
            model.TotalChequeIssuingVolumes = model.TotalChequeIssuingVolumes + 100;
            model.TotalChequeDepositVolumes = model.TotalChequeDepositVolumes + 100;
            model.TotalChequeEncashmentVolumes = model.TotalChequeEncashmentVolumes + 100;
            model.TotalChequeEncashmentValues = model.TotalChequeEncashmentValues + 100;
            model.TotalCashWithdrawalVolumes = model.TotalCashWithdrawalVolumes + 100;
            model.TotalCashWithdrawalValues = model.TotalCashWithdrawalValues + 100;
            model.AverageChequeIssuingValue = model.AverageChequeIssuingValue + 100;
            model.AverageChequeIssuingPrice = model.AverageChequeIssuingPrice + 100;
            model.AverageChequeDepositValue = model.AverageChequeDepositValue + 100;
            model.AverageChequeDepositPrice = model.AverageChequeDepositPrice + 100;
            model.AverageChequeEncashmentPrice = model.AverageChequeEncashmentPrice + 100;
            model.AverageCashWithdrawalPrice = model.AverageCashWithdrawalPrice + 100;

            InstantiatedDependencies.FinancialTransactionalRepository.Update(model);

            var updatedModel = InstantiatedDependencies.FinancialTransactionalRepository.ReadById(id);

            Assert.NotNull(updatedModel);
            Assert.Equal(updatedModel.Id, model.Id);
            Assert.Equal(updatedModel.RiskGroupId, model.RiskGroupId);
            Assert.Equal(updatedModel.TotalNumberOfAccounts, model.TotalNumberOfAccounts);
            Assert.Equal(updatedModel.AverageAccountManagementFee, model.AverageAccountManagementFee);
            Assert.Equal(updatedModel.AverageMinimumMonthlyFee, model.AverageMinimumMonthlyFee);
            Assert.Equal(updatedModel.TotalChequeIssuingVolumes, model.TotalChequeIssuingVolumes);
            Assert.Equal(updatedModel.TotalChequeDepositVolumes, model.TotalChequeDepositVolumes);
            Assert.Equal(updatedModel.TotalChequeEncashmentVolumes, model.TotalChequeEncashmentVolumes);
            Assert.Equal(updatedModel.TotalChequeEncashmentValues, model.TotalChequeEncashmentValues);
            Assert.Equal(updatedModel.TotalCashWithdrawalVolumes, model.TotalCashWithdrawalVolumes);
            Assert.Equal(updatedModel.TotalCashWithdrawalValues, model.TotalCashWithdrawalValues);
            Assert.Equal(updatedModel.AverageChequeIssuingValue, model.AverageChequeIssuingValue);
            Assert.Equal(updatedModel.AverageChequeIssuingPrice, model.AverageChequeIssuingPrice);
            Assert.Equal(updatedModel.AverageChequeDepositValue, model.AverageChequeDepositValue);
            Assert.Equal(updatedModel.AverageChequeDepositPrice, model.AverageChequeDepositPrice);
            Assert.Equal(updatedModel.AverageChequeEncashmentPrice, model.AverageChequeEncashmentPrice);
            Assert.Equal(updatedModel.AverageCashWithdrawalPrice, model.AverageCashWithdrawalPrice);
        }

        /// <summary>
        /// Tests that Delete executes positive.
        /// </summary>
        [Fact]
        public void Delete_Executes_Positive()
        {
            var model = new FinancialTransactional
            {
                RiskGroupId = DataHelper.GetRiskGroupId(),
                TotalNumberOfAccounts = 6797,
                AverageAccountManagementFee = 1226,
                AverageMinimumMonthlyFee = 2378,
                TotalChequeIssuingVolumes = 3243,
                TotalChequeDepositVolumes = 4441,
                TotalChequeEncashmentVolumes = 4095,
                TotalChequeEncashmentValues = 478,
                TotalCashWithdrawalVolumes = 5462,
                TotalCashWithdrawalValues = 2749,
                AverageChequeIssuingValue = 6623,
                AverageChequeIssuingPrice = 5114,
                AverageChequeDepositValue = 8129,
                AverageChequeDepositPrice = 1770,
                AverageChequeEncashmentPrice = 7428,
                AverageCashWithdrawalPrice = 5713
            };

            var temporaryEntity = InstantiatedDependencies.FinancialTransactionalRepository.Create(model);

            Assert.NotNull(temporaryEntity);
            Assert.NotEqual(temporaryEntity.Id, 0);

            InstantiatedDependencies.FinancialTransactionalRepository.Delete(temporaryEntity);

            var result = InstantiatedDependencies.FinancialTransactionalRepository.ReadById(temporaryEntity.Id);

            Assert.Null(result);
        }
    }
}
