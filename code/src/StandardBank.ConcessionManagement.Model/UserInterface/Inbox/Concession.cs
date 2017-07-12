using System;

namespace StandardBank.ConcessionManagement.Model.UserInterface.Inbox
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
        /// Gets or sets the risk group.
        /// </summary>
        /// <value>
        /// The risk group.
        /// </value>
        public string RiskGroup { get; set; }

        /// <summary>
        /// Gets or sets the name of the customer.
        /// </summary>
        /// <value>
        /// The name of the customer.
        /// </value>
        public string CustomerName { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the date opened.
        /// </summary>
        /// <value>
        /// The date opened.
        /// </value>
        public DateTime DateOpened { get; set; }

        /// <summary>
        /// Gets or sets the concession identifier.
        /// </summary>
        /// <value>
        /// The concession identifier.
        /// </value>
        public string ConcessionId { get; set; }

        /// <summary>
        /// Gets or sets the segment.
        /// </summary>
        /// <value>
        /// The segment.
        /// </value>
        public string Segment { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [sent for approval].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [sent for approval]; otherwise, <c>false</c>.
        /// </value>
        public bool SentForApproval { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public ConcessionStatus Status { get; set; }
    }
}
