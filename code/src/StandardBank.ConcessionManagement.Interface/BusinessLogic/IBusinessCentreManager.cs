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

        /// <summary>
        /// Gets the business centre management lookup model.
        /// </summary>
        /// <returns></returns>
        BusinessCentreManagementLookupModel GetBusinessCentreManagementLookupModel();

        /// <summary>
        /// Creates the centre.
        /// </summary>
        /// <param name="regionId">The region identifier.</param>
        /// <param name="centreName">Name of the centre.</param>
        /// <returns></returns>
        Model.Repository.Centre CreateCentre(int regionId, string centreName);

        /// <summary>
        /// Creates the centre user.
        /// </summary>
        /// <param name="centreId">The centre identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Model.Repository.CentreUser CreateCentreUser(int centreId, int userId, Model.UserInterface.User user);

        /// <summary>
        /// Updates the centre user.
        /// </summary>
        /// <param name="currentCentreId">The current centre identifier.</param>
        /// <param name="newCentreId">The new centre identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Model.Repository.CentreUser UpdateCentreUser(int currentCentreId, int newCentreId, int userId, Model.UserInterface.User user);

        /// <summary>
        /// Updates the centre.
        /// </summary>
        /// <param name="centreId">The centre identifier.</param>
        /// <param name="regionId">The region identifier.</param>
        /// <param name="centreName">Name of the centre.</param>
        /// <returns></returns>
        Model.Repository.Centre UpdateCentre(int centreId, int regionId, string centreName);

        /// <summary>
        /// Deletes the centre user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="centreId">The centre identifier.</param>
        /// <returns></returns>
        Model.Repository.CentreUser DeleteCentreUser(int userId, int centreId);

        /// <summary>
        /// Gets the region centres.
        /// </summary>
        /// <returns></returns>
        IEnumerable<RegionCentresModel> GetRegionCentres();
    }
}
