namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// ConcessionBol entity
    /// </summary>
    public class ConcessionBol : ConcessionDetail, IAuditable
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        /// <value>
        /// The Id.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the TransactionGroupId.
        /// </summary>
        /// <value>
        /// The TransactionGroupId.
        /// </value>
        public int? TransactionGroupId { get; set; }

        /// <summary>
        /// Gets or sets the BusinesOnlineTransactionTypeId.
        /// </summary>
        /// <value>
        /// The BusinesOnlineTransactionTypeId.
        /// </value>
        public int? BusinesOnlineTransactionTypeId { get; set; }

        /// <summary>
        /// Gets or sets the BolUseId.
        /// </summary>
        /// <value>
        /// The BolUseId.
        /// </value>
        public int? BolUseId { get; set; }

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
        /// Gets or sets the Fee.
        /// </summary>
        /// <value>
        /// The Fee.
        /// </value>
        public decimal? Fee { get; set; }

        /// <summary>
        /// Gets or the name of the table.
        /// </summary>
        /// <value>
        /// The name of the table.
        /// </value>
        public string TableName => "tblConcessionBol";

        /// <summary>
        /// Gets the name of the primary key column.
        /// </summary>
        /// <value>
        /// The name of the primary key column.
        /// </value>
        public string PrimaryKeyColumnName => "pkConcessionBolId";

        /// <summary>
        /// Gets the primary key value.
        /// </summary>
        /// <value>
        /// The primary key value.
        /// </value>
        public object PrimaryKeyValue => Id;
    }
}
