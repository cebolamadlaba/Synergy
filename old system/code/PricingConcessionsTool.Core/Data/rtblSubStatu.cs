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
    
    public partial class rtblSubStatu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public rtblSubStatu()
        {
            this.tblConcessionApprovals = new HashSet<tblConcessionApproval>();
            this.tblConcessionApprovals1 = new HashSet<tblConcessionApproval>();
            this.tblConcessions = new HashSet<tblConcession>();
        }
    
        public int pkSubStatusId { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblConcessionApproval> tblConcessionApprovals { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblConcessionApproval> tblConcessionApprovals1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblConcession> tblConcessions { get; set; }
    }
}