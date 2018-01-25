using System.Collections.Generic;
using System.Linq;
using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.Administration
{
    /// <summary>
    /// Creates the business centre management model handler
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler{CreateBusinessCentreManagementModel}" />
    public class CreateBusinessCentreManagementModelHandler : IRequestHandler<CreateBusinessCentreManagementModel>
    {
        /// <summary>
        /// The business centre manager
        /// </summary>
        private readonly IBusinessCentreManager _businessCentreManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateBusinessCentreManagementModelHandler"/> class.
        /// </summary>
        /// <param name="businessCentreManager">The business centre manager.</param>
        public CreateBusinessCentreManagementModelHandler(IBusinessCentreManager businessCentreManager)
        {
            _businessCentreManager = businessCentreManager;
        }

        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Handle(CreateBusinessCentreManagementModel message)
        {
            var auditRecords = new List<AuditRecord>();

            var centre = _businessCentreManager.CreateCentre(message.BusinessCentreManagementModel.RegionId.Value,
                message.BusinessCentreManagementModel.CentreName);

            auditRecords.Add(new AuditRecord(centre, message.CurrentUser, AuditType.Insert));

            if (message.BusinessCentreManagementModel.BusinessCentreManagerId.HasValue)
            {
                //if there is a business centre manager id, we need to add this user to this centre
                var bcmCentreUser = _businessCentreManager.CreateCentreUser(centre.Id,
                    message.BusinessCentreManagementModel.BusinessCentreManagerId.Value);

                auditRecords.Add(new AuditRecord(bcmCentreUser, message.CurrentUser, AuditType.Insert));
            }

            if (message.BusinessCentreManagementModel.AccountExecutives != null &&
                message.BusinessCentreManagementModel.AccountExecutives.Any())
            {
                //add the selected account executives
                foreach (var accountExecutive in message.BusinessCentreManagementModel.AccountExecutives)
                {
                    var centreUser = _businessCentreManager.CreateCentreUser(centre.Id, accountExecutive.Id);

                    auditRecords.Add(new AuditRecord(centreUser, message.CurrentUser, AuditType.Insert));
                }
            }

            message.AuditRecords = auditRecords;
        }
    }
}
