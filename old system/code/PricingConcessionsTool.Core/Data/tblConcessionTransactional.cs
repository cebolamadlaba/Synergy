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
    
    public partial class tblConcessionTransactional
    {
        public int pkConcessionTransactionalId { get; set; }
        public Nullable<int> fkConcessionId { get; set; }
        public Nullable<int> fkTransactionTypeId { get; set; }
        public Nullable<int> fkChannelTypeId { get; set; }
        public Nullable<int> TableNumber { get; set; }
        public Nullable<int> TransactionVolume { get; set; }
        public Nullable<decimal> TransactionValue { get; set; }
        public Nullable<int> fkBaseRateId { get; set; }
        public Nullable<decimal> AdValorem { get; set; }
    
        public virtual rtblBaseRate rtblBaseRate { get; set; }
        public virtual rtblChannelType rtblChannelType { get; set; }
        public virtual rtblTransactionType rtblTransactionType { get; set; }
        public virtual tblConcession tblConcession { get; set; }
    }
}
