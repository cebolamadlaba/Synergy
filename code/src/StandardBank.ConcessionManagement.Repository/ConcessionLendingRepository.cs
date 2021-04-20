using Dapper;
using StandardBank.ConcessionManagement.Interface.Common;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StandardBank.ConcessionManagement.Repository
{
    /// <summary>
    /// ConcessionLending repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IConcessionLendingRepository" />
    public class ConcessionLendingRepository : IConcessionLendingRepository
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
        /// Initializes a new instance of the <see cref="ConcessionLendingRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The database connection factory.</param>
        /// <param name="concessionDetailRepository">The concession detail repository.</param>
        public ConcessionLendingRepository(IDbConnectionFactory dbConnectionFactory,
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
        public ConcessionLending Create(ConcessionLending model)
        {
            var concessionDetail = _concessionDetailRepository.Create(model);
            model.ConcessionDetailId = concessionDetail.ConcessionDetailId;

            const string sql =
                @"INSERT [dbo].[tblConcessionLending] ([fkConcessionId], [fkConcessionDetailId], [fkProductTypeId], [fkReviewFeeTypeId], [Limit], [Term], [MarginToPrime], [ApprovedMarginToPrime], [LoadedMarginToPrime], [InitiationFee], [ReviewFee], [UFFFee], [AverageBalance],[Frequency],[ServiceFee],[MRS_ERI],[ExtensionFee])
                VALUES (@ConcessionId, @ConcessionDetailId, @ProductTypeId, @ReviewFeeTypeId, @Limit, @Term, @MarginToPrime, @ApprovedMarginToPrime, @LoadedMarginToPrime, @InitiationFee, @ReviewFee, @UFFFee, @AverageBalance, @Frequency,@ServiceFee,@MRS_ERI,@ExtensionFee)
                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql,
                    new
                    {
                        ConcessionId = model.ConcessionId,
                        ConcessionDetailId = model.ConcessionDetailId,
                        ProductTypeId = model.ProductTypeId,
                        ReviewFeeTypeId = model.ReviewFeeTypeId,
                        Limit = model.Limit,
                        Term = model.Term,
                        MarginToPrime = model.MarginToPrime,
                        ApprovedMarginToPrime = model.ApprovedMarginToPrime,
                        LoadedMarginToPrime = model.LoadedMarginToPrime,
                        InitiationFee = model.InitiationFee,
                        ReviewFee = model.ReviewFee,
                        UFFFee = model.UFFFee,
                        AverageBalance = model.AverageBalance,
                        ServiceFee = model.ServiceFee,
                        Frequency = model.Frequency,
                        MRS_ERI = model.MRS_ERI,
                        ExtensionFee = model.ExtensionFee
                    }).Single();
            }

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ConcessionLending ReadById(int id)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionLending>(
                    @"SELECT [pkConcessionLendingId] [Id],
		                    t.[fkConcessionId] [ConcessionId],
		                    [fkConcessionDetailId] [ConcessionDetailId],
		                    [fkProductTypeId] [ProductTypeId],
		                    [fkReviewFeeTypeId] [ReviewFeeTypeId],
		                    [Limit],
		                    [Term],
		                    [MarginToPrime],
		                    [ApprovedMarginToPrime],
		                    [LoadedMarginToPrime],
		                    [InitiationFee],
		                    [ReviewFee],
		                    [UFFFee],
		                    [AverageBalance],
		                    [Frequency],
		                    [ServiceFee],
		                    d.[fkLegalEntityId] [LegalEntityId],
		                    d.[fkLegalEntityAccountId] [LegalEntityAccountId],
		                    d.[ExpiryDate],
		                    d.[DateApproved],
		                    [MRS_ERI],
                            [ExtensionFee]
                    FROM [dbo].[tblConcessionLending] t
                    JOIN [dbo].[tblConcessionDetail] d ON d.[pkConcessionDetailId] = t.[fkConcessionDetailId]
                    WHERE [pkConcessionLendingId] = @Id",
                    new { id }).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads by the concession id
        /// </summary>
        /// <param name="concessionId"></param>
        /// <returns></returns>
        public IEnumerable<ConcessionLending> ReadByConcessionId(int concessionId)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionLending>(
                    @"SELECT [pkConcessionLendingId] [Id],
		                    t.[fkConcessionId] [ConcessionId],
		                    [fkConcessionDetailId] [ConcessionDetailId],
		                    [fkProductTypeId] [ProductTypeId],
		                    [fkReviewFeeTypeId] [ReviewFeeTypeId],
		                    [Limit],
		                    [Term],
		                    [MarginToPrime],
		                    [ApprovedMarginToPrime],
		                    [LoadedMarginToPrime],
		                    [InitiationFee],
		                    [ReviewFee],
		                    [UFFFee],
		                    [AverageBalance],
		                    [Frequency],
		                    [ServiceFee],
		                    d.[fkLegalEntityId] [LegalEntityId],
		                    d.[fkLegalEntityAccountId] [LegalEntityAccountId],
		                    d.[ExpiryDate],
		                    d.[DateApproved],
		                    [MRS_ERI],
                            [ExtensionFee]
                    FROM [dbo].[tblConcessionLending] t
                    JOIN [dbo].[tblConcessionDetail] d ON d.[pkConcessionDetailId] = t.[fkConcessionDetailId]
                    WHERE t.[fkConcessionId] = @concessionId",
                    new { concessionId });
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ConcessionLending> ReadAll()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionLending>(
                    @"SELECT [pkConcessionLendingId] [Id],
		                    t.[fkConcessionId] [ConcessionId],
		                    [fkConcessionDetailId] [ConcessionDetailId],
		                    [fkProductTypeId] [ProductTypeId],
		                    [fkReviewFeeTypeId] [ReviewFeeTypeId],
		                    [Limit],
		                    [Term],
		                    [MarginToPrime],
		                    [ApprovedMarginToPrime],
		                    [LoadedMarginToPrime],
		                    [InitiationFee],
		                    [ReviewFee],
		                    [UFFFee],
		                    [AverageBalance],
		                    [Frequency],
		                    [ServiceFee],
		                    d.[fkLegalEntityId] [LegalEntityId],
		                    d.[fkLegalEntityAccountId] [LegalEntityAccountId],
		                    d.[ExpiryDate],
		                    d.[DateApproved],
		                    [MRS_ERI],
                            [ExtensionFee]
                    FROM [dbo].[tblConcessionLending] t
                    JOIN [dbo].[tblConcessionDetail] d ON d.[pkConcessionDetailId] = t.[fkConcessionDetailId]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(ConcessionLending model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(
                    @"UPDATE [dbo].[tblConcessionLending]
                    SET [fkConcessionId] = @ConcessionId,
	                    [fkConcessionDetailId] = @ConcessionDetailId,
	                    [fkProductTypeId] = @ProductTypeId,
	                    [fkReviewFeeTypeId] = @ReviewFeeTypeId,
	                    [Limit] = @Limit,
	                    [Term] = @Term,
	                    [MarginToPrime] = @MarginToPrime,
	                    [ApprovedMarginToPrime] = @ApprovedMarginToPrime,
	                    [LoadedMarginToPrime] = @LoadedMarginToPrime,
	                    [InitiationFee] = @InitiationFee,
	                    [ReviewFee] = @ReviewFee,
	                    [UFFFee] = @UFFFee,
	                    [AverageBalance] = @AverageBalance,
	                    [ServiceFee] = @ServiceFee,
	                    [Frequency] = @Frequency,
	                    [MRS_ERI] = @MRS_ERI,
                        [ExtensionFee] = @ExtensionFee
                    WHERE [pkConcessionLendingId] = @Id",
                    new
                    {
                        Id = model.Id,
                        ConcessionId = model.ConcessionId,
                        ConcessionDetailId = model.ConcessionDetailId,
                        ProductTypeId = model.ProductTypeId,
                        ReviewFeeTypeId = model.ReviewFeeTypeId,
                        Limit = model.Limit,
                        Term = model.Term,
                        MarginToPrime = model.MarginToPrime,
                        ApprovedMarginToPrime = model.ApprovedMarginToPrime,
                        LoadedMarginToPrime = model.LoadedMarginToPrime,
                        InitiationFee = model.InitiationFee,
                        ReviewFee = model.ReviewFee,
                        UFFFee = model.UFFFee,
                        AverageBalance = model.AverageBalance,
                        ServiceFee = model.ServiceFee,
                        Frequency = model.Frequency,
                        MRS_ERI = model.MRS_ERI,
                        ExtensionFee = model.ExtensionFee
                    });
            }

            _concessionDetailRepository.Update(model);
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void UpdateMarginToPrime(int Id, decimal MarginToPrime, decimal ApprovedMarginToPrime)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(
                    @"UPDATE [dbo].[tblConcessionLending]
                    SET [MarginToPrime] = @MarginToPrime,
	                    [ApprovedMarginToPrime] = @ApprovedMarginToPrime
                    WHERE [pkConcessionLendingId] = @Id",
                    new
                    {
                        Id = Id,
                        MarginToPrime = MarginToPrime,
                        ApprovedMarginToPrime = ApprovedMarginToPrime,

                    });

            }
   
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(ConcessionLending model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"DELETE [dbo].[tblConcessionLending] 
                            WHERE [pkConcessionLendingId] = @Id",
                    new { model.Id });
            }

            _concessionDetailRepository.Delete(model);
        }
    }
}