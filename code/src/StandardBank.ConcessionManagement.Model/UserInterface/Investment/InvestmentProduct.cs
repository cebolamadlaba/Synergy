using System;
using System.Collections.Generic;
using System.Text;

namespace StandardBank.ConcessionManagement.Model.UserInterface.Investment
{
    public class InvestmentProduct
    {
        /// <summary>
        /// Gets or sets the cash product identifier.
        /// </summary>
        /// <value>
        /// The cash product identifier.
        /// </value>
        public int InvestmentProductId { get; set; }

        public string InvestmentProductType { get; set; }

        public string InvestmentProductTypeId { get; set; }

        public string InvestmentProductName{ get; set; }

        public string RiskGroupName { get; set; }

        public string LegalEntity  { get; set; }

        public string AccountNumber { get; set; }      

        public string LoadedRate { get; set; }

    }

    public class InvestmentProductGroup
    {
        /// <summary>
        /// Gets or sets the cash product identifier.
        /// </summary>
        /// <value>
        /// The cash product identifier.       /// </value>
      

        public string RiskGroupName { get; set; }

        public string LegalEntity { get; set; }

        public List<InvestmentProduct> InvestmentProducts { get; set; }

    }
}
