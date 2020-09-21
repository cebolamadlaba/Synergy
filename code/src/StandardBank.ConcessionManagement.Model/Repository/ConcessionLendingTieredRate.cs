namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// ConcessionLendingTieredRate entity
    /// </summary>
    public class ConcessionLendingTieredRate : IAuditable
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        /// <value>
        /// The Id.
        /// </value>
        public int Id { get; set; }

        public int ConcessionLendingId { get; set; }

        /// <summary>
        /// Gets or sets the Limit.
        /// </summary>
        /// <value>
        /// The Limit.
        /// </value>
        public decimal? Limit { get; set; }

        /// <summary>
        /// Gets or sets the MarginToPrime.
        /// </summary>
        /// <value>
        /// The MarginToPrime.
        /// </value>
        public decimal? MarginToPrime { get; set; }

        /// <summary>
        /// Gets or sets the ApprovedMarginToPrime.
        /// </summary>
        /// <value>
        /// The MarginToPrime.
        /// </value>
        public decimal? ApprovedMarginToPrime { get; set; }

        /// <summary>
        /// Gets or the name of the table.
        /// </summary>
        /// <value>
        /// The name of the table.
        /// </value>
        public string TableName => "tblConcessionLendingTieredRate";

        /// <summary>
        /// Gets the name of the primary key column.
        /// </summary>
        /// <value>
        /// The name of the primary key column.
        /// </value>
        public string PrimaryKeyColumnName => "pkConcessionLendingTieredRateId";

        /// <summary>
        /// Gets the primary key value.
        /// </summary>
        /// <value>
        /// The primary key value.
        /// </value>
        public object PrimaryKeyValue => Id;
    }
}
