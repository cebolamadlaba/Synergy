using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface;
using System.Collections.Generic;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.Common;

namespace StandardBank.ConcessionManagement.Repository
{
    /// <summary>
    /// UserSubRole repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IUserSubRoleRepository" />
    public class UserSubRoleRepository : IUserSubRoleRepository
    {
        /// <summary>
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserSubRoleRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        public UserSubRoleRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public UserSubRole Create(UserSubRole model)
        {
            const string sql = @"INSERT [dbo].[rtblSubRole] ([Name]) 
                                VALUES (@Name) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.SubRoleId = db.Query<int>(sql,
                    new { Name = model.Name}).Single();
            }

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public UserSubRole ReadById(int id)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<UserSubRole>(
                    "SELECT [SubRoleId],[Name], [Active] FROM [dbo].[rtblSubRole] WHERE [SubRoleId] = @id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserSubRole> ReadAll()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<UserSubRole>(
                    "SELECT [SubRoleId],[Name], [Active] FROM [dbo].[rtblSubRole]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(UserSubRole model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"UPDATE [dbo].[rtblSubRole]
                            SET  [Name] = @Name, [Active] = @Active
                            WHERE [SubRoleId] = @SubRoleId",
                    new { Name = model.Name, Active = model.Active});
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(UserSubRole model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute("DELETE [dbo].[rtblSubRole] WHERE [SubRoleId] = @SubRoleId",
                    new {model.SubRoleId});
            }
        }
    }
}