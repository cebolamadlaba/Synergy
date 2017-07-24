using System;
using System.Collections.Generic;
using System.Linq;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.Common;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Common;
using StandardBank.ConcessionManagement.Model.UserInterface;

namespace StandardBank.ConcessionManagement.BusinessLogic
{
    /// <summary>
    /// User manager
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.BusinessLogic.IUserManager" />
    public class UserManager : IUserManager
    {
        /// <summary>
        /// The cache manager
        /// </summary>
        private readonly ICacheManager _cacheManager;

        /// <summary>
        /// The lookup table manager
        /// </summary>
        private readonly ILookupTableManager _lookupTableManager;

        /// <summary>
        /// The user repository
        /// </summary>
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// The user role repository
        /// </summary>
        private readonly IUserRoleRepository _userRoleRepository;

        /// <summary>
        /// The role repository
        /// </summary>
        private readonly IRoleRepository _roleRepository;

        /// <summary>
        /// The user region repository
        /// </summary>
        private readonly IUserRegionRepository _userRegionRepository;

        /// <summary>
        /// The region repository
        /// </summary>
        private readonly IRegionRepository _regionRepository;

        /// <summary>
        /// The centre repository
        /// </summary>
        private readonly ICentreRepository _centreRepository;

        /// <summary>
        /// The centre user repository
        /// </summary>
        private readonly ICentreUserRepository _centreUserRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserManager"/> class.
        /// </summary>
        /// <param name="cacheManager">The cache manager.</param>
        /// <param name="lookupTableManager"></param>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="userRoleRepository">The user role repository.</param>
        /// <param name="roleRepository">The role repository.</param>
        /// <param name="userRegionRepository"></param>
        /// <param name="regionRepository"></param>
        /// <param name="centreRepository"></param>
        /// <param name="centreUserRepository"></param>
        public UserManager(ICacheManager cacheManager, ILookupTableManager lookupTableManager,
            IUserRepository userRepository, IUserRoleRepository userRoleRepository, IRoleRepository roleRepository,
            IUserRegionRepository userRegionRepository, IRegionRepository regionRepository,
            ICentreRepository centreRepository, ICentreUserRepository centreUserRepository)
        {
            _cacheManager = cacheManager;
            _lookupTableManager = lookupTableManager;
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
            _userRegionRepository = userRegionRepository;
            _regionRepository = regionRepository;
            _centreRepository = centreRepository;
            _centreUserRepository = centreUserRepository;
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="aNumber">a number.</param>
        /// <returns></returns>
        public User GetUser(string aNumber)
        {
            Func<User> function = () =>
            {
                var user = _userRepository.ReadByANumber(aNumber);

                if (user != null)
                {
                    return new User
                    {
                        ANumber = user.ANumber,
                        Id = user.Id,
                        IsActive = user.IsActive,
                        EmailAddress = user.EmailAddress,
                        FirstName = user.FirstName,
                        Surname = user.Surname,
                        UserRoles = GetUserRoles(user.Id),
                        UserRegions = GetUserRegions(user.Id),
                        UserCentres = GetUserCentres(user.Id)
                    };
                }

                return null;
            };

            return _cacheManager.ReturnFromCache(function, 1440, CacheKey.UserInterface.SiteHelper.LoggedInUser,
                new CacheKeyParameter(nameof(aNumber), aNumber));
        }

        /// <summary>
        /// Gets the user centres
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private IEnumerable<Centre> GetUserCentres(int userId)
        {
            var userCentres = new List<Centre>();
            var userCentreIds = _centreUserRepository.ReadByUserId(userId);

            if (userCentreIds != null && userCentreIds.Any())
            {
                var centres = _centreRepository.ReadAll();

                foreach (var centre in centres)
                {
                    if (userCentreIds.Any(_ => _.CentreId == centre.Id && _.IsActive && centre.IsActive))
                    {
                        userCentres.Add(new Centre
                        {
                            Id = centre.Id,
                            Name = centre.CentreName,
                            Province = _lookupTableManager.GetProvinceName(centre.ProvinceId)
                        });
                    }
                }
            }

            return userCentres;
        }

        /// <summary>
        /// Gets the user regions
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private IEnumerable<Region> GetUserRegions(int userId)
        {
            var userRegions = new List<Region>();
            var userRegionIds = _userRegionRepository.ReadByUserId(userId);

            if (userRegionIds != null && userRegionIds.Any())
            {
                var regions = _regionRepository.ReadAll();

                foreach (var region in regions)
                {
                    if (userRegionIds.Any(_ => _.RegionId == region.Id && _.IsActive && region.IsActive))
                    {
                        userRegions.Add(new Region
                        {
                            Id = region.Id,
                            Description = region.Description
                        });
                    }
                }
            }

            return userRegions;
        }

        /// <summary>
        /// Gets the user roles
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private IEnumerable<Role> GetUserRoles(int userId)
        {
            var userRoles = new List<Role>();
            var userRoleIds = _userRoleRepository.ReadByUserId(userId);

            if (userRoleIds != null && userRoleIds.Any())
            {
                var roles = _roleRepository.ReadAll();

                foreach (var role in roles)
                {
                    if (userRoleIds.Any(_ => _.RoleId == role.Id && _.IsActive && role.IsActive))
                    {
                        userRoles.Add(new Role
                        {
                            Description = role.RoleDescription,
                            Id = role.Id,
                            Name = role.RoleName
                        });
                    }
                }
            }

            return userRoles;
        }

    }
}
