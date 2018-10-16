using System;
using System.Collections.Generic;
using System.Text;

namespace StandardBank.ConcessionManagement.Model.UserInterface.Trade
{
    public class TradeProduct
    {
        /// <summary>
        /// Gets or sets the cash product identifier.
        /// </summary>
        /// <value>
        /// The cash product identifier.
        /// </value>
        public int TradeProductId { get; set; }

        public string TradeProductType { get; set; }

        public string TradeProductTypeId { get; set; }

        public string TradeProductName{ get; set; }

        public string RiskGroupName { get; set; }

        public string LegalEntity  { get; set; }

        public string AccountNumber { get; set; }      

        public string LoadedRate { get; set; }

    }

    public class TradeProductGroup
    {
        /// <summary>
        /// Gets or sets the cash product identifier.
        /// </summary>
        /// <value>
        /// The cash product identifier.       /// </value>
      

        public string RiskGroupName { get; set; }

        public string LegalEntity { get; set; }

        public List<TradeProduct> TradeProducts { get; set; }

    }
}
