using System;

namespace StandardBank.ConcessionManagement.Model.Repository
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
        /// Gets or sets the contact person.
        /// </summary>
        /// <value>
        /// The contact person.
        /// </value>
        public string ContactPerson { get; set; }

        /// <summary>
        /// Gets or sets the postal address.
        /// </summary>
        /// <value>
        /// The postal address.
        /// </value>
        public string PostalAddress { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        /// <value>
        /// The postal code.
        /// </value>
        public string PostalCode { get; set; }
    }
}
