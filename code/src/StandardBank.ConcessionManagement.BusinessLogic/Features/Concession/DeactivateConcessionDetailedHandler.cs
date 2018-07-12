using System;
using System.Threading.Tasks;
using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.Concession
{
    /// <summary>
    /// Deactivate concession command handler
    /// </summary>
    /// <seealso cref="MediatR.IAsyncRequestHandler{DeactiveConcessionCommand, Concession}" />
    public class DeactivateConcessionDetailedHandler : IAsyncRequestHandler<DeactivateConcessionDetailed, string>
    {
        /// <summary>
        /// The concession manager
        /// </summary>
        private readonly IConcessionManager _concessionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeactivateConcessionDetailedHandler"/> class.
        /// </summary>
        /// <param name="concessionManager">The concession manager.</param>
        public DeactivateConcessionDetailedHandler(IConcessionManager concessionManager)
        {
            _concessionManager = concessionManager;
        }

        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>    

        public async Task<string> Handle(DeactivateConcessionDetailed message)
        {

            var result = _concessionManager.DeactivateConcessionDetailed(message.ConcessionDetailId, message.User);

            message.AuditRecord = new AuditRecord(result, message.User, AuditType.Update);

            return message.ConcessionDetailId.ToString();
        }
    }
}
