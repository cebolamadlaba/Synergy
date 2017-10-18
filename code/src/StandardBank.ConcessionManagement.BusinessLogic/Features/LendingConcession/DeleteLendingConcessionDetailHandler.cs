using System.Threading.Tasks;
using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface.Lending;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.LendingConcession
{
    /// <summary>
    /// Delete lending concession detail command handler
    /// </summary>
    /// <seealso cref="MediatR.IAsyncRequestHandler{DeleteLendingConcessionDetailCommand, LendingConcessionDetail}" />
    public class DeleteLendingConcessionDetailHandler : IAsyncRequestHandler<DeleteLendingConcessionDetail, LendingConcessionDetail>
    {
        /// <summary>
        /// The lending manager
        /// </summary>
        private readonly ILendingManager _lendingManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteLendingConcessionDetailHandler"/> class.
        /// </summary>
        /// <param name="lendingManager">The lending manager.</param>
        public DeleteLendingConcessionDetailHandler(ILendingManager lendingManager)
        {
            _lendingManager = lendingManager;
        }

        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public async Task<LendingConcessionDetail> Handle(DeleteLendingConcessionDetail message)
        {
            var result = _lendingManager.DeleteConcessionLending(message.LendingConcessionDetail);

            message.AuditRecord = new AuditRecord(result, message.User, AuditType.Delete);

            return message.LendingConcessionDetail;
        }
    }
}
