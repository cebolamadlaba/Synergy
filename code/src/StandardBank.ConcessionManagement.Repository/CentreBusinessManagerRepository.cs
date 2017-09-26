using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.Common;

namespace StandardBank.ConcessionManagement.Repository
{
    /// <summary>
    /// CentreBusinessManager repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.ICentreBusinessManagerRepository" />
    public class CentreBusinessManagerRepository : ICentreBusinessManagerRepository
    {
        /// <summary>
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="CentreBusinessManagerRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        public CentreBusinessManagerRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public CentreBusinessManager Create(CentreBusinessManager model)
        {
            const string sql = @"INSERT [dbo].[tblCentreBusinessManager] ([fkCentreId], [fkUserId], [IsActive]) 
                                VALUES (@CentreId, @UserId, @IsActive) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql, new {CentreId = model.CentreId, UserId = model.UserId, IsActive = model.IsActive}).Single();
            }

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public CentreBusinessManager ReadById(int id)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<CentreBusinessManager>(
                    "SELECT [pkCentreBusinessManagerId] [Id], [fkCentreId] [CentreId], [fkUserId] [UserId], [IsActive] FROM [dbo].[tblCentreBusinessManager] WHERE [pkCentreBusinessManagerId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CentreBusinessManager> ReadAll()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<CentreBusinessManager>("SELECT [pkCentreBusinessManagerId] [Id], [fkCentreId] [CentreId], [fkUserId] [UserId], [IsActive] FROM [dbo].[tblCentreBusinessManager]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(CentreBusinessManager model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"UPDATE [dbo].[tblCentreBusinessManager]
                            SET [fkCentreId] = @CentreId, [fkUserId] = @UserId, [IsActive] = @IsActive
                            WHERE [pkCentreBusinessManagerId] = @Id",
                    new {Id = model.Id, CentreId = model.CentreId, UserId = model.UserId, IsActive = model.IsActive});
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(CentreBusinessManager model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute("DELETE [dbo].[tblCentreBusinessManager] WHERE [pkCentreBusinessManagerId] = @Id",
                    new {model.Id});
            }
        }
    }
}
