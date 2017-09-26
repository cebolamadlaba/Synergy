using System;
using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic.Features;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.Concession
{
    /// <summary>
    /// Adds concession comment command
    /// </summary>
    /// <seealso cref="MediatR.IRequest{ConcessionComment}" />
    /// <seealso cref="IAuditableCommand" />
    public class AddConcessionComment : IRequest<ConcessionComment>, IAuditableCommand
    {
        /// <summary>
        /// Gets or sets the concession comment.
        /// </summary>
        /// <value>
        /// The concession comment.
        /// </value>
        public ConcessionComment ConcessionComment { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public Model.UserInterface.User User { get; set; }

        /// <summary>
        /// Gets or sets the audit record.
        /// </summary>
        /// <value>
        /// The audit record.
        /// </value>
        public AuditRecord AuditRecord { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddConcessionComment"/> class.
        /// </summary>
        /// <param name="concessionId">The concession identifier.</param>
        /// <param name="concessionSubStatusId">The concession sub status identifier.</param>
        /// <param name="comment">The comment.</param>
        /// <param name="user">The user.</param>
        public AddConcessionComment(int concessionId, int concessionSubStatusId, string comment, Model.UserInterface.User user)
        {
            ConcessionComment = new ConcessionComment
            {
                Comment = comment,
                ConcessionId = concessionId,
                ConcessionSubStatusId = concessionSubStatusId,
                IsActive = true,
                SystemDate = DateTime.Now,
                UserId = user.Id
            };

            User = user;
        }
    }
}
