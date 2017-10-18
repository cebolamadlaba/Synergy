namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// FinancialLending entity
    /// </summary>
    public class FinancialLending
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
        /// Gets or sets the TotalExposure.
        /// </summary>
        /// <value>
        /// The TotalExposure.
        /// </value>
        public decimal TotalExposure { get; set; }

        /// <summary>
        /// Gets or sets the WeightedAverageMap.
        /// </summary>
        /// <value>
        /// The WeightedAverageMap.
        /// </value>
        public decimal WeightedAverageMap { get; set; }

        /// <summary>
        /// Gets or sets the WeightedCrsOrMrs.
        /// </summary>
        /// <value>
        /// The WeightedCrsOrMrs.
        /// </value>
        public decimal WeightedCrsOrMrs { get; set; }

        /// <summary>
        /// Gets or sets the LatestCrsOrMrs.
        /// </summary>
        /// <value>
        /// The LatestCrsOrMrs.
        /// </value>
        public decimal? LatestCrsOrMrs { get; set; }
    }
}
