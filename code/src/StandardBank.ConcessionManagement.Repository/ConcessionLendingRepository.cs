using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.Common;

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
        /// Initializes a new instance of the <see cref="ConcessionLendingRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        public ConcessionLendingRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ConcessionLending Create(ConcessionLending model)
        {
            const string sql = @"INSERT [dbo].[tblConcessionLending] ([fkConcessionId], [fkConcessionDetailId], [fkProductTypeId], [fkReviewFeeTypeId], [Limit], [Term], [MarginToPrime], [ApprovedMarginToPrime], [LoadedMarginToPrime], [InitiationFee], [ReviewFee], [UFFFee], [AverageBalance]) 
                                VALUES (@ConcessionId, @ConcessionDetailId, @ProductTypeId, @ReviewFeeTypeId, @Limit, @Term, @MarginToPrime, @ApprovedMarginToPrime, @LoadedMarginToPrime, @InitiationFee, @ReviewFee, @UFFFee, @AverageBalance) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql, new {ConcessionId = model.ConcessionId, ConcessionDetailId = model.ConcessionDetailId, ProductTypeId = model.ProductTypeId, ReviewFeeTypeId = model.ReviewFeeTypeId, Limit = model.Limit, Term = model.Term, MarginToPrime = model.MarginToPrime, ApprovedMarginToPrime = model.ApprovedMarginToPrime, LoadedMarginToPrime = model.LoadedMarginToPrime, InitiationFee = model.InitiationFee, ReviewFee = model.ReviewFee, UFFFee = model.UFFFee, AverageBalance = model.AverageBalance}).Single();
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
                    "SELECT [pkConcessionLendingId] [Id], [fkConcessionId] [ConcessionId], [fkConcessionDetailId] [ConcessionDetailId], [fkProductTypeId] [ProductTypeId], [fkReviewFeeTypeId] [ReviewFeeTypeId], [Limit], [Term], [MarginToPrime], [ApprovedMarginToPrime], [LoadedMarginToPrime], [InitiationFee], [ReviewFee], [UFFFee], [AverageBalance] FROM [dbo].[tblConcessionLending] WHERE [pkConcessionLendingId] = @Id",
                    new {id}).SingleOrDefault();
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
                    @"SELECT [pkConcessionLendingId] [Id], [fkConcessionId] [ConcessionId], [fkConcessionDetailId] [ConcessionDetailId], [fkProductTypeId] [ProductTypeId], [fkReviewFeeTypeId] [ReviewFeeTypeId], [Limit], [Term], [MarginToPrime], [ApprovedMarginToPrime], [LoadedMarginToPrime], [InitiationFee], [ReviewFee], [UFFFee], [AverageBalance] 
                    FROM [dbo].[tblConcessionLending] 
                    WHERE [fkConcessionId] = @concessionId",
                    new {concessionId});
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
                return db.Query<ConcessionLending>("SELECT [pkConcessionLendingId] [Id], [fkConcessionId] [ConcessionId], [fkConcessionDetailId] [ConcessionDetailId], [fkProductTypeId] [ProductTypeId], [fkReviewFeeTypeId] [ReviewFeeTypeId], [Limit], [Term], [MarginToPrime], [ApprovedMarginToPrime], [LoadedMarginToPrime], [InitiationFee], [ReviewFee], [UFFFee], [AverageBalance] FROM [dbo].[tblConcessionLending]");
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
                db.Execute(@"UPDATE [dbo].[tblConcessionLending]
                            SET [fkConcessionId] = @ConcessionId, [fkConcessionDetailId] = @ConcessionDetailId, [fkProductTypeId] = @ProductTypeId, [fkReviewFeeTypeId] = @ReviewFeeTypeId, [Limit] = @Limit, [Term] = @Term, [MarginToPrime] = @MarginToPrime, [ApprovedMarginToPrime] = @ApprovedMarginToPrime, [LoadedMarginToPrime] = @LoadedMarginToPrime, [InitiationFee] = @InitiationFee, [ReviewFee] = @ReviewFee, [UFFFee] = @UFFFee, [AverageBalance] = @AverageBalance
                            WHERE [pkConcessionLendingId] = @Id",
                    new {Id = model.Id, ConcessionId = model.ConcessionId, ConcessionDetailId = model.ConcessionDetailId, ProductTypeId = model.ProductTypeId, ReviewFeeTypeId = model.ReviewFeeTypeId, Limit = model.Limit, Term = model.Term, MarginToPrime = model.MarginToPrime, ApprovedMarginToPrime = model.ApprovedMarginToPrime, LoadedMarginToPrime = model.LoadedMarginToPrime, InitiationFee = model.InitiationFee, ReviewFee = model.ReviewFee, UFFFee = model.UFFFee, AverageBalance = model.AverageBalance});
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
                db.Execute("DELETE [dbo].[tblConcessionLending] WHERE [pkConcessionLendingId] = @Id",
                    new {model.Id});
            }
        }
    }
}
