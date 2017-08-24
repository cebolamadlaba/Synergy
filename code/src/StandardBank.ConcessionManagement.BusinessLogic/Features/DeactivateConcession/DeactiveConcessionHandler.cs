using System;
using System.Threading.Tasks;
using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using Concession = StandardBank.ConcessionManagement.Model.UserInterface.Concession;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.DeactivateConcession
{
    /// <summary>
    /// Deactivate concession command handler
    /// </summary>
    /// <seealso cref="MediatR.IAsyncRequestHandler{DeactiveConcessionCommand, Concession}" />
    public class DeactiveConcessionHandler : IAsyncRequestHandler<DeactiveConcession, Concession>
    {
        /// <summary>
        /// The concession manager
        /// </summary>
        private readonly IConcessionManager _concessionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeactiveConcessionHandler"/> class.
        /// </summary>
        /// <param name="concessionManager">The concession manager.</param>
        public DeactiveConcessionHandler(IConcessionManager concessionManager)
        {
            _concessionManager = concessionManager;
        }

        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public async Task<Concession> Handle(DeactiveConcession message)
        {
            if (string.IsNullOrWhiteSpace(message.Concession.ReferenceNumber))
                throw new ArgumentNullException(nameof(message.Concession.ReferenceNumber));

            var result = _concessionManager.DeactivateConcession(message.Concession.ReferenceNumber, message.User);

            message.AuditRecord = new AuditRecord(result, message.User, AuditType.Update);

            return message.Concession;
        }
    }
}
