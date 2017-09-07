namespace StandardBank.ConcessionManagement.Model.UserInterface.Cash
{
    /// <summary>
    /// Cash financial entity
    /// </summary>
    public class CashFinancial
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the weighted average branch price.
        /// </summary>
        /// <value>
        /// The weighted average branch price.
        /// </value>
        public decimal WeightedAverageBranchPrice { get; set; }

        /// <summary>
        /// Gets or sets the total cash centr cash turnover.
        /// </summary>
        /// <value>
        /// The total cash centr cash turnover.
        /// </value>
        public decimal TotalCashCentrCashTurnover { get; set; }

        /// <summary>
        /// Gets or sets the total cash centr cash volume.
        /// </summary>
        /// <value>
        /// The total cash centr cash volume.
        /// </value>
        public decimal TotalCashCentrCashVolume { get; set; }

        /// <summary>
        /// Gets or sets the total branch cash turnover.
        /// </summary>
        /// <value>
        /// The total branch cash turnover.
        /// </value>
        public decimal TotalBranchCashTurnover { get; set; }

        /// <summary>
        /// Gets or sets the total branch cash volume.
        /// </summary>
        /// <value>
        /// The total branch cash volume.
        /// </value>
        public decimal TotalBranchCashVolume { get; set; }

        /// <summary>
        /// Gets or sets the total autosafe cash turnover.
        /// </summary>
        /// <value>
        /// The total autosafe cash turnover.
        /// </value>
        public decimal TotalAutosafeCashTurnover { get; set; }

        /// <summary>
        /// Gets or sets the total autosafe cash volume.
        /// </summary>
        /// <value>
        /// The total autosafe cash volume.
        /// </value>
        public decimal TotalAutosafeCashVolume { get; set; }

        /// <summary>
        /// Gets or sets the weighted average cc price.
        /// </summary>
        /// <value>
        /// The weighted average cc price.
        /// </value>
        public decimal WeightedAverageCCPrice { get; set; }

        /// <summary>
        /// Gets or sets the weighted average af price.
        /// </summary>
        /// <value>
        /// The weighted average af price.
        /// </value>
        public decimal WeightedAverageAFPrice { get; set; }

        /// <summary>
        /// Gets or sets the latest CRS or MRS.
        /// </summary>
        /// <value>
        /// The latest CRS or MRS.
        /// </value>
        public decimal LatestCrsOrMrs { get; set; }

        /// <summary>
        /// Gets or sets the loaded price.
        /// </summary>
        /// <value>
        /// The loaded price.
        /// </value>
        public decimal LoadedPrice { get; set; }
    }
}
