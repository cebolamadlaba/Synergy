using System;
using System.Collections.Generic;
using System.Text;

namespace StandardBank.ConcessionManagement.Model.UserInterface.Trade
{
    public class TradeView
    {
        /// <summary>
        /// Gets or sets the risk group.
        /// </summary>
        /// <value>
        /// The risk group.
        /// </value>
        public RiskGroup RiskGroup { get; set; }

        public LegalEntity LegalEntity { get; set; }

        /// <summary>
        /// Gets or sets the cash concessions.
        /// </summary>
        /// <value>
        /// The cash concessions.
        /// </value>
        public IEnumerable<TradeConcession> TradeConcessions { get; set; }

        /// <summary>
        /// Gets or sets the cash financial.
        /// </summary>
        /// <value>
        /// The cash financial.
        /// </value>
        public TradeFinancial TradeFinancial { get; set; }

        /// <summary>
        /// Gets or sets the cash products.
        /// </summary>
        /// <value>
        /// The cash products.
        /// </value>
        public IEnumerable<TradeProductGroup> TradeProductGroups { get; set; }

    }
}
