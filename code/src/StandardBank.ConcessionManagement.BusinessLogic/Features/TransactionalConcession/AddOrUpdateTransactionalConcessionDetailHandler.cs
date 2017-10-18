using System.Threading.Tasks;
using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface.Transactional;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.TransactionalConcession
{
    /// <summary>
    /// Add or update transaction concession detail handler
    /// </summary>
    /// <seealso cref="MediatR.IAsyncRequestHandler{AddOrUpdateTransactionalConcessionDetail, TransactionalConcessionDetail}" />
    public class AddOrUpdateTransactionalConcessionDetailHandler : IAsyncRequestHandler<
        AddOrUpdateTransactionalConcessionDetail, TransactionalConcessionDetail>
    {
        /// <summary>
        /// The transactional manager
        /// </summary>
        private readonly ITransactionalManager _transactionalManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddOrUpdateTransactionalConcessionDetailHandler"/> class.
        /// </summary>
        /// <param name="transactionalManager">The transactional manager.</param>
        public AddOrUpdateTransactionalConcessionDetailHandler(ITransactionalManager transactionalManager)
        {
            _transactionalManager = transactionalManager;
        }

        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public async Task<TransactionalConcessionDetail> Handle(AddOrUpdateTransactionalConcessionDetail message)
        {
            if (message.TransactionalConcessionDetail.TransactionalConcessionDetailId == 0)
            {
                var result =
                    _transactionalManager.CreateConcessionTransactional(message.TransactionalConcessionDetail, message.Concession);

                message.AuditRecord = new AuditRecord(result, message.User, AuditType.Insert);
                message.TransactionalConcessionDetail.TransactionalConcessionDetailId = result.Id;
            }
            else
            {
                var result =
                    _transactionalManager.UpdateConcessionTransactional(message.TransactionalConcessionDetail, message.Concession);

                message.AuditRecord = new AuditRecord(result, message.User, AuditType.Update);
            }

            return message.TransactionalConcessionDetail;
        }
    }
}
