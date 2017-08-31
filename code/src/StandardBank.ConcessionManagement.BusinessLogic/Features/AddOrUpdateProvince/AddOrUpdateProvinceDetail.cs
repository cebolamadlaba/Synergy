using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic.Features;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.AddOrUpdateProvinceDetail
{
    /// <summary>
    /// Add cash concession detail command
    /// </summary>
    /// <seealso cref="MediatR.IRequest{CashConcessionDetail}" />
    /// <seealso cref="IAuditableCommand" />
    public class AddOrUpdateProvinceDetail : IRequest<Province>, IAuditableCommand
    {
        /// <summary>
        /// Gets or sets the concession
        /// </summary>
        public Province Province { get; set; }

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
        /// Initializes a new instance of the <see cref="AddOrUpdateProvinceDetail"/> class.
        /// </summary>
        /// <param name="cashConcessionDetail">The cash concession detail.</param>
        /// <param name="user">The user.</param>
        /// <param name="concession"></param>
        public AddOrUpdateProvinceDetail(Province province, User user)
        {
            Province = province;
            User = user;
        }
    }
}
