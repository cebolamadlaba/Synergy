using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic.Features;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.Model.UserInterface.Administration;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.Administration
{
    /// <summary>
    /// Create business centre management model
    /// </summary>
    /// <seealso cref="MediatR.IRequest" />
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.BusinessLogic.Features.IAuditableCommand" />
    public class CreateBusinessCentreManagementModel : IRequest, IAuditableCommand
    {
        /// <summary>
        /// Gets or sets the audit record.
        /// </summary>
        /// <value>
        /// The audit record.
        /// </value>
        public AuditRecord AuditRecord { get; set; }

        /// <summary>
        /// Gets or sets the business centre management model.
        /// </summary>
        /// <value>
        /// The business centre management model.
        /// </value>
        public BusinessCentreManagementModel BusinessCentreManagementModel { get; set; }

        /// <summary>
        /// Gets or sets the current user.
        /// </summary>
        /// <value>
        /// The current user.
        /// </value>
        public User CurrentUser { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateBusinessCentreManagementModel"/> class.
        /// </summary>
        /// <param name="businessCentreManagementModel">The business centre management model.</param>
        /// <param name="user">The user.</param>
        public CreateBusinessCentreManagementModel(BusinessCentreManagementModel businessCentreManagementModel,
            User user)
        {
            BusinessCentreManagementModel = businessCentreManagementModel;
            CurrentUser = user;
        }
    }
}
