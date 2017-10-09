namespace StandardBank.ConcessionManagement.Model.UserInterface.Transactional
{
    /// <summary>
    /// Transactional concession detail
    /// </summary>
    public class TransactionalConcessionDetail : BaseConcessionDetail
    {
        /// <summary>
        /// Gets or sets the transactional concession detail identifier.
        /// </summary>
        /// <value>
        /// The transactional concession detail identifier.
        /// </value>
        public int TransactionalConcessionDetailId { get; set; }

        /// <summary>
        /// Gets or sets the type of the transaction.
        /// </summary>
        /// <value>
        /// The type of the transaction.
        /// </value>
        public string TransactionType { get; set; }

        /// <summary>
        /// Gets or sets the transaction type identifier.
        /// </summary>
        /// <value>
        /// The transaction type identifier.
        /// </value>
        public int? TransactionTypeId { get; set; }

        /// <summary>
        /// Gets or sets the volume.
        /// </summary>
        /// <value>
        /// The volume.
        /// </value>
        public decimal? Volume { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public decimal? Value { get; set; }

        /// <summary>
        /// Gets or sets the ad valorem.
        /// </summary>
        /// <value>
        /// The ad valorem.
        /// </value>
        public decimal? AdValorem { get; set; }

        /// <summary>
        /// Gets or sets the fee.
        /// </summary>
        /// <value>
        /// The fee.
        /// </value>
        public decimal? Fee { get; set; }

        /// <summary>
        /// Gets or sets the transaction table number identifier.
        /// </summary>
        /// <value>
        /// The transaction table number identifier.
        /// </value>
        public int TransactionTableNumberId { get; set; }

        /// <summary>
        /// Gets or sets the loaded price.
        /// </summary>
        /// <value>
        /// The loaded price.
        /// </value>
        public decimal LoadedPrice { get; set; }

        /// <summary>
        /// Gets or sets the approved price.
        /// </summary>
        /// <value>
        /// The approved price.
        /// </value>
        public decimal ApprovedPrice { get; set; }

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
        /// Gets or sets the loaded table number.
        /// </summary>
        /// <value>
        /// The loaded table number.
        /// </value>
        public string LoadedTableNumber { get; set; }

        /// <summary>
        /// Gets or sets the approved table number.
        /// </summary>
        /// <value>
        /// The approved table number.
        /// </value>
        public string ApprovedTableNumber { get; set; }
    }
}
