using System.Threading.Tasks;
using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface.Investment;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.InvestmentConcession
{
    /// <summary>
    /// Add cash concession detail command handler
    /// </summary>
    /// <seealso cref="MediatR.IAsyncRequestHandler{AddCashConcessionDetailCommand, CashConcessionDetail}" />
    public class AddOrUpdateInvestmentConcessionDetailHandler : IAsyncRequestHandler<AddOrUpdateInvestmentConcessionDetail, InvestmentConcessionDetail>
    {
        /// <summary>
        /// The cash manager
        /// </summary>
        private readonly IInvestmentManager _investmentManager;
     
        public AddOrUpdateInvestmentConcessionDetailHandler(IInvestmentManager investmentManager)
        {
            _investmentManager = investmentManager;
        }

        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public async Task<InvestmentConcessionDetail> Handle(AddOrUpdateInvestmentConcessionDetail message)
        {
            if (message.InvestmentConcessionDetail.InvestmentConcessionDetailId == 0)
            {
                var result = _investmentManager.CreateConcessionInvestment(message.InvestmentConcessionDetail, message.Concession);

                message.AuditRecord = new AuditRecord(result, message.User, AuditType.Insert);
                message.InvestmentConcessionDetail.InvestmentConcessionDetailId = result.Id;
            }
            else
            {
                var result = _investmentManager.UpdateConcessionInvestment(message.InvestmentConcessionDetail, message.Concession);
                message.AuditRecord = new AuditRecord(result, message.User, AuditType.Update);
            }

            return message.InvestmentConcessionDetail;
        }

      
    }
}
