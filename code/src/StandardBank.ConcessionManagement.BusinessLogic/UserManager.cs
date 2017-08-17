using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
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
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;

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
        /// <param name="mapper"></param>
        public UserManager(ICacheManager cacheManager, ILookupTableManager lookupTableManager,
            IUserRepository userRepository, IUserRoleRepository userRoleRepository, IRoleRepository roleRepository,
            IUserRegionRepository userRegionRepository, IRegionRepository regionRepository,
            ICentreRepository centreRepository, ICentreUserRepository centreUserRepository, IMapper mapper)
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
            _mapper = mapper;
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
                    var mappedUser = _mapper.Map<User>(user);

                    mappedUser.UserRoles = GetUserRoles(user.Id);
                    mappedUser.UserRegions = GetUserRegions(user.Id);
                    mappedUser.SelectedRegion = GetSelectedRegion(mappedUser.UserRegions, user);
                    mappedUser.UserCentres = GetUserCentres(user.Id);
                    mappedUser.SelectedCentre = mappedUser.UserCentres.FirstOrDefault();
                    mappedUser.CanRequest = mappedUser.UserRoles.Any(_ => _.Name == "Requestor");
                    mappedUser.CanBcmApprove =
                        mappedUser.UserRoles.Any(_ => _.Name.Trim() == "Suite Head" || _.Name == "BCM");
                    mappedUser.CanPcmApprove =
                        mappedUser.UserRoles.Any(_ => _.Name == "PCM" || _.Name == "Head Office");

                    return mappedUser;
                }

                return null;
            };

            return _cacheManager.ReturnFromCache(function, 1440, CacheKey.UserInterface.SiteHelper.LoggedInUser,
                new CacheKeyParameter(nameof(aNumber), aNumber));
        }

        /// <summary>
        /// Gets the selected region
        /// </summary>
        /// <param name="userRegions"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        private Region GetSelectedRegion(IEnumerable<Region> userRegions, Model.Repository.User user)
        {
            var selectedRegion = userRegions.FirstOrDefault(_ => _.IsSelected);

            //if there isn't a selected region but there are user regions, default the selected region to the first one
            if (selectedRegion == null && userRegions.Any())
            {
                selectedRegion = userRegions.First();
                SetUserSelectedRegion(user.Id, selectedRegion.Id);
                selectedRegion.IsSelected = true;
            }

            return selectedRegion;
        }

        /// <summary>
        /// Sets the user's selected region
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="regionId"></param>
        public void SetUserSelectedRegion(int userId, int regionId)
        {
            //first update the region accordingly
            _userRegionRepository.UpdateSelectedRegion(userId, regionId);

            //then delete the user cache so that the user object is populated from scratch with the changes
            var user = _userRepository.ReadById(userId);
            var aNumber = user.ANumber;

            _cacheManager.Remove(CacheKey.UserInterface.SiteHelper.LoggedInUser,
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
                        var mappedCentre = _mapper.Map<Centre>(centre);
                        mappedCentre.Province = _lookupTableManager.GetProvinceName(centre.ProvinceId);
                        userCentres.Add(mappedCentre);
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
                    var userRegion =
                        userRegionIds.FirstOrDefault(_ => _.RegionId == region.Id && _.IsActive && region.IsActive);

                    if (userRegion != null)
                    {
                        var mappedRegion = _mapper.Map<Region>(region);
                        mappedRegion.IsSelected = userRegion.IsSelected;
                        userRegions.Add(mappedRegion);
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
                    if (userRoleIds.Any(_ => _.RoleId == role.Id && _.IsActive && role.IsActive))
                        userRoles.Add(_mapper.Map<Role>(role));
            }

            return userRoles;
        }

    }
}
