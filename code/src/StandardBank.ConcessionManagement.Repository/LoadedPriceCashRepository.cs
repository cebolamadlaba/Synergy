using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.Common;

namespace StandardBank.ConcessionManagement.Repository
{
    /// <summary>
    /// LoadedPriceCash repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.ILoadedPriceCashRepository" />
    public class LoadedPriceCashRepository : ILoadedPriceCashRepository
    {
        /// <summary>
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadedPriceCashRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        public LoadedPriceCashRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public LoadedPriceCash Create(LoadedPriceCash model)
        {
            const string sql =
                @"INSERT [dbo].[tblLoadedPriceCash] ([fkChannelTypeId], [fkLegalEntityAccountId], [fkTableNumberId]) 
                                VALUES (@ChannelTypeId, @LegalEntityAccountId, @TableNumberId) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql,
                    new
                    {
                        ChannelTypeId = model.ChannelTypeId,
                        LegalEntityAccountId = model.LegalEntityAccountId,
                        TableNumberId = model.TableNumberId
                    }).Single();
            }

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public LoadedPriceCash ReadById(int id)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<LoadedPriceCash>(
                    "SELECT [pkLoadedPriceCashId] [Id], [fkChannelTypeId] [ChannelTypeId], [fkLegalEntityAccountId] [LegalEntityAccountId], [fkTableNumberId] [TableNumberId] FROM [dbo].[tblLoadedPriceCash] WHERE [pkLoadedPriceCashId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads by the channel type id and the legal entity account id
        /// </summary>
        /// <param name="channelTypeId"></param>
        /// <param name="legalEntityAccountId"></param>
        /// <returns></returns>
        public LoadedPriceCash ReadByChannelTypeIdLegalEntityAccountId(int channelTypeId, int legalEntityAccountId)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<LoadedPriceCash>(
                    @"SELECT [pkLoadedPriceCashId] [Id], [fkChannelTypeId] [ChannelTypeId], [fkLegalEntityAccountId] [LegalEntityAccountId], [fkTableNumberId] [TableNumberId] 
                    FROM [dbo].[tblLoadedPriceCash] 
                    WHERE [fkChannelTypeId] = @channelTypeId
                    AND [fkLegalEntityAccountId] = @legalEntityAccountId",
                    new { channelTypeId, legalEntityAccountId }).FirstOrDefault();
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<LoadedPriceCash> ReadAll()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<LoadedPriceCash>(
                    "SELECT [pkLoadedPriceCashId] [Id], [fkChannelTypeId] [ChannelTypeId], [fkLegalEntityAccountId] [LegalEntityAccountId], [fkTableNumberId] [TableNumberId] FROM [dbo].[tblLoadedPriceCash]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(LoadedPriceCash model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"UPDATE [dbo].[tblLoadedPriceCash]
                            SET [fkChannelTypeId] = @ChannelTypeId, [fkLegalEntityAccountId] = @LegalEntityAccountId, [fkTableNumberId] = @TableNumberId
                            WHERE [pkLoadedPriceCashId] = @Id",
                    new
                    {
                        Id = model.Id,
                        ChannelTypeId = model.ChannelTypeId,
                        LegalEntityAccountId = model.LegalEntityAccountId,
                        TableNumberId = model.TableNumberId
                    });
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(LoadedPriceCash model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute("DELETE [dbo].[tblLoadedPriceCash] WHERE [pkLoadedPriceCashId] = @Id",
                    new {model.Id});
            }
        }
    }
}
