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
        /// <param name="overrideLoggedInUser"></param>
        /// <param name="databaseType"></param>
        public ConfigurationData(string connectionString, string overrideLoggedInUser, string databaseType , string smtpServer , string smtpUserName , string smtpPassword , string defaultEmail , int smtpPort , string templatePath)
        {
            ConnectionString = connectionString;
            OverrideLoggedInUser = overrideLoggedInUser;
            DatabaseType = databaseType == "SqlServer" ? DatabaseType.SqlServer : DatabaseType.SqlLite;
            SmtpServer = smtpServer;
            SmtpServerPassword = smtpPassword;
            SmtpServerUserName = smtpUserName;
            DefaultEmail = defaultEmail;
            SmtpPort = smtpPort;
            TemplatePath = templatePath;

        }
        public ConfigurationData(string connectionString, string overrideLoggedInUser, string databaseType)
        {
            ConnectionString = connectionString;
            OverrideLoggedInUser = overrideLoggedInUser;
            DatabaseType = databaseType == "SqlServer" ? DatabaseType.SqlServer : DatabaseType.SqlLite;

        }
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

        public string TemplatePath  { get; set; }
    }
}
