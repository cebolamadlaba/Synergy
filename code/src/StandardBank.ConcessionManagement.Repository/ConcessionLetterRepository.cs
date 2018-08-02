using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.Common;


namespace StandardBank.ConcessionManagement.Repository
{

    public class ConcessionLetterRepository : IConcessionLetterRepository
    {

        /// <summary>
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;
             

        public ConcessionLetterRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public ConcessionLetter Create(ConcessionLetter model)
        {
            const string sql =
                @"delete from [tblConcessionLetter] where fkConcessionDetailId = @fkConcessionDetailId;
                INSERT [tblConcessionLetter] ([fkConcessionDetailId], [Location]) 
                                VALUES (@fkConcessionDetailId, @Location) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.pkConcessionLetter = db.Query<int>(sql,
                    new
                    {
                        fkConcessionDetailId = model.fkConcessionDetailId,
                        Location = model.Location
                    }).Single();
            }

            return model;
        }


        public void Delete(ConcessionLetter model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute("DELETE from [dbo].[tblConcessionLetter] WHERE [pkConcessionLetter] = @pkConcessionLetter",
                    new { model.pkConcessionLetter });
            }
        }


        public IEnumerable<ConcessionLetter> ReadByConcessionDetailId(int concessionDetailId)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ConcessionLetter>(
                    @"SELECT [pkConcessionLetter], [fkConcessionDetailId], [Location]
                    FROM [dbo].[tblConcessionLetter] 
                    WHERE [fkConcessionDetailId] = @fkConcessionDetailId",
                    new { concessionDetailId });
            }
        }
    }
}
