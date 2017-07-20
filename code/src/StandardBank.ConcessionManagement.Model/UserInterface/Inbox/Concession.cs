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
        /// Gets or sets the risk group number
        /// </summary>
        public int RiskGroupNumber { get; set; }

        /// <summary>
        /// Gets or sets the risk group name
        /// </summary>
        public string RiskGroupName { get; set; }

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
    }
}
