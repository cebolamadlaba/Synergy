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

        public string GlmsProductType { get; set; }

        public string GlmsProductTypeId { get; set; }

        public string GlmsProductName { get; set; }

        public string RiskGroupName { get; set; }

        public string LegalEntity  { get; set; }

        public string AccountNumber { get; set; }      

        public string LoadedRate { get; set; }

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
