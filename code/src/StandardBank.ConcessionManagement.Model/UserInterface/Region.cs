namespace StandardBank.ConcessionManagement.Model.UserInterface
{
    /// <summary>
    /// Region entity
    /// </summary>
    public class Region
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
        /// Gets or sets whether or not this region is selected
        /// </summary>
        public bool IsSelected { get; set; }
    }
}
