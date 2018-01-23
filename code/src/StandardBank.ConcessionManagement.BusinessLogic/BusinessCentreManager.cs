using System.Collections.Generic;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface.Administration;

namespace StandardBank.ConcessionManagement.BusinessLogic
{
    /// <summary>
    /// Business centre manager
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.BusinessLogic.IBusinessCentreManager" />
    public class BusinessCentreManager : IBusinessCentreManager
    {
        /// <summary>
        /// The misc performance repository
        /// </summary>
        private readonly IMiscPerformanceRepository _miscPerformanceRepository;

        /// <summary>
        /// The region manager
        /// </summary>
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// The user manager
        /// </summary>
        private readonly IUserManager _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessCentreManager"/> class.
        /// </summary>
        /// <param name="miscPerformanceRepository">The misc performance repository.</param>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="userManager">The user manager.</param>
        public BusinessCentreManager(IMiscPerformanceRepository miscPerformanceRepository, IRegionManager regionManager, IUserManager userManager)
        {
            _miscPerformanceRepository = miscPerformanceRepository;
            _regionManager = regionManager;
            _userManager = userManager;
        }

        /// <summary>
        /// Gets the business centre management models.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BusinessCentreManagementModel> GetBusinessCentreManagementModels()
        {
            return _miscPerformanceRepository.GetBusinessCentreManagementModels();
        }

        /// <summary>
        /// Validates the business centre management model.
        /// </summary>
        /// <param name="businessCentreManagementModel">The business centre management model.</param>
        /// <returns></returns>
        public IEnumerable<string> ValidateBusinessCentreManagementModel(BusinessCentreManagementModel businessCentreManagementModel)
        {
            var errors = new List<string>();

            //TODO: Validate the business centre

            return errors;
        }

        /// <summary>
        /// Gets the business centre management lookup model.
        /// </summary>
        /// <returns></returns>
        public BusinessCentreManagementLookupModel GetBusinessCentreManagementLookupModel()
        {
            var model = new BusinessCentreManagementLookupModel
            {
                Regions = _regionManager.GetRegions(),
                AccountExecutives = _userManager.GetUsersByRole(Constants.Roles.Requestor),
                BusinessCentreManagers = _userManager.GetUsersByRole(Constants.Roles.BCM)
            };

            return model;
        }
    }
}
