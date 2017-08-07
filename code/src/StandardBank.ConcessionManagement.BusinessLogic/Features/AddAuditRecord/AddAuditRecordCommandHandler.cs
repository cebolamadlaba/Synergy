using MediatR;
using StandardBank.ConcessionManagement.Interface.Repository;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.AddAuditRecord
{
    /// <summary>
    /// Add audit record command handler
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler{AddAuditRecordCommand, Unit}" />
    public class AddAuditRecordCommandHandler : IRequestHandler<AddAuditRecordCommand, Unit>
    {
        /// <summary>
        /// The audit repository
        /// </summary>
        private readonly IAuditRepository _auditRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddAuditRecordCommandHandler"/> class.
        /// </summary>
        /// <param name="auditRepository">The audit repository.</param>
        public AddAuditRecordCommandHandler(IAuditRepository auditRepository)
        {
            _auditRepository = auditRepository;
        }

        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public Unit Handle(AddAuditRecordCommand message)
        {
            _auditRepository.Audit(message.AuditRecord.Entity, message.AuditRecord.AuditType,
                message.AuditRecord.User?.ANumber);

            return new Unit();
        }
    }
}
