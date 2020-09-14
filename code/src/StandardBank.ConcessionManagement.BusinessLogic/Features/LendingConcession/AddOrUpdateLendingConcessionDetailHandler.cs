using System.Threading.Tasks;
using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface.Lending;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.LendingConcession
{
    /// <summary>
    /// Add lending concession detail command handler
    /// </summary>
    /// <seealso cref="MediatR.IAsyncRequestHandler{AddLendingConcessionCommand, LendingConcessionDetail}" />
    public class AddOrUpdateLendingConcessionDetailHandler : IAsyncRequestHandler<AddOrUpdateLendingConcessionDetail, LendingConcessionDetail>
    {
        /// <summary>
        /// The lending manager
        /// </summary>
        private readonly ILendingManager _lendingManager;

        /// <summary>
        /// Initializes the class
        /// </summary>
        /// <param name="lendingManager"></param>
        public AddOrUpdateLendingConcessionDetailHandler(ILendingManager lendingManager)
        {
            _lendingManager = lendingManager;
        }

        /// <summary>
        /// Handles the message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<LendingConcessionDetail> Handle(AddOrUpdateLendingConcessionDetail message)
        {
            if (message.LendingConcessionDetail.LendingConcessionDetailId == 0)
            {
                var result =
                    _lendingManager.CreateConcessionLending(message.LendingConcessionDetail, message.Concession);

                message.AuditRecord = new AuditRecord(result, message.User, AuditType.Insert);
                message.LendingConcessionDetail.LendingConcessionDetailId = result.Id;

                if (result.Id > 0)
                {
                    foreach (var lendingConcessionDetailTieredRate in message.LendingConcessionDetail.LendingConcessionDetailTieredRates)
                        lendingConcessionDetailTieredRate.ConcessionLendingId = result.Id;

                    _lendingManager.CreateConcessionLendingTieredRates(message.LendingConcessionDetail.LendingConcessionDetailTieredRates);
                }
            }
            else
            {
                var result =
                    _lendingManager.UpdateConcessionLending(message.LendingConcessionDetail, message.Concession);

                message.AuditRecord = new AuditRecord(result, message.User, AuditType.Update);
            }

            return message.LendingConcessionDetail;
        }
    }
}
