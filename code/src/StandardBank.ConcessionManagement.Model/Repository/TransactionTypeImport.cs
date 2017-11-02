using System;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// TransactionTypeImport entity
    /// </summary>
    public class TransactionTypeImport
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
        /// Gets or sets the ImportFileChannel.
        /// </summary>
        /// <value>
        /// The ImportFileChannel.
        /// </value>
        public string ImportFileChannel { get; set; }
    }
}
