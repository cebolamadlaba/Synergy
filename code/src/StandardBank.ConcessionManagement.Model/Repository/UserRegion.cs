namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// UserRegion entity
    /// </summary>
    public class UserRegion
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        /// <value>
        /// The Id.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the UserId.
        /// </summary>
        /// <value>
        /// The UserId.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the RegionId.
        /// </summary>
        /// <value>
        /// The RegionId.
        /// </value>
        public int RegionId { get; set; }

        /// <summary>
        /// Gets or sets the IsActive.
        /// </summary>
        /// <value>
        /// The IsActive.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the IsSelected.
        /// </summary>
        /// <value>
        /// The IsSelected.
        /// </value>
        public bool IsSelected { get; set; }
    }
}
