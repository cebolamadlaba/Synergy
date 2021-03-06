using System;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// LoadedPriceTransactional entity
    /// </summary>
    public class LoadedPriceTransactional
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
        /// Gets or sets the LegalEntityAccountId.
        /// </summary>
        /// <value>
        /// The LegalEntityAccountId.
        /// </value>
        public int LegalEntityAccountId { get; set; }

        /// <summary>
        /// Gets or sets the transaction table number identifier.
        /// </summary>
        /// <value>
        /// The transaction table number identifier.
        /// </value>
        public int TransactionTableNumberId { get; set; }
    }
}
