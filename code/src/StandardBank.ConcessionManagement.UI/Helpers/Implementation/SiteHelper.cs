using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.Interface.Common;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Common;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.UI.Helpers.Interface;

namespace StandardBank.ConcessionManagement.UI.Helpers.Implementation
{
    /// <summary>
    /// Site helper implementation
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.UI.Helpers.Interface.ISiteHelper" />
    public class SiteHelper : ISiteHelper
    {
        /// <summary>
        /// The user repository
        /// </summary>
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// The user role repository
        /// </summary>
        private readonly IUserRoleRepository _userRoleRepository;

        /// <summary>
        /// The configuration data
        /// </summary>
        private readonly IConfigurationData _configurationData;

        /// <summary>
        /// The cache manager
        /// </summary>
        private readonly ICacheManager _cacheManager;

        /// <summary>
        /// The role repository
        /// </summary>
        private readonly IRoleRepository _roleRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteHelper"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="userRoleRepository">The user role repository.</param>
        /// <param name="configurationData">The configuration data.</param>
        /// <param name="cacheManager">The cache manager.</param>
        /// <param name="roleRepository"></param>
        public SiteHelper(IUserRepository userRepository, IUserRoleRepository userRoleRepository,
            IConfigurationData configurationData, ICacheManager cacheManager, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _configurationData = configurationData;
            _cacheManager = cacheManager;
            _roleRepository = roleRepository;
        }

        /// <summary>
        /// Gets the logged in user.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <returns></returns>
        public User LoggedInUser(Controller controller)
        {
            var aNumber = UserIdentity(controller);

            if (!string.IsNullOrWhiteSpace(aNumber))
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
                            UserRoles = GetUserRoles(user.Id)
                        };
                    }

                    return null;
                };

                return _cacheManager.ReturnFromCache(function, 1440, CacheKey.UserInterface.SiteHelper.LoggedInUser,
                    new CacheKeyParameter(nameof(aNumber), aNumber));
            }

            return null;
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
                    if (userRoleIds.Any(_ => _.RoleId == role.Id && _.IsActive))
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

        /// <summary>
        /// Gets the user identity.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <returns></returns>
        public string UserIdentity(Controller controller)
        {
            if (!string.IsNullOrWhiteSpace(_configurationData.OverrideLoggedInUser))
                return _configurationData.OverrideLoggedInUser;

            var aNumber = controller.ControllerContext?.HttpContext?.User?.Identity?.Name;

            if (!string.IsNullOrWhiteSpace(aNumber) && aNumber.Contains("\\"))
                aNumber = aNumber.Substring(aNumber.LastIndexOf("\\", StringComparison.Ordinal) + 1);

            return aNumber;
        }
    }
}
