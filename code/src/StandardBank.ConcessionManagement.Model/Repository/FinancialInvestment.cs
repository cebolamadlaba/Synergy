using System;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// FinancialInvestment entity
    /// </summary>
    public class FinancialInvestment
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        /// <value>
        /// The Id.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the RiskGroupId.
        /// </summary>
        /// <value>
        /// The RiskGroupId.
        /// </value>
        public int RiskGroupId { get; set; }

        /// <summary>
        /// Gets or sets the TotalLiabilityBalances.
        /// </summary>
        /// <value>
        /// The TotalLiabilityBalances.
        /// </value>
        public decimal TotalLiabilityBalances { get; set; }

        /// <summary>
        /// Gets or sets the WeightedAverageMTP.
        /// </summary>
        /// <value>
        /// The WeightedAverageMTP.
        /// </value>
        public decimal WeightedAverageMTP { get; set; }

        /// <summary>
        /// Gets or sets the WeightedAverageNetMargin.
        /// </summary>
        /// <value>
        /// The WeightedAverageNetMargin.
        /// </value>
        public decimal WeightedAverageNetMargin { get; set; }

        /// <summary>
        /// Gets or sets the LatestCrsOrMrs.
        /// </summary>
        /// <value>
        /// The LatestCrsOrMrs.
        /// </value>
        public decimal LatestCrsOrMrs { get; set; }
    }
}
