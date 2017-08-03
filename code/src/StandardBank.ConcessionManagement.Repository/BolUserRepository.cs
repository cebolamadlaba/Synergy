using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.Common;

namespace StandardBank.ConcessionManagement.Repository
{
    /// <summary>
    /// BolUser repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IBolUserRepository" />
    public class BolUserRepository : IBolUserRepository
    {
        /// <summary>
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="BolUserRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        public BolUserRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public BolUser Create(BolUser model)
        {
            const string sql = @"INSERT [dbo].[tblBolUser] ([UserName], [IsActive]) 
                                VALUES (@UserName, @IsActive) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql, new {UserName = model.UserName, IsActive = model.IsActive}).Single();
            }

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public BolUser ReadById(int id)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<BolUser>(
                    "SELECT [pkBolUserId] [Id], [UserName], [IsActive] FROM [dbo].[tblBolUser] WHERE [pkBolUserId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BolUser> ReadAll()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<BolUser>("SELECT [pkBolUserId] [Id], [UserName], [IsActive] FROM [dbo].[tblBolUser]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(BolUser model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"UPDATE [dbo].[tblBolUser]
                            SET [UserName] = @UserName, [IsActive] = @IsActive
                            WHERE [pkBolUserId] = @Id",
                    new {Id = model.Id, UserName = model.UserName, IsActive = model.IsActive});
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(BolUser model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute("DELETE [dbo].[tblBolUser] WHERE [pkBolUserId] = @Id",
                    new {model.Id});
            }
        }
    }
}
