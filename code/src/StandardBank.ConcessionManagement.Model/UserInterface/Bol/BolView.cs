using System;
using System.Collections.Generic;
using System.Text;

namespace StandardBank.ConcessionManagement.Model.UserInterface.Bol
{
    public class BolView
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
        public IEnumerable<BolConcession> BolConcessions { get; set; }

        /// <summary>
        /// Gets or sets the cash financial.
        /// </summary>
        /// <value>
        /// The cash financial.
        /// </value>
        public BolFinancial BolFinancial { get; set; }

        /// <summary>
        /// Gets or sets the cash products.
        /// </summary>
        /// <value>
        /// The cash products.
        /// </value>
        public IEnumerable<BolProductGroup> BolProductGroups { get; set; }

    }
}
