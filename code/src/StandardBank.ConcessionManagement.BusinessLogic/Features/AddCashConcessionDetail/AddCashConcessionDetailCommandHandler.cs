using System.Threading.Tasks;
using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface.Cash;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.AddCashConcessionDetail
{
    /// <summary>
    /// Add cash concession detail command handler
    /// </summary>
    /// <seealso cref="MediatR.IAsyncRequestHandler{AddCashConcessionDetailCommand, CashConcessionDetail}" />
    public class AddCashConcessionDetailCommandHandler : IAsyncRequestHandler<AddCashConcessionDetailCommand, CashConcessionDetail>
    {
        /// <summary>
        /// The cash manager
        /// </summary>
        private readonly ICashManager _cashManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddCashConcessionDetailCommandHandler"/> class.
        /// </summary>
        /// <param name="cashManager">The cash manager.</param>
        public AddCashConcessionDetailCommandHandler(ICashManager cashManager)
        {
            _cashManager = cashManager;
        }

        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public async Task<CashConcessionDetail> Handle(AddCashConcessionDetailCommand message)
        {
            var result = _cashManager.CreateConcessionCash(message.CashConcessionDetail, message.Concession);

            message.AuditRecord = new AuditRecord(result, message.User, AuditType.Insert);

            return message.CashConcessionDetail;
        }
    }
}
