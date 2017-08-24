using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic.Features;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.DeactivateConcession
{
    /// <summary>
    /// Deactivate concession command
    /// </summary>
    /// <seealso cref="MediatR.IRequest{Concession}" />
    /// <seealso cref="IAuditableCommand" />
    public class DeactiveConcession : IRequest<Concession>, IAuditableCommand
    {
        /// <summary>
        /// Gets or sets the concession.
        /// </summary>
        /// <value>
        /// The concession.
        /// </value>
        public Concession Concession { get; set; }

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
        /// Initializes a new instance of the <see cref="DeactiveConcession"/> class.
        /// </summary>
        /// <param name="concession">The concession.</param>
        /// <param name="user">The user.</param>
        public DeactiveConcession(Concession concession, User user)
        {
            Concession = concession;
            User = user;
        }
    }
}
