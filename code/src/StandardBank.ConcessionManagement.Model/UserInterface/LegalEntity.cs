namespace StandardBank.ConcessionManagement.Model.UserInterface
{
    /// <summary>
    /// LegalEntity entity
    /// </summary>
    public class LegalEntity
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        /// <value>
        /// The Id.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the MarketSegmentId.
        /// </summary>
        /// <value>
        /// The MarketSegmentId.
        /// </value>
        public int MarketSegmentId { get; set; }

        /// <summary>
        /// Gets or sets the RiskGroupId.
        /// </summary>
        /// <value>
        /// The RiskGroupId.
        /// </value>
        public int RiskGroupId { get; set; }

        /// <summary>
        /// Gets or sets the CustomerName.
        /// </summary>
        /// <value>
        /// The CustomerName.
        /// </value>
        public string CustomerName { get; set; }

        /// <summary>
        /// Gets or sets the CustomerNumber.
        /// </summary>
        /// <value>
        /// The CustomerNumber.
        /// </value>
        public string CustomerNumber { get; set; }

        /// <summary>
        /// Gets or sets the IsActive.
        /// </summary>
        /// <value>
        /// The IsActive.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the ContactPerson.
        /// </summary>
        /// <value>
        /// The ContactPerson.
        /// </value>
        public string ContactPerson { get; set; }

        /// <summary>
        /// Gets or sets the PostalAddress.
        /// </summary>
        /// <value>
        /// The PostalAddress.
        /// </value>
        public string PostalAddress { get; set; }

        /// <summary>
        /// Gets or sets the City.
        /// </summary>
        /// <value>
        /// The City.
        /// </value>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the PostalCode.
        /// </summary>
        /// <value>
        /// The PostalCode.
        /// </value>
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int? UserId { get; set; }

    }
}
