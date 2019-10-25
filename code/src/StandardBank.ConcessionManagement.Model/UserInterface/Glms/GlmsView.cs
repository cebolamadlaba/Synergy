using System;
using System.Collections.Generic;
using System.Text;

namespace StandardBank.ConcessionManagement.Model.UserInterface.Glms
{
    public class GlmsView
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
        /// Gets or sets the Glms concessions.
        /// </summary>
        /// <value>
        /// The Glms concessions.
        /// </value>
        public IEnumerable<GlmsConcession> GlmsConcessions { get; set; }

        /// <summary>
        /// Gets or sets the Glms products.
        /// </summary>
        /// <value>
        /// The cash products.
        /// </value>
        public IEnumerable<GlmsProductGroup> GlmsProductGroups { get; set; }

    }
}
