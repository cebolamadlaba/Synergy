using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.Administration
{
    /// <summary>
    /// Create region handler
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler{StandardBank.ConcessionManagement.BusinessLogic.Features.Administration.CreateRegion}" />
    public class CreateRegionHandler : IRequestHandler<CreateRegion>
    {
        /// <summary>
        /// The region manager
        /// </summary>
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateRegionHandler"/> class.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        public CreateRegionHandler(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Handle(CreateRegion message)
        {
            var region = _regionManager.CreateRegion(message.Region);

            message.AuditRecord = new AuditRecord(region, message.CurrentUser, AuditType.Insert);
        }
    }
}
