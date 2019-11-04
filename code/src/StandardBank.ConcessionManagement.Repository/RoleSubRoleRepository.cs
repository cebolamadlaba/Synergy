using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface;
using System.Collections.Generic;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.Common;

namespace StandardBank.ConcessionManagement.Repository
{
    /// <summary>
    /// RoleSubRole repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IRoleSubRoleRepository" />
    public class RoleSubRoleRepository : IRoleSubRoleRepository
    {
        /// <summary>
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleSubRoleRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        public RoleSubRoleRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public RoleSubRole ReadById(int id)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<RoleSubRole>(
                    "SELECT [SubRoleId],[Name], [Active], [fkRoleId] [RoleId] FROM [dbo].[rtblSubRole] WHERE [SubRoleId] = @id",
                    new { id }).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RoleSubRole> ReadAll()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<RoleSubRole>(
                    "SELECT [SubRoleId],[Name], [Active], [fkRoleId] [RoleId] FROM [dbo].[rtblSubRole]");
            }
        }

    }
}