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
                @"INSERT [dbo].[rtblTableNumber] ([fkConcessionTypeId], [TariffTable], [AdValorem], [BaseRate], [IsActive]) 
                                VALUES (@ConcessionTypeId, @TariffTable, @AdValorem, @BaseRate, @IsActive) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql,
                    new
                    {
                        ConcessionTypeId = model.ConcessionTypeId,
                        TariffTable = model.TariffTable,
                        AdValorem = model.AdValorem,
                        BaseRate = model.BaseRate,
                        IsActive = model.IsActive
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
                        "SELECT [pkTableNumberId] [Id], [fkConcessionTypeId] [ConcessionTypeId], [TariffTable], [AdValorem], [BaseRate], [IsActive] FROM [dbo].[rtblTableNumber] where ActiveUntil is null");
                }
            };

            return _cacheManager.ReturnFromCache(function, 1440, CacheKey.Repository.TableNumberRepository.ReadAll);
        }

        public IEnumerable<TableNumber> ReadAll(string ConcessionType, int ConcessionTypeId)
        {
            Func<IEnumerable<TableNumber>> function = () =>
            {
                using (var db = _dbConnectionFactory.Connection())
                {
                    //disregard standard pricing
                    if (ConcessionType.ToLower() == "cash")
                    {
                        return db.Query<TableNumber>(
                            "SELECT [pkTableNumberId] [Id], [fkConcessionTypeId] [ConcessionTypeId], [TariffTable], [AdValorem], [BaseRate], [IsActive] FROM [dbo].[rtblTableNumber] where TariffTable not in (select StandardPricingTable from rtblChannelType where StandardPricingTable is not null)");
                   
                    }
                    //disregard standard pricing
                    else if (ConcessionType.ToLower() == "transactional")
                    {
                        return db.Query<TableNumber>(
                                             string.Format("SELECT [pkTableNumberId] [Id], [fkConcessionTypeId] [ConcessionTypeId], [TariffTable], [AdValorem], [BaseRate], [IsActive] FROM [dbo].[rtblTableNumber]  where TariffTable not in (select StandardPricingTable from rtblTransactionType where fkConcessionTypeId = {0} and StandardPricingTable is not null)", ConcessionTypeId));


                    }
                    else
                    {

                        return db.Query<TableNumber>(
                            "SELECT [pkTableNumberId] [Id], [fkConcessionTypeId] [ConcessionTypeId], [TariffTable], [AdValorem], [BaseRate], [IsActive] FROM [dbo].[rtblTableNumber]");
                    }

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
            try
            {
                using (var db = _dbConnectionFactory.Connection())
                {
                    db.Execute(@"if not exists(Select [fkConcessionTypeId],TariffTable, AdValorem, BaseRate, isactive from rtblTableNumber where pkTableNumberId = @pkTableNumberId and TariffTable = @TariffTable and AdValorem =  @AdValorem and BaseRate = @BaseRate)
                                    INSERT [dbo].[rtblTableNumber] ([fkConcessionTypeId], [TariffTable], [AdValorem], [BaseRate],[IsActive],[ActiveUntil]) 
                                        Select [fkConcessionTypeId], [TariffTable], [AdValorem], [BaseRate], 0, @ActiveUntil from rtblTableNumber where pkTableNumberId = @pkTableNumberId;

                            UPDATE [dbo].[rtblTableNumber]
                            SET [fkConcessionTypeId] = @ConcessionTypeId, [TariffTable] = @TariffTable, [AdValorem] = @AdValorem, [BaseRate] = @BaseRate, [IsActive] = @IsActive
                            WHERE [pkTableNumberId] = @pkTableNumberId",
                        new
                        {
                            pkTableNumberId = model.Id,
                            ConcessionTypeId = model.ConcessionTypeId,
                            TariffTable = model.TariffTable,
                            AdValorem = model.AdValorem,
                            BaseRate = model.BaseRate,
                            IsActive = model.IsActive,
                            ActiveUntil = DateTime.Now
                        });
                }
            }
            catch (Exception ex)
            {

                throw ex;
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
                    new { model.Id });
            }

            //clear out the cache because the data has changed
            _cacheManager.Remove(CacheKey.Repository.TableNumberRepository.ReadAll);
        }
    }
}
