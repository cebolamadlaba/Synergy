using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.Common;

namespace StandardBank.ConcessionManagement.Repository
{
    /// <summary>
    /// ConcessionInvestment repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IConcessionInvestmentRepository" />
    public class ConcessionInvestmentRepository : IConcessionInvestmentRepository
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
        /// Initializes a new instance of the <see cref="ConcessionInvestmentRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The database connection factory.</param>
        /// <param name="concessionDetailRepository">The concession detail repository.</param>
        public ConcessionInvestmentRepository(IDbConnectionFactory dbConnectionFactory,
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
        public ConcessionInvestment Create(ConcessionInvestment model)
        {
            try
            {
                var concessionDetail = _concessionDetailRepository.Create(model);
                model.ConcessionDetailId = concessionDetail.ConcessionDetailId;

                const string sql =
                    @"INSERT [dbo].[tblConcessionInvestment] ([fkConcessionId], [fkConcessionDetailId], [fkProductId],[fkLegalEntityAccountId], [Balance], [Term], [LoadedRate]) 
                    VALUES (@ConcessionId, @ConcessionDetailId, @fkProductId, @fkLegalEntityAccountId,  @Balance, @Term, @LoadedRate) 
                    SELECT CAST(SCOPE_IDENTITY() as int)";

                using (var db = _dbConnectionFactory.Connection())
                {
                    model.Id = db.Query<int>(sql,
                        new
                        {
                            ConcessionId = model.ConcessionId,
                            ConcessionDetailId = model.ConcessionDetailId,
                            fkProductId = model.ProductTypeId,
                            fkLegalEntityAccountId = model.LegalEntityAccountId,
                            Balance = model.Balance,
                            Term = model.Term,
                            LoadedRate = model.LoadedRate

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
        public ConcessionInvestment ReadById(int id)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionInvestment>(
                    @"SELECT [pkConcessionInvestmentId] [Id],
		                    t.[fkConcessionId] [ConcessionId],
		                    [fkConcessionDetailId] [ConcessionDetailId],
		                    [fkProductId],
		                    d.[fkLegalEntityAccountId],
		                    [Balance],
		                    [Term],
		                    [LoadedRate],
		                    [ApprovedRate],
		                    d.[fkLegalEntityId] [LegalEntityId],
		                    d.[fkLegalEntityAccountId] [LegalEntityAccountId],
		                    d.[ExpiryDate] 
                    FROM [dbo].[tblConcessionInvestment] t
                    JOIN [dbo].[tblConcessionDetail] d ON d.[pkConcessionDetailId] = t.[fkConcessionDetailId]
                    WHERE t.[fkConcessionId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ConcessionInvestment> ReadAll()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionInvestment>(
                    @"Select [pkConcessionInvestmentId] [Id],
		                    t.[fkConcessionId] [ConcessionId],
		                    [fkConcessionDetailId] [ConcessionDetailId],
		                    [fkProductId],
		                    d.[fkLegalEntityAccountId],
		                    [Balance],
		                    [Term],
		                    [LoadedRate],
		                    [ApprovedRate],
		                    d.[fkLegalEntityId] [LegalEntityId],
		                    d.[fkLegalEntityAccountId] [LegalEntityAccountId],
		                    d.[ExpiryDate] 
                    FROM [dbo].[tblConcessionInvestment] t
                    JOIN [dbo].[tblConcessionDetail] d ON d.[pkConcessionDetailId] = t.[fkConcessionDetailId]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(ConcessionInvestment model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(
                    @"UPDATE [dbo].[tblConcessionInvestment]
                    SET [fkConcessionId] = @ConcessionId,
                        [fkConcessionDetailId] = @ConcessionDetailId,
                        [fkProductId] = @fkProductId,
                        [fkLegalEntityAccountId] = @fkLegalEntityAccountId,
                        [Balance] = @Balance,
                        [Term] = @Term,
                        LoadedRate = @LoadedRate,
                        ApprovedRate = @ApprovedRate
                    WHERE [pkConcessionInvestmentId] = @Id",
                    new
                    {
                        Id = model.Id,
                        ConcessionId = model.ConcessionId,
                        ConcessionDetailId = model.ConcessionDetailId,
                        fkProductId = model.ProductTypeId,
                        fkLegalEntityAccountId = model.LegalEntityAccountId,
                        Balance = model.Balance,
                        Term = model.Term,
                        LoadedRate = model.LoadedRate,
                        ApprovedRate = model.ApprovedRate

                    });
            }

            _concessionDetailRepository.Update(model);
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(ConcessionInvestment model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"DELETE [dbo].[tblConcessionInvestment] 
                            WHERE [pkConcessionInvestmentId] = @Id",
                    new {model.Id});
            }

            _concessionDetailRepository.Delete(model);
        }

        public IEnumerable<InvestmentProduct> GetInvestmentProducts()
        {
            using (var db = _dbConnectionFactory.Connection())
            {

                return db.Query<InvestmentProduct>(
                    @"SELECT description [tradeProductName],
		                    pkTradeProductId [tradeProductId],
		                    fkTradeProductTypeId [tradeProductTypeId]  
                    from rtblTradeProduct");
            }
        }
    }
}
