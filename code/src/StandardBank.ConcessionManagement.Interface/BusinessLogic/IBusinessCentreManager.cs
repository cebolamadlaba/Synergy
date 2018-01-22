using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.UserInterface.Administration;

namespace StandardBank.ConcessionManagement.Interface.BusinessLogic
{
    /// <summary>
    /// Business centre manager
    /// </summary>
    public interface IBusinessCentreManager
    {
        /// <summary>
        /// Gets the business centre management models.
        /// </summary>
        /// <returns></returns>
        IEnumerable<BusinessCentreManagementModel> GetBusinessCentreManagementModels();

        /// <summary>
        /// Validates the business centre management model.
        /// </summary>
        /// <param name="businessCentreManagementModel">The business centre management model.</param>
        /// <returns></returns>
        IEnumerable<string> ValidateBusinessCentreManagementModel(
            BusinessCentreManagementModel businessCentreManagementModel);
    }
}
