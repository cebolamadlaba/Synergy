using System.Collections.Generic;

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
        /// Gets or sets the cash concessions.
        /// </summary>
        /// <value>
        /// The cash concessions.
        /// </value>
        public IEnumerable<CashConcession> CashConcessions { get; set; }

        /// <summary>
        /// Gets or sets the cash financial.
        /// </summary>
        /// <value>
        /// The cash financial.
        /// </value>
        public CashFinancial CashFinancial { get; set; }

        /// <summary>
        /// Gets or sets the cash products.
        /// </summary>
        /// <value>
        /// The cash products.
        /// </value>
        public IEnumerable<CashProduct> CashProducts { get; set; }
    }
}
