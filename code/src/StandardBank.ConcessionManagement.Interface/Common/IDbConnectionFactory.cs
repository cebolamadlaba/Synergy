using System;
using System.Data;

namespace StandardBank.ConcessionManagement.Interface.Common
{
    /// <summary>
    /// Database connection factory
    /// </summary>
    public interface IDbConnectionFactory : IDisposable
    {
        /// <summary>
        /// Connection to the database.
        /// </summary>
        /// <returns></returns>
        IDbConnection Connection();
    }
}
