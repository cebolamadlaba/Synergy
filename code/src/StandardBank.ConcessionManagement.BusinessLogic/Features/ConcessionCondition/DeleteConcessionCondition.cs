using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic.Features;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.ConcessionCondition
{
    /// <summary>
    /// Deletes the concession condition
    /// </summary>
    /// <seealso cref="MediatR.IRequest{ConcessionCondition}" />
    /// <seealso cref="IAuditableCommand" />
    public class DeleteConcessionCondition : IRequest<Model.UserInterface.ConcessionCondition>, IAuditableCommand
    {
        /// <summary>
        /// Gets or sets the concession condition.
        /// </summary>
        /// <value>
        /// The concession condition.
        /// </value>
        public Model.UserInterface.ConcessionCondition ConcessionCondition { get; set; }

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
        /// Initializes a new instance of the <see cref="DeleteConcessionCondition"/> class.
        /// </summary>
        /// <param name="concessionCondition">The concession condition.</param>
        /// <param name="user">The user.</param>
        public DeleteConcessionCondition(Model.UserInterface.ConcessionCondition concessionCondition, User user)
        {
            ConcessionCondition = concessionCondition;
            User = user;
        }
    }
}
