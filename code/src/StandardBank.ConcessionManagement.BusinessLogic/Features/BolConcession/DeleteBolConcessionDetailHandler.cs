using System.Threading.Tasks;
using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface.Bol;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.BolConcession
{
    /// <summary>
    /// Delete cash concession detail command handler
    /// </summary>
    /// <seealso cref="MediatR.IAsyncRequestHandler{DeleteCashConcessionDetailCommand, CashConcessionDetail}" />
    public class DeleteBolConcessionDetailHandler : IAsyncRequestHandler<DeleteBolConcessionDetail, BolConcessionDetail>
    {
        /// <summary>
        /// The cash manager
        /// </summary>
        private readonly IBolManager _bolManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCashConcessionDetailHandler"/> class.
        /// </summary>
        /// <param name="cashManager">The cash manager.</param>
        public DeleteBolConcessionDetailHandler(IBolManager bolManager)
        {
            _bolManager = bolManager;
        }

        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public async Task<BolConcessionDetail> Handle(DeleteBolConcessionDetail message)
        {
            var result = _bolManager.DeleteConcessionBol(message.BolConcessionDetail);

            message.AuditRecord = new AuditRecord(result, message.User, AuditType.Delete);

            return message.BolConcessionDetail;
        }
    }
}
