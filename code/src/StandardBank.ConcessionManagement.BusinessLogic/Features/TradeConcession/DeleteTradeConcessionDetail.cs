using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic.Features;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.Model.UserInterface.Trade;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.TradeConcession
{
    /// <summary>
    /// Delete cash concession detail command
    /// </summary>
    /// <seealso cref="MediatR.IRequest{CashConcessionDetail}" />
    /// <seealso cref="IAuditableCommand" />
    public class DeleteTradeConcessionDetail : IRequest<TradeConcessionDetail>, IAuditableCommand
    {
        /// <summary>
        /// Gets or sets the cash concession detail.
        /// </summary>
        /// <value>
        /// The cash concession detail.
        /// </value>
        public TradeConcessionDetail TradeConcessionDetail { get; set; }

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
        /// Initializes a new instance of the <see cref="DeleteCashConcessionDetail"/> class.
        /// </summary>
        /// <param name="cashConcessionDetail">The cash concession detail.</param>
        /// <param name="user">The user.</param>
        public DeleteTradeConcessionDetail(TradeConcessionDetail tradeConcessionDetail, User user)
        {
            TradeConcessionDetail = tradeConcessionDetail;
            User = user;
        }
    }
}
