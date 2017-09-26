using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic.Features;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.Model.UserInterface.Cash;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.CashConcession
{
    /// <summary>
    /// Add cash concession detail command
    /// </summary>
    /// <seealso cref="MediatR.IRequest{CashConcessionDetail}" />
    /// <seealso cref="IAuditableCommand" />
    public class AddOrUpdateCashConcessionDetail : IRequest<CashConcessionDetail>, IAuditableCommand
    {
        /// <summary>
        /// Gets or sets the cash concession detail
        /// </summary>
        public CashConcessionDetail CashConcessionDetail { get; set; }

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

        /// <summary>
        /// Initializes a new instance of the <see cref="AddOrUpdateCashConcessionDetail"/> class.
        /// </summary>
        /// <param name="cashConcessionDetail">The cash concession detail.</param>
        /// <param name="user">The user.</param>
        /// <param name="concession"></param>
        public AddOrUpdateCashConcessionDetail(CashConcessionDetail cashConcessionDetail, User user, Model.UserInterface.Concession concession)
        {
            CashConcessionDetail = cashConcessionDetail;
            User = user;
            Concession = concession;
        }
    }
}
