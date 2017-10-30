namespace StandardBank.ConcessionManagement.Model.UserInterface
{
    /// <summary>
    /// Channel type entity
    /// </summary>
    public class ChannelType
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
        /// Gets or sets the import file product identifier.
        /// </summary>
        /// <value>
        /// The import file product identifier.
        /// </value>
        public string ImportFileProductId { get; set; }
    }
}
