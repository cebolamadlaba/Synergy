using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic.Features;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.AddConcessionRelationship
{
    /// <summary>
    /// Add concession relationship command
    /// </summary>
    /// <seealso cref="ConcessionRelationship}" />
    /// <seealso cref="IAuditableCommand" />
    public class AddConcessionRelationship : IRequest<ConcessionRelationship>, IAuditableCommand
    {
        /// <summary>
        /// Gets or sets the concession relationship.
        /// </summary>
        /// <value>
        /// The concession relationship.
        /// </value>
        public ConcessionRelationship ConcessionRelationship { get; set; }

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
        /// Initializes a new instance of the <see cref="AddConcessionRelationship"/> class.
        /// </summary>
        /// <param name="concessionRelationship">The concession relationship.</param>
        /// <param name="user">The user.</param>
        public AddConcessionRelationship(ConcessionRelationship concessionRelationship, User user)
        {
            ConcessionRelationship = concessionRelationship;
            User = user;
        }
    }
}
