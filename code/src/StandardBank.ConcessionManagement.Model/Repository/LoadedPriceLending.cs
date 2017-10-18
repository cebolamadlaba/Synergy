using System;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// LoadedPriceLending entity
    /// </summary>
    public class LoadedPriceLending
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        /// <value>
        /// The Id.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ProductTypeId.
        /// </summary>
        /// <value>
        /// The ProductTypeId.
        /// </value>
        public int ProductTypeId { get; set; }

        /// <summary>
        /// Gets or sets the LegalEntityAccountId.
        /// </summary>
        /// <value>
        /// The LegalEntityAccountId.
        /// </value>
        public int LegalEntityAccountId { get; set; }

        /// <summary>
        /// Gets or sets the MarginToPrime.
        /// </summary>
        /// <value>
        /// The MarginToPrime.
        /// </value>
        public decimal MarginToPrime { get; set; }
    }
}
