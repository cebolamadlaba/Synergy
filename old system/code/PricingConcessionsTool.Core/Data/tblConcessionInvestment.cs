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
    
    public partial class tblConcessionInvestment
    {
        public int pkConcessionInvestmentId { get; set; }
        public int fkConcessionId { get; set; }
        public int fkProductTypeId { get; set; }
        public decimal Balance { get; set; }
        public int Term { get; set; }
        public decimal InterestToCustomer { get; set; }
    
        public virtual rtblProduct rtblProduct { get; set; }
        public virtual tblConcession tblConcession { get; set; }
    }
}
