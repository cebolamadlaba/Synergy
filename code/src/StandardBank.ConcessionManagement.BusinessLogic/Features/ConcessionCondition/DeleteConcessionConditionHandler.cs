using System.Threading.Tasks;
using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.ConcessionCondition
{
    /// <summary>
    /// Delete concession condition command handler
    /// </summary>
    /// <seealso cref="MediatR.IAsyncRequestHandler{DeleteConcessionConditionCommand, ConcessionCondition}" />
    public class DeleteConcessionConditionHandler : IAsyncRequestHandler<DeleteConcessionCondition, Model.UserInterface.ConcessionCondition>
    {
        /// <summary>
        /// The concession manager
        /// </summary>
        private readonly IConcessionManager _concessionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteConcessionConditionHandler"/> class.
        /// </summary>
        /// <param name="concessionManager">The concession manager.</param>
        public DeleteConcessionConditionHandler(IConcessionManager concessionManager)
        {
            _concessionManager = concessionManager;
        }

        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public async Task<Model.UserInterface.ConcessionCondition> Handle(DeleteConcessionCondition message)
        {
            var result = _concessionManager.DeleteConcessionCondition(message.ConcessionCondition);

            message.AuditRecord = new AuditRecord(result, message.User, AuditType.Delete);

            message.ConcessionCondition.ConcessionConditionId = result.Id;
            return message.ConcessionCondition;
        }
    }
}
