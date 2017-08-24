using System.Threading.Tasks;
using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface.Cash;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.AddOrUpdateCashConcessionDetail
{
    /// <summary>
    /// Add cash concession detail command handler
    /// </summary>
    /// <seealso cref="MediatR.IAsyncRequestHandler{AddCashConcessionDetailCommand, CashConcessionDetail}" />
    public class AddOrUpdateCashConcessionDetailCommandHandler : IAsyncRequestHandler<AddOrUpdateCashConcessionDetailCommand, CashConcessionDetail>
    {
        /// <summary>
        /// The cash manager
        /// </summary>
        private readonly ICashManager _cashManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddOrUpdateCashConcessionDetailCommandHandler"/> class.
        /// </summary>
        /// <param name="cashManager">The cash manager.</param>
        public AddOrUpdateCashConcessionDetailCommandHandler(ICashManager cashManager)
        {
            _cashManager = cashManager;
        }

        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public async Task<CashConcessionDetail> Handle(AddOrUpdateCashConcessionDetailCommand message)
        {
            if (message.CashConcessionDetail.CashConcessionDetailId == 0)
            {
                var result = _cashManager.CreateConcessionCash(message.CashConcessionDetail, message.Concession);
                message.AuditRecord = new AuditRecord(result, message.User, AuditType.Insert);
                message.CashConcessionDetail.CashConcessionDetailId = result.Id;
            }
            else
            {
                var result = _cashManager.UpdateConcessionCash(message.CashConcessionDetail, message.Concession);
                message.AuditRecord = new AuditRecord(result, message.User, AuditType.Update);
            }

            return message.CashConcessionDetail;
        }
    }
}
