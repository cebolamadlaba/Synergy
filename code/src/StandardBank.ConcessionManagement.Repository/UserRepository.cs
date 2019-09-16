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
                @"INSERT [dbo].[tblUser] ([ANumber], [EmailAddress], [FirstName], [Surname], [IsActive], [ContactNumber], [CanApprove]) 
                VALUES (@ANumber, @EmailAddress, @FirstName, @Surname, @IsActive, @ContactNumber, @CanApprove) 
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
                        ContactNumber = model.ContactNumber,
                        CanApprove = model.CanApprove
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
                    "SELECT [pkUserId] [Id], [ANumber], [EmailAddress], [FirstName], [Surname], [IsActive], [ContactNumber], [CanApprove] FROM [dbo].[tblUser] WHERE [pkUserId] = @Id",
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
                    @"SELECT [pkUserId] [Id], [ANumber], [EmailAddress], [FirstName], [Surname], [IsActive], [ContactNumber], [CanApprove] 
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
                    "SELECT [pkUserId] [Id], [ANumber], [EmailAddress], [FirstName], [Surname], [IsActive], [ContactNumber], [CanApprove] FROM [dbo].[tblUser]");
            }
        }

        /// <summary>
        /// Reads the by role.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <returns></returns>
        public IEnumerable<User> ReadByRole(string roleName)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<User>(
                    @"SELECT u.[pkUserId] [Id], u.[ANumber], u.[EmailAddress], u.[FirstName], u.[Surname], u.[IsActive], u.[ContactNumber], u.[CanApprove] FROM [dbo].[tblUser] u
                    JOIN [dbo].[tblUserRole] ur ON ur.[fkUserId] = u.[pkUserId]
                    JOIN [dbo].[rtblRole] r ON r.[pkRoleId] = ur.[fkRoleId]
                    WHERE r.[RoleName] = @roleName
                    ORDER BY u.[FirstName], u.[Surname]", new {roleName});
            }
        }

        /// <summary>
        /// Reads the by role centre identifier.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <param name="centreId">The centre identifier.</param>
        /// <returns></returns>
        public IEnumerable<User> ReadByRoleCentreId(string roleName, int centreId)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<User>(
                    @"SELECT u.[pkUserId] [Id], u.[ANumber], u.[EmailAddress], u.[FirstName], u.[Surname], u.[IsActive], u.[ContactNumber], u.[CanApprove] FROM [dbo].[tblUser] u
                    JOIN [dbo].[tblCentreUser] cu ON cu.[fkUserId] = u.[pkUserId]
                    JOIN [dbo].[tblUserRole] ur ON ur.[fkUserId] = u.[pkUserId]
                    JOIN [dbo].[rtblRole] r ON r.[pkRoleId] = ur.[fkRoleId]
                    WHERE r.[RoleName] = @roleName
                    AND cu.[fkCentreId] = @centreId
                    ORDER BY u.[FirstName], u.[Surname]", new {roleName, centreId});
            }
        }

        /// <summary>
        /// Reads the by centre identifier.
        /// </summary>
        /// <param name="centreId">The centre identifier.</param>
        /// <returns></returns>
        public IEnumerable<User> ReadByCentreId(int centreId)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<User>(
                    @"SELECT u.[pkUserId] [Id], u.[ANumber], u.[EmailAddress], u.[FirstName], u.[Surname], u.[IsActive], u.[ContactNumber], u.[CanApprove] FROM [dbo].[tblUser] u
                    JOIN [dbo].[tblCentreUser] cu ON cu.[fkUserId] = u.[pkUserId]
                    WHERE cu.[fkCentreId] = @centreId
                    ORDER BY u.[FirstName], u.[Surname]", new {centreId});
            }
        }

        /// <summary>
        /// Reads the by risk group identifier.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group identifier.</param>
        /// <returns></returns>
        public User ReadByRiskGroupNumber(int riskGroupNumber)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<User>(
                    @" SELECT Distinct
                            us.FirstName
		                    ,us.Surname
		                    ,us.pkUserId Id
                       FROM [dbo].[tblRiskGroup] riskgroup
                          INNER JOIN [dbo].[tblLegalEntity] lea 
	                        ON lea.fkRiskGroupId = riskgroup.pkRiskGroupId
                          INNER JOIN tblUser us 
	                        ON us.pkUserId=lea.fkUserId
                      WHERE riskgroup.RiskGroupNumber = @riskGroupNumber", new { riskGroupNumber }).FirstOrDefault();
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
                            SET [ANumber] = @ANumber, [EmailAddress] = @EmailAddress, [FirstName] = @FirstName, [Surname] = @Surname, [IsActive] = @IsActive, [ContactNumber] = @ContactNumber, [CanApprove] = @CanApprove
                            WHERE [pkUserId] = @Id",
                    new
                    {
                        Id = model.Id,
                        ANumber = model.ANumber,
                        EmailAddress = model.EmailAddress,
                        FirstName = model.FirstName,
                        Surname = model.Surname,
                        IsActive = model.IsActive,
                        ContactNumber = model.ContactNumber,
                        CanApprove = model.CanApprove
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
                        user.ContactNumber,
                        user.IsActive,
                        user.CanApprove,
                        user.SubRoleId
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
                        user.SubRoleId,
                        user.Id,
                        user.ContactNumber,
                        user.IsActive,
                        user.CanApprove
                    }, transaction: tx, commandType: System.Data.CommandType.StoredProcedure);
                tx.Commit();
            }
        }
    }
}