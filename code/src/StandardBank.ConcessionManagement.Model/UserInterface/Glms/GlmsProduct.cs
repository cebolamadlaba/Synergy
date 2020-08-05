using System;
using System.Collections.Generic;
using System.Text;

namespace StandardBank.ConcessionManagement.Model.UserInterface.Glms
{
    public class GlmsProduct
    {
        /// <summary>
        /// Gets or sets the Glms product identifier.
        /// </summary>
        /// <value>
        /// The Glms product identifier.
        /// </value>
        public int GlmsProductId { get; set; }

        public string GroupType { get; set; }

        public string pricingDescription { get; set; }

        public int GroupNumber { get; set; }

        public string GlmsProductName { get; set; }

        public string RiskGroupName { get; set; }

        public string LegalEntity  { get; set; }

        public decimal Spread { get; set; }      

        public string RateType { get; set; }

    }

    public class GlmsProductGroup
    {
        /// <summary>
        /// Gets or sets the Glms product identifier.
        /// </summary>
        /// <value>
        /// The Glms product identifier.       /// </value>

        public string RiskGroupName { get; set; }

        public string LegalEntity { get; set; }

        public List<GlmsProduct> GlmsProducts { get; set; }

    }
}
