using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.Common;

namespace StandardBank.ConcessionManagement.Repository
{
    /// <summary>
    /// ChannelTypeBaseRate repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IChannelTypeBaseRateRepository" />
    public class ChannelTypeBaseRateRepository : IChannelTypeBaseRateRepository
    {
        /// <summary>
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelTypeBaseRateRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        public ChannelTypeBaseRateRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ChannelTypeBaseRate Create(ChannelTypeBaseRate model)
        {
            const string sql = @"INSERT [dbo].[tblChannelTypeBaseRate] ([fkChannelTypeId], [fkBaseRateId]) 
                                VALUES (@ChannelTypeId, @BaseRateId) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql, new {ChannelTypeId = model.ChannelTypeId, BaseRateId = model.BaseRateId}).Single();
            }

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ChannelTypeBaseRate ReadById(int id)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ChannelTypeBaseRate>(
                    @"SELECT [pkChannelTypeBaseRateId] [Id], 
                             [fkChannelTypeId] [ChannelTypeId], 
                             [fkBaseRateId] [BaseRateId] 
                    FROM [dbo].[tblChannelTypeBaseRate] 
                    WHERE [pkChannelTypeBaseRateId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ChannelTypeBaseRate> ReadAll()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<ChannelTypeBaseRate>(
                    @"SELECT [pkChannelTypeBaseRateId] [Id], 
                             [fkChannelTypeId] [ChannelTypeId], 
                             [fkBaseRateId] [BaseRateId] 
                    FROM [dbo].[tblChannelTypeBaseRate]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(ChannelTypeBaseRate model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(
                    @"UPDATE [dbo].[tblChannelTypeBaseRate]
                    SET [fkChannelTypeId] = @ChannelTypeId, 
                        [fkBaseRateId] = @BaseRateId
                    WHERE [pkChannelTypeBaseRateId] = @Id",
                    new {Id = model.Id, ChannelTypeId = model.ChannelTypeId, BaseRateId = model.BaseRateId});
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(ChannelTypeBaseRate model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"DELETE [dbo].[tblChannelTypeBaseRate] 
                            WHERE [pkChannelTypeBaseRateId] = @Id",
                    new {model.Id});
            }
        }
    }
}
