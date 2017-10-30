using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.Common;

namespace StandardBank.ConcessionManagement.Repository
{
    /// <summary>
    /// SapDataImport repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.ISapDataImportRepository" />
    public class SapDataImportRepository : ISapDataImportRepository
    {
        /// <summary>
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="SapDataImportRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        public SapDataImportRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public SapDataImport Create(SapDataImport model)
        {
            const string sql =
                @"INSERT [dbo].[tblSapDataImport] ([PricepointId], [CustomerId], [AccountName], [ProductId], [Description], [GroupId], [SubGroupId], [BankIdentifierId], [AccountNo], [OptionId], [UserId], [TierFromValue], [TierToValue], [AdvaloremFee], [MinimumFee], [MaximumFee], [FlatFee], [CommunicationFee], [TableNo], [TransactionVolume], [TransactionRevenue], [ProductName], [Channel], [MarketSegment], [SequenceId], [EntryDate], [EffectiveDate], [ExpiryDate], [TerminationDate], [Status], [ImportDate], [LastUpdatedDate]) 
                                VALUES (@PricepointId, @CustomerId, @AccountName, @ProductId, @Description, @GroupId, @SubGroupId, @BankIdentifierId, @AccountNo, @OptionId, @UserId, @TierFromValue, @TierToValue, @AdvaloremFee, @MinimumFee, @MaximumFee, @FlatFee, @CommunicationFee, @TableNo, @TransactionVolume, @TransactionRevenue, @ProductName, @Channel, @MarketSegment, @SequenceId, @EntryDate, @EffectiveDate, @ExpiryDate, @TerminationDate, @Status, @ImportDate, @LastUpdatedDate) 
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = _dbConnectionFactory.Connection())
            {
                model.Id = db.Query<int>(sql,
                    new
                    {
                        PricepointId = model.PricepointId,
                        CustomerId = model.CustomerId,
                        AccountName = model.AccountName,
                        ProductId = model.ProductId,
                        Description = model.Description,
                        GroupId = model.GroupId,
                        SubGroupId = model.SubGroupId,
                        BankIdentifierId = model.BankIdentifierId,
                        AccountNo = model.AccountNo,
                        OptionId = model.OptionId,
                        UserId = model.UserId,
                        TierFromValue = model.TierFromValue,
                        TierToValue = model.TierToValue,
                        AdvaloremFee = model.AdvaloremFee,
                        MinimumFee = model.MinimumFee,
                        MaximumFee = model.MaximumFee,
                        FlatFee = model.FlatFee,
                        CommunicationFee = model.CommunicationFee,
                        TableNo = model.TableNo,
                        TransactionVolume = model.TransactionVolume,
                        TransactionRevenue = model.TransactionRevenue,
                        ProductName = model.ProductName,
                        Channel = model.Channel,
                        MarketSegment = model.MarketSegment,
                        SequenceId = model.SequenceId,
                        EntryDate = model.EntryDate,
                        EffectiveDate = model.EffectiveDate,
                        ExpiryDate = model.ExpiryDate,
                        TerminationDate = model.TerminationDate,
                        Status = model.Status,
                        ImportDate = model.ImportDate,
                        LastUpdatedDate = model.LastUpdatedDate
                    }).Single();
            }

            return model;
        }

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public SapDataImport ReadById(int id)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<SapDataImport>(
                    "SELECT [pkSapDataImportId] [Id], [PricepointId], [CustomerId], [AccountName], [ProductId], [Description], [GroupId], [SubGroupId], [BankIdentifierId], [AccountNo], [OptionId], [UserId], [TierFromValue], [TierToValue], [AdvaloremFee], [MinimumFee], [MaximumFee], [FlatFee], [CommunicationFee], [TableNo], [TransactionVolume], [TransactionRevenue], [ProductName], [Channel], [MarketSegment], [SequenceId], [EntryDate], [EffectiveDate], [ExpiryDate], [TerminationDate], [Status], [ImportDate], [LastUpdatedDate] FROM [dbo].[tblSapDataImport] WHERE [pkSapDataImportId] = @Id",
                    new {id}).SingleOrDefault();
            }
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SapDataImport> ReadAll()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<SapDataImport>(
                    "SELECT [pkSapDataImportId] [Id], [PricepointId], [CustomerId], [AccountName], [ProductId], [Description], [GroupId], [SubGroupId], [BankIdentifierId], [AccountNo], [OptionId], [UserId], [TierFromValue], [TierToValue], [AdvaloremFee], [MinimumFee], [MaximumFee], [FlatFee], [CommunicationFee], [TableNo], [TransactionVolume], [TransactionRevenue], [ProductName], [Channel], [MarketSegment], [SequenceId], [EntryDate], [EffectiveDate], [ExpiryDate], [TerminationDate], [Status], [ImportDate], [LastUpdatedDate] FROM [dbo].[tblSapDataImport]");
            }
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(SapDataImport model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(@"UPDATE [dbo].[tblSapDataImport]
                            SET [PricepointId] = @PricepointId, [CustomerId] = @CustomerId, [AccountName] = @AccountName, [ProductId] = @ProductId, [Description] = @Description, [GroupId] = @GroupId, [SubGroupId] = @SubGroupId, [BankIdentifierId] = @BankIdentifierId, [AccountNo] = @AccountNo, [OptionId] = @OptionId, [UserId] = @UserId, [TierFromValue] = @TierFromValue, [TierToValue] = @TierToValue, [AdvaloremFee] = @AdvaloremFee, [MinimumFee] = @MinimumFee, [MaximumFee] = @MaximumFee, [FlatFee] = @FlatFee, [CommunicationFee] = @CommunicationFee, [TableNo] = @TableNo, [TransactionVolume] = @TransactionVolume, [TransactionRevenue] = @TransactionRevenue, [ProductName] = @ProductName, [Channel] = @Channel, [MarketSegment] = @MarketSegment, [SequenceId] = @SequenceId, [EntryDate] = @EntryDate, [EffectiveDate] = @EffectiveDate, [ExpiryDate] = @ExpiryDate, [TerminationDate] = @TerminationDate, [Status] = @Status, [ImportDate] = @ImportDate, [LastUpdatedDate] = @LastUpdatedDate
                            WHERE [pkSapDataImportId] = @Id",
                    new
                    {
                        Id = model.Id,
                        PricepointId = model.PricepointId,
                        CustomerId = model.CustomerId,
                        AccountName = model.AccountName,
                        ProductId = model.ProductId,
                        Description = model.Description,
                        GroupId = model.GroupId,
                        SubGroupId = model.SubGroupId,
                        BankIdentifierId = model.BankIdentifierId,
                        AccountNo = model.AccountNo,
                        OptionId = model.OptionId,
                        UserId = model.UserId,
                        TierFromValue = model.TierFromValue,
                        TierToValue = model.TierToValue,
                        AdvaloremFee = model.AdvaloremFee,
                        MinimumFee = model.MinimumFee,
                        MaximumFee = model.MaximumFee,
                        FlatFee = model.FlatFee,
                        CommunicationFee = model.CommunicationFee,
                        TableNo = model.TableNo,
                        TransactionVolume = model.TransactionVolume,
                        TransactionRevenue = model.TransactionRevenue,
                        ProductName = model.ProductName,
                        Channel = model.Channel,
                        MarketSegment = model.MarketSegment,
                        SequenceId = model.SequenceId,
                        EntryDate = model.EntryDate,
                        EffectiveDate = model.EffectiveDate,
                        ExpiryDate = model.ExpiryDate,
                        TerminationDate = model.TerminationDate,
                        Status = model.Status,
                        ImportDate = model.ImportDate,
                        LastUpdatedDate = model.LastUpdatedDate
                    });
            }
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Delete(SapDataImport model)
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute("DELETE [dbo].[tblSapDataImport] WHERE [pkSapDataImportId] = @Id",
                    new {model.Id});
            }
        }
    }
}