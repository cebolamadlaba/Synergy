using System.Threading.Tasks;
using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface.Lending;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.AddOrUpdateLendingConcessionDetail
{
    /// <summary>
    /// Add lending concession detail command handler
    /// </summary>
    /// <seealso cref="MediatR.IAsyncRequestHandler{AddLendingConcessionCommand, LendingConcessionDetail}" />
    public class AddOrUpdateLendingConcessionDetailCommandHandler : IAsyncRequestHandler<AddOrUpdateLendingConcessionDetailCommand, LendingConcessionDetail>
    {
        /// <summary>
        /// The lending manager
        /// </summary>
        private readonly ILendingManager _lendingManager;

        /// <summary>
        /// Initializes the class
        /// </summary>
        /// <param name="lendingManager"></param>
        public AddOrUpdateLendingConcessionDetailCommandHandler(ILendingManager lendingManager)
        {
            _lendingManager = lendingManager;
        }

        /// <summary>
        /// Handles the message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<LendingConcessionDetail> Handle(AddOrUpdateLendingConcessionDetailCommand message)
        {
            if (message.LendingConcessionDetail.LendingConcessionDetailId == 0)
            {
                var result =
                    _lendingManager.CreateConcessionLending(message.LendingConcessionDetail, message.Concession);

                message.AuditRecord = new AuditRecord(result, message.User, AuditType.Insert);
                message.LendingConcessionDetail.LendingConcessionDetailId = result.Id;
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
