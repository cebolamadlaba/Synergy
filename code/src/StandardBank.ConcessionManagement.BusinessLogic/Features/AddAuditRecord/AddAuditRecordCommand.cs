using MediatR;
using StandardBank.ConcessionManagement.Model.BusinessLogic;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.AddAuditRecord
{
    /// <summary>
    /// Add audit record command
    /// </summary>
    /// <seealso cref="Unit" />
    public class AddAuditRecordCommand : IRequest<Unit>
    {
        /// <summary>
        /// Gets or sets the audit record.
        /// </summary>
        /// <value>
        /// The audit record.
        /// </value>
        public AuditRecord AuditRecord { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddAuditRecordCommand"/> class.
        /// </summary>
        /// <param name="auditRecord">The audit record.</param>
        public AddAuditRecordCommand(AuditRecord auditRecord)
        {
            AuditRecord = auditRecord;
        }
    }
}
