namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// ConcessionTrade entity
    /// </summary>
    public class ConcessionTrade : ConcessionDetail, IAuditable
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        /// <value>
        /// The Id.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the TransactionTypeId.
        /// </summary>
        /// <value>
        /// The TransactionTypeId.
        /// </value>
        public int? TransactionTypeId { get; set; }

        /// <summary>
        /// Gets or sets the ChannelTypeId.
        /// </summary>
        /// <value>
        /// The ChannelTypeId.
        /// </value>
        public int? ChannelTypeId { get; set; }

        /// <summary>
        /// Gets or sets the BaseRateId.
        /// </summary>
        /// <value>
        /// The BaseRateId.
        /// </value>
        public int? BaseRateId { get; set; }

        /// <summary>
        /// Gets or sets the TableNumber.
        /// </summary>
        /// <value>
        /// The TableNumber.
        /// </value>
        public int? TableNumber { get; set; }

        /// <summary>
        /// Gets or sets the TransactionVolume.
        /// </summary>
        /// <value>
        /// The TransactionVolume.
        /// </value>
        public int? TransactionVolume { get; set; }

        /// <summary>
        /// Gets or sets the TransactionValue.
        /// </summary>
        /// <value>
        /// The TransactionValue.
        /// </value>
        public decimal? TransactionValue { get; set; }

        /// <summary>
        /// Gets or sets the AdValorem.
        /// </summary>
        /// <value>
        /// The AdValorem.
        /// </value>
        public decimal? AdValorem { get; set; }

        /// <summary>
        /// Gets or the name of the table.
        /// </summary>
        /// <value>
        /// The name of the table.
        /// </value>
        public string TableName => "tblConcessionTrade";

        /// <summary>
        /// Gets the name of the primary key column.
        /// </summary>
        /// <value>
        /// The name of the primary key column.
        /// </value>
        public string PrimaryKeyColumnName => "pkConcessionTradeId";

        /// <summary>
        /// Gets the primary key value.
        /// </summary>
        /// <value>
        /// The primary key value.
        /// </value>
        public object PrimaryKeyValue => Id;
    }
}
