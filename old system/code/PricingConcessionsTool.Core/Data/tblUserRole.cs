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
    
    public partial class tblUserRole
    {
        public int pkUserRoleId { get; set; }
        public int fkUserId { get; set; }
        public int fkRoleId { get; set; }
        public bool IsActive { get; set; }
    
        public virtual rtblRole rtblRole { get; set; }
        public virtual tblUser tblUser { get; set; }
    }
}
