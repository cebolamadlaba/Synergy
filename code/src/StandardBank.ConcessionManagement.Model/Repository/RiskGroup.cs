namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// RiskGroup entity
    /// </summary>
    public class RiskGroup
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        /// <value>
        /// The Id.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the MarketSegmentId.
        /// </summary>
        /// <value>
        /// The MarketSegmentId.
        /// </value>
        public int MarketSegmentId { get; set; }

        /// <summary>
        /// Gets or sets the RiskGroupNumber.
        /// </summary>
        /// <value>
        /// The RiskGroupNumber.
        /// </value>
        public int RiskGroupNumber { get; set; }

        /// <summary>
        /// Gets or sets the RiskGroupName.
        /// </summary>
        /// <value>
        /// The RiskGroupName.
        /// </value>
        public string RiskGroupName { get; set; }

        /// <summary>
        /// Gets or sets the IsActive.
        /// </summary>
        /// <value>
        /// The IsActive.
        /// </value>
        public bool IsActive { get; set; }
    }
}
