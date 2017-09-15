using System.Threading.Tasks;
using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface.Transactional;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.DeleteTransactionalConcessionDetail
{
    /// <summary>
    /// Deletes the transactional concession detail
    /// </summary>
    /// <seealso cref="MediatR.IAsyncRequestHandler{DeleteTransactionalConcessionDetail, TransactionalConcessionDetail}" />
    public class DeleteTransactionalConcessionDetailHandler : IAsyncRequestHandler<DeleteTransactionalConcessionDetail, TransactionalConcessionDetail>
    {
        /// <summary>
        /// The transactional manager
        /// </summary>
        private readonly ITransactionalManager _transactionalManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteTransactionalConcessionDetailHandler"/> class.
        /// </summary>
        /// <param name="transactionalManager">The transactional manager.</param>
        public DeleteTransactionalConcessionDetailHandler(ITransactionalManager transactionalManager)
        {
            _transactionalManager = transactionalManager;
        }

        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public async Task<TransactionalConcessionDetail> Handle(DeleteTransactionalConcessionDetail message)
        {
            var result = _transactionalManager.DeleteConcessionTransactional(message.TransactionalConcessionDetail);

            message.AuditRecord = new AuditRecord(result, message.User, AuditType.Delete);

            return message.TransactionalConcessionDetail;
        }
    }
}
