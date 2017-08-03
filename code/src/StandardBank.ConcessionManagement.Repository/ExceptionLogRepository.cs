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
    /// ExceptionLog repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IExceptionLogRepository" />
    public class ExceptionLogRepository : IExceptionLogRepository
    {
        /// <summary>
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionLogRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        public ExceptionLogRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ExceptionLog Create(ExceptionLog model)
        {
            const string sql =
                @"INSERT [dbo].[tblExceptionLog] ([ExceptionMessage], [ExceptionType], [ExceptionSource], [ExceptionData], [Logdate]) 
                                VALUES (@ExceptionMessage, @ExceptionType, @ExceptionSource, @ExceptionData, @Logdate) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.ExceptionLogId = db.Query<int>(sql,
                    new
                    {
                        ExceptionMessage = model.ExceptionMessage,
                        ExceptionType = model.ExceptionType,
                        ExceptionSource = model.ExceptionSource,
                        ExceptionData = model.ExceptionData,
                        Logdate = model.Logdate
                    }).Single();
            }

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ExceptionLog ReadById(int id)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ExceptionLog>(
                    "SELECT [ExceptionLogId], [ExceptionMessage], [ExceptionType], [ExceptionSource], [ExceptionData], [Logdate] FROM [dbo].[tblExceptionLog] WHERE [ExceptionLogId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ExceptionLog> ReadAll()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ExceptionLog>(
                    "SELECT [ExceptionLogId], [ExceptionMessage], [ExceptionType], [ExceptionSource], [ExceptionData], [Logdate] FROM [dbo].[tblExceptionLog]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(ExceptionLog model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"UPDATE [dbo].[tblExceptionLog]
                            SET [ExceptionMessage] = @ExceptionMessage, [ExceptionType] = @ExceptionType, [ExceptionSource] = @ExceptionSource, [ExceptionData] = @ExceptionData, [Logdate] = @Logdate
                            WHERE [ExceptionLogId] = @Id",
                    new
                    {
                        Id = model.ExceptionLogId,
                        ExceptionMessage = model.ExceptionMessage,
                        ExceptionType = model.ExceptionType,
                        ExceptionSource = model.ExceptionSource,
                        ExceptionData = model.ExceptionData,
                        Logdate = model.Logdate
                    });
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(ExceptionLog model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute("DELETE [dbo].[tblExceptionLog] WHERE [ExceptionLogId] = @ExceptionLogId",
                    new {model.ExceptionLogId});
            }
        }
    }
}