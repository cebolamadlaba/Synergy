using System.Threading.Tasks;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.Interface.Repository
{
    /// <summary>
    /// Audit repository interface
    /// </summary>
    public interface IAuditRepository
    {
        /// <summary>
        /// Audits the specified entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="auditType">Type of the audit.</param>
        /// <param name="username">The username.</param>
        Task Audit<T>(T entity, AuditType auditType, string username) where T : IAuditable;
    }
}
