using System.Collections.Generic;
using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic.Features;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.Model.UserInterface.Administration;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.Administration
{
    /// <summary>
    /// Creates or updates the account executives
    /// </summary>
    /// <seealso cref="MediatR.IRequest" />
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.BusinessLogic.Features.IMultipleAuditableCommand" />
    public class CreateOrUpdateAccountExecutives : IRequest, IMultipleAuditableCommand
    {
        /// <summary>
        /// Gets or sets the audit records.
        /// </summary>
        /// <value>
        /// The audit records.
        /// </value>
        public IEnumerable<AuditRecord> AuditRecords { get; set; }

        /// <summary>
        /// Gets or sets the account executive.
        /// </summary>
        /// <value>
        /// The account executive.
        /// </value>
        public AccountExecutive AccountExecutive { get; set; }

        /// <summary>
        /// Gets or sets the current user.
        /// </summary>
        /// <value>
        /// The current user.
        /// </value>
        public User CurrentUser { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateOrUpdateAccountExecutives"/> class.
        /// </summary>
        /// <param name="accountExecutive">The account executive.</param>
        /// <param name="currentUser">The current user.</param>
        public CreateOrUpdateAccountExecutives(AccountExecutive accountExecutive, User currentUser)
        {
            AccountExecutive = accountExecutive;
            CurrentUser = currentUser;
        }
    }
}
