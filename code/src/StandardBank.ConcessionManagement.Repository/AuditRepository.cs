using System.Threading.Tasks;
using Dapper;
using StandardBank.ConcessionManagement.Interface.Common;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.Repository
{
    /// <summary>
    /// Audit repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IAuditRepository" />
    public class AuditRepository : IAuditRepository
    {
        /// <summary>
        /// The database connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// The marshaller
        /// </summary>
        private readonly IMarshaller _marshaller;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The database connection factory.</param>
        /// <param name="marshaller">The marshaller.</param>
        public AuditRepository(IDbConnectionFactory dbConnectionFactory, IMarshaller marshaller)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _marshaller = marshaller;
        }

        /// <summary>
        /// Audits the specified entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TU"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="auditType">Type of the audit.</param>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        public async Task Audit<T>(T entity, AuditType auditType, string username) where T : IAuditable
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                await db.ExecuteAsync(
                    $"INSERT INTO [Audit].[{entity.TableName}] ([{entity.PrimaryKeyColumnName}], [fkAuditTypeId], [Entity], [Username]) VALUES (@pk, @fkAuditTypeId, @Entity, @Username)",
                    new
                    {
                        pk = entity.PrimaryKeyValue,
                        fkAuditTypeId = (int) auditType,
                        Entity = _marshaller.SerializeObject(entity),
                        Username = username
                    });
            }
        }
    }
}
