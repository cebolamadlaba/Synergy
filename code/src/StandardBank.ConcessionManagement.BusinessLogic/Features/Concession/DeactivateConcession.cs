﻿using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic.Features;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.Concession
{
    /// <summary>
    /// Deactivate concession command
    /// </summary>
    /// <seealso cref="MediatR.IRequest{Concession}" />
    /// <seealso cref="IAuditableCommand" />
    public class DeactivateConcession : IRequest<string>, IAuditableCommand
    {
        /// <summary>
        /// Gets or sets the concession reference number.
        /// </summary>
        /// <value>
        /// The concession reference number.
        /// </value>
        public string ConcessionReferenceNumber { get; set; }

        public bool IsRecall { get; set; }

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
        /// Initializes a new instance of the <see cref="DeactivateConcession"/> class.
        /// </summary>
        /// <param name="concessionReferenceNumber">The concession reference number.</param>
        /// <param name="user">The user.</param>
        public DeactivateConcession(string concessionReferenceNumber, bool isRecall, User user)
        {
            ConcessionReferenceNumber = concessionReferenceNumber;
            User = user;
            IsRecall = isRecall;
        }
    }
}
