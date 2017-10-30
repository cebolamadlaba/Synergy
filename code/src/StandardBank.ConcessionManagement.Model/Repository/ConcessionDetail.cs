using System;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// ConcessionDetail entity
    /// </summary>
    public class ConcessionDetail
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
        /// Gets or sets the LegalEntityId.
        /// </summary>
        /// <value>
        /// The LegalEntityId.
        /// </value>
        public int LegalEntityId { get; set; }

        /// <summary>
        /// Gets or sets the LegalEntityAccountId.
        /// </summary>
        /// <value>
        /// The LegalEntityAccountId.
        /// </value>
        public int LegalEntityAccountId { get; set; }

        /// <summary>
        /// Gets or sets the ExpiryDate.
        /// </summary>
        /// <value>
        /// The ExpiryDate.
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

        /// <summary>
        /// Gets or sets a value indicating whether [price exported].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [price exported]; otherwise, <c>false</c>.
        /// </value>
        public bool PriceExported { get; set; }

        /// <summary>
        /// Gets or sets the price exported date.
        /// </summary>
        /// <value>
        /// The price exported date.
        /// </value>
        public DateTime? PriceExportedDate { get; set; }
    }
}
