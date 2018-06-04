using System.Threading.Tasks;
using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface.Trade;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.TradeConcession
{
    /// <summary>
    /// Delete cash concession detail command handler
    /// </summary>
    /// <seealso cref="MediatR.IAsyncRequestHandler{DeleteCashConcessionDetailCommand, CashConcessionDetail}" />
    public class DeleteTradeConcessionDetailHandler : IAsyncRequestHandler<DeleteTradeConcessionDetail, TradeConcessionDetail>
    {
        /// <summary>
        /// The cash manager
        /// </summary>
        private readonly ITradeManager _tradeManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCashConcessionDetailHandler"/> class.
        /// </summary>
        /// <param name="cashManager">The cash manager.</param>
        public DeleteTradeConcessionDetailHandler(ITradeManager tradeManager)
        {
            _tradeManager = tradeManager;
        }

        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public async Task<TradeConcessionDetail> Handle(DeleteTradeConcessionDetail message)
        {
            var result = _tradeManager.DeleteConcessionTrade(message.TradeConcessionDetail);

            message.AuditRecord = new AuditRecord(result, message.User, AuditType.Delete);

            return message.TradeConcessionDetail;
        }
    }
}
