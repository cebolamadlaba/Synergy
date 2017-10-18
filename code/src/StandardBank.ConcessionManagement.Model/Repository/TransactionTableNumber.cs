using System;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// TransactionTableNumber entity
    /// </summary>
    public class TransactionTableNumber
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
        /// Gets or sets the TariffTable.
        /// </summary>
        /// <value>
        /// The TariffTable.
        /// </value>
        public int TariffTable { get; set; }

        /// <summary>
        /// Gets or sets the Fee.
        /// </summary>
        /// <value>
        /// The Fee.
        /// </value>
        public decimal? Fee { get; set; }

        /// <summary>
        /// Gets or sets the AdValorem.
        /// </summary>
        /// <value>
        /// The AdValorem.
        /// </value>
        public decimal? AdValorem { get; set; }

        /// <summary>
        /// Gets or sets the IsActive.
        /// </summary>
        /// <value>
        /// The IsActive.
        /// </value>
        public bool IsActive { get; set; }
    }
}
