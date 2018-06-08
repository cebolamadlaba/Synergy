using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.Common;


namespace StandardBank.ConcessionManagement.Repository
{
    /// <summary>
    /// ConcessionTrade repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IConcessionTradeRepository" />
    public class ConcessionTradeRepository : IConcessionTradeRepository
    {
        /// <summary>
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// The concession detail repository
        /// </summary>
        private readonly IConcessionDetailRepository _concessionDetailRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcessionTradeRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The database connection factory.</param>
        /// <param name="concessionDetailRepository">The concession detail repository.</param>
        public ConcessionTradeRepository(IDbConnectionFactory dbConnectionFactory,
            IConcessionDetailRepository concessionDetailRepository)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _concessionDetailRepository = concessionDetailRepository;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ConcessionTrade Create(ConcessionTrade model)
        {
            try
            {
                var concessionDetail = _concessionDetailRepository.Create(model);
                model.ConcessionDetailId = concessionDetail.ConcessionDetailId;

                const string sql =
                    @"INSERT [dbo].[tblConcessionTrade] ([fkConcessionId], [fkConcessionDetailId],[fkTradeProductId], [fkLegalEntityAccountId],
                                                    [LoadedRate], [ApprovedRate], [GBBNumber], [Term],[Min],[Max],[Communication],[FlatFee],[EstablishmentFee],[AdValorem],[Currency]) 
                VALUES (@fkConcessionId, @fkConcessionDetailId, @fkTradeProductId, @fkLegalEntityAccountId, @LoadedRate, @ApprovedRate, @GBBNumber, @Term, @Min,@Max,@Communication,@FlatFee,@EstablishmentFee,@AdValorem,@Currency) 
                SELECT CAST(SCOPE_IDENTITY() as int)";

                using (var db = _dbConnectionFactory.Connection())
                {
                    model.Id = db.Query<int>(sql,
                        new
                        {
                            fkConcessionId = model.ConcessionId,
                            fkConcessionDetailId = model.ConcessionDetailId,
                            fkTradeProductId = model.fkTradeProductId,
                            fkLegalEntityAccountId = model.LegalEntityAccountId,
                            LoadedRate = model.LoadedRate,
                            ApprovedRate = model.ApprovedRate,
                            GBBNumber = model.GBBNumber,
                            Term = model.term,
                            Min = model.min,
                            Max = model.max,
                            Communication = model.Communication,
                            FlatFee = model.FlatFee,
                            EstablishmentFee = model.EstablishmentFee,
                            AdValorem = model.AdValorem,
                            Currency = model.Currency

                        }).Single();
                }

                return model;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ConcessionTrade ReadById(int id)
        {
            using (var db = _dbConnectionFactory.Connection())
            {

                return db.Query<ConcessionTrade>(
                    @"SELECT [pkConcessionTradeId] [Id], t.[fkConcessionId] [ConcessionId],d.[fkLegalEntityAccountId] [LegalEntityAccountId], d.[ExpiryDate], [fkConcessionDetailId],[fkTradeProductId],
                                                    [LoadedRate], [ApprovedRate], [GBBNumber], [Term],[Min],[Max],[Communication],[FlatFee],[EstablishmentFee],[AdValorem],[Currency] 
                    FROM [dbo].[tblConcessionTrade] t
                    JOIN [dbo].[tblConcessionDetail] d ON d.[pkConcessionDetailId] = t.[fkConcessionDetailId]
                    WHERE [pkConcessionTradeId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ConcessionTrade> ReadAll()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionTrade>(
                    @"SELECT[pkConcessionTradeId] [Id], t.[fkConcessionId] [ConcessionId],d.[fkLegalEntityAccountId] [LegalEntityAccountId], d.[ExpiryDate], [fkConcessionDetailId],[fkTradeProductId],
                                                    [LoadedRate], [ApprovedRate], [GBBNumber], [Term],[Min],[Max],[Communication],[FlatFee],[EstablishmentFee],[AdValorem],[Currency] 
                    FROM [dbo].[tblConcessionTrade] t
                    JOIN [dbo].[tblConcessionDetail] d ON d.[pkConcessionDetailId] = t.[fkConcessionDetailId]");
            }
        }


        public IEnumerable<TradeProduct> GetTradeProducts()
        {
            using (var db = _dbConnectionFactory.Connection())
            {

                return db.Query<TradeProduct>(
                    @"SELECT description [tradeProductName],  pkTradeProductId [tradeProductId], fkTradeProductTypeId [tradeProductTypeId]  from rtblTradeProduct");
            }
        }

        public IEnumerable<TradeProductType> GetTradeProductTypes()
        {
            using (var db = _dbConnectionFactory.Connection())
            {

                return db.Query<TradeProductType>(
                    @"SELECT pkTradeProductTypeId [tradeProductTypeID], description [tradeProductType] from rtblTradeProductType");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(ConcessionTrade model)
        {
            try
            {
                using (var db = _dbConnectionFactory.Connection())
                {
                    const string sql =
           @"UPDATE [dbo].[tblConcessionTrade] set [fkConcessionId] = @fkConcessionId, [fkConcessionDetailId] = @fkConcessionDetailId,[fkTradeProductId] = @fkTradeProductId, [fkLegalEntityAccountId] = @fkLegalEntityAccountId,
                                                    [LoadedRate] = @LoadedRate, [ApprovedRate] = @ApprovedRate, [GBBNumber] = @GBBNumber, [Term] = @Term,[Min] = @Min,[Max] = @Max,[Communication] = @Communication,[FlatFee] = @FlatFee,[EstablishmentFee] = @EstablishmentFee,[AdValorem] = @AdValorem,[Currency] = @Currency where
                pkConcessionTradeId =  @Id
               ";

                    db.Execute(sql, new
                    {
                        @Id = model.Id,
                        fkConcessionId = model.ConcessionId,
                        fkConcessionDetailId = model.ConcessionDetailId,
                        fkTradeProductId = model.fkTradeProductId,
                        fkLegalEntityAccountId = model.LegalEntityAccountId,
                        LoadedRate = model.LoadedRate,
                        ApprovedRate = model.ApprovedRate,
                        GBBNumber = model.GBBNumber,
                        Term = model.term,
                        Min = model.min,
                        Max = model.max,
                        Communication = model.Communication,
                        FlatFee = model.FlatFee,
                        EstablishmentFee = model.EstablishmentFee,
                        AdValorem = model.AdValorem,
                        Currency = model.Currency

                    });
                }

                _concessionDetailRepository.Update(model);
            }
            catch (System.Exception ex)
            {

                throw;
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(ConcessionTrade model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute("DELETE [dbo].[tblConcessionTrade] WHERE [pkConcessionTradeId] = @Id",
                    new {model.Id});
            }

            _concessionDetailRepository.Delete(model);
        }
    }
}