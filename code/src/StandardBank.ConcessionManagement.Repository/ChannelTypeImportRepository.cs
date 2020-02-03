using System;
using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.Common;
using StandardBank.ConcessionManagement.Model.Common;

namespace StandardBank.ConcessionManagement.Repository
{
    /// <summary>
    /// ChannelTypeImport repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IChannelTypeImportRepository" />
    public class ChannelTypeImportRepository : IChannelTypeImportRepository
    {
        /// <summary>
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// The cache manager
        /// </summary>
        private readonly ICacheManager _cacheManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelTypeImportRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        /// <param name="cacheManager">The cache manager.</param>
        public ChannelTypeImportRepository(IDbConnectionFactory dbConnectionFactory, ICacheManager cacheManager)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ChannelTypeImport Create(ChannelTypeImport model)
        {
            const string sql = @"INSERT [dbo].[rtblChannelTypeImport] ([fkChannelTypeId], [ImportFileChannel]) 
                                VALUES (@ChannelTypeId, @ImportFileChannel) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql, new {ChannelTypeId = model.ChannelTypeId, ImportFileChannel = model.ImportFileChannel}).Single();
            }

            //clear out the cache because the data has changed
            _cacheManager.Remove(CacheKey.Repository.ChannelTypeImportRepository.ReadAll);

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ChannelTypeImport ReadById(int id)
        {
            return ReadAll().FirstOrDefault(_ => _.Id == id);
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ChannelTypeImport> ReadAll()
        {
            Func<IEnumerable<ChannelTypeImport>> function = () =>
            {
                using (var db = _dbConnectionFactory.Connection())
            	{
                	return db.Query<ChannelTypeImport>(
                        @"SELECT [pkChannelTypeImportId] [Id], 
                                 [fkChannelTypeId] [ChannelTypeId], 
                                 [ImportFileChannel] 
                        FROM [dbo].[rtblChannelTypeImport]");
            	}
            };

            return _cacheManager.ReturnFromCache(function, 1440, CacheKey.Repository.ChannelTypeImportRepository.ReadAll);
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(ChannelTypeImport model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(
                    @"UPDATE [dbo].[rtblChannelTypeImport]
                    SET [fkChannelTypeId] = @ChannelTypeId, 
                        [ImportFileChannel] = @ImportFileChannel
                    WHERE [pkChannelTypeImportId] = @Id",
                    new {Id = model.Id, ChannelTypeId = model.ChannelTypeId, ImportFileChannel = model.ImportFileChannel});
            }

            //clear out the cache because the data has changed
            _cacheManager.Remove(CacheKey.Repository.ChannelTypeImportRepository.ReadAll);
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(ChannelTypeImport model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"DELETE [dbo].[rtblChannelTypeImport] 
                            WHERE [pkChannelTypeImportId] = @Id",
                    new {model.Id});
            }

            //clear out the cache because the data has changed
            _cacheManager.Remove(CacheKey.Repository.ChannelTypeImportRepository.ReadAll);
        }
    }
}
