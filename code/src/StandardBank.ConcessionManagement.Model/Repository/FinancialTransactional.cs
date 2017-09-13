using System;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// FinancialTransactional entity
    /// </summary>
    public class FinancialTransactional
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        /// <value>
        /// The Id.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the RiskGroupId.
        /// </summary>
        /// <value>
        /// The RiskGroupId.
        /// </value>
        public int RiskGroupId { get; set; }

        /// <summary>
        /// Gets or sets the TotalNumberOfAccounts.
        /// </summary>
        /// <value>
        /// The TotalNumberOfAccounts.
        /// </value>
        public decimal TotalNumberOfAccounts { get; set; }

        /// <summary>
        /// Gets or sets the AverageAccountManagementFee.
        /// </summary>
        /// <value>
        /// The AverageAccountManagementFee.
        /// </value>
        public decimal AverageAccountManagementFee { get; set; }

        /// <summary>
        /// Gets or sets the AvergageMinimumMonthlyFee.
        /// </summary>
        /// <value>
        /// The AvergageMinimumMonthlyFee.
        /// </value>
        public decimal AvergageMinimumMonthlyFee { get; set; }

        /// <summary>
        /// Gets or sets the TotalChequeIssuingVolumes.
        /// </summary>
        /// <value>
        /// The TotalChequeIssuingVolumes.
        /// </value>
        public decimal TotalChequeIssuingVolumes { get; set; }

        /// <summary>
        /// Gets or sets the TotalChequeDepositVolumes.
        /// </summary>
        /// <value>
        /// The TotalChequeDepositVolumes.
        /// </value>
        public decimal TotalChequeDepositVolumes { get; set; }

        /// <summary>
        /// Gets or sets the TotalChequeEncashmentVolumes.
        /// </summary>
        /// <value>
        /// The TotalChequeEncashmentVolumes.
        /// </value>
        public decimal TotalChequeEncashmentVolumes { get; set; }

        /// <summary>
        /// Gets or sets the TotalChequeEncashmentValues.
        /// </summary>
        /// <value>
        /// The TotalChequeEncashmentValues.
        /// </value>
        public decimal TotalChequeEncashmentValues { get; set; }

        /// <summary>
        /// Gets or sets the TotalCashWithdrawalVolumes.
        /// </summary>
        /// <value>
        /// The TotalCashWithdrawalVolumes.
        /// </value>
        public decimal TotalCashWithdrawalVolumes { get; set; }

        /// <summary>
        /// Gets or sets the TotalCashWithdrawalValues.
        /// </summary>
        /// <value>
        /// The TotalCashWithdrawalValues.
        /// </value>
        public decimal TotalCashWithdrawalValues { get; set; }

        /// <summary>
        /// Gets or sets the AvergageChequeIssuingValue.
        /// </summary>
        /// <value>
        /// The AvergageChequeIssuingValue.
        /// </value>
        public decimal AvergageChequeIssuingValue { get; set; }

        /// <summary>
        /// Gets or sets the AverageChequeIssuingPrice.
        /// </summary>
        /// <value>
        /// The AverageChequeIssuingPrice.
        /// </value>
        public decimal AverageChequeIssuingPrice { get; set; }

        /// <summary>
        /// Gets or sets the AverageChequeDepositValue.
        /// </summary>
        /// <value>
        /// The AverageChequeDepositValue.
        /// </value>
        public decimal AverageChequeDepositValue { get; set; }

        /// <summary>
        /// Gets or sets the AverageChequeDepositPrice.
        /// </summary>
        /// <value>
        /// The AverageChequeDepositPrice.
        /// </value>
        public decimal AverageChequeDepositPrice { get; set; }

        /// <summary>
        /// Gets or sets the AverageChequeEncashmentPrice.
        /// </summary>
        /// <value>
        /// The AverageChequeEncashmentPrice.
        /// </value>
        public decimal AverageChequeEncashmentPrice { get; set; }

        /// <summary>
        /// Gets or sets the AverageCashWithdrawalPrice.
        /// </summary>
        /// <value>
        /// The AverageCashWithdrawalPrice.
        /// </value>
        public decimal AverageCashWithdrawalPrice { get; set; }
    }
}
