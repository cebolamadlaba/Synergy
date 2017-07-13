using System;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// LegalEntityAccount entity
    /// </summary>
    public class LegalEntityAccount
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        /// <value>
        /// The Id.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the LegalEntityId.
        /// </summary>
        /// <value>
        /// The LegalEntityId.
        /// </value>
        public int LegalEntityId { get; set; }

        /// <summary>
        /// Gets or sets the AccountNumber.
        /// </summary>
        /// <value>
        /// The AccountNumber.
        /// </value>
        public string AccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the IsActive.
        /// </summary>
        /// <value>
        /// The IsActive.
        /// </value>
        public bool IsActive { get; set; }
    }
}
