using System.Threading.Tasks;
using MediatR;
using MediatR.Pipeline;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.AddConcession
{
    /// <summary>
    /// Add concession post handler
    /// </summary>
    /// <seealso cref="MediatR.Pipeline.IRequestPostProcessor{AddConcessionCommand, Unit}" />
    public class AddConcessionPostHandler : IRequestPostProcessor<AddConcessionCommand, Unit>
    {
        /// <summary>
        /// The audit repository
        /// </summary>
        private readonly IAuditRepository _auditRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddConcessionPostHandler"/> class.
        /// </summary>
        /// <param name="auditRepository">The audit repository.</param>
        public AddConcessionPostHandler(IAuditRepository auditRepository)
        {
            _auditRepository = auditRepository;
        }

        /// <summary>
        /// Processes the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="response">The response.</param>
        /// <returns></returns>
        public async Task Process(AddConcessionCommand request, Unit response)
        {
            _auditRepository.Audit(request.RepositoryConcession, AuditType.Insert, request.User?.ANumber);
        }
    }
}
