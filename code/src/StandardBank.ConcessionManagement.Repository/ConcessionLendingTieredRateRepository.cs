using Dapper;
using StandardBank.ConcessionManagement.Interface.Common;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
using System.Linq;

namespace StandardBank.ConcessionManagement.Repository
{
    public class ConcessionLendingTieredRateRepository : IConcessionLendingTieredRateRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public ConcessionLendingTieredRateRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public ConcessionLendingTieredRate Create(ConcessionLendingTieredRate model)
        {
            const string sql =
                @"INSERT INTO [dbo].[tblConcessionLendingTieredRate]([fkConcessionLendingId],[Limit],[MarginToPrime],[ApprovedMarginToPrime])
                    VALUES(@ConcessionLendingId, @Limit, @MarginToPrime, @ApprovedMarginToPrime)
                    SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql,
                    new
                    {
                        model.ConcessionLendingId,
                        model.Limit,
                        model.MarginToPrime,
                        model.ApprovedMarginToPrime
                    }).Single();
            }

            return model;
        }

        public ConcessionLendingTieredRate ReadById(int id)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionLendingTieredRate>(
                    @"SELECT [pkConcessionLendingTieredRateId] [Id]
                            ,[fkConcessionLendingId] [ConcessionLendingId]
                            ,[Limit]
                            ,[MarginToPrime]
                            ,[ApprovedMarginToPrime]
                        FROM [tblConcessionLendingTieredRate]
                        Where [pkConcessionLendingTieredRateId] = @Id",
                    new { id }).SingleOrDefault();
            }
        }

        public IEnumerable<ConcessionLendingTieredRate> ReadByConcessionLendingId(int concessionLendingId)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionLendingTieredRate>(
                    @"SELECT [pkConcessionLendingTieredRateId] [Id]
                            ,[fkConcessionLendingId] [ConcessionLendingId]
                            ,[Limit]
                            ,[MarginToPrime]
                            ,[ApprovedMarginToPrime]
                        FROM [tblConcessionLendingTieredRate]
                        Where [fkConcessionLendingId] = @concessionLendingId",
                    new { concessionLendingId });
            }
        }

        public IEnumerable<ConcessionLendingTieredRate> ReadAll()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionLendingTieredRate>(
                    @"SELECT [pkConcessionLendingTieredRateId] [Id]
                            ,[fkConcessionLendingId] [ConcessionLendingId]
                            ,[Limit]
                            ,[MarginToPrime]
                            ,[ApprovedMarginToPrime]
                        FROM [tblConcessionLendingTieredRate]");
            }
        }

        public void Update(ConcessionLendingTieredRate model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(
                    @"UPDATE [dbo].[tblConcessionLendingTieredRate]
                        SET [fkConcessionLendingId] = @ConcessionLendingId
	                        ,[Limit] = @Limit
	                        ,[MarginToPrime] = @MarginToPrime
                            ,[ApprovedMarginToPrime] = @ApprovedMarginToPrime
                        WHERE pkConcessionLendingTieredRateId = @Id",
                    new
                    {
                        model.Id,
                        model.ConcessionLendingId,
                        model.Limit,
                        model.MarginToPrime,
                        model.ApprovedMarginToPrime
                    });
            }
        }

        public void Delete(int concessionLendingTieredRateId)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"DELETE [dbo].[tblConcessionLendingTieredRate] 
                            WHERE [pkConcessionLendingTieredRateId] = @Id",
                    new { Id = concessionLendingTieredRateId });
            }
        }
    }
}