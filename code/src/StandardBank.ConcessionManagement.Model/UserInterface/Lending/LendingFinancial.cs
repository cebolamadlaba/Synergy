namespace StandardBank.ConcessionManagement.Model.UserInterface.Lending
{
    /// <summary>
    /// Lending financial entity
    /// </summary>
    public class LendingFinancial
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the total exposure.
        /// </summary>
        /// <value>
        /// The total exposure.
        /// </value>
        public decimal TotalExposure { get; set; }

        /// <summary>
        /// Gets or sets the weighted average map.
        /// </summary>
        /// <value>
        /// The weighted average map.
        /// </value>
        public decimal WeightedAverageMap { get; set; }

        /// <summary>
        /// Gets or sets the weighted CRS MRS.
        /// </summary>
        /// <value>
        /// The weighted CRS MRS.
        /// </value>
        public decimal WeightedCrsOrMrs { get; set; }

        /// <summary>
        /// Gets or sets the latest CRS or MRS.
        /// </summary>
        /// <value>
        /// The latest CRS or MRS.
        /// </value>
        public decimal LatestCrsOrMrs { get; set; }
    }
}
