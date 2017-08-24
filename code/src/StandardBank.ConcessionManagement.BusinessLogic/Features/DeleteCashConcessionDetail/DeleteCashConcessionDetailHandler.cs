using System.Threading.Tasks;
using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface.Cash;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.DeleteCashConcessionDetail
{
    /// <summary>
    /// Delete cash concession detail command handler
    /// </summary>
    /// <seealso cref="MediatR.IAsyncRequestHandler{DeleteCashConcessionDetailCommand, CashConcessionDetail}" />
    public class DeleteCashConcessionDetailHandler : IAsyncRequestHandler<DeleteCashConcessionDetail, CashConcessionDetail>
    {
        /// <summary>
        /// The cash manager
        /// </summary>
        private readonly ICashManager _cashManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCashConcessionDetailHandler"/> class.
        /// </summary>
        /// <param name="cashManager">The cash manager.</param>
        public DeleteCashConcessionDetailHandler(ICashManager cashManager)
        {
            _cashManager = cashManager;
        }

        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public async Task<CashConcessionDetail> Handle(DeleteCashConcessionDetail message)
        {
            var result = _cashManager.DeleteConcessionCash(message.CashConcessionDetail);

            message.AuditRecord = new AuditRecord(result, message.User, AuditType.Delete);

            return message.CashConcessionDetail;
        }
    }
}
