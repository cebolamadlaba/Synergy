using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic.Features;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.Model.UserInterface.Lending;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.AddOrUpdateLendingConcessionDetail
{
    /// <summary>
    /// Add lending concession detail command
    /// </summary>
    /// <seealso cref="MediatR.IRequest{LendingConcession}" />
    /// <seealso cref="IAuditableCommand" />
    public class AddOrUpdateLendingConcessionDetailCommand : IRequest<LendingConcessionDetail>, IAuditableCommand
    {
        /// <summary>
        /// Gets or sets the lending concession detail
        /// </summary>
        public LendingConcessionDetail LendingConcessionDetail { get; set; }

        /// <summary>
        /// Gets or sets the concession
        /// </summary>
        public Concession Concession { get; set; }

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
        /// Initializes a new instance of the <see cref="AddOrUpdateLendingConcessionDetailCommand"/> class.
        /// </summary>
        /// <param name="lendingConcessionDetail">The lending concession detail.</param>
        /// <param name="user">The user.</param>
        /// <param name="concession"></param>
        public AddOrUpdateLendingConcessionDetailCommand(LendingConcessionDetail lendingConcessionDetail, User user, Concession concession)
        {
            LendingConcessionDetail = lendingConcessionDetail;
            User = user;
            Concession = concession;
        }
    }
}
