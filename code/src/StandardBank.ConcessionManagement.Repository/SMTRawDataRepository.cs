using Dapper;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using StandardBank.ConcessionManagement.Interface.Common;

namespace StandardBank.ConcessionManagement.Repository
{
    /// <summary>
    /// SMTRawData repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.ISMTRawDataRepository" />
    public class SMTRawDataRepository : ISMTRawDataRepository
    {
        /// <summary>
        /// The db connection factory
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="SMTRawDataRepository"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory">The db connection factory.</param>
        public SMTRawDataRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public SMTRawData Create(SMTRawData model)
        {
            const string sql =
                @"INSERT [dbo].[SMTRawData] ([DealID], [DealSubmitDate], [Status], [Segment], [USER NAME], [Risk_Group_Cd], [Risk_Group_Type], [Risk_Group_Name], [RGHeadlineEarnings], [After Deal HE], [RGROE], [RGCapital], [RGOI], [DealROE], [DealCapital], [DealOI], [BASE QUADRANT], [DEAL QUADRANT], [Mandate Quadrant Pass?], [Mandate Lending Pass?], [Mandate Branch R100m Exceeded?], [Mandate Branch ADVL Pass?], [Mandate Centre R100m Exceeded?], [Mandate Centre ADVL Pass?], [Mandate Auto R100m Exceeded?], [Mandate Auto ADVL Pass?], [Mandate BOL Pass?], [Lending Profit], [Lending ROE], [Lending Equity], [Lending OI], [BOL Interchange], [BOL Profit], [BOL Revenue], [BOL Capital], [CASH Branch Volume], [CASH Centre Volume], [CASH Auto Volume], [CASH Branch Value], [CASH Centre Value], [CASH Auto Value], [CASH Branch Base], [CASH Centre Base], [CASH Auto Base], [CASH Branch Ad Valorem], [CASH Centre Ad Valorem], [CASH Auto Ad Valorem], [CASH Branch Profit], [CASH Centre Profit], [CASH Auto Profit], [CASH Branch Capital], [CASH Centre Capital], [CASH Auto Capital], [CASH Branch OI], [CASH Centre OI], [CASH Auto OI]) 
                                VALUES (@DealID, @DealSubmitDate, @Status, @Segment, @USERNAME, @Risk_Group_Cd, @Risk_Group_Type, @Risk_Group_Name, @RGHeadlineEarnings, @AfterDealHE, @RGROE, @RGCapital, @RGOI, @DealROE, @DealCapital, @DealOI, @BASEQUADRANT, @DEALQUADRANT, @MandateQuadrantPass, @MandateLendingPass, @MandateBranchR100mExceeded, @MandateBranchADVLPass, @MandateCentreR100mExceeded, @MandateCentreADVLPass, @MandateAutoR100mExceeded, @MandateAutoADVLPass, @MandateBOLPass, @LendingProfit, @LendingROE, @LendingEquity, @LendingOI, @BOLInterchange, @BOLProfit, @BOLRevenue, @BOLCapital, @CASHBranchVolume, @CASHCentreVolume, @CASHAutoVolume, @CASHBranchValue, @CASHCentreValue, @CASHAutoValue, @CASHBranchBase, @CASHCentreBase, @CASHAutoBase, @CASHBranchAdValorem, @CASHCentreAdValorem, @CASHAutoAdValorem, @CASHBranchProfit, @CASHCentreProfit, @CASHAutoProfit, @CASHBranchCapital, @CASHCentreCapital, @CASHAutoCapital, @CASHBranchOI, @CASHCentreOI, @CASHAutoOI)";

            using (var db = _dbConnectionFactory.Connection())
            {
                db.Execute(sql,
                    new
                    {
                        DealID = model.DealID,
                        DealSubmitDate = model.DealSubmitDate,
                        Status = model.Status,
                        Segment = model.Segment,
                        USERNAME = model.USERNAME,
                        Risk_Group_Cd = model.Risk_Group_Cd,
                        Risk_Group_Type = model.Risk_Group_Type,
                        Risk_Group_Name = model.Risk_Group_Name,
                        RGHeadlineEarnings = model.RGHeadlineEarnings,
                        AfterDealHE = model.AfterDealHE,
                        RGROE = model.RGROE,
                        RGCapital = model.RGCapital,
                        RGOI = model.RGOI,
                        DealROE = model.DealROE,
                        DealCapital = model.DealCapital,
                        DealOI = model.DealOI,
                        BASEQUADRANT = model.BASEQUADRANT,
                        DEALQUADRANT = model.DEALQUADRANT,
                        MandateQuadrantPass = model.MandateQuadrantPass,
                        MandateLendingPass = model.MandateLendingPass,
                        MandateBranchR100mExceeded = model.MandateBranchR100mExceeded,
                        MandateBranchADVLPass = model.MandateBranchADVLPass,
                        MandateCentreR100mExceeded = model.MandateCentreR100mExceeded,
                        MandateCentreADVLPass = model.MandateCentreADVLPass,
                        MandateAutoR100mExceeded = model.MandateAutoR100mExceeded,
                        MandateAutoADVLPass = model.MandateAutoADVLPass,
                        MandateBOLPass = model.MandateBOLPass,
                        LendingProfit = model.LendingProfit,
                        LendingROE = model.LendingROE,
                        LendingEquity = model.LendingEquity,
                        LendingOI = model.LendingOI,
                        BOLInterchange = model.BOLInterchange,
                        BOLProfit = model.BOLProfit,
                        BOLRevenue = model.BOLRevenue,
                        BOLCapital = model.BOLCapital,
                        CASHBranchVolume = model.CASHBranchVolume,
                        CASHCentreVolume = model.CASHCentreVolume,
                        CASHAutoVolume = model.CASHAutoVolume,
                        CASHBranchValue = model.CASHBranchValue,
                        CASHCentreValue = model.CASHCentreValue,
                        CASHAutoValue = model.CASHAutoValue,
                        CASHBranchBase = model.CASHBranchBase,
                        CASHCentreBase = model.CASHCentreBase,
                        CASHAutoBase = model.CASHAutoBase,
                        CASHBranchAdValorem = model.CASHBranchAdValorem,
                        CASHCentreAdValorem = model.CASHCentreAdValorem,
                        CASHAutoAdValorem = model.CASHAutoAdValorem,
                        CASHBranchProfit = model.CASHBranchProfit,
                        CASHCentreProfit = model.CASHCentreProfit,
                        CASHAutoProfit = model.CASHAutoProfit,
                        CASHBranchCapital = model.CASHBranchCapital,
                        CASHCentreCapital = model.CASHCentreCapital,
                        CASHAutoCapital = model.CASHAutoCapital,
                        CASHBranchOI = model.CASHBranchOI,
                        CASHCentreOI = model.CASHCentreOI,
                        CASHAutoOI = model.CASHAutoOI
                    });
            }

            return model;
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SMTRawData> ReadAll()
        {
            using (var db = _dbConnectionFactory.Connection())
            {
                return db.Query<SMTRawData>("SELECT [DealID], [DealSubmitDate], [Status], [Segment], [USER NAME] [USERNAME], [Risk_Group_Cd], [Risk_Group_Type], [Risk_Group_Name], [RGHeadlineEarnings], [After Deal HE] [AfterDealHE], [RGROE], [RGCapital], [RGOI], [DealROE], [DealCapital], [DealOI], [BASE QUADRANT] [BASEQUADRANT], [DEAL QUADRANT] [DEALQUADRANT], [Mandate Quadrant Pass?] [MandateQuadrantPass], [Mandate Lending Pass?] [MandateLendingPass], [Mandate Branch R100m Exceeded?] [MandateBranchR100mExceeded], [Mandate Branch ADVL Pass?] [MandateBranchADVLPass], [Mandate Centre R100m Exceeded?] [MandateCentreR100mExceeded], [Mandate Centre ADVL Pass?] [MandateCentreADVLPass], [Mandate Auto R100m Exceeded?] [MandateAutoR100mExceeded], [Mandate Auto ADVL Pass?] [MandateAutoADVLPass], [Mandate BOL Pass?] [MandateBOLPass], [Lending Profit] [LendingProfit], [Lending ROE] [LendingROE], [Lending Equity] [LendingEquity], [Lending OI] [LendingOI], [BOL Interchange] [BOLInterchange], [BOL Profit] [BOLProfit], [BOL Revenue] [BOLRevenue], [BOL Capital] [BOLCapital], [CASH Branch Volume] [CASHBranchVolume], [CASH Centre Volume] [CASHCentreVolume], [CASH Auto Volume] [CASHAutoVolume], [CASH Branch Value] [CASHBranchValue], [CASH Centre Value] [CASHCentreValue], [CASH Auto Value] [CASHAutoValue], [CASH Branch Base] [CASHBranchBase], [CASH Centre Base] [CASHCentreBase], [CASH Auto Base] [CASHAutoBase], [CASH Branch Ad Valorem] [CASHBranchAdValorem], [CASH Centre Ad Valorem] [CASHCentreAdValorem], [CASH Auto Ad Valorem] [CASHAutoAdValorem], [CASH Branch Profit] [CASHBranchProfit], [CASH Centre Profit] [CASHCentreProfit], [CASH Auto Profit] [CASHAutoProfit], [CASH Branch Capital] [CASHBranchCapital], [CASH Centre Capital] [CASHCentreCapital], [CASH Auto Capital] [CASHAutoCapital], [CASH Branch OI] [CASHBranchOI], [CASH Centre OI] [CASHCentreOI], [CASH Auto OI] [CASHAutoOI] FROM [dbo].[SMTRawData]");
            }
        }
    }
}
