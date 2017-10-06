using System.Collections.Generic;

namespace StandardBank.ConcessionManagement.Model.BusinessLogic.EmailTemplates
{
    /// <summary>
    /// Expiring concession entity
    /// </summary>
    public class ExpiringConcession
    {
        /// <summary>
        /// Gets or sets the name of the requestor.
        /// </summary>
        /// <value>
        /// The name of the requestor.
        /// </value>
        public string RequestorName { get; set; }

        /// <summary>
        /// Gets or sets the requestor email.
        /// </summary>
        /// <value>
        /// The requestor email.
        /// </value>
        public string RequestorEmail { get; set; }

        /// <summary>
        /// Gets or sets the requestor identifier.
        /// </summary>
        /// <value>
        /// The requestor identifier.
        /// </value>
        public int RequestorId { get; set; }

        /// <summary>
        /// Gets or sets the expiring concession details.
        /// </summary>
        /// <value>
        /// The expiring concession details.
        /// </value>
        public IEnumerable<ExpiringConcessionDetail> ExpiringConcessionDetails { get; set; }
    }
}
