using System.Threading.Tasks;
using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface.Trade;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.TradeConcession
{
    /// <summary>
    /// Add cash concession detail command handler
    /// </summary>
    /// <seealso cref="MediatR.IAsyncRequestHandler{AddCashConcessionDetailCommand, CashConcessionDetail}" />
    public class AddOrUpdateTradeConcessionDetailHandler : IAsyncRequestHandler<AddOrUpdateTradeConcessionDetail, TradeConcessionDetail>
    {
        /// <summary>
        /// The cash manager
        /// </summary>
        private readonly ITradeManager _tradeManager;
     
        public AddOrUpdateTradeConcessionDetailHandler(ITradeManager tradeManager)
        {
            _tradeManager = tradeManager;
        }

        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public async Task<TradeConcessionDetail> Handle(AddOrUpdateTradeConcessionDetail message)
        {
            if (message.TradeConcessionDetail.TradeConcessionDetailId == 0)
            {
                var result = _tradeManager.CreateConcessionTrade(message.TradeConcessionDetail, message.Concession);

                message.AuditRecord = new AuditRecord(result, message.User, AuditType.Insert);
                message.TradeConcessionDetail.TradeConcessionDetailId = result.Id;
            }
            else
            {
                var result = _tradeManager.UpdateConcessionTrade(message.TradeConcessionDetail, message.Concession);
                message.AuditRecord = new AuditRecord(result, message.User, AuditType.Update);
            }

            return message.TradeConcessionDetail;
        }

      
    }
}
