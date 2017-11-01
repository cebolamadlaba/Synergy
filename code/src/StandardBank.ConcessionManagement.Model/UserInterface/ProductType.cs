namespace StandardBank.ConcessionManagement.Model.UserInterface
{
    /// <summary>
    /// Product type entity
    /// </summary>
    public class ProductType
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the type of the concession.
        /// </summary>
        /// <value>
        /// The type of the concession.
        /// </value>
        public ConcessionType ConcessionType { get; set; }

        /// <summary>
        /// Gets or sets the import file channel.
        /// </summary>
        /// <value>
        /// The import file channel.
        /// </value>
        public string ImportFileChannel { get; set; }
    }
}
