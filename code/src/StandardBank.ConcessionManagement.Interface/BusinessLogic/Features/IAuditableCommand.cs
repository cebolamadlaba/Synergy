using StandardBank.ConcessionManagement.Model.BusinessLogic;

namespace StandardBank.ConcessionManagement.Interface.BusinessLogic.Features
{
    /// <summary>
    /// Auditable command interface
    /// </summary>
    public interface IAuditableCommand
    {
        /// <summary>
        /// Gets or sets the audit record.
        /// </summary>
        /// <value>
        /// The audit record.
        /// </value>
        AuditRecord AuditRecord { get; set; }
    }
}
