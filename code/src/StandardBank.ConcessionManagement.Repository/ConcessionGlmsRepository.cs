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
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IConcessionGlmsRepository" />
    public class ConcessionGlmsRepository : IConcessionGlmsRepository
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
        /// Initializes a new instance of the <see cref="ConcessionGlmsRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The database connection factory.</param>
        /// <param name="concessionDetailRepository">The concession detail repository.</param>
        public ConcessionGlmsRepository(IDbConnectionFactory dbConnectionFactory,
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
        public ConcessionGlms Create(ConcessionGlms model)
        {
             var concessionDetail = _concessionDetailRepository.Create(model);
                model.ConcessionDetailId = concessionDetail.ConcessionDetailId;

                const string sql =
                    @"INSERT [dbo].[tblConcessionGlms] ([fkConcessionId], [fkConcessionDetailId], [fkGroupId], [fkInterestPricingCategoryId],[fkSlabTypeId],[fkInterestTypeId]) 
                    VALUES (@fkConcessionId, @fkConcessionDetailId, @fkGroupId, @fkInterestPricingCategoryId,@fkSlabTypeId,@fkInterestTypeId) 
                    SELECT CAST(SCOPE_IDENTITY() as int)";

                using (var db = _dbConnectionFactory.Connection())
                {
                    model.Id = db.Query<int>(sql,
                        new
                        {
                            fkConcessionId = model.ConcessionId,
                            fkConcessionDetailId = model.ConcessionDetailId,
                            fkGroupId = model.GlmsGroupId,
                            fkSlabTypeId = model.SlabTypeId,
                            fkInterestPricingCategoryId = model.InterestPricingCategoryId,
                            fkInterestTypeId = model.InterestTypeId       

                        }).Single();
                }

                return model;          
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ConcessionGlms ReadById(int id)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionGlms>(
                    @"SELECT [pkConcessionGlmsId] [Id],
                             t.[fkConcessionId] [ConcessionId],
                             [fkConcessionDetailId] [ConcessionDetailId],
                             d.[fkLegalEntityId] [LegalEntityId],
                             d.[ExpiryDate] 
                    FROM [dbo].[tblConcessionGlms] t
                      JOIN [dbo].[tblConcessionDetail] d ON d.[pkConcessionDetailId] = t.[fkConcessionDetailId]
                    WHERE t.[pkConcessionGlmsId] = @Id",
                    new { id }).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ConcessionGlms> ReadAll()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionGlms>(
                    @"Select [pkConcessionGlmsId] [Id],
                             t.[fkConcessionId] [ConcessionId],
                             [fkConcessionDetailId] [ConcessionDetailId],
                             d.[fkLegalEntityId] [LegalEntityId],
                             d.[ExpiryDate] 
                    FROM [dbo].[tblConcessionGlms] t
                    JOIN [dbo].[tblConcessionDetail] d ON d.[pkConcessionDetailId] = t.[fkConcessionDetailId]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(ConcessionGlms model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(
                    @"UPDATE [dbo].[tblConcessionGlms]
                    SET [fkConcessionId] = @ConcessionId,
                        [fkConcessionDetailId] = @ConcessionDetailId, 
                        [fkGroupId] = @GlmsGroupId,
                        [fkInterestPricingCategoryId] = @InterestPricingCategoryId,
                        [fkSlabTypeId] = @SlabTypeId,
                        [fkInterestTypeId] = @InterestTypeId,
                        [fkArchiveTypeId] = @ArchiveTypeId
                    WHERE [fkConcessionId] = @ConcessionId AND [fkConcessionDetailId] = @ConcessionDetailId",
                    new
                    {
                        model.Id,
                        model.ConcessionId,
                        model.ConcessionDetailId,
                        model.GlmsGroupId,
                        model.SlabTypeId,
                        model.InterestPricingCategoryId,
                        model.InterestTypeId,
                        model.ArchiveTypeId
                    });
            }

            _concessionDetailRepository.Update(model);
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(ConcessionGlms model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"DELETE [dbo].[tblConcessionGlms] 
                            WHERE [pkConcessionGlmsId] = @Id",
                    new { model.Id });
            }

            _concessionDetailRepository.Delete(model);
        }

        public IEnumerable<GlmsProduct> GetGlmsProducts()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<GlmsProduct>(
                    @"SELECT description [tradeProductName], 
                             pkTradeProductId [tradeProductId], 
                             fkTradeProductTypeId [tradeProductTypeId]  
                    from rtblTradeProduct");
            }
        }
    }
}
