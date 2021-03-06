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
    
    public partial class tblConcessionLending
    {
        public int pkConcessionLendingId { get; set; }
        public int fkConcessionId { get; set; }
        public int fkProductTypeId { get; set; }
        public Nullable<decimal> Limit { get; set; }
        public Nullable<int> Term { get; set; }
        public Nullable<decimal> MarginToPrime { get; set; }
        public Nullable<decimal> InitiationFee { get; set; }
        public Nullable<decimal> ReviewFee { get; set; }
        public Nullable<decimal> UFFFee { get; set; }
        public Nullable<int> fkReviewFeeTypeId { get; set; }
    
        public virtual rtblProduct rtblProduct { get; set; }
        public virtual rtblReviewFeeType rtblReviewFeeType { get; set; }
        public virtual tblConcession tblConcession { get; set; }
    }
}
