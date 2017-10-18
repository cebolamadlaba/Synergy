using System.Collections.Generic;

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
        /// Gets or sets the lending concessions.
        /// </summary>
        /// <value>
        /// The lending concessions.
        /// </value>
        public IEnumerable<LendingConcession> LendingConcessions { get; set; }

        /// <summary>
        /// Gets or sets the lending products.
        /// </summary>
        /// <value>
        /// The lending products.
        /// </value>
        public IEnumerable<LendingProduct> LendingProducts { get; set; }

        /// <summary>
        /// Gets or sets the lending financial.
        /// </summary>
        /// <value>
        /// The lending financial.
        /// </value>
        public LendingFinancial LendingFinancial { get; set; }
    }
}
