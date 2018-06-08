using System.Threading.Tasks;
using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface.Bol;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.BolConcession
{
    /// <summary>
    /// Add cash concession detail command handler
    /// </summary>
    /// <seealso cref="MediatR.IAsyncRequestHandler{AddCashConcessionDetailCommand, CashConcessionDetail}" />
    public class AddOrUpdateBolConcessionDetailHandler : IAsyncRequestHandler<AddOrUpdateBolConcessionDetail, BolConcessionDetail>
    {
        /// <summary>
        /// The cash manager
        /// </summary>
        private readonly IBolManager _bolManager;

     
        public AddOrUpdateBolConcessionDetailHandler(IBolManager bolManager)
        {
            _bolManager = bolManager;
        }

        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public async Task<BolConcessionDetail> Handle(AddOrUpdateBolConcessionDetail message)
        {
            if (message.BolConcessionDetail.BolConcessionDetailId == 0)
            {
                var result = _bolManager.CreateConcessionBol(message.BolConcessionDetail, message.Concession);
                message.AuditRecord = new AuditRecord(result, message.User, AuditType.Insert);
                message.BolConcessionDetail.BolConcessionDetailId = result.Id;
            }
            else
            {
                var result = _bolManager.UpdateConcessionBol(message.BolConcessionDetail, message.Concession);
                message.AuditRecord = new AuditRecord(result, message.User, AuditType.Update);
            }

            return message.BolConcessionDetail;
        }
    }
}
