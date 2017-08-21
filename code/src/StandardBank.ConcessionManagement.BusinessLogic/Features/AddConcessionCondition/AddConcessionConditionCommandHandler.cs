using System.Threading.Tasks;
using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using ConcessionCondition = StandardBank.ConcessionManagement.Model.UserInterface.ConcessionCondition;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.AddConcessionCondition
{
    /// <summary>
    /// Add concession condition command handler
    /// </summary>
    /// <seealso cref="MediatR.IAsyncRequestHandler{AddConcessionConditionCommand, ConcessionCondition}" />
    public class AddConcessionConditionCommandHandler : IAsyncRequestHandler<AddConcessionConditionCommand, ConcessionCondition>
    {
        /// <summary>
        /// The concession manager
        /// </summary>
        private readonly IConcessionManager _concessionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddConcessionConditionCommandHandler"/> class.
        /// </summary>
        /// <param name="concessionManager">The concession manager.</param>
        public AddConcessionConditionCommandHandler(IConcessionManager concessionManager)
        {
            _concessionManager = concessionManager;
        }

        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public async Task<ConcessionCondition> Handle(AddConcessionConditionCommand message)
        {
            var result = _concessionManager.CreateConcessionCondition(message.ConcessionCondition, message.Concession);

            message.AuditRecord = new AuditRecord(result, message.User, AuditType.Insert);

            message.ConcessionCondition.Id = result.Id;
            return message.ConcessionCondition;
        }
    }
}
