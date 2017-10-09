namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// ConcessionTransactional entity
    /// </summary>
    public class ConcessionTransactional : ConcessionDetail, IAuditable
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
        /// Gets or sets the transaction table number identifier.
        /// </summary>
        /// <value>
        /// The transaction table number identifier.
        /// </value>
        public int TransactionTableNumberId { get; set; }

        /// <summary>
        /// Gets or sets the approved transaction table number identifier.
        /// </summary>
        /// <value>
        /// The approved transaction table number identifier.
        /// </value>
        public int? ApprovedTransactionTableNumberId { get; set; }

        /// <summary>
        /// Gets or sets the loaded transaction table number identifier.
        /// </summary>
        /// <value>
        /// The loaded transaction table number identifier.
        /// </value>
        public int? LoadedTransactionTableNumberId { get; set; }

        /// <summary>
        /// Gets or sets the AdValorem.
        /// </summary>
        /// <value>
        /// The AdValorem.
        /// </value>
        public decimal? AdValorem { get; set; }

        /// <summary>
        /// Gets or sets the BaseRate.
        /// </summary>
        /// <value>
        /// The BaseRate.
        /// </value>
        public decimal? Fee { get; set; }

        /// <summary>
        /// Gets or the name of the table.
        /// </summary>
        /// <value>
        /// The name of the table.
        /// </value>
        public string TableName => "tblConcessionTransactional";

        /// <summary>
        /// Gets the name of the primary key column.
        /// </summary>
        /// <value>
        /// The name of the primary key column.
        /// </value>
        public string PrimaryKeyColumnName => "pkConcessionTransactionalId";

        /// <summary>
        /// Gets the primary key value.
        /// </summary>
        /// <value>
        /// The primary key value.
        /// </value>
        public object PrimaryKeyValue => Id;
    }
}
