using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic.Features;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.Model.UserInterface.Glms;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.GlmsConcession
{
    /// <summary>
    /// Delete Glms concession detail command
    /// </summary>
    /// <seealso cref="MediatR.IRequest{GlmsConcessionDetail}" />
    /// <seealso cref="IAuditableCommand" />
    public class DeleteGlmsConcessionDetail : IRequest<GlmsConcessionDetail>, IAuditableCommand
    {
        /// <summary>
        /// Gets or sets the Glms concession detail.
        /// </summary>
        /// <value>
        /// The Glms concession detail.
        /// </value>
        public GlmsConcessionDetail GlmsConcessionDetail { get; set; }

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
        /// Initializes a new instance of the <see cref="DeleteGlmsConcessionDetail"/> class.
        /// </summary>
        /// <param name="glmsConcessionDetail">The glms concession detail.</param>
        /// <param name="user">The user.</param>
        public DeleteGlmsConcessionDetail(GlmsConcessionDetail glmsConcessionDetail, User user)
        {
            GlmsConcessionDetail = glmsConcessionDetail;
            User = user;
        }
    }
}
