using System;
using System.Collections.Generic;
using System.Text;

namespace StandardBank.ConcessionManagement.Model.UserInterface.Investment
{
    public class InvestmentView
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
        public IEnumerable<InvestmentConcession> InvestmentConcessions { get; set; }

        /// <summary>
        /// Gets or sets the cash financial.
        /// </summary>
        /// <value>
        /// The cash financial.
        /// </value>
        public InvestmentFinancial InvestmentFinancial { get; set; }

        /// <summary>
        /// Gets or sets the cash products.
        /// </summary>
        /// <value>
        /// The cash products.
        /// </value>
        public IEnumerable<InvestmentProductGroup> InvestmentProductGroups { get; set; }

    }
}
