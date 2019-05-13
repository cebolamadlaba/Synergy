using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.Common;

namespace StandardBank.ConcessionManagement.Repository
{
    /// <summary>
    /// UserRole repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IRiskGroupNonUniversalChargeCodeRepository" />
    public class RiskGroupNonUniversalChargeCodeRepository : IRiskGroupNonUniversalChargeCodeRepository
    {
        /// <summary>
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="RiskGroupNonUniversalChargeCodeRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        public RiskGroupNonUniversalChargeCodeRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public void Create(int RiskGroupId, int ChargeCodeId)
        {

            const string sql = @"INSERT [dbo].[tblRiskGroupNonUniversalChargeCode] ([RiskGroupId], [ChargeCodeId]) 
                                VALUES (@RiskGroupId, @ChargeCodeId) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
              var model = db.Query<int>(sql,
                    new { RiskGroupId = RiskGroupId, ChargeCodeId = ChargeCodeId }).Single();
            }

      
        }

        /// <summary>
        /// Reads by the Charge Code Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<RiskGroupNonUniversalChargeCode> ReadByChargeCodeId(int chargeCodeId)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<RiskGroupNonUniversalChargeCode>(
                    @"SELECT [Id], [RiskGroupId], [ChargeCodeId], [IsActive] 
                    FROM [dbo].[tblRiskGroupNonUniversalChargeCode] 
                    WHERE [ChargeCodeId] = @chargeCodeId",
                    new { chargeCodeId });
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="chargeCodeId">The chargeCodeId.</param>
        public void Delete(int chargeCodeId)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute("DELETE [dbo].[tblRiskGroupNonUniversalChargeCode] WHERE [ChargeCodeId] = @chargeCodeId",
                    new { chargeCodeId });
            }
        }

    }
}