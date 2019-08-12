using System;
using System.Collections.Generic;
using System.Text;

namespace StandardBank.ConcessionManagement.Model.Repository
{ 

    public class BOLChargeCode
    {
        public int pkChargeCodeId { get; set; }

        public string Description { get; set; }     

        public string ChargeCodeDesc { get; set; }

        public string ChargeCode { get; set; }

        public int BOLChargeCodeTypeId { get; set; }

        public int fkChargeCodeTypeId { get; set; }

        public int length { get; set; }

        public bool IsActive { get; set; }

        public decimal StandardPricingOption1 { get; set; }

        public decimal StandardPricingOption2 { get; set; }

        public decimal StandardPricingOption3 { get; set; }

        public bool IsNonUniversal { get; set; }
    }
}
