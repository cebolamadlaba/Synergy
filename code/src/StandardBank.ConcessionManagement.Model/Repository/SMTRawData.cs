using System;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// SMTRawData entity
    /// </summary>
    public class SMTRawData
    {
        /// <summary>
        /// Gets or sets the DealID.
        /// </summary>
        /// <value>
        /// The DealID.
        /// </value>
        public string DealID { get; set; }

        /// <summary>
        /// Gets or sets the DealSubmitDate.
        /// </summary>
        /// <value>
        /// The DealSubmitDate.
        /// </value>
        public DateTime? DealSubmitDate { get; set; }

        /// <summary>
        /// Gets or sets the Status.
        /// </summary>
        /// <value>
        /// The Status.
        /// </value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the Segment.
        /// </summary>
        /// <value>
        /// The Segment.
        /// </value>
        public string Segment { get; set; }

        /// <summary>
        /// Gets or sets the USERNAME.
        /// </summary>
        /// <value>
        /// The USERNAME.
        /// </value>
        public string USERNAME { get; set; }

        /// <summary>
        /// Gets or sets the Risk_Group_Cd.
        /// </summary>
        /// <value>
        /// The Risk_Group_Cd.
        /// </value>
        public int? Risk_Group_Cd { get; set; }

        /// <summary>
        /// Gets or sets the Risk_Group_Type.
        /// </summary>
        /// <value>
        /// The Risk_Group_Type.
        /// </value>
        public string Risk_Group_Type { get; set; }

        /// <summary>
        /// Gets or sets the Risk_Group_Name.
        /// </summary>
        /// <value>
        /// The Risk_Group_Name.
        /// </value>
        public string Risk_Group_Name { get; set; }

        /// <summary>
        /// Gets or sets the RGHeadlineEarnings.
        /// </summary>
        /// <value>
        /// The RGHeadlineEarnings.
        /// </value>
        public decimal RGHeadlineEarnings { get; set; }

        /// <summary>
        /// Gets or sets the AfterDealHE.
        /// </summary>
        /// <value>
        /// The AfterDealHE.
        /// </value>
        public decimal? AfterDealHE { get; set; }

        /// <summary>
        /// Gets or sets the RGROE.
        /// </summary>
        /// <value>
        /// The RGROE.
        /// </value>
        public decimal RGROE { get; set; }

        /// <summary>
        /// Gets or sets the RGCapital.
        /// </summary>
        /// <value>
        /// The RGCapital.
        /// </value>
        public decimal RGCapital { get; set; }

        /// <summary>
        /// Gets or sets the RGOI.
        /// </summary>
        /// <value>
        /// The RGOI.
        /// </value>
        public decimal RGOI { get; set; }

        /// <summary>
        /// Gets or sets the DealROE.
        /// </summary>
        /// <value>
        /// The DealROE.
        /// </value>
        public decimal? DealROE { get; set; }

        /// <summary>
        /// Gets or sets the DealCapital.
        /// </summary>
        /// <value>
        /// The DealCapital.
        /// </value>
        public decimal? DealCapital { get; set; }

        /// <summary>
        /// Gets or sets the DealOI.
        /// </summary>
        /// <value>
        /// The DealOI.
        /// </value>
        public decimal? DealOI { get; set; }

        /// <summary>
        /// Gets or sets the BASEQUADRANT.
        /// </summary>
        /// <value>
        /// The BASEQUADRANT.
        /// </value>
        public string BASEQUADRANT { get; set; }

        /// <summary>
        /// Gets or sets the DEALQUADRANT.
        /// </summary>
        /// <value>
        /// The DEALQUADRANT.
        /// </value>
        public string DEALQUADRANT { get; set; }

        /// <summary>
        /// Gets or sets the MandateQuadrantPass?.
        /// </summary>
        /// <value>
        /// The MandateQuadrantPass?.
        /// </value>
        public string MandateQuadrantPass { get; set; }

        /// <summary>
        /// Gets or sets the MandateLendingPass?.
        /// </summary>
        /// <value>
        /// The MandateLendingPass?.
        /// </value>
        public string MandateLendingPass { get; set; }

        /// <summary>
        /// Gets or sets the MandateBranchR100mExceeded?.
        /// </summary>
        /// <value>
        /// The MandateBranchR100mExceeded?.
        /// </value>
        public string MandateBranchR100mExceeded { get; set; }

        /// <summary>
        /// Gets or sets the MandateBranchADVLPass?.
        /// </summary>
        /// <value>
        /// The MandateBranchADVLPass?.
        /// </value>
        public string MandateBranchADVLPass { get; set; }

        /// <summary>
        /// Gets or sets the MandateCentreR100mExceeded?.
        /// </summary>
        /// <value>
        /// The MandateCentreR100mExceeded?.
        /// </value>
        public string MandateCentreR100mExceeded { get; set; }

        /// <summary>
        /// Gets or sets the MandateCentreADVLPass?.
        /// </summary>
        /// <value>
        /// The MandateCentreADVLPass?.
        /// </value>
        public string MandateCentreADVLPass { get; set; }

        /// <summary>
        /// Gets or sets the MandateAutoR100mExceeded?.
        /// </summary>
        /// <value>
        /// The MandateAutoR100mExceeded?.
        /// </value>
        public string MandateAutoR100mExceeded { get; set; }

        /// <summary>
        /// Gets or sets the MandateAutoADVLPass?.
        /// </summary>
        /// <value>
        /// The MandateAutoADVLPass?.
        /// </value>
        public string MandateAutoADVLPass { get; set; }

        /// <summary>
        /// Gets or sets the MandateBOLPass?.
        /// </summary>
        /// <value>
        /// The MandateBOLPass?.
        /// </value>
        public string MandateBOLPass { get; set; }

        /// <summary>
        /// Gets or sets the LendingProfit.
        /// </summary>
        /// <value>
        /// The LendingProfit.
        /// </value>
        public decimal? LendingProfit { get; set; }

        /// <summary>
        /// Gets or sets the LendingROE.
        /// </summary>
        /// <value>
        /// The LendingROE.
        /// </value>
        public decimal? LendingROE { get; set; }

        /// <summary>
        /// Gets or sets the LendingEquity.
        /// </summary>
        /// <value>
        /// The LendingEquity.
        /// </value>
        public decimal? LendingEquity { get; set; }

        /// <summary>
        /// Gets or sets the LendingOI.
        /// </summary>
        /// <value>
        /// The LendingOI.
        /// </value>
        public decimal? LendingOI { get; set; }

        /// <summary>
        /// Gets or sets the BOLInterchange.
        /// </summary>
        /// <value>
        /// The BOLInterchange.
        /// </value>
        public decimal? BOLInterchange { get; set; }

        /// <summary>
        /// Gets or sets the BOLProfit.
        /// </summary>
        /// <value>
        /// The BOLProfit.
        /// </value>
        public decimal? BOLProfit { get; set; }

        /// <summary>
        /// Gets or sets the BOLRevenue.
        /// </summary>
        /// <value>
        /// The BOLRevenue.
        /// </value>
        public decimal? BOLRevenue { get; set; }

        /// <summary>
        /// Gets or sets the BOLCapital.
        /// </summary>
        /// <value>
        /// The BOLCapital.
        /// </value>
        public decimal? BOLCapital { get; set; }

        /// <summary>
        /// Gets or sets the CASHBranchVolume.
        /// </summary>
        /// <value>
        /// The CASHBranchVolume.
        /// </value>
        public int? CASHBranchVolume { get; set; }

        /// <summary>
        /// Gets or sets the CASHCentreVolume.
        /// </summary>
        /// <value>
        /// The CASHCentreVolume.
        /// </value>
        public int? CASHCentreVolume { get; set; }

        /// <summary>
        /// Gets or sets the CASHAutoVolume.
        /// </summary>
        /// <value>
        /// The CASHAutoVolume.
        /// </value>
        public int? CASHAutoVolume { get; set; }

        /// <summary>
        /// Gets or sets the CASHBranchValue.
        /// </summary>
        /// <value>
        /// The CASHBranchValue.
        /// </value>
        public decimal? CASHBranchValue { get; set; }

        /// <summary>
        /// Gets or sets the CASHCentreValue.
        /// </summary>
        /// <value>
        /// The CASHCentreValue.
        /// </value>
        public decimal? CASHCentreValue { get; set; }

        /// <summary>
        /// Gets or sets the CASHAutoValue.
        /// </summary>
        /// <value>
        /// The CASHAutoValue.
        /// </value>
        public decimal? CASHAutoValue { get; set; }

        /// <summary>
        /// Gets or sets the CASHBranchBase.
        /// </summary>
        /// <value>
        /// The CASHBranchBase.
        /// </value>
        public int? CASHBranchBase { get; set; }

        /// <summary>
        /// Gets or sets the CASHCentreBase.
        /// </summary>
        /// <value>
        /// The CASHCentreBase.
        /// </value>
        public int? CASHCentreBase { get; set; }

        /// <summary>
        /// Gets or sets the CASHAutoBase.
        /// </summary>
        /// <value>
        /// The CASHAutoBase.
        /// </value>
        public int? CASHAutoBase { get; set; }

        /// <summary>
        /// Gets or sets the CASHBranchAdValorem.
        /// </summary>
        /// <value>
        /// The CASHBranchAdValorem.
        /// </value>
        public decimal? CASHBranchAdValorem { get; set; }

        /// <summary>
        /// Gets or sets the CASHCentreAdValorem.
        /// </summary>
        /// <value>
        /// The CASHCentreAdValorem.
        /// </value>
        public decimal? CASHCentreAdValorem { get; set; }

        /// <summary>
        /// Gets or sets the CASHAutoAdValorem.
        /// </summary>
        /// <value>
        /// The CASHAutoAdValorem.
        /// </value>
        public decimal? CASHAutoAdValorem { get; set; }

        /// <summary>
        /// Gets or sets the CASHBranchProfit.
        /// </summary>
        /// <value>
        /// The CASHBranchProfit.
        /// </value>
        public decimal? CASHBranchProfit { get; set; }

        /// <summary>
        /// Gets or sets the CASHCentreProfit.
        /// </summary>
        /// <value>
        /// The CASHCentreProfit.
        /// </value>
        public decimal? CASHCentreProfit { get; set; }

        /// <summary>
        /// Gets or sets the CASHAutoProfit.
        /// </summary>
        /// <value>
        /// The CASHAutoProfit.
        /// </value>
        public decimal? CASHAutoProfit { get; set; }

        /// <summary>
        /// Gets or sets the CASHBranchCapital.
        /// </summary>
        /// <value>
        /// The CASHBranchCapital.
        /// </value>
        public decimal? CASHBranchCapital { get; set; }

        /// <summary>
        /// Gets or sets the CASHCentreCapital.
        /// </summary>
        /// <value>
        /// The CASHCentreCapital.
        /// </value>
        public decimal? CASHCentreCapital { get; set; }

        /// <summary>
        /// Gets or sets the CASHAutoCapital.
        /// </summary>
        /// <value>
        /// The CASHAutoCapital.
        /// </value>
        public decimal? CASHAutoCapital { get; set; }

        /// <summary>
        /// Gets or sets the CASHBranchOI.
        /// </summary>
        /// <value>
        /// The CASHBranchOI.
        /// </value>
        public decimal? CASHBranchOI { get; set; }

        /// <summary>
        /// Gets or sets the CASHCentreOI.
        /// </summary>
        /// <value>
        /// The CASHCentreOI.
        /// </value>
        public decimal? CASHCentreOI { get; set; }

        /// <summary>
        /// Gets or sets the CASHAutoOI.
        /// </summary>
        /// <value>
        /// The CASHAutoOI.
        /// </value>
        public decimal? CASHAutoOI { get; set; }
    }
}
