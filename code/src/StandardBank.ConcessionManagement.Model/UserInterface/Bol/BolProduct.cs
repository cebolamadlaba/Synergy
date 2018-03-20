using System;
using System.Collections.Generic;
using System.Text;

namespace StandardBank.ConcessionManagement.Model.UserInterface.Bol
{
    public class BolProduct
    {
        /// <summary>
        /// Gets or sets the cash product identifier.
        /// </summary>
        /// <value>
        /// The cash product identifier.
        /// </value>
        public int BolProductId { get; set; }

        public string RiskGroupName { get; set; }

        public string LegalEntity  { get; set; }

        public string BOLUserId { get; set; }

        public string ChargeCode { get; set; }

        public string ChargeCodeDesc { get; set; }

        public decimal LoadedRate { get; set; }

    }
}
