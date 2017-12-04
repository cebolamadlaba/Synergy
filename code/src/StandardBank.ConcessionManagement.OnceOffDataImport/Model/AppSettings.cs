using StandardBank.ConcessionManagement.Common;

namespace StandardBank.ConcessionManagement.OnceOffDataImport.Model
{
    /// <summary>
    /// App settings entity
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Gets or sets the configuration data.
        /// </summary>
        /// <value>
        /// The configuration data.
        /// </value>
        public ConfigurationData ConfigurationData { get; set; }

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
        /// Gets or sets the error log.
        /// </summary>
        /// <value>
        /// The error log.
        /// </value>
        public string ErrorLog { get; set; }
    }
}
