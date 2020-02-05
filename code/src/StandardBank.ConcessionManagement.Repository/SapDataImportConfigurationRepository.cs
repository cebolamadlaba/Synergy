using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.Common;

namespace StandardBank.ConcessionManagement.Repository
{
    /// <summary>
    /// SapDataImportConfiguration repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.ISapDataImportConfigurationRepository" />
    public class SapDataImportConfigurationRepository : ISapDataImportConfigurationRepository
    {
        /// <summary>
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="SapDataImportConfigurationRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        public SapDataImportConfigurationRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public SapDataImportConfiguration Create(SapDataImportConfiguration model)
        {
            const string sql = @"INSERT [dbo].[tblSapDataImportConfiguration] ([FileImportLocation], [FileExportLocation], [SupportEmailAddress]) 
                                VALUES (@FileImportLocation, @FileExportLocation, @SupportEmailAddress) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql, new {FileImportLocation = model.FileImportLocation, FileExportLocation = model.FileExportLocation, SupportEmailAddress = model.SupportEmailAddress}).Single();
            }

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public SapDataImportConfiguration ReadById(int id)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<SapDataImportConfiguration>(
                    @"SELECT [pkSapDataImportConfigurationId] [Id], 
                        [FileImportLocation], 
                        [FileExportLocation], 
                        [SupportEmailAddress] 
                    FROM [dbo].[tblSapDataImportConfiguration] 
                    WHERE [pkSapDataImportConfigurationId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SapDataImportConfiguration> ReadAll()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<SapDataImportConfiguration>(
                    @"SELECT [pkSapDataImportConfigurationId] [Id], 
                        [FileImportLocation], 
                        [FileExportLocation], 
                        [SupportEmailAddress] 
                    FROM [dbo].[tblSapDataImportConfiguration]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(SapDataImportConfiguration model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(
                    @"UPDATE [dbo].[tblSapDataImportConfiguration]
                    SET [FileImportLocation] = @FileImportLocation, 
                        [FileExportLocation] = @FileExportLocation, 
                        [SupportEmailAddress] = @SupportEmailAddress
                    WHERE [pkSapDataImportConfigurationId] = @Id",
                    new {Id = model.Id, FileImportLocation = model.FileImportLocation, FileExportLocation = model.FileExportLocation, SupportEmailAddress = model.SupportEmailAddress});
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(SapDataImportConfiguration model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"DELETE [dbo].[tblSapDataImportConfiguration] 
                            WHERE [pkSapDataImportConfigurationId] = @Id",
                    new {model.Id});
            }
        }
    }
}
