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
        /// Gets or sets the base rate.
        /// </summary>
        /// <value>
        /// The base rate.
        /// </value>
        public decimal? BaseRate { get; set; }

        /// <summary>
        /// Gets or sets the table number identifier.
        /// </summary>
        /// <value>
        /// The table number identifier.
        /// </value>
        public int TableNumberId { get; set; }

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
