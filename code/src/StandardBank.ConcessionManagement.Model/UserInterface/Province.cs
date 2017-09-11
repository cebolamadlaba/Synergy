namespace StandardBank.ConcessionManagement.Model.UserInterface
{
    /// <summary>
    /// Province entity
    /// </summary>
    public class Province
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
        /// Get or set the ative indicator
        /// </summary>
        public bool IsActive { get; set; }
    }
}
