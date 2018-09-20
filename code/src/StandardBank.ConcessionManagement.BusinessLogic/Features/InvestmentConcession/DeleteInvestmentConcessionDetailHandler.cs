using System.Threading.Tasks;
using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface.Investment;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.InvestmentConcession
{
    /// <summary>
    /// Delete cash concession detail command handler
    /// </summary>
    /// <seealso cref="MediatR.IAsyncRequestHandler{DeleteCashConcessionDetailCommand, CashConcessionDetail}" />
    public class DeleteInvestmentConcessionDetailHandler : IAsyncRequestHandler<DeleteInvestmentConcessionDetail, InvestmentConcessionDetail>
    {
        /// <summary>
        /// The cash manager
        /// </summary>
        private readonly IInvestmentManager _investmentManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCashConcessionDetailHandler"/> class.
        /// </summary>
        /// <param name="cashManager">The cash manager.</param>
        public DeleteInvestmentConcessionDetailHandler(IInvestmentManager investmentManager)
        {
            _investmentManager = investmentManager;
        }

        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public async Task<InvestmentConcessionDetail> Handle(DeleteInvestmentConcessionDetail message)
        {
            var result = _investmentManager.DeleteConcessionInvestment(message.InvestmentConcessionDetail);

            message.AuditRecord = new AuditRecord(result, message.User, AuditType.Delete);

            return message.InvestmentConcessionDetail;
        }
    }
}
