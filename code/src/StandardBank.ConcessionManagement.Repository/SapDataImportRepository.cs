using System;
using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
using System.Data;
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
                @"INSERT [dbo].[tblSapDataImport] ([PricepointId], [CustomerId], [AccountName], [ProductId], [Description], [GroupId], [SubGroupId], [BankIdentifierId], [AccountNo], [OptionId], [UserId], [TierFromValue], [TierToValue], [AdvaloremFee], [MinimumFee], [MaximumFee], [FlatFee], [CommunicationFee], [TableNo], [TransactionVolume], [TransactionRevenue], [ProductName], [Channel], [MarketSegment], [SequenceId], [EntryDate], [EffectiveDate], [ExpiryDate], [TerminationDate], [Status], [ImportDate], [LastUpdatedDate], [ExportRow]) 
                VALUES (@PricepointId, @CustomerId, @AccountName, @ProductId, @Description, @GroupId, @SubGroupId, @BankIdentifierId, @AccountNo, @OptionId, @UserId, @TierFromValue, @TierToValue, @AdvaloremFee, @MinimumFee, @MaximumFee, @FlatFee, @CommunicationFee, @TableNo, @TransactionVolume, @TransactionRevenue, @ProductName, @Channel, @MarketSegment, @SequenceId, @EntryDate, @EffectiveDate, @ExpiryDate, @TerminationDate, @Status, @ImportDate, @LastUpdatedDate, @ExportRow)";

            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(sql,
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
                        LastUpdatedDate = model.LastUpdatedDate,
                        ExportRow = model.ExportRow
                    });
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
                    @"SELECT [PricepointId],
	                    [CustomerId],
	                    [AccountName],
	                    [ProductId],
	                    [Description],
	                    [GroupId],
	                    [SubGroupId],
	                    [BankIdentifierId],
	                    [AccountNo],
	                    [OptionId],
	                    [UserId],
	                    [TierFromValue],
	                    [TierToValue],
	                    [AdvaloremFee],
	                    [MinimumFee],
	                    [MaximumFee],
	                    [FlatFee],
	                    [CommunicationFee],
	                    [TableNo],
	                    [TransactionVolume],
	                    [TransactionRevenue],
	                    [ProductName],
	                    [Channel],
	                    [MarketSegment],
	                    [SequenceId],
	                    [EntryDate],
	                    [EffectiveDate],
	                    [ExpiryDate],
	                    [TerminationDate],
	                    [Status],
	                    [ImportDate],
	                    [LastUpdatedDate],
	                    [ExportRow] 
                    FROM [dbo].[tblSapDataImport] 
                    WHERE [PricepointId] = @Id",
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
                    @"SELECT [PricepointId],
	                    [CustomerId],
	                    [AccountName],
	                    [ProductId],
	                    [Description],
	                    [GroupId],
	                    [SubGroupId],
	                    [BankIdentifierId],
	                    [AccountNo],
	                    [OptionId],
	                    [UserId],
	                    [TierFromValue],
	                    [TierToValue],
	                    [AdvaloremFee],
	                    [MinimumFee],
	                    [MaximumFee],
	                    [FlatFee],
	                    [CommunicationFee],
	                    [TableNo],
	                    [TransactionVolume],
	                    [TransactionRevenue],
	                    [ProductName],
	                    [Channel],
	                    [MarketSegment],
	                    [SequenceId],
	                    [EntryDate],
	                    [EffectiveDate],
	                    [ExpiryDate],
	                    [TerminationDate],
	                    [Status],
	                    [ImportDate],
	                    [LastUpdatedDate],
	                    [ExportRow] 
                    FROM [dbo].[tblSapDataImport]");
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
                db.Execute(
                    @"UPDATE [dbo].[tblSapDataImport]
                    SET [CustomerId] = @CustomerId,
                        [AccountName] = @AccountName,
                        [ProductId] = @ProductId,
                        [Description] = @Description,
                        [GroupId] = @GroupId,
                        [SubGroupId] = @SubGroupId,
                        [BankIdentifierId] = @BankIdentifierId,
                        [AccountNo] = @AccountNo,
                        [OptionId] = @OptionId,
                        [UserId] = @UserId,
                        [TierFromValue] = @TierFromValue,
                        [TierToValue] = @TierToValue,
                        [AdvaloremFee] = @AdvaloremFee,
                        [MinimumFee] = @MinimumFee,
                        [MaximumFee] = @MaximumFee,
                        [FlatFee] = @FlatFee,
                        [CommunicationFee] = @CommunicationFee,
                        [TableNo] = @TableNo,
                        [TransactionVolume] = @TransactionVolume,
                        [TransactionRevenue] = @TransactionRevenue,
                        [ProductName] = @ProductName,
                        [Channel] = @Channel,
                        [MarketSegment] = @MarketSegment,
                        [SequenceId] = @SequenceId,
                        [EntryDate] = @EntryDate,
                        [EffectiveDate] = @EffectiveDate,
                        [ExpiryDate] = @ExpiryDate,
                        [TerminationDate] = @TerminationDate,
                        [Status] = @Status,
                        [ImportDate] = @ImportDate,
                        [LastUpdatedDate] = @LastUpdatedDate,
                        [ExportRow] = @ExportRow
                    WHERE [PricepointId] = @PricepointId",
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
                        LastUpdatedDate = model.LastUpdatedDate,
                        ExportRow = model.ExportRow
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
                db.Execute(@"DELETE [dbo].[tblSapDataImport] 
                            WHERE [PricepointId] = @PricepointId",
                    new {model.PricepointId});
            }
        }

        /// <summary>
        /// Generates the sap export.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SapDataImport> GenerateSapExport()
        {
            using (var db = _dbConnectionFactory.Connection())
                return db.Query<SapDataImport>("[dbo].[GenerateSapExport]", commandType: CommandType.StoredProcedure,
                    commandTimeout: Int32.MaxValue);
        }

        /// <summary>
        /// Gets the sap data import issues.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SapDataImportIssue> GetSapDataImportIssues()
        {
            using (var db = _dbConnectionFactory.Connection())
                return db.Query<SapDataImportIssue>("[dbo].[SapImportDataIssues]",
                    commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Updates the prices and mismatches.
        /// </summary>
        public void UpdatePricesAndMismatches()
        {
            using (var db = _dbConnectionFactory.Connection())
                db.Execute("[dbo].[UpdatePricesAndMismatches]", commandType: CommandType.StoredProcedure,
                    commandTimeout: Int32.MaxValue);
        }

        /// <summary>
        /// Updates the loaded prices.
        /// </summary>
        /// <param name="sapDataImport">The sap data import.</param>
        public void UpdateLoadedPrices(SapDataImport sapDataImport)
        {
            using (var db = _dbConnectionFactory.Connection())
                db.Execute("[dbo].[UpdateLoadedPrices]",
                    new
                    {
                        AccountNo = sapDataImport.AccountNo,
                        ChannelType = sapDataImport.Channel,
                        FlatFee = sapDataImport.FlatFee,
                        TableNo = sapDataImport.TableNo
                    }, commandType: CommandType.StoredProcedure, commandTimeout: Int32.MaxValue);
        }

        /// <summary>
        /// Updates the mismatches.
        /// </summary>
        public void UpdateMismatches()
        {
            using (var db = _dbConnectionFactory.Connection())
                db.Execute("[dbo].[UpdateMismatches]", commandType: CommandType.StoredProcedure,
                    commandTimeout: Int32.MaxValue);
        }

        /// <summary>
        /// Updates the loaded prices tables.
        /// </summary>
        public void UpdateLoadedPricesTables()
        {
            using (var db = _dbConnectionFactory.Connection())
                db.Execute("[dbo].[UpdateLoadedPricesTables]", commandType: CommandType.StoredProcedure,
                    commandTimeout: Int32.MaxValue);
        }
    }
}