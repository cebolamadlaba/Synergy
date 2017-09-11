using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.Common;

namespace StandardBank.ConcessionManagement.Repository
{
    /// <summary>
    /// User repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IUserRepository" />
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        public UserRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public User Create(User model)
        {
            const string sql =
                @"INSERT [dbo].[tblUser] ([ANumber], [EmailAddress], [FirstName], [Surname], [IsActive], [ContactNumber]) 
                                VALUES (@ANumber, @EmailAddress, @FirstName, @Surname, @IsActive, @ContactNumber) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql,
                    new
                    {
                        ANumber = model.ANumber,
                        EmailAddress = model.EmailAddress,
                        FirstName = model.FirstName,
                        Surname = model.Surname,
                        IsActive = model.IsActive,
                        ContactNumber = model.ContactNumber
                    }).Single();
            }

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public User ReadById(int id)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<User>(
                    "SELECT [pkUserId] [Id], [ANumber], [EmailAddress], [FirstName], [Surname], [IsActive], [ContactNumber] FROM [dbo].[tblUser] WHERE [pkUserId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads by the a number
        /// </summary>
        /// <param name="aNumber"></param>
        /// <returns></returns>
        public User ReadByANumber(string aNumber)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<User>(
                    @"SELECT [pkUserId] [Id], [ANumber], [EmailAddress], [FirstName], [Surname], [IsActive], [ContactNumber] 
                    FROM [dbo].[tblUser] 
                    WHERE [ANumber] = @aNumber",
                    new {aNumber}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> ReadAll()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<User>(
                    "SELECT [pkUserId] [Id], [ANumber], [EmailAddress], [FirstName], [Surname], [IsActive], [ContactNumber] FROM [dbo].[tblUser]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(User model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"UPDATE [dbo].[tblUser]
                            SET [ANumber] = @ANumber, [EmailAddress] = @EmailAddress, [FirstName] = @FirstName, [Surname] = @Surname, [IsActive] = @IsActive, [ContactNumber] = @ContactNumber
                            WHERE [pkUserId] = @Id",
                    new
                    {
                        Id = model.Id,
                        ANumber = model.ANumber,
                        EmailAddress = model.EmailAddress,
                        FirstName = model.FirstName,
                        Surname = model.Surname,
                        IsActive = model.IsActive,
                        ContactNumber = model.ContactNumber
                    });
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(User model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute("DELETE [dbo].[tblUser] WHERE [pkUserId] = @Id",
                    new {model.Id});
            }
        }

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public int CreateUser(User user)
        {
            using (var connection = _dbConnectionFactory.Connection())
            {
                var tx = connection.BeginTransaction();
                var id = connection.ExecuteScalar<int>("CreateUser",
                    new
                    {
                        user.ANumber,
                        user.EmailAddress,
                        user.FirstName,
                        LastName = user.Surname,
                        user.RoleId,
                        user.RegionId,
                        user.CentreId,
                        user.ContactNumber
                    }, transaction: tx, commandType: System.Data.CommandType.StoredProcedure);
                tx.Commit();
                return id;
            }
        }

        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <param name="user">The user.</param>
        public void UpdateUser(User user)
        {
            using (var conn = _dbConnectionFactory.Connection())
            {
                var tx = conn.BeginTransaction();
                var id = conn.ExecuteScalar<int>("UpdateUser",
                    new
                    {
                        user.ANumber,
                        user.EmailAddress,
                        user.FirstName,
                        LastName = user.Surname,
                        user.RoleId,
                        user.RegionId,
                        user.CentreId,
                        user.Id,
                        user.ContactNumber
                    }, transaction: tx, commandType: System.Data.CommandType.StoredProcedure);
                tx.Commit();
            }
        }
    }
}