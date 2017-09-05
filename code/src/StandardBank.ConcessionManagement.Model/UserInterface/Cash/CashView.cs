using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.UserInterface.Pricing;

namespace StandardBank.ConcessionManagement.Model.UserInterface.Cash
{
    /// <summary>
    /// Cash view entity
    /// </summary>
    public class CashView
    {
        /// <summary>
        /// Gets or sets the risk group.
        /// </summary>
        /// <value>
        /// The risk group.
        /// </value>
        public RiskGroup RiskGroup { get; set; }

        /// <summary>
        /// Gets or sets the cash centre turnover.
        /// </summary>
        /// <value>
        /// The cash centre turnover.
        /// </value>
        public decimal CashCentreTurnover { get; set; }

        /// <summary>
        /// Gets or sets the cash centre volume.
        /// </summary>
        /// <value>
        /// The cash centre volume.
        /// </value>
        public decimal CashCentreVolume { get; set; }

        /// <summary>
        /// Gets or sets the cash centre price.
        /// </summary>
        /// <value>
        /// The cash centre price.
        /// </value>
        public decimal CashCentrePrice { get; set; }

        /// <summary>
        /// Gets or sets the branch turnover.
        /// </summary>
        /// <value>
        /// The branch turnover.
        /// </value>
        public decimal BranchTurnover { get; set; }

        /// <summary>
        /// Gets or sets the branch volume.
        /// </summary>
        /// <value>
        /// The branch volume.
        /// </value>
        public decimal BranchVolume { get; set; }

        /// <summary>
        /// Gets or sets the branch price.
        /// </summary>
        /// <value>
        /// The branch price.
        /// </value>
        public decimal BranchPrice { get; set; }

        /// <summary>
        /// Gets or sets the automatic safe turnover.
        /// </summary>
        /// <value>
        /// The automatic safe turnover.
        /// </value>
        public decimal AutoSafeTurnover { get; set; }

        /// <summary>
        /// Gets or sets the automatic safe volume.
        /// </summary>
        /// <value>
        /// The automatic safe volume.
        /// </value>
        public decimal AutoSafeVolume { get; set; }

        /// <summary>
        /// Gets or sets the automatic safe price.
        /// </summary>
        /// <value>
        /// The automatic safe price.
        /// </value>
        public decimal AutoSafePrice { get; set; }

        /// <summary>
        /// Gets or sets the cash concessions.
        /// </summary>
        /// <value>
        /// The cash concessions.
        /// </value>
        public IEnumerable<CashConcession> CashConcessions { get; set; }
    }
}
