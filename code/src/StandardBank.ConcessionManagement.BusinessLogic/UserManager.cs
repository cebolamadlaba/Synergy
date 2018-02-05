using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="UserManager"/> class.
        /// </summary>
        /// <param name="cacheManager">The cache manager.</param>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="userRoleRepository">The user role repository.</param>
        /// <param name="roleRepository">The role repository.</param>
        /// <param name="centreRepository">The centre repository.</param>
        /// <param name="centreUserRepository">The centre user repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="accountExecutiveAssistantRepository">The account executive assistant repository.</param>
        /// <param name="regionManager">The region manager.</param>
        public UserManager(ICacheManager cacheManager, IUserRepository userRepository,
            IUserRoleRepository userRoleRepository, IRoleRepository roleRepository, ICentreRepository centreRepository,
            ICentreUserRepository centreUserRepository, IMapper mapper,
            IAccountExecutiveAssistantRepository accountExecutiveAssistantRepository, IRegionManager regionManager)
        {
            _cacheManager = cacheManager;
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
            _centreRepository = centreRepository;
            _centreUserRepository = centreUserRepository;
            _mapper = mapper;
            _accountExecutiveAssistantRepository = accountExecutiveAssistantRepository;
            _regionManager = regionManager;
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

            return _cacheManager.ReturnFromCache(function, 1440, CacheKey.UserInterface.SiteHelper.LoggedInUser,
                new CacheKeyParameter(nameof(aNumber), aNumber));
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
            mappedUser.RoleId = mappedUser.UserRoles.First().Id;

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

            mappedUser.IsHO = mappedUser.UserRoles.Any(_ => _.Name == Constants.Roles.HeadOffice);
            mappedUser.IsPCM = mappedUser.UserRoles.Any(_ => _.Name == Constants.Roles.PCM);
            mappedUser.IsBCM = mappedUser.UserRoles.Any(_ => _.Name == Constants.Roles.BCM);

            mappedUser.IsAdminAssistant = mappedUser.UserRoles.Any(_ => _.Name == Constants.Roles.AA);

            if (mappedUser.IsAdminAssistant)
                mappedUser.AccountExecutive = GetAccountExecutive(user.Id);

            return mappedUser;
        }

        /// <summary>
        /// Gets the account executive.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        private User GetAccountExecutive(int userId)
        {
            var accountExecutiveAssistant = _accountExecutiveAssistantRepository.ReadByAccountAssistantUserId(userId);

            return accountExecutiveAssistant != null ? GetUser(accountExecutiveAssistant.AccountExecutiveUserId) : null;
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
    }
}
