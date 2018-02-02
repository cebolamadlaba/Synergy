using System.Collections.Generic;
using MediatR;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.Interface.BusinessLogic.Features;
using StandardBank.ConcessionManagement.Model.BusinessLogic;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.Administration
{
    /// <summary>
    /// Create user request
    /// </summary>
    /// <seealso cref="int" />
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.BusinessLogic.Features.IMultipleAuditableCommand" />
    public class CreateUser : IRequest<int>, IMultipleAuditableCommand
    {
        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public User User { get; set; }

        /// <summary>
        /// Gets or sets the current user.
        /// </summary>
        /// <value>
        /// The current user.
        /// </value>
        public User CurrentUser { get; set; }

        /// <summary>
        /// Gets or sets the audit records.
        /// </summary>
        /// <value>
        /// The audit records.
        /// </value>
        public IEnumerable<AuditRecord> AuditRecords { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUser"/> class.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="currentUser">The current user.</param>
        public CreateUser(User user, User currentUser)
        {
            User = user;
            CurrentUser = currentUser;
        }
    }
}
