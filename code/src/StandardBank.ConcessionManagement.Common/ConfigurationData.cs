using StandardBank.ConcessionManagement.Interface.Common;

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
        public ConfigurationData(string connectionString, string overrideLoggedInUser)
        {
            ConnectionString = connectionString;
            OverrideLoggedInUser = overrideLoggedInUser;
        }

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <value>
        /// The connection string.
        /// </value>
        public string ConnectionString { get; }

        /// <summary>
        /// Gets the override logged in user
        /// </summary>
        public string OverrideLoggedInUser { get; }
    }
}
