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
        /// Gets or sets the name of the server.
        /// </summary>
        /// <value>
        /// The name of the server.
        /// </value>
        public string ServerName { get; set; }

        /// <summary>
        /// Gets or sets the database server.
        /// </summary>
        /// <value>
        /// The database server.
        /// </value>
        public string DatabaseServer { get; set; }

        /// <summary>
        /// Gets or sets the name of the database.
        /// </summary>
        /// <value>
        /// The name of the database.
        /// </value>
        public string DatabaseName { get; set; }

        /// <summary>
        /// Gets or sets the import folder.
        /// </summary>
        /// <value>
        /// The import folder.
        /// </value>
        public string ImportFolder { get; set; }

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
