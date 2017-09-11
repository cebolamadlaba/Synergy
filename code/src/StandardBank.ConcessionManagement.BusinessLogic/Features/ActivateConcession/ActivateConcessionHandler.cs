using System;
using System.Threading.Tasks;
using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.ActivateConcession
{
    /// <summary>
    /// Activate concession handler
    /// </summary>
    /// <seealso cref="MediatR.IAsyncRequestHandler{ActivateConcession, String}" />
    public class ActivateConcessionHandler : IAsyncRequestHandler<ActivateConcession, string>
    {
        /// <summary>
        /// The concession manager
        /// </summary>
        private readonly IConcessionManager _concessionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivateConcessionHandler"/> class.
        /// </summary>
        /// <param name="concessionManager">The concession manager.</param>
        public ActivateConcessionHandler(IConcessionManager concessionManager)
        {
            _concessionManager = concessionManager;
        }

        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">ConcessionReferenceNumber</exception>
        public async Task<string> Handle(ActivateConcession message)
        {
            if (string.IsNullOrWhiteSpace(message.ConcessionReferenceNumber))
                throw new ArgumentNullException(nameof(message.ConcessionReferenceNumber));

            var result = _concessionManager.ActivateConcession(message.ConcessionReferenceNumber, message.User);

            message.AuditRecord = new AuditRecord(result, message.User, AuditType.Update);

            return message.ConcessionReferenceNumber;
        }
    }
}
