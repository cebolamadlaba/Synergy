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
        public ConfigurationData(string connectionString, string overrideLoggedInUser, string databaseType)
        {
            ConnectionString = connectionString;
            OverrideLoggedInUser = overrideLoggedInUser;
            DatabaseType = databaseType == "SqlServer" ? DatabaseType.SqlServer : DatabaseType.SqlLite;
        }

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <value>
        /// The connection string.
        /// </value>
        public string ConnectionString { get; }

        /// <summary>
        /// Gets the database type
        /// </summary>
        public DatabaseType DatabaseType { get; }

        /// <summary>
        /// Gets the override logged in user
        /// </summary>
        public string OverrideLoggedInUser { get; }
    }
}
