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
    /// TransactionTableNumber repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.ITransactionTableNumberRepository" />
    public class TransactionTableNumberRepository : ITransactionTableNumberRepository
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
        /// Initializes a new instance of the <see cref="TransactionTableNumberRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        /// <param name="cacheManager">The cache manager.</param>
        public TransactionTableNumberRepository(IDbConnectionFactory dbConnectionFactory, ICacheManager cacheManager)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public TransactionTableNumber Create(TransactionTableNumber model)
        {
            const string sql = @"INSERT [dbo].[rtblTransactionTableNumber] ([fkTransactionTypeId], [TariffTable], [Fee], [AdValorem], [IsActive]) 
                                VALUES (@TransactionTypeId, @TariffTable, @Fee, @AdValorem, @IsActive) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql, new {TransactionTypeId = model.TransactionTypeId, TariffTable = model.TariffTable, Fee = model.Fee, AdValorem = model.AdValorem, IsActive = model.IsActive}).Single();
            }

            //clear out the cache because the data has changed
            _cacheManager.Remove(CacheKey.Repository.TransactionTableNumberRepository.ReadAll);

            return model;
        }



        public TransactionTableNumber CreateupdateTransactionTableNumber(TransactionTableNumber model)
        {

           
                if (model.Id == 0)
                {
                    const string sql =
                    @"INSERT [dbo].[rtblTransactionTableNumber] ([fkTransactionTypeId], [TariffTable], [Fee], [AdValorem],[IsActive]) 
                        VALUES (@fkTransactionTypeId, @TariffTable, @Fee, @AdValorem,@IsActive) 
                        SELECT CAST(SCOPE_IDENTITY() as int)";

                    using (var db = _dbConnectionFactory.Connection())
                    {
                    model.Id = db.Query<int>(sql,
                        new
                        {
                            fkTransactionTypeId = model.TransactionTypeId,
                            TariffTable = model.TariffTable,
                            Fee = model.Fee,
                            AdValorem = model.AdValorem,
                            IsActive = true
                            }).Single();
                    }
                }
                else
                {
                    const string sql =
                   @"Update [dbo].[rtblTransactionTableNumber] set [fkTransactionTypeId] = @fkTransactionTypeId , [TariffTable] =  @TariffTable, [Fee] = @Fee , [AdValorem] = @AdValorem, [IsActive] = @IsActive where pkTransactionTableNumberId = @pkTransactionTableNumberId";

                    using (var db = _dbConnectionFactory.Connection())
                    {
                        db.Execute(sql,
                           new
                           {
                               fkTransactionTypeId = model.TransactionTypeId,
                               TariffTable = model.TariffTable,
                               Fee = model.Fee,
                               AdValorem = model.AdValorem,
                               IsActive = model.IsActive,
                               pkTransactionTableNumberId = model.Id
                           });
                    }
                }

            //clear out the cache because the data has changed
            _cacheManager.Remove(CacheKey.Repository.TransactionTableNumberRepository.ReadAll);

            return model;

        }

        public TransactionType Create(TransactionType model)
        {
            const string sql = @"INSERT [dbo].[rtblTransactionType] ([fkConcessionTypeId], [Description], [IsActive]) 
                                VALUES (@fkConcessionTypeId, @Description, @IsActive) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql, new { fkConcessionTypeId = model.ConcessionTypeId, Description = model.Description, IsActive = true }).Single();
            }

            //clear out the cache because the data has changed
            _cacheManager.Remove(CacheKey.Repository.TransactionTypeRepository.ReadAll);

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public TransactionTableNumber ReadById(int id)
        {
            return ReadAll().FirstOrDefault(_ => _.Id == id);
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TransactionTableNumber> ReadAll()
        {
            Func<IEnumerable<TransactionTableNumber>> function = () =>
            {
                using (var db = _dbConnectionFactory.Connection())
            	{
                	return db.Query<TransactionTableNumber>("SELECT [pkTransactionTableNumberId] [Id], [fkTransactionTypeId] [TransactionTypeId], [TariffTable], [Fee], [AdValorem], [IsActive] FROM [dbo].[rtblTransactionTableNumber]");
            	}
            };

            return _cacheManager.ReturnFromCache(function, 1440, CacheKey.Repository.TransactionTableNumberRepository.ReadAll);
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(TransactionTableNumber model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"UPDATE [dbo].[rtblTransactionTableNumber]
                            SET [fkTransactionTypeId] = @TransactionTypeId, [TariffTable] = @TariffTable, [Fee] = @Fee, [AdValorem] = @AdValorem, [IsActive] = @IsActive
                            WHERE [pkTransactionTableNumberId] = @Id",
                    new {Id = model.Id, TransactionTypeId = model.TransactionTypeId, TariffTable = model.TariffTable, Fee = model.Fee, AdValorem = model.AdValorem, IsActive = model.IsActive});
            }

            //clear out the cache because the data has changed
            _cacheManager.Remove(CacheKey.Repository.TransactionTableNumberRepository.ReadAll);
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(TransactionTableNumber model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute("DELETE [dbo].[rtblTransactionTableNumber] WHERE [pkTransactionTableNumberId] = @Id",
                    new {model.Id});
            }

            //clear out the cache because the data has changed
            _cacheManager.Remove(CacheKey.Repository.TransactionTableNumberRepository.ReadAll);
        }
    }
}
