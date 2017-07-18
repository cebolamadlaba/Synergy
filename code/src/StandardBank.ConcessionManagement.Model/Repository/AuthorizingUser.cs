namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// Authorizing user entity
    /// </summary>
    public class AuthorizingUser
    {
        /// <summary>
        /// Gets or sets the center.
        /// </summary>
        /// <value>
        /// The center.
        /// </value>
        public string Center { get; set; }

        /// <summary>
        /// Gets or sets the segment.
        /// </summary>
        /// <value>
        /// The segment.
        /// </value>
        public string Segment { get; set; }

        /// <summary>
        /// Gets or sets the authorizing user identifier.
        /// </summary>
        /// <value>
        /// The authorizing user identifier.
        /// </value>
        public string AuthorizingUserId { get; set; }

        /// <summary>
        /// Gets or sets the provincial user identifier.
        /// </summary>
        /// <value>
        /// The provincial user identifier.
        /// </value>
        public string ProvincialUserId { get; set; }

        /// <summary>
        /// Gets or sets the pricing user identifier.
        /// </summary>
        /// <value>
        /// The pricing user identifier.
        /// </value>
        public string PricingUserId { get; set; }
    }
}
