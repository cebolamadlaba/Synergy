using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.Model.BusinessLogic.EmailTemplates
{
    /// <summary>
    /// Sap data import issues email entity
    /// </summary>
    public class SapDataImportIssuesEmail
    {
        /// <summary>
        /// Gets or sets the support email address.
        /// </summary>
        /// <value>
        /// The support email address.
        /// </value>
        public string SupportEmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the sap data import issues.
        /// </summary>
        /// <value>
        /// The sap data import issues.
        /// </value>
        public IEnumerable<SapDataImportIssue> SapDataImportIssues { get; set; }
    }
}
