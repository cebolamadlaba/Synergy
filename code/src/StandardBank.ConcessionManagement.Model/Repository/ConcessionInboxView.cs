using System;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// Concession inbox entity
    /// </summary>
    public class ConcessionInboxView
    {
        /// <summary>
        /// Gets or sets the concession identifier.
        /// </summary>
        /// <value>
        /// The concession identifier.
        /// </value>
        public int ConcessionId { get; set; }

        /// <summary>
        /// Gets or sets the risk group identifier.
        /// </summary>
        /// <value>
        /// The risk group identifier.
        /// </value>
        public int RiskGroupId { get; set; }

        /// <summary>
        /// Gets or sets the risk group number.
        /// </summary>
        /// <value>
        /// The risk group number.
        /// </value>
        public int RiskGroupNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the risk group.
        /// </summary>
        /// <value>
        /// The name of the risk group.
        /// </value>
        public string RiskGroupName { get; set; }

        /// <summary>
        /// Gets or sets the legal entity identifier.
        /// </summary>
        /// <value>
        /// The legal entity identifier.
        /// </value>
        public int LegalEntityId { get; set; }

        /// <summary>
        /// Gets or sets the name of the customer.
        /// </summary>
        /// <value>
        /// The name of the customer.
        /// </value>
        public string CustomerName { get; set; }

        public int CustomerNumber { get; set; }

        /// <summary>
        /// Gets or sets the legal entity account identifier.
        /// </summary>
        /// <value>
        /// The legal entity account identifier.
        /// </value>
        public int LegalEntityAccountId { get; set; }

        /// <summary>
        /// Gets or sets the account number.
        /// </summary>
        /// <value>
        /// The account number.
        /// </value>
        public string AccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the concession type identifier.
        /// </summary>
        /// <value>
        /// The concession type identifier.
        /// </value>
        public int ConcessionTypeId { get; set; }

        /// <summary>
        /// Gets or sets the type of the concession.
        /// </summary>
        /// <value>
        /// The type of the concession.
        /// </value>
        public string ConcessionType { get; set; }

        /// <summary>
        /// Gets or sets the concession date.
        /// </summary>
        /// <value>
        /// The concession date.
        /// </value>
        public DateTime ConcessionDate { get; set; }

        /// <summary>
        /// Gets or sets the status identifier.
        /// </summary>
        /// <value>
        /// The status identifier.
        /// </value>
        public int StatusId { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the sub status identifier.
        /// </summary>
        /// <value>
        /// The sub status identifier.
        /// </value>
        public int SubStatusId { get; set; }

        /// <summary>
        /// Gets or sets the sub status.
        /// </summary>
        /// <value>
        /// The sub status.
        /// </value>
        public string SubStatus { get; set; }

        /// <summary>
        /// Gets or sets the concession reference.
        /// </summary>
        /// <value>
        /// The concession reference.
        /// </value>
        public string ConcessionRef { get; set; }

        /// <summary>
        /// Gets or sets the market segment identifier.
        /// </summary>
        /// <value>
        /// The market segment identifier.
        /// </value>
        public int MarketSegmentId { get; set; }

        /// <summary>
        /// Gets or sets the segment.
        /// </summary>
        /// <value>
        /// The segment.
        /// </value>
        public string Segment { get; set; }

        /// <summary>
        /// Gets or sets the datesent for approval.
        /// </summary>
        /// <value>
        /// The datesent for approval.
        /// </value>
        public DateTime DatesentForApproval { get; set; }

        /// <summary>
        /// Gets or sets the concession detail identifier.
        /// </summary>
        /// <value>
        /// The concession detail identifier.
        /// </value>
        public int ConcessionDetailId { get; set; }

        /// <summary>
        /// Gets or sets the expiry date.
        /// </summary>
        /// <value>
        /// The expiry date.
        /// </value>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// Gets or sets the date approved.
        /// </summary>
        /// <value>
        /// The date approved.
        /// </value>
        public DateTime? DateApproved { get; set; }

        /// <summary>
        /// Gets or sets the aa user identifier.
        /// </summary>
        /// <value>
        /// The aa user identifier.
        /// </value>
        public int? AAUserId { get; set; }

        /// <summary>
        /// Gets or sets the requestor identifier.
        /// </summary>
        /// <value>
        /// The requestor identifier.
        /// </value>
        public int? RequestorId { get; set; }

        /// <summary>
        /// Gets or sets the BCM user identifier.
        /// </summary>
        /// <value>
        /// The BCM user identifier.
        /// </value>
        public int? BCMUserId { get; set; }

        /// <summary>
        /// Gets or sets the PCM user identifier.
        /// </summary>
        /// <value>
        /// The PCM user identifier.
        /// </value>
        public int? PCMUserId { get; set; }

        /// <summary>
        /// Gets or sets the ho user identifier.
        /// </summary>
        /// <value>
        /// The ho user identifier.
        /// </value>
        public int? HOUserId { get; set; }

        /// <summary>
        /// Gets or sets the centre identifier.
        /// </summary>
        /// <value>
        /// The centre identifier.
        /// </value>
        public int CentreId { get; set; }

        /// <summary>
        /// Gets or sets the name of the centre.
        /// </summary>
        /// <value>
        /// The name of the centre.
        /// </value>
        public string CentreName { get; set; }

        /// <summary>
        /// Gets or sets the region identifier.
        /// </summary>
        /// <value>
        /// The region identifier.
        /// </value>
        public int RegionId { get; set; }

        /// <summary>
        /// Gets or sets the region.
        /// </summary>
        /// <value>
        /// The region.
        /// </value>
        public string Region { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is mismatched.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is mismatched; otherwise, <c>false</c>.
        /// </value>
        public bool IsMismatched { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is current.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is current; otherwise, <c>false</c>.
        /// </value>
        public bool IsCurrent { get; set; }

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

        public string ConcessionLetterURL { get; set; }

        public string AAUserFullName { get; set; }

        public string AEUserFullName { get; set; }
    }
}
