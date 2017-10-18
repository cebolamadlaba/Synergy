namespace StandardBank.ConcessionManagement.Model.UserInterface
{
    /// <summary>
    /// Risk group entity
    /// </summary>
    public class RiskGroup
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
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
        /// Gets or sets the market segment.
        /// </summary>
        /// <value>
        /// The market segment.
        /// </value>
        public string MarketSegment { get; set; }

        /// <summary>
        /// Gets or sets the region identifier.
        /// </summary>
        /// <value>
        /// The region identifier.
        /// </value>
        public int RegionId { get; set; }

        /// <summary>
        /// Gets or sets the region.
        /// </summary>
        /// <value>
        /// The region.
        /// </value>
        public string Region { get; set; }

        /// <summary>
        /// Gets or sets the number.
        /// </summary>
        /// <value>
        /// The number.
        /// </value>
        public int Number { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
    }
}
