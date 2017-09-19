namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// ConcessionTransactional entity
    /// </summary>
    public class ConcessionTransactional : IAuditable
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        /// <value>
        /// The Id.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ConcessionId.
        /// </summary>
        /// <value>
        /// The ConcessionId.
        /// </value>
        public int ConcessionId { get; set; }

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
        /// Gets or sets the BaseRateId.
        /// </summary>
        /// <value>
        /// The BaseRateId.
        /// </value>
        public int? BaseRateId { get; set; }

        /// <summary>
        /// Gets or sets the AdValorem.
        /// </summary>
        /// <value>
        /// The AdValorem.
        /// </value>
        public decimal? AdValorem { get; set; }

        /// <summary>
        /// Gets or sets the LegalEntityId.
        /// </summary>
        /// <value>
        /// The LegalEntityId.
        /// </value>
        public int LegalEntityId { get; set; }

        /// <summary>
        /// Gets or sets the LegalEntityAccountId.
        /// </summary>
        /// <value>
        /// The LegalEntityAccountId.
        /// </value>
        public int LegalEntityAccountId { get; set; }

        /// <summary>
        /// Gets or sets the BaseRate.
        /// </summary>
        /// <value>
        /// The BaseRate.
        /// </value>
        public decimal? BaseRate { get; set; }

        /// <summary>
        /// Gets or sets the TableNumberId.
        /// </summary>
        /// <value>
        /// The TableNumberId.
        /// </value>
        public int TableNumberId { get; set; }

        /// <summary>
        /// Gets or the name of the table.
        /// </summary>
        /// <value>
        /// The name of the table.
        /// </value>
        public string TableName => "tblConcessionTransactional";

        /// <summary>
        /// Gets the primary key column name
        /// </summary>
        public string PrimaryKeyColumnName => "pkConcessionTransactionalId";

        /// <summary>
        /// Gets the primary key value
        /// </summary>
        public object PrimaryKeyValue => Id;

        /// <summary>
        /// Gets or sets the approved table number identifier.
        /// </summary>
        /// <value>
        /// The approved table number identifier.
        /// </value>
        public int? ApprovedTableNumberId { get; set; }

        /// <summary>
        /// Gets or sets the loaded table number identifier.
        /// </summary>
        /// <value>
        /// The loaded table number identifier.
        /// </value>
        public int? LoadedTableNumberId { get; set; }
    }
}
