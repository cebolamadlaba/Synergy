namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// ConcessionMas entity
    /// </summary>
    public class ConcessionMas : ConcessionDetail, IAuditable
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
        public int TransactionTypeId { get; set; }

        /// <summary>
        /// Gets or sets the MerchantNumber.
        /// </summary>
        /// <value>
        /// The MerchantNumber.
        /// </value>
        public string MerchantNumber { get; set; }

        /// <summary>
        /// Gets or sets the Turnover.
        /// </summary>
        /// <value>
        /// The Turnover.
        /// </value>
        public decimal? Turnover { get; set; }

        /// <summary>
        /// Gets or sets the CommissionRate.
        /// </summary>
        /// <value>
        /// The CommissionRate.
        /// </value>
        public decimal? CommissionRate { get; set; }

        /// <summary>
        /// Gets or the name of the table.
        /// </summary>
        /// <value>
        /// The name of the table.
        /// </value>
        public string TableName => "tblConcessionMas";

        /// <summary>
        /// Gets the name of the primary key column.
        /// </summary>
        /// <value>
        /// The name of the primary key column.
        /// </value>
        public string PrimaryKeyColumnName => "pkConcessionMasId";

        /// <summary>
        /// Gets the primary key value.
        /// </summary>
        /// <value>
        /// The primary key value.
        /// </value>
        public object PrimaryKeyValue => Id;
    }
}
