using System;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// ProductTransactional entity
    /// </summary>
    public class ProductTransactional
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        /// <value>
        /// The Id.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the RiskGroupId.
        /// </summary>
        /// <value>
        /// The RiskGroupId.
        /// </value>
        public int RiskGroupId { get; set; }

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
        /// Gets or sets the transaction table number identifier.
        /// </summary>
        /// <value>
        /// The transaction table number identifier.
        /// </value>
        public int TransactionTableNumberId { get; set; }

        /// <summary>
        /// Gets or sets the TransactionTypeId.
        /// </summary>
        /// <value>
        /// The TransactionTypeId.
        /// </value>
        public int TransactionTypeId { get; set; }

        /// <summary>
        /// Gets or sets the Volume.
        /// </summary>
        /// <value>
        /// The Volume.
        /// </value>
        public decimal? Volume { get; set; }

        /// <summary>
        /// Gets or sets the Value.
        /// </summary>
        /// <value>
        /// The Value.
        /// </value>
        public decimal? Value { get; set; }

        /// <summary>
        /// Gets or sets the LoadedPrice.
        /// </summary>
        /// <value>
        /// The LoadedPrice.
        /// </value>
        public string LoadedPrice { get; set; }
    }
}
