using System;
using System.Collections.Generic;

namespace StandardBank.ConcessionManagement.Model.UserInterface
{
    /// <summary>
    /// Concession entity
    /// </summary>
    public class Concession
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the reference number
        /// </summary>
        public string ReferenceNumber { get; set; }

        /// <summary>
        /// Gets or sets the child reference number.
        /// </summary>
        /// <value>
        /// The child reference number.
        /// </value>
        public string ChildReferenceNumber { get; set; }

        /// <summary>
        /// Gets or sets the risk group id
        /// </summary>
        public int RiskGroupId { get; set; }

        /// <summary>
        /// Gets or sets the risk group number
        /// </summary>
        public int? RiskGroupNumber { get; set; }

        /// <summary>
        /// Gets or sets the risk group name
        /// </summary>
        public string RiskGroupName { get; set; }

        /// <summary>
        /// Gets or sets the concession type
        /// </summary>
        public string ConcessionType { get; set; }

        /// <summary>
        /// Gets or sets the smt deal number
        /// </summary>
        public string SmtDealNumber { get; set; }

        /// <summary>
        /// Gets or sets the motivation
        /// </summary>
        public string Motivation { get; set; }

        /// <summary>
        /// Gets or sets the mrs crs
        /// </summary>
        public decimal MrsCrs { get; set; }
        
        /// <summary>
        /// Gets or sets the type
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the date opened
        /// </summary>
        public DateTime DateOpened { get; set; }
        
        /// <summary>
        /// Gets or sets the segment
        /// </summary>
        public string Seqment { get; set; }
        
        /// <summary>
        /// Gets or sets the date sent for approval
        /// </summary>
        public DateTime? DateSentForApproval { get; set; }

        /// <summary>
        /// Gets or sets the account number
        /// </summary>
        public string AccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the sub status
        /// </summary>
        public string SubStatus { get; set; }

        /// <summary>
        /// Gets or sets the sub status id
        /// </summary>
        public int SubStatusId { get; set; }

        /// <summary>
        /// Gets or sets the comments
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// Gets or sets the requestor identifier.
        /// </summary>
        /// <value>
        /// The requestor identifier.
        /// </value>
        public int? RequestorId { get; set; }

        /// <summary>
        /// Gets or sets the bcm user id
        /// </summary>
        public int? BcmUserId { get; set; }

        /// <summary>
        /// Gets or sets the pcm user id
        /// </summary>
        public int? PcmUserId { get; set; }

        /// <summary>
        /// Gets or sets the ho user id
        /// </summary>
        public int? HoUserId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance can extend.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can extend; otherwise, <c>false</c>.
        /// </value>
        public bool CanExtend { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance can renew.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can renew; otherwise, <c>false</c>.
        /// </value>
        public bool CanRenew { get; set; }

        /// <summary>
        /// Gets or sets the concession comments.
        /// </summary>
        /// <value>
        /// The concession comments.
        /// </value>
        public IEnumerable<ConcessionComment> ConcessionComments { get; set; }

        /// <summary>
        /// Gets or sets the concession relationship details.
        /// </summary>
        /// <value>
        /// The concession relationship details.
        /// </value>
        public IEnumerable<ConcessionRelationshipDetail> ConcessionRelationshipDetails { get; set; }

        /// <summary>
        /// Gets or sets the requestor.
        /// </summary>
        /// <value>
        /// The requestor.
        /// </value>
        public User Requestor { get; set; }

        /// <summary>
        /// Gets the status description.
        /// </summary>
        /// <value>
        /// The status description.
        /// </value>
        public string StatusDescription => $"{Status} - {SubStatus}";

        /// <summary>
        /// Gets or sets a value indicating whether this instance can resubmit.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can resubmit; otherwise, <c>false</c>.
        /// </value>
        public bool CanResubmit { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance can update.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can update; otherwise, <c>false</c>.
        /// </value>
        public bool CanUpdate { get; set; }
    }
}
