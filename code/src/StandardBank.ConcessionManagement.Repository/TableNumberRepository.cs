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
    /// TableNumber repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.ITableNumberRepository" />
    public class TableNumberRepository : ITableNumberRepository
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
        /// Initializes a new instance of the <see cref="TableNumberRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        /// <param name="cacheManager">The cache manager.</param>
        public TableNumberRepository(IDbConnectionFactory dbConnectionFactory, ICacheManager cacheManager)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public TableNumber Create(TableNumber model)
        {
            const string sql =
                @"INSERT [dbo].[rtblTableNumber] ([TariffTable], [AdValorem], [BaseRate], [IsActive], [fkConcessionTypeId]) 
                                VALUES (@TariffTable, @AdValorem, @BaseRate, @IsActive, @fkConcessionTypeId) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql,
                    new
                    {
                        TariffTable = model.TariffTable,
                        AdValorem = model.AdValorem,
                        BaseRate = model.BaseRate,
                        IsActive = model.IsActive,
                        fkConcessionTypeId = model.ConcessionTypeId
                    }).Single();
            }

            //clear out the cache because the data has changed
            _cacheManager.Remove(CacheKey.Repository.TableNumberRepository.ReadAll);

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public TableNumber ReadById(int id)
        {
            return ReadAll().FirstOrDefault(_ => _.Id == id);
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TableNumber> ReadAll()
        {
            Func<IEnumerable<TableNumber>> function = () =>
            {
                using (var db = _dbConnectionFactory.Connection())
                {
                    return db.Query<TableNumber>(
                        "SELECT [pkTableNumberId] [Id], [TariffTable], [AdValorem], [BaseRate], [IsActive], [fkConcessionTypeId] [ConcessionTypeId] FROM [dbo].[rtblTableNumber]");
                }
            };

            return _cacheManager.ReturnFromCache(function, 1440, CacheKey.Repository.TableNumberRepository.ReadAll);
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(TableNumber model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"UPDATE [dbo].[rtblTableNumber]
                            SET [TariffTable] = @TariffTable, [AdValorem] = @AdValorem, [BaseRate] = @BaseRate, [IsActive] = @IsActive, [fkConcessionTypeId] = @fkConcessionTypeId
                            WHERE [pkTableNumberId] = @Id",
                    new
                    {
                        Id = model.Id,
                        TariffTable = model.TariffTable,
                        AdValorem = model.AdValorem,
                        BaseRate = model.BaseRate,
                        IsActive = model.IsActive,
                        fkConcessionTypeId = model.ConcessionTypeId
                    });
            }

            //clear out the cache because the data has changed
            _cacheManager.Remove(CacheKey.Repository.TableNumberRepository.ReadAll);
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(TableNumber model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute("DELETE [dbo].[rtblTableNumber] WHERE [pkTableNumberId] = @Id",
                    new {model.Id});
            }

            //clear out the cache because the data has changed
            _cacheManager.Remove(CacheKey.Repository.TableNumberRepository.ReadAll);
        }
    }
}
