using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
            const string sql = @"INSERT [dbo].[tblConcessionLending] ([fkConcessionId], [fkProductTypeId], [Limit], [Term], [MarginToPrime], [ApprovedMarginToPrime], [InitiationFee], [ReviewFee], [UFFFee], [fkReviewFeeTypeId]) 
                                VALUES (@fkConcessionId, @fkProductTypeId, @Limit, @Term, @MarginToPrime, @ApprovedMarginToPrime, @InitiationFee, @ReviewFee, @UFFFee, @fkReviewFeeTypeId) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql,
                    new
                    {
                        fkConcessionId = model.ConcessionId,
                        fkProductTypeId = model.ProductTypeId,
                        Limit = model.Limit,
                        Term = model.Term,
                        MarginToPrime = model.MarginToPrime,
                        ApprovedMarginToPrime = model.ApprovedMarginToPrime,
                        InitiationFee = model.InitiationFee,
                        ReviewFee = model.ReviewFee,
                        UFFFee = model.UFFFee,
                        fkReviewFeeTypeId = model.ReviewFeeTypeId
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
                    "SELECT [pkConcessionLendingId] [Id], [fkConcessionId] [ConcessionId], [fkProductTypeId] [ProductTypeId], [Limit], [Term], [MarginToPrime], [ApprovedMarginToPrime], [InitiationFee], [ReviewFee], [UFFFee], [fkReviewFeeTypeId] [ReviewFeeTypeId] FROM [dbo].[tblConcessionLending] WHERE [pkConcessionLendingId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads by the concession id
        /// </summary>
        /// <param name="concessionId"></param>
        /// <returns></returns>
        public ConcessionLending ReadByConcessionId(int concessionId)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionLending>(
                    @"SELECT [pkConcessionLendingId] [Id], [fkConcessionId] [ConcessionId], [fkProductTypeId] [ProductTypeId], [Limit], [Term], [MarginToPrime], [ApprovedMarginToPrime], [InitiationFee], [ReviewFee], [UFFFee], [fkReviewFeeTypeId] [ReviewFeeTypeId] 
                    FROM [dbo].[tblConcessionLending] 
                    WHERE [fkConcessionId] = @concessionId",
                    new { concessionId }).FirstOrDefault();
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
                return db.Query<ConcessionLending>("SELECT [pkConcessionLendingId] [Id], [fkConcessionId] [ConcessionId], [fkProductTypeId] [ProductTypeId], [Limit], [Term], [MarginToPrime], [ApprovedMarginToPrime], [InitiationFee], [ReviewFee], [UFFFee], [fkReviewFeeTypeId] [ReviewFeeTypeId] FROM [dbo].[tblConcessionLending]");
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
                            SET [fkConcessionId] = @fkConcessionId, [fkProductTypeId] = @fkProductTypeId, [Limit] = @Limit, [Term] = @Term, [MarginToPrime] = @MarginToPrime, [ApprovedMarginToPrime] = @ApprovedMarginToPrime, [InitiationFee] = @InitiationFee, [ReviewFee] = @ReviewFee, [UFFFee] = @UFFFee, [fkReviewFeeTypeId] = @fkReviewFeeTypeId
                            WHERE [pkConcessionLendingId] = @Id",
                    new
                    {
                        Id = model.Id,
                        fkConcessionId = model.ConcessionId,
                        fkProductTypeId = model.ProductTypeId,
                        Limit = model.Limit,
                        Term = model.Term,
                        MarginToPrime = model.MarginToPrime,
                        ApprovedMarginToPrime = model.ApprovedMarginToPrime,
                        InitiationFee = model.InitiationFee,
                        ReviewFee = model.ReviewFee,
                        UFFFee = model.UFFFee,
                        fkReviewFeeTypeId = model.ReviewFeeTypeId
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
                db.Execute("DELETE [dbo].[tblConcessionLending] WHERE [pkConcessionLendingId] = @Id",
                    new {model.Id});
            }
        }
    }
}
