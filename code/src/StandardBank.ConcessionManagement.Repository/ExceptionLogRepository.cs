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
        /// The configuration data
        /// </summary>
        private readonly IConfigurationData _configurationData;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionLogRepository"/> class.
        /// </summary>
        /// <param name="configurationData">The configuration data.</param>
        public ExceptionLogRepository(IConfigurationData configurationData)
        {
            _configurationData = configurationData;
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

            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
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
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
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
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
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
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
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
            using (IDbConnection db = new SqlConnection(_configurationData.ConnectionString))
            {
                db.Execute("DELETE [dbo].[tblExceptionLog] WHERE [ExceptionLogId] = @ExceptionLogId",
                    new {model.ExceptionLogId});
            }
        }
    }
}