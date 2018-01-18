using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic.Features;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.Administration
{
    /// <summary>
    /// Create a region
    /// </summary>
    /// <seealso cref="MediatR.IRequest" />
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.BusinessLogic.Features.IAuditableCommand" />
    public class CreateRegion : IRequest, IAuditableCommand
    {
        /// <summary>
        /// Gets or sets the region.
        /// </summary>
        /// <value>
        /// The region.
        /// </value>
        public Region Region { get; set; }

        /// <summary>
        /// Gets or sets the current user.
        /// </summary>
        /// <value>
        /// The current user.
        /// </value>
        public User CurrentUser { get; set; }

        /// <summary>
        /// Gets or sets the audit record.
        /// </summary>
        /// <value>
        /// The audit record.
        /// </value>
        public AuditRecord AuditRecord { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateRegion"/> class.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="user">The user.</param>
        public CreateRegion(Region region, User user)
        {
            Region = region;
            CurrentUser = user;
        }
    }
}
