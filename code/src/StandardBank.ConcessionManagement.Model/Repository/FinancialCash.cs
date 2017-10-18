namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// FinancialCash entity
    /// </summary>
    public class FinancialCash
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
        /// Gets or sets the WeightedAverageBranchPrice.
        /// </summary>
        /// <value>
        /// The WeightedAverageBranchPrice.
        /// </value>
        public decimal WeightedAverageBranchPrice { get; set; }

        /// <summary>
        /// Gets or sets the TotalCashCentrCashTurnover.
        /// </summary>
        /// <value>
        /// The TotalCashCentrCashTurnover.
        /// </value>
        public decimal TotalCashCentrCashTurnover { get; set; }

        /// <summary>
        /// Gets or sets the TotalCashCentrCashVolume.
        /// </summary>
        /// <value>
        /// The TotalCashCentrCashVolume.
        /// </value>
        public decimal TotalCashCentrCashVolume { get; set; }

        /// <summary>
        /// Gets or sets the TotalBranchCashTurnover.
        /// </summary>
        /// <value>
        /// The TotalBranchCashTurnover.
        /// </value>
        public decimal TotalBranchCashTurnover { get; set; }

        /// <summary>
        /// Gets or sets the TotalBranchCashVolume.
        /// </summary>
        /// <value>
        /// The TotalBranchCashVolume.
        /// </value>
        public decimal TotalBranchCashVolume { get; set; }

        /// <summary>
        /// Gets or sets the TotalAutosafeCashTurnover.
        /// </summary>
        /// <value>
        /// The TotalAutosafeCashTurnover.
        /// </value>
        public decimal TotalAutosafeCashTurnover { get; set; }

        /// <summary>
        /// Gets or sets the TotalAutosafeCashVolume.
        /// </summary>
        /// <value>
        /// The TotalAutosafeCashVolume.
        /// </value>
        public decimal TotalAutosafeCashVolume { get; set; }

        /// <summary>
        /// Gets or sets the WeightedAverageCCPrice.
        /// </summary>
        /// <value>
        /// The WeightedAverageCCPrice.
        /// </value>
        public decimal WeightedAverageCCPrice { get; set; }

        /// <summary>
        /// Gets or sets the WeightedAverageAFPrice.
        /// </summary>
        /// <value>
        /// The WeightedAverageAFPrice.
        /// </value>
        public decimal WeightedAverageAFPrice { get; set; }

        /// <summary>
        /// Gets or sets the LatestCrsOrMrs.
        /// </summary>
        /// <value>
        /// The LatestCrsOrMrs.
        /// </value>
        public decimal LatestCrsOrMrs { get; set; }
    }
}
