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
    
    public partial class tblLegalEntityAccount
    {
        public int pkLegalEntityAccountId { get; set; }
        public int fkLegalEntityId { get; set; }
        public string AccountNumber { get; set; }
        public bool IsActive { get; set; }
    
        public virtual tblLegalEntity tblLegalEntity { get; set; }
    }
}
