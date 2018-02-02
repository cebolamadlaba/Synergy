using System.Collections.Generic;
using MediatR;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.Interface.BusinessLogic.Features;
using StandardBank.ConcessionManagement.Model.BusinessLogic;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.Administration
{
    /// <summary>
    /// Update user request
    /// </summary>
    /// <seealso cref="MediatR.IRequest" />
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.BusinessLogic.Features.IAuditableCommand" />
    public class UpdateUser : IRequest, IMultipleAuditableCommand
    {
        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        public User Model { get; set; }

        /// <summary>
        /// Gets or sets the current user.
        /// </summary>
        /// <value>
        /// The current user.
        /// </value>
        public User CurrentUser { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateUser"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="currentUser">The current user.</param>
        public UpdateUser(User model, User currentUser)
        {
            Model = model;
            CurrentUser = currentUser;
        }

        /// <summary>
        /// Gets or sets the audit records.
        /// </summary>
        /// <value>
        /// The audit records.
        /// </value>
        public IEnumerable<AuditRecord> AuditRecords { get; set; }
    }
}
