﻿using System.Collections.Generic;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
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
        /// The centre repository
        /// </summary>
        private readonly ICentreRepository _centreRepository;

        /// <summary>
        /// The centre user repository
        /// </summary>
        private readonly ICentreUserRepository _centreUserRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessCentreManager"/> class.
        /// </summary>
        /// <param name="miscPerformanceRepository">The misc performance repository.</param>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="userManager">The user manager.</param>
        /// <param name="centreRepository">The centre repository.</param>
        /// <param name="centreUserRepository">The centre user repository.</param>
        public BusinessCentreManager(IMiscPerformanceRepository miscPerformanceRepository, IRegionManager regionManager,
            IUserManager userManager, ICentreRepository centreRepository, ICentreUserRepository centreUserRepository)
        {
            _miscPerformanceRepository = miscPerformanceRepository;
            _regionManager = regionManager;
            _userManager = userManager;
            _centreRepository = centreRepository;
            _centreUserRepository = centreUserRepository;
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

            if (businessCentreManagementModel == null)
            {
                errors.Add("No data supplied");
            }
            else
            {
                var centres = _centreRepository.ReadAll();

                if (!string.IsNullOrWhiteSpace(businessCentreManagementModel.CentreName) && centres != null && centres.Any())
                {
                    if (businessCentreManagementModel.CentreId > 0)
                    {
                        //this means it's an update
                        foreach (var matchingDescriptionRegion in centres.Where(_ =>
                            _.CentreName.ToLowerInvariant() == businessCentreManagementModel.CentreName.ToLowerInvariant()))
                        {
                            if (matchingDescriptionRegion.Id != businessCentreManagementModel.CentreId)
                                errors.Add("There is already a business centre with the same description, please use another description");
                        }
                    }
                    else
                    {
                        //this means it's a create
                        if (centres.Any(_ => _.CentreName.ToLowerInvariant() == businessCentreManagementModel.CentreName.ToLowerInvariant()))
                            errors.Add(
                                "There is already a business centre with the same description, please use another description");
                    }
                }

                if (!businessCentreManagementModel.RegionId.HasValue || businessCentreManagementModel.RegionId.Value == 0)
                    errors.Add("Please select a region");

                if (string.IsNullOrWhiteSpace(businessCentreManagementModel.CentreName))
                    errors.Add("Please supply a centre name");
            }

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

        /// <summary>
        /// Creates the centre.
        /// </summary>
        /// <param name="regionId">The region identifier.</param>
        /// <param name="centreName">Name of the centre.</param>
        /// <returns></returns>
        public Centre CreateCentre(int regionId, string centreName)
        {
            return _centreRepository.Create(new Centre
            {
                RegionId = regionId,
                CentreName = centreName,
                IsActive = true
            });
        }

        /// <summary>
        /// Creates the centre user.
        /// </summary>
        /// <param name="centreId">The centre identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public CentreUser CreateCentreUser(int centreId, int userId)
        {
            return _centreUserRepository.Create(new CentreUser
            {
                CentreId = centreId,
                IsActive = true,
                UserId = userId
            });
        }

        /// <summary>
        /// Updates the centre user.
        /// </summary>
        /// <param name="currentCentreId">The current centre identifier.</param>
        /// <param name="newCentreId">The new centre identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public CentreUser UpdateCentreUser(int currentCentreId, int newCentreId, int userId)
        {
            var userCentres = _centreUserRepository.ReadByUserId(userId);

            foreach (var userCentre in userCentres)
            {
                if (userCentre.CentreId == currentCentreId)
                {
                    userCentre.CentreId = newCentreId;
                    _centreUserRepository.Update(userCentre);

                    return userCentre;
                }
            }

            return null;
        }

        /// <summary>
        /// Updates the centre.
        /// </summary>
        /// <param name="centreId">The centre identifier.</param>
        /// <param name="regionId">The region identifier.</param>
        /// <param name="centreName">Name of the centre.</param>
        /// <returns></returns>
        public Centre UpdateCentre(int centreId, int regionId, string centreName)
        {
            var centre = _centreRepository.ReadById(centreId);
            centre.RegionId = regionId;
            centre.CentreName = centreName;

            _centreRepository.Update(centre);

            return centre;
        }

        /// <summary>
        /// Deletes the centre user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="centreId">The centre identifier.</param>
        /// <returns></returns>
        public CentreUser DeleteCentreUser(int userId, int centreId)
        {
            var centreUsers = _centreUserRepository.ReadByUserId(userId);

            var centreToDelete = centreUsers.FirstOrDefault(_ => _.CentreId == centreId);

            if (centreToDelete != null)
            {
                _centreUserRepository.Delete(centreToDelete);
                return centreToDelete;
            }

            return null;
        }
    }
}
