using StandardBank.ConcessionManagement.Model.Common;

namespace StandardBank.ConcessionManagement.Interface.Common
{
    /// <summary>
    /// Configuration data interface
    /// </summary>
    public interface IConfigurationData
    {
        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <value>
        /// The connection string.
        /// </value>
        string ConnectionString { get; }

        /// <summary>
        /// Gets the database type
        /// </summary>
        DatabaseType DatabaseType { get; }

        /// <summary>
        /// Gets the override logged in user
        /// </summary>
        string OverrideLoggedInUser { get; }
    }
}
