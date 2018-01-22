using System;
using System.Collections.Generic;
using System.Text;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.Repository;
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
        /// Initializes a new instance of the <see cref="BusinessCentreManager"/> class.
        /// </summary>
        /// <param name="miscPerformanceRepository">The misc performance repository.</param>
        public BusinessCentreManager(IMiscPerformanceRepository miscPerformanceRepository)
        {
            _miscPerformanceRepository = miscPerformanceRepository;
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
    }
}
