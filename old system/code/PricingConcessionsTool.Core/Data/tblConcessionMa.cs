//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PricingConcessionsTool.Core.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblConcessionMa
    {
        public int pkConcessionMasId { get; set; }
        public int fkConcessionId { get; set; }
        public int fkTransactionTypeId { get; set; }
        public string MerchantNumber { get; set; }
        public Nullable<decimal> Turnover { get; set; }
        public Nullable<decimal> CommissionRate { get; set; }
    
        public virtual rtblTransactionType rtblTransactionType { get; set; }
        public virtual tblConcession tblConcession { get; set; }
    }
}
