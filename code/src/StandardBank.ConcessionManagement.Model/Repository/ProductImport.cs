using System;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// ProductImport entity
    /// </summary>
    public class ProductImport
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        /// <value>
        /// The Id.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ProductId.
        /// </summary>
        /// <value>
        /// The ProductId.
        /// </value>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the ImportFileChannel.
        /// </summary>
        /// <value>
        /// The ImportFileChannel.
        /// </value>
        public string ImportFileChannel { get; set; }
    }
}
