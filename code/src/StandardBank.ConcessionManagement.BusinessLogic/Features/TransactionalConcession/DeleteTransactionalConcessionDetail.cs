using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic.Features;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.Model.UserInterface.Transactional;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.TransactionalConcession
{
    /// <summary>
    /// Delete transactional concession detail
    /// </summary>
    /// <seealso cref="MediatR.IRequest{TransactionalConcessionDetail}" />
    /// <seealso cref="IAuditableCommand" />
    public class DeleteTransactionalConcessionDetail : IRequest<TransactionalConcessionDetail>, IAuditableCommand
    {
        /// <summary>
        /// Gets or sets the transactional concession detail.
        /// </summary>
        /// <value>
        /// The transactional concession detail.
        /// </value>
        public TransactionalConcessionDetail TransactionalConcessionDetail { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public User User { get; set; }

        /// <summary>
        /// Gets or sets the audit record.
        /// </summary>
        /// <value>
        /// The audit record.
        /// </value>
        public AuditRecord AuditRecord { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteTransactionalConcessionDetail"/> class.
        /// </summary>
        /// <param name="transactionalConcessionDetail">The transactional concession detail.</param>
        /// <param name="user">The user.</param>
        public DeleteTransactionalConcessionDetail(TransactionalConcessionDetail transactionalConcessionDetail,
            User user)
        {
            TransactionalConcessionDetail = transactionalConcessionDetail;
            User = user;
        }
    }
}
