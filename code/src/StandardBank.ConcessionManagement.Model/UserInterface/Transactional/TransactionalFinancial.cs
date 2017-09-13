namespace StandardBank.ConcessionManagement.Model.UserInterface.Transactional
{
    /// <summary>
    /// Transactional financial entity
    /// </summary>
    public class TransactionalFinancial
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the total number of accounts.
        /// </summary>
        /// <value>
        /// The total number of accounts.
        /// </value>
        public decimal TotalNumberOfAccounts { get; set; }

        /// <summary>
        /// Gets or sets the average account management fee.
        /// </summary>
        /// <value>
        /// The average account management fee.
        /// </value>
        public decimal AverageAccountManagementFee { get; set; }

        /// <summary>
        /// Gets or sets the average minimum monthly fee.
        /// </summary>
        /// <value>
        /// The average minimum monthly fee.
        /// </value>
        public decimal AverageMinimumMonthlyFee { get; set; }

        /// <summary>
        /// Gets or sets the total cheque issuing volumes.
        /// </summary>
        /// <value>
        /// The total cheque issuing volumes.
        /// </value>
        public decimal TotalChequeIssuingVolumes { get; set; }

        /// <summary>
        /// Gets or sets the total cheque deposit volumes.
        /// </summary>
        /// <value>
        /// The total cheque deposit volumes.
        /// </value>
        public decimal TotalChequeDepositVolumes { get; set; }

        /// <summary>
        /// Gets or sets the total cheque encashment volumes.
        /// </summary>
        /// <value>
        /// The total cheque encashment volumes.
        /// </value>
        public decimal TotalChequeEncashmentVolumes { get; set; }

        /// <summary>
        /// Gets or sets the total cheque encashment values.
        /// </summary>
        /// <value>
        /// The total cheque encashment values.
        /// </value>
        public decimal TotalChequeEncashmentValues { get; set; }

        /// <summary>
        /// Gets or sets the total cash withdrawal volumes.
        /// </summary>
        /// <value>
        /// The total cash withdrawal volumes.
        /// </value>
        public decimal TotalCashWithdrawalVolumes { get; set; }

        /// <summary>
        /// Gets or sets the total cash withdrawal values.
        /// </summary>
        /// <value>
        /// The total cash withdrawal values.
        /// </value>
        public decimal TotalCashWithdrawalValues { get; set; }

        /// <summary>
        /// Gets or sets the average cheque issuing value.
        /// </summary>
        /// <value>
        /// The average cheque issuing value.
        /// </value>
        public decimal AverageChequeIssuingValue { get; set; }

        /// <summary>
        /// Gets or sets the average cheque issuing price.
        /// </summary>
        /// <value>
        /// The average cheque issuing price.
        /// </value>
        public decimal AverageChequeIssuingPrice { get; set; }

        /// <summary>
        /// Gets or sets the average cheque deposit value.
        /// </summary>
        /// <value>
        /// The average cheque deposit value.
        /// </value>
        public decimal AverageChequeDepositValue { get; set; }

        /// <summary>
        /// Gets or sets the average cheque deposit price.
        /// </summary>
        /// <value>
        /// The average cheque deposit price.
        /// </value>
        public decimal AverageChequeDepositPrice { get; set; }

        /// <summary>
        /// Gets or sets the average cheque encashment price.
        /// </summary>
        /// <value>
        /// The average cheque encashment price.
        /// </value>
        public decimal AverageChequeEncashmentPrice { get; set; }

        /// <summary>
        /// Gets or sets the average cash withdrawal price.
        /// </summary>
        /// <value>
        /// The average cash withdrawal price.
        /// </value>
        public decimal AverageCashWithdrawalPrice { get; set; }

        /// <summary>
        /// Gets or sets the latest CRS or MRS.
        /// </summary>
        /// <value>
        /// The latest CRS or MRS.
        /// </value>
        public decimal LatestCrsOrMrs { get; set; }
    }
}
