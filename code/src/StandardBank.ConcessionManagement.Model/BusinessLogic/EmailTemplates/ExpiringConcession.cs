using System.Collections.Generic;

namespace StandardBank.ConcessionManagement.Model.BusinessLogic.EmailTemplates
{
    /// <summary>
    /// Expiring concession entity
    /// </summary>
    public class ExpiringConcession
    {
        /// <summary>
        /// Gets or sets the name of the recipient.
        /// </summary>
        /// <value>
        /// The name of the recipient.
        /// </value>
        public string RecipientName { get; set; }

        /// <summary>
        /// Gets or sets the recipient email.
        /// </summary>
        /// <value>
        /// The recipient email.
        /// </value>
        public string RecipientEmail { get; set; }

        /// <summary>
        /// Gets or sets the recipient identifier.
        /// </summary>
        /// <value>
        /// The recipient identifier.
        /// </value>
        public int RecipientId { get; set; }

        /// <summary>
        /// Gets or sets the expiring concession details.
        /// </summary>
        /// <value>
        /// The expiring concession details.
        /// </value>
        public IList<ExpiringConcessionDetail> ExpiringConcessionDetails { get; set; }
    }
}
