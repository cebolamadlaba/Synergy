using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic.Features;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.Model.UserInterface.Transactional;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.TransactionalConcession
{
    /// <summary>
    /// Add or update transactional concession detail command
    /// </summary>
    /// <seealso cref="MediatR.IRequest{StandardBank.ConcessionManagement.Model.UserInterface.Transactional.TransactionalConcessionDetail}" />
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.BusinessLogic.Features.IAuditableCommand" />
    public class AddOrUpdateTransactionalConcessionDetail : IRequest<TransactionalConcessionDetail>, IAuditableCommand
    {
        /// <summary>
        /// Gets or sets the transactional concession detail.
        /// </summary>
        /// <value>
        /// The transactional concession detail.
        /// </value>
        public TransactionalConcessionDetail TransactionalConcessionDetail { get; set; }

        /// <summary>
        /// Gets or sets the concession.
        /// </summary>
        /// <value>
        /// The concession.
        /// </value>
        public Model.UserInterface.Concession Concession { get; set; }

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
        /// Initializes a new instance of the <see cref="AddOrUpdateTransactionalConcessionDetail"/> class.
        /// </summary>
        /// <param name="transactionalConcessionDetail">The transactional concession detail.</param>
        /// <param name="user">The user.</param>
        /// <param name="concession">The concession.</param>
        public AddOrUpdateTransactionalConcessionDetail(TransactionalConcessionDetail transactionalConcessionDetail,
            User user, Model.UserInterface.Concession concession)
        {
            TransactionalConcessionDetail = transactionalConcessionDetail;
            User = user;
            Concession = concession;
        }
    }
}
