using System.Threading.Tasks;
using MediatR.Pipeline;
using StandardBank.ConcessionManagement.Interface.BusinessLogic.Features;
using StandardBank.ConcessionManagement.Interface.Repository;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.AuditRecordPostHandler
{
    /// <summary>
    /// Audit record post handler
    /// </summary>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    /// <seealso cref="MediatR.Pipeline.IRequestPostProcessor{TRequest, TResponse}" />
    public class AuditRecordPostHandler<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
    {
        /// <summary>
        /// The audit repository
        /// </summary>
        private readonly IAuditRepository _auditRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditRecordPostHandler{TRequest, TResponse}"/> class.
        /// </summary>
        /// <param name="auditRepository">The audit repository.</param>
        public AuditRecordPostHandler(IAuditRepository auditRepository)
        {
            _auditRepository = auditRepository;
        }

        /// <summary>
        /// Processes the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="response">The response.</param>
        /// <returns></returns>
        public async Task Process(TRequest request, TResponse response)
        {
            var command = request as IAuditableCommand;

            if (command?.AuditRecord != null)
                await _auditRepository.Audit(command.AuditRecord.Entity, command.AuditRecord.AuditType,
                    command?.AuditRecord?.User?.ANumber);
        }
    }
}
