namespace StandardBank.ConcessionManagement.Model.UserInterface.Investment
{
    /// <summary>
    /// Cash financial entity
    /// </summary>
    public class InvestmentFinancial
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }      

        public int RiskGroupId { get; set; }

        public decimal TotalLiabilityBalances { get; set; }

        /// <summary>
        /// Gets or sets the WeightedAverageMTP.
        /// </summary>
        /// <value>
        /// The WeightedAverageMTP.
        /// </value>
        public decimal WeightedAverageMTP { get; set; }

        /// <summary>
        /// Gets or sets the WeightedAverageNetMargin.
        /// </summary>
        /// <value>
        /// The WeightedAverageNetMargin.
        /// </value>
        public decimal WeightedAverageNetMargin { get; set; }
    }
}
