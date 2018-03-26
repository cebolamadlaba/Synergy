using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.Sqlite;
using StandardBank.ConcessionManagement.Interface.Common;
using StandardBank.ConcessionManagement.Model.Common;

namespace StandardBank.ConcessionManagement.Common
{
    /// <summary>
    /// Sql db connection factory
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Common.IDbConnectionFactory" />
    public class DbConnectionFactory : IDbConnectionFactory
    {
        /// <summary>
        /// The configuration data
        /// </summary>
        private readonly IConfigurationData _configurationData;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbConnectionFactory"/> class.
        /// </summary>
        /// <param name="configurationData">The configuration data.</param>
        public DbConnectionFactory(IConfigurationData configurationData)
        {
            _configurationData = configurationData;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="DbConnectionFactory"/> class.
        /// </summary>
        ~DbConnectionFactory()
        {
            Dispose(false);
        }

        IDbConnection connection;

        /// <summary>
        /// Connection to the database.
        /// </summary>
        /// <returns></returns>
        public IDbConnection Connection()
        {
            
            
            if (_configurationData.DatabaseType == DatabaseType.SqlServer)
                connection = new SqlConnection(_configurationData.ConnectionString);
            else
                connection = new SqliteConnection(_configurationData.ConnectionString);

            connection.Open();

            return connection;
        }

        /// <summary>
        /// Releases the unmanaged resources.
        /// </summary>
        private void ReleaseUnmanagedResources()
        {
            if (connection != null)
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            ReleaseUnmanagedResources();
            if (disposing)
            {
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
