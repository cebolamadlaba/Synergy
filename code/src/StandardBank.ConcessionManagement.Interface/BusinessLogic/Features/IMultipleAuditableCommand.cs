using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.BusinessLogic;

namespace StandardBank.ConcessionManagement.Interface.BusinessLogic.Features
{
    /// <summary>
    /// Multiple auditable commands
    /// </summary>
    public interface IMultipleAuditableCommand
    {
        /// <summary>
        /// Gets or sets the audit records.
        /// </summary>
        /// <value>
        /// The audit records.
        /// </value>
        IEnumerable<AuditRecord> AuditRecords { get; set; }
    }
}
