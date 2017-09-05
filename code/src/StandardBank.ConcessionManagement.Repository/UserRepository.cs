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
            const string sql = @"INSERT [dbo].[tblUser] ([ANumber], [EmailAddress], [FirstName], [Surname], [IsActive]) 
                                VALUES (@ANumber, @EmailAddress, @FirstName, @Surname, @IsActive) 
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
                        IsActive = model.IsActive
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
                    "SELECT [pkUserId] [Id], [ANumber], [EmailAddress], [FirstName], [Surname], [IsActive] FROM [dbo].[tblUser] WHERE [pkUserId] = @Id",
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
                    @"SELECT [pkUserId] [Id], [ANumber], [EmailAddress], [FirstName], [Surname], [IsActive] 
                    FROM [dbo].[tblUser] 
                    WHERE [ANumber] = @aNumber",
                    new { aNumber }).SingleOrDefault();
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
                    "SELECT [pkUserId] [Id], [ANumber], [EmailAddress], [FirstName], [Surname], [IsActive] FROM [dbo].[tblUser]");
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
                            SET [ANumber] = @ANumber, [EmailAddress] = @EmailAddress, [FirstName] = @FirstName, [Surname] = @Surname, [IsActive] = @IsActive
                            WHERE [pkUserId] = @Id",
                    new
                    {
                        Id = model.Id,
                        ANumber = model.ANumber,
                        EmailAddress = model.EmailAddress,
                        FirstName = model.FirstName,
                        Surname = model.Surname,
                        IsActive = model.IsActive
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
    }
}