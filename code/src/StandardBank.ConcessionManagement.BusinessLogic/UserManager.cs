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
        /// Initializes a new instance of the <see cref="UserManager"/> class.
        /// </summary>
        /// <param name="cacheManager">The cache manager.</param>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="userRoleRepository">The user role repository.</param>
        /// <param name="roleRepository">The role repository.</param>
        public UserManager(ICacheManager cacheManager, IUserRepository userRepository,
            IUserRoleRepository userRoleRepository, IRoleRepository roleRepository)
        {
            _cacheManager = cacheManager;
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
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
                        UserRoles = GetUserRoles(user.Id)
                    };
                }

                return null;
            };

            return _cacheManager.ReturnFromCache(function, 1440, CacheKey.UserInterface.SiteHelper.LoggedInUser,
                new CacheKeyParameter(nameof(aNumber), aNumber));
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

    }
}
