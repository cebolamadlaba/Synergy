using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic.Features;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.AddConcessionCondition
{
    /// <summary>
    /// Add concession condition command
    /// </summary>
    /// <seealso cref="MediatR.IRequest{ConcessionCondition}" />
    /// <seealso cref="IAuditableCommand" />
    public class AddConcessionConditionCommand : IRequest<ConcessionCondition>, IAuditableCommand
    {
        /// <summary>
        /// Gets or sets the concession condition.
        /// </summary>
        /// <value>
        /// The concession condition.
        /// </value>
        public ConcessionCondition ConcessionCondition { get; set; }

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
        /// Initializes a new instance of the <see cref="AddConcessionConditionCommand"/> class.
        /// </summary>
        /// <param name="concessionCondition">The concession condition.</param>
        /// <param name="user">The user.</param>
        /// <param name="concession"></param>
        public AddConcessionConditionCommand(ConcessionCondition concessionCondition, User user, Concession concession)
        {
            ConcessionCondition = concessionCondition;
            User = user;
            Concession = concession;
        }
    }
}
