using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.UserInterface.Integration;
using StandardBank.ConcessionManagement.Model.UserInterface.Pricing;

namespace StandardBank.ConcessionManagement.Model.UserInterface.Lending
{
    /// <summary>
    /// Lending view
    /// </summary>
    public class LendingView
    {
        /// <summary>
        /// Gets or sets the risk group.
        /// </summary>
        /// <value>
        /// The risk group.
        /// </value>
        public RiskGroup RiskGroup { get; set; }

        /// <summary>
        /// Gets or sets the total exposure.
        /// </summary>
        /// <value>
        /// The total exposure.
        /// </value>
        public decimal TotalExposure { get; set; }

        /// <summary>
        /// Gets or sets the weighted average map.
        /// </summary>
        /// <value>
        /// The weighted average map.
        /// </value>
        public decimal WeightedAverageMap { get; set; }

        /// <summary>
        /// Gets or sets the weighted CRS MRS.
        /// </summary>
        /// <value>
        /// The weighted CRS MRS.
        /// </value>
        public decimal WeightedCrsMrs { get; set; }

        /// <summary>
        /// Gets or sets the source system products.
        /// </summary>
        /// <value>
        /// The source system products.
        /// </value>
        public IEnumerable<SourceSystemProduct> SourceSystemProducts { get; set; }

        /// <summary>
        /// Gets or sets the lending concessions.
        /// </summary>
        /// <value>
        /// The lending concessions.
        /// </value>
        public IEnumerable<LendingConcession> LendingConcessions { get; set; }
    }
}
