using System.Threading.Tasks;
using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using ConcessionCondition = StandardBank.ConcessionManagement.Model.UserInterface.ConcessionCondition;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.DeleteConcessionCondition
{
    /// <summary>
    /// Delete concession condition command handler
    /// </summary>
    /// <seealso cref="MediatR.IAsyncRequestHandler{DeleteConcessionConditionCommand, ConcessionCondition}" />
    public class DeleteConcessionConditionCommandHandler : IAsyncRequestHandler<DeleteConcessionConditionCommand, ConcessionCondition>
    {
        /// <summary>
        /// The concession manager
        /// </summary>
        private readonly IConcessionManager _concessionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteConcessionConditionCommandHandler"/> class.
        /// </summary>
        /// <param name="concessionManager">The concession manager.</param>
        public DeleteConcessionConditionCommandHandler(IConcessionManager concessionManager)
        {
            _concessionManager = concessionManager;
        }

        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public async Task<ConcessionCondition> Handle(DeleteConcessionConditionCommand message)
        {
            var result = _concessionManager.DeleteConcessionCondition(message.ConcessionCondition);

            message.AuditRecord = new AuditRecord(result, message.User, AuditType.Delete);

            message.ConcessionCondition.Id = result.Id;
            return message.ConcessionCondition;
        }
    }
}
