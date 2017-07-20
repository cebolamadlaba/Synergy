using System;
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
        /// Initializes a new instance of the <see cref="SiteHelper"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="userRoleRepository">The user role repository.</param>
        /// <param name="configurationData">The configuration data.</param>
        /// <param name="cacheManager">The cache manager.</param>
        public SiteHelper(IUserRepository userRepository, IUserRoleRepository userRoleRepository,
            IConfigurationData configurationData, ICacheManager cacheManager)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _configurationData = configurationData;
            _cacheManager = cacheManager;
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
                        var userRoles = _userRoleRepository.ReadByUserId(user.Id);
                        var roles = from userRole in userRoles select userRole.Id;

                        return new User
                        {
                            ANumber = user.ANumber,
                            Id = user.Id,
                            IsActive = user.IsActive,
                            EmailAddress = user.EmailAddress,
                            FirstName = user.FirstName,
                            Surname = user.Surname,
                            UserRoles = roles
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
