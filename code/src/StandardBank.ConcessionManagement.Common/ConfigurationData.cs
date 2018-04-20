using System;
using StandardBank.ConcessionManagement.Interface.Common;
using StandardBank.ConcessionManagement.Model.Common;

namespace StandardBank.ConcessionManagement.Common
{
    /// <summary>
    /// Configuration data
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Common.IConfigurationData" />
    public class ConfigurationData : IConfigurationData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationData"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="overrideLoggedInUser">The override logged in user.</param>
        /// <param name="databaseType">Type of the database.</param>
        /// <param name="smtpServer">The SMTP server.</param>
        /// <param name="smtpUserName">Name of the SMTP user.</param>
        /// <param name="smtpPassword">The SMTP password.</param>
        /// <param name="defaultEmail">The default email.</param>
        /// <param name="smtpPort">The SMTP port.</param>
        /// <param name="emailTemplatePath">The email template path.</param>
        /// <param name="letterTemplatePath">The letter template path.</param>
        public ConfigurationData(string connectionString, string overrideLoggedInUser, string databaseType,
            string smtpServer, string smtpUserName, string smtpPassword, string defaultEmail, int smtpPort,
            string emailTemplatePath, string letterTemplatePath, string enforceMyAccess)
        {
            ConnectionString = connectionString;
            OverrideLoggedInUser = overrideLoggedInUser;
            DatabaseType = databaseType == "SqlServer" ? DatabaseType.SqlServer : DatabaseType.SqlLite;
            SmtpServer = smtpServer;
            SmtpServerPassword = smtpPassword;
            SmtpServerUserName = smtpUserName;
            DefaultEmail = defaultEmail;
            SmtpPort = smtpPort;
            EmailTemplatePath = emailTemplatePath;
            LetterTemplatePath = letterTemplatePath;
            EnforceMyAccess = enforceMyAccess;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationData"/> class.
        /// </summary>
        public ConfigurationData()
        {
        }

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <value>
        /// The connection string.
        /// </value>
        public string ConnectionString  { get; set; }

        /// <summary>
        /// Gets the database type
        /// </summary>
        public DatabaseType DatabaseType  { get; set; }

        /// <summary>
        /// Gets the override logged in user
        /// </summary>
        public string OverrideLoggedInUser  { get; set; }
        /// <summary>
        /// Get the stmp server
        /// </summary>
        public string SmtpServer  { get; set; }

        /// <summary>
        /// Gets the stmp user 
        /// </summary>
        public string SmtpServerUserName  { get; set; }
        /// <summary>
        /// Gets the smtp password
        /// </summary>
        public string SmtpServerPassword  { get; set; }

        /// <summary>
        /// Get the default email address to use when sending emails
        /// </summary>
        public string DefaultEmail  { get; set; }

        public int SmtpPort  { get; set; }

        public string EmailTemplatePath  { get; set; }

        /// <summary>
        /// Gets or sets the letter template path.
        /// </summary>
        /// <value>
        /// The letter template path.
        /// </value>
        public string LetterTemplatePath { get; set; }

        /// <summary>
        /// Gets or sets the WK HTML to PDF executable.
        /// </summary>
        /// <value>
        /// The w KHTML to PDF executable.
        /// </value>
        public string WKhtmlToPDFExecutable { get; set; }

        public string DateDatabaseConnection { get ; set ; }

        public string EnforceMyAccess { get; set; }
    }
}
