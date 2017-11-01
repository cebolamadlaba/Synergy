namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// Product entity
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        /// <value>
        /// The Id.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ConcessionTypeId.
        /// </summary>
        /// <value>
        /// The ConcessionTypeId.
        /// </value>
        public int ConcessionTypeId { get; set; }

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

        /// <summary>
        /// Gets or sets the import file channel.
        /// </summary>
        /// <value>
        /// The import file channel.
        /// </value>
        public string ImportFileChannel { get; set; }
    }
}
