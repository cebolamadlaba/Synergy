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
    public class DeactivateConcessionHandler : IAsyncRequestHandler<DeactivateConcession, string>
    {
        /// <summary>
        /// The concession manager
        /// </summary>
        private readonly IConcessionManager _concessionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeactivateConcessionHandler"/> class.
        /// </summary>
        /// <param name="concessionManager">The concession manager.</param>
        public DeactivateConcessionHandler(IConcessionManager concessionManager)
        {
            _concessionManager = concessionManager;
        }

        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public async Task<string> Handle(DeactivateConcession message)
        {
            if (string.IsNullOrWhiteSpace(message.ConcessionReferenceNumber))
                throw new ArgumentNullException(nameof(message.ConcessionReferenceNumber));

            var result = _concessionManager.DeactivateConcession(message.ConcessionReferenceNumber, message.IsRecall, message.User);

            message.AuditRecord = new AuditRecord(result, message.User, AuditType.Update);

            return message.ConcessionReferenceNumber;
        }

    }
}
