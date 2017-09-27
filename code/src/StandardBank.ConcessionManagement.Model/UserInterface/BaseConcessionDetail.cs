using System;

namespace StandardBank.ConcessionManagement.Model.UserInterface
{
    /// <summary>
    /// Base concession detail
    /// </summary>
    public abstract class BaseConcessionDetail
    {
        /// <summary>
        /// Gets or sets the ConcessionDetailId.
        /// </summary>
        /// <value>
        /// The ConcessionDetailId.
        /// </value>
        public int ConcessionDetailId { get; set; }

        /// <summary>
        /// Gets or sets the ConcessionId.
        /// </summary>
        /// <value>
        /// The ConcessionId.
        /// </value>
        public int ConcessionId { get; set; }

        /// <summary>
        /// Gets or sets the legal entity identifier.
        /// </summary>
        /// <value>
        /// The legal entity identifier.
        /// </value>
        public int? LegalEntityId { get; set; }

        /// <summary>
        /// Gets or sets the legal entity account identifier.
        /// </summary>
        /// <value>
        /// The legal entity account identifier.
        /// </value>
        public int? LegalEntityAccountId { get; set; }

        /// <summary>
        /// Gets or sets the name of the customer.
        /// </summary>
        /// <value>
        /// The name of the customer.
        /// </value>
        public string CustomerName { get; set; }

        /// <summary>
        /// Gets or sets the account number.
        /// </summary>
        /// <value>
        /// The account number.
        /// </value>
        public string AccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the expiry date.
        /// </summary>
        /// <value>
        /// The expiry date.
        /// </value>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// Gets or sets the DateApproved.
        /// </summary>
        /// <value>
        /// The DateApproved.
        /// </value>
        public DateTime? DateApproved { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is mismatched.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is mismatched; otherwise, <c>false</c>.
        /// </value>
        public bool IsMismatched { get; set; }
    }
}
