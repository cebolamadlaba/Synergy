using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.Common;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
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
        /// The user repository
        /// </summary>
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// The user role repository
        /// </summary>
        private readonly IUserRoleRepository _userRoleRepository;

        /// <summary>
        /// The user sub role repository
        /// </summary>
        private readonly IRoleSubRoleRepository _RoleSubRoleRepository;

        /// <summary>
        /// The role repository
        /// </summary>
        private readonly IRoleRepository _roleRepository;

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
        /// The account executive assistant repository
        /// </summary>
        private readonly IAccountExecutiveAssistantRepository _accountExecutiveAssistantRepository;

        /// <summary>
        /// The region manager
        /// </summary>
        private readonly IRegionManager _regionManager;

        private readonly IMemoryCache _cache;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserManager"/> class.
        /// </summary>
        /// <param name="cacheManager">The cache manager.</param>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="userRoleRepository">The user role repository.</param>
        /// <param name="RoleSubRoleRepository">The user sub role repository.</param>
        /// <param name="roleRepository">The role repository.</param>
        /// <param name="centreRepository">The centre repository.</param>
        /// <param name="centreUserRepository">The centre user repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="accountExecutiveAssistantRepository">The account executive assistant repository.</param>
        /// <param name="regionManager">The region manager.</param>
        public UserManager(ICacheManager cacheManager, IUserRepository userRepository,
            IUserRoleRepository userRoleRepository, IRoleRepository roleRepository, ICentreRepository centreRepository,
            ICentreUserRepository centreUserRepository, IMapper mapper, IRoleSubRoleRepository RoleSubRoleRepository,
            IAccountExecutiveAssistantRepository accountExecutiveAssistantRepository, IRegionManager regionManager, IMemoryCache memoryCache)
        {
            _cacheManager = cacheManager;
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _RoleSubRoleRepository = RoleSubRoleRepository;
            _roleRepository = roleRepository;
            _centreRepository = centreRepository;
            _centreUserRepository = centreUserRepository;
            _mapper = mapper;
            _accountExecutiveAssistantRepository = accountExecutiveAssistantRepository;
            _regionManager = regionManager;
            _cache = memoryCache;
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
                    return Map(user);

                return null;
            };

            var usr = _cacheManager.ReturnFromCache(function, 1440, CacheKey.UserInterface.SiteHelper.LoggedInUser, new CacheKeyParameter(nameof(aNumber), aNumber));

            if (usr != null)
            {
                var RoleSubRole = _userRoleRepository.ReadByUserId(usr.Id).Select(x => x.SubRoleId);

                foreach (var subRole in RoleSubRole)
                {
                    if (subRole.HasValue)
                    {
                        usr.SubRoleId = subRole;
                    }
                }
            }

            //If an accountExecitive was set from UI side, re-populate it.
            if (usr != null)
            {
                if (usr.IsAdminAssistant)
                {
                    int accountExecutiveUserId;

                    // Look for cache key.
                    if (_cache.TryGetValue(aNumber.ToLower() + "_accountExecutiveUserId", out accountExecutiveUserId))
                    {
                        usr.AccountExecutiveUserId = accountExecutiveUserId;
                        usr.AccountExecutive = usr.AccountExecutiveUserId != null ? GetUser(usr.AccountExecutiveUserId) : null;
                    }
                }
            }
            return usr;
        }

        /// <summary>
        /// Maps the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        private User Map(Model.Repository.User user)
        {
            var mappedUser = _mapper.Map<User>(user);

            mappedUser.UserRoles = GetUserRoles(user.Id);
            if (mappedUser.UserRoles.Count() > 0)
                mappedUser.RoleId = mappedUser.UserRoles.FirstOrDefault().Id;

            var userRole = _userRoleRepository.ReadByUserId(mappedUser.Id).FirstOrDefault(x => x.SubRoleId.HasValue);
            if (userRole != null)
                mappedUser.SubRoleId = userRole.SubRoleId.Value;

            mappedUser.UserCentres = GetUserCentres(user.Id);
            mappedUser.SelectedCentre = mappedUser.UserCentres.FirstOrDefault();

            if (mappedUser.SelectedCentre != null)
                mappedUser.CentreId = mappedUser.SelectedCentre.Id;

            mappedUser.CanRequest =
                mappedUser.UserRoles.Any(_ => _.Name == Constants.Roles.Requestor || _.Name == Constants.Roles.AA);

            mappedUser.CanBcmApprove =
                mappedUser.UserRoles.Any(_ => _.Name.Trim() == Constants.Roles.BCM);

            mappedUser.CanPcmApprove =
                mappedUser.UserRoles.Any(_ => _.Name == Constants.Roles.PCM || _.Name == Constants.Roles.HeadOffice);

            mappedUser.IsRequestor = mappedUser.UserRoles.Any(_ => _.Name == Constants.Roles.Requestor);
            mappedUser.IsHO = mappedUser.UserRoles.Any(_ => _.Name == Constants.Roles.HeadOffice);
            mappedUser.IsPCM = mappedUser.UserRoles.Any(_ => _.Name == Constants.Roles.PCM);
            mappedUser.IsBCM = mappedUser.UserRoles.Any(_ => _.Name == Constants.Roles.BCM);

            mappedUser.IsAdminAssistant = mappedUser.UserRoles.Any(_ => _.Name == Constants.Roles.AA);

            if (mappedUser.IsAdminAssistant)
            {
                mappedUser.AccountExecutives = _accountExecutiveAssistantRepository.ReadByAccountAssistantUserId(user.Id).ToList();

                if (mappedUser.AccountExecutives != null && mappedUser.AccountExecutives.Count > 0)
                {
                    //set current to first one
                    mappedUser.AccountExecutiveUserId = mappedUser.AccountExecutives.FirstOrDefault().AccountExecutiveUserId;
                    mappedUser.AccountExecutive = mappedUser.AccountExecutiveUserId != null ? GetUser(mappedUser.AccountExecutiveUserId) : null;
                }
            }
            else
            {
                mappedUser.AccountAssistants = _accountExecutiveAssistantRepository.ReadByAccountExecutiveUserId(user.Id).ToList();
            }

            return mappedUser;
        }


        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public User GetUser(int? userId)
        {
            if (userId.HasValue)
            {
                var user = _userRepository.ReadById(userId.Value);

                return Map(user);
            }

            return null;
        }

        /// <summary>
        /// Gets the name of the user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public string GetUserName(int userId)
        {
            var user = _userRepository.ReadById(userId);
            return $"{user.FirstName} {user.Surname}";
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
                        mappedCentre.Region = _regionManager.GetRegionDescription(centre.RegionId);
                        userCentres.Add(mappedCentre);
                    }
                }
            }

            return userCentres;
        }

        /// <summary>
        /// Gets the user sub role.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RoleSubRole> GetRoleSubRole()
        {
            return _mapper.Map<IEnumerable<RoleSubRole>>(_RoleSubRoleRepository.ReadAll());
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

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="userModel">The user model.</param>
        /// <returns></returns>
        public int CreateUser(User userModel)
        {
            ResetUserCache(userModel.ANumber);

            return _userRepository.CreateUser(_mapper.Map<Model.Repository.User>(userModel));
        }

        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> GetUsers()
        {
            return _mapper.Map<IEnumerable<User>>(_userRepository.ReadAll());
        }

        /// <summary>
        /// Gets the user identifier for filtering.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public int GetUserIdForFiltering(User user)
        {
            var userId = user.Id;

            if (user.IsAdminAssistant && user.AccountExecutiveUserId.HasValue)
                userId = user.AccountExecutiveUserId.Value;

            return userId;
        }

        /// <summary>
        /// Gets the users by role.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <returns></returns>
        public IEnumerable<User> GetUsersByRole(string roleName)
        {
            var users = _userRepository.ReadByRole(roleName);

            foreach (var user in users)
                yield return Map(user);
        }

        /// <summary>
        /// Resets the user cache.
        /// </summary>
        /// <param name="aNumber">a number.</param>
        public void ResetUserCache(string aNumber)
        {
            _cacheManager.Remove(CacheKey.UserInterface.SiteHelper.LoggedInUser,
                new CacheKeyParameter(nameof(aNumber), aNumber));
        }

        /// <summary>
        /// Gets the users by role and centre identifier.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <param name="centreId">The centre identifier.</param>
        /// <returns></returns>
        public IEnumerable<User> GetUsersByRoleCentreId(string roleName, int centreId)
        {
            return _mapper.Map<IEnumerable<User>>(_userRepository.ReadByRoleCentreId(roleName, centreId));
        }

        /// <summary>
        /// Gets the users by centre identifier.
        /// </summary>
        /// <param name="centreId">The centre identifier.</param>
        /// <returns></returns>
        public IEnumerable<User> GetUsersByCentreId(int centreId)
        {
            return _mapper.Map<IEnumerable<User>>(_userRepository.ReadByCentreId(centreId));
        }

        /// <summary>
        /// Validates the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="roleName">Name of the role.</param>
        /// <returns></returns>
        public IEnumerable<string> ValidateUser(User user, string roleName)
        {
            var errors = new List<string>();

            if (user == null)
            {
                errors.Add("No data supplied");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(user.ANumber))
                {
                    errors.Add("Please supply an A Number");
                }
                else
                {
                    var possibleDuplicateUser = GetUser(user.ANumber);

                    if (possibleDuplicateUser != null)
                        if (possibleDuplicateUser.Id != user.Id)
                            errors.Add("You are attempting to add a duplicate A Number");
                }

                if (string.IsNullOrWhiteSpace(user.EmailAddress))
                    errors.Add("Please supply an email address");

                if (string.IsNullOrWhiteSpace(user.FirstName))
                    errors.Add("Please supply a first name");

                if (string.IsNullOrWhiteSpace(user.Surname))
                    errors.Add("Please supply a surname");

                if (roleName == Constants.Roles.PCM)
                {
                    if (user.UserCentres == null || !user.UserCentres.Any())
                        errors.Add("Please link the user to a centre");
                }

                if (roleName == Constants.Roles.BCM)
                {
                    if (user.CentreId == 0)
                        errors.Add("Please select a business centre");
                }
            }

            return errors;
        }

        /// <summary>
        /// Gets the account assistants for account executive.
        /// </summary>
        /// <param name="accountExecutiveUserId">The account executive user identifier.</param>
        /// <returns></returns>
        public IEnumerable<User> GetAccountAssistantsForAccountExecutive(int accountExecutiveUserId)
        {
            var accountAssistants =
                _accountExecutiveAssistantRepository.ReadByAccountExecutiveUserId(accountExecutiveUserId);

            foreach (var accountAssistant in accountAssistants)
                yield return GetUser(accountAssistant.AccountAssistantUserId);
        }
    }
}
