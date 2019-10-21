using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic.Features;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.Model.UserInterface.Glms;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.GlmsConcession
{
    /// <summary>
    /// Add cash concession detail command
    /// </summary>
    /// <seealso cref="MediatR.IRequest{CashConcessionDetail}" />
    /// <seealso cref="IAuditableCommand" />
    public class AddOrUpdateGlmsConcessionDetail : IRequest<GlmsConcessionDetail>, IAuditableCommand
    {
        /// <summary>
        /// Gets or sets the cash concession detail
        /// </summary>
        public GlmsConcessionDetail GlmsConcessionDetail { get; set; }

        /// <summary>
        /// Gets or sets the concession
        /// </summary>
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

   
        public AddOrUpdateGlmsConcessionDetail(GlmsConcessionDetail glmsConcessionDetail, User user, Model.UserInterface.Concession concession)
        {
            GlmsConcessionDetail = glmsConcessionDetail;
            User = user;
            Concession = concession;
        }
    }
}
