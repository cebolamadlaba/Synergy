using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.Common;
using System;

namespace StandardBank.ConcessionManagement.Repository
{
    /// <summary>
    /// GlmsTierData repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IGlmsTierDataRepository" />
    public class GlmsTierDataRepository : IGlmsTierDataRepository
    {
        /// <summary>
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="GlmsTierDataRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The database connection factory.</param>
        /// <param name="concessionDetailRepository">The concession detail repository.</param>
        public GlmsTierDataRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public GlmsTierData Create(GlmsTierData model)
        {
                const string sql =
                    @"INSERT [dbo].[tblGlmsTierData] ([fkGlmsConcessionId], [TierFrom], [TierTo],[fkRateTypeId], [fkBaseRateId], [Spread],[Value]) 
                    VALUES (@fkGlmsConcessionId, @TierFrom, @TierTo,@fkRateTypeId, @fkBaseRateId, @Spread,@Value) 
                    SELECT CAST(SCOPE_IDENTITY() as int)";

                using (var db = _dbConnectionFactory.Connection())
                {

                        model.Id = db.Query<int>(sql,
                            new
                            {
                                fkGlmsConcessionId = model.GlmsConcessionId,
                                TierFrom = model.TierFrom,
                                TierTo = model.TierTo,
                                fkRateTypeId = model.RateTypeId,
                                fkBaseRateId = model.BaseRateId == 0 ? null : model.BaseRateId,
                                Spread = model.Spread,
                                Value = model.Value

                            }).Single();      
                
                }

                return model;          
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public GlmsTierData ReadById(int id)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<GlmsTierData>(
                    @"SELECT [GlmsTierDataId] [Id],
                            [fkGlmsConcessionId] [GlmsConcessionId],
                            [TierFrom], 
                            [TierTo],
                            [fkRateTypeId] [RateTypeId],
                            [fkBaseRateId] [BaseRateId], 
                            [Spread],
                            [Value]
                    FROM [dbo].[tblGlmsTierData] 
                    WHERE [GlmsTierDataId] = @Id",
                    new { id }).SingleOrDefault();
            }
        }


        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public IEnumerable<GlmsTierData> ReadAllById(int Id)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
               var results= db.Query<GlmsTierData>(
                    @"SELECT [GlmsTierDataId] [Id],
                            [fkGlmsConcessionId] [GlmsConcessionId],
                            [TierFrom], 
                            [TierTo],
                            [fkRateTypeId] [RateTypeId],
                            [fkBaseRateId] [BaseRateId], 
                            [Spread],
                            [Value]
                    FROM [dbo].[tblGlmsTierData] 
                    WHERE [fkGlmsConcessionId] = @Id",
                    new { Id }).ToList();

                return results;
            }
        }


        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public IEnumerable<GlmsTierDataView> GetGlmsTierDataViewById(int Id)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                var results = db.Query<GlmsTierDataView>(
                     @"SELECT rate.Description Rate,
		                    code.Description BaseRate
		                    ,tierData.TierTo
		                    ,tierData.TierFrom
		                    ,tierData.Spread
		                    ,tierData.Value
                      FROM tblGlmsTierData tierData
                        INNER JOIN tblRateType rate 
	                        ON rate.pkRateTypeId=tierData.fkRateTypeId
	                    LEFT JOIN tblBaseRateCode code 
		                    ON code.pkBaseRateCodeId = tierData.fkBaseRateId
                      WHERE tierData.fkGlmsConcessionId = @Id",
                     new { Id }).ToList();

                return results;
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GlmsTierData> ReadAll()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<GlmsTierData>(
                    @"SELECT [GlmsTierDataId] [Id],
                            [fkGlmsConcessionId] [GlmsConcessionId], 
                            [TierFrom], 
                            [TierTo],
                            [fkRateTypeId] [RateTypeId], 
                            [fkBaseRateId] [BaseRateId], 
                            [Spread], [Value]
                    FROM [dbo].[tblGlmsTierData]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public GlmsTierData Update(GlmsTierData model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(
                    @"UPDATE [dbo].[tblConcessionGlms]
                    SET [fkGlmsConcessionId] = @GlmsConcessionId,
                        [TierFrom] = @TierFrom, 
                        [TierTo] = @TierTo,
                        [fkRateTypeId] = @RateTypeId, 
                        [fkBaseRateId] = @BaseRateId, 
                        [Spread] = @Spread,
                        [Value] = @Value
                    WHERE [GlmsTierDataId] = @Id",
                    new
                    {
                        fkGlmsConcessionId = model.GlmsConcessionId,
                        TierFrom = model.TierFrom,
                        TierTo = model.TierTo,
                        fkRateTypeId = model.RateTypeId,
                        fkBaseRateId = model.BaseRateId,
                        Spread = model.Spread,
                        Value = model.Value                        

                    });
            }

            return model;

        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="ID">The model.</param>
        public void Delete(int Id)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"DELETE [dbo].[tblGlmsTierData] 
                            WHERE [fkGlmsConcessionId] = @Id",
                    new { Id });
            }
        }
    }
}
