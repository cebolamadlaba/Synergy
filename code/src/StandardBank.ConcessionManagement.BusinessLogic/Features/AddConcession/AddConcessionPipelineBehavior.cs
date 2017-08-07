using System.Threading.Tasks;
using MediatR;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.AddConcession
{
    /// <summary>
    /// Add concession pipeline behaviour
    /// </summary>
    /// <seealso cref="MediatR.IPipelineBehavior{AddConcessionCommand, Unit}" />
    public class AddConcessionPipelineBehavior : IPipelineBehavior<AddConcessionCommand, Unit>
    {
        /// <summary>
        /// The audit repository
        /// </summary>
        private readonly IAuditRepository _auditRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddConcessionPipelineBehavior"/> class.
        /// </summary>
        /// <param name="auditRepository">The audit repository.</param>
        public AddConcessionPipelineBehavior(IAuditRepository auditRepository)
        {
            _auditRepository = auditRepository;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="next">The next.</param>
        /// <returns></returns>
        public async Task<Unit> Handle(AddConcessionCommand request, RequestHandlerDelegate<Unit> next)
        {
            var response = await next();
            
            _auditRepository.Audit(request.RepositoryConcession, AuditType.Insert, request.User?.ANumber);

            return response;
        }
    }
}
