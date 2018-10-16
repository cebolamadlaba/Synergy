using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic.Features;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.Model.UserInterface.Investment;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.InvestmentConcession
{
    /// <summary>
    /// Add cash concession detail command
    /// </summary>
    /// <seealso cref="MediatR.IRequest{CashConcessionDetail}" />
    /// <seealso cref="IAuditableCommand" />
    public class AddOrUpdateInvestmentConcessionDetail : IRequest<InvestmentConcessionDetail>, IAuditableCommand
    {
        /// <summary>
        /// Gets or sets the cash concession detail
        /// </summary>
        public InvestmentConcessionDetail InvestmentConcessionDetail { get; set; }

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

   
        public AddOrUpdateInvestmentConcessionDetail(InvestmentConcessionDetail investmentConcessionDetail, User user, Model.UserInterface.Concession concession)
        {
            InvestmentConcessionDetail = investmentConcessionDetail;
            User = user;
            Concession = concession;
        }
    }
}
