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
    
    public partial class tblCentreUser
    {
        public int pkCentreUserId { get; set; }
        public int fkCentreId { get; set; }
        public int fkUserId { get; set; }
        public bool IsActive { get; set; }
    
        public virtual tblCentre tblCentre { get; set; }
        public virtual tblUser tblUser { get; set; }
    }
}
