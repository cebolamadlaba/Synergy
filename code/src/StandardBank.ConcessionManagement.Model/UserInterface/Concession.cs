﻿using System;

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
        /// Gets or sets the customer name
        /// </summary>
        public string CustomerName { get; set; }
        
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
        public int? SubStatusId { get; set; }

        /// <summary>
        /// Gets or sets the comments
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// Gets or sets the bcm user id
        /// </summary>
        public int? BcmUserId { get; set; }
    }
}
