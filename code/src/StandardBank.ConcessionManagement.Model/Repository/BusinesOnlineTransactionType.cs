using System;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// BusinesOnlineTransactionType entity
    /// </summary>
    public class BusinesOnlineTransactionType
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
        public int TransactionGroupId { get; set; }

        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        /// <value>
        /// The Description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the IsActive.
        /// </summary>
        /// <value>
        /// The IsActive.
        /// </value>
        public bool IsActive { get; set; }
    }
}
