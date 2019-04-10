using System.Collections;
using StandardBank.ConcessionManagement.Model.UserInterface;
using System.Collections.Generic;

namespace StandardBank.ConcessionManagement.Interface.BusinessLogic
{
    /// <summary>
    /// User manager interface
    /// </summary>
    public interface IUserManager
    {
        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="aNumber">a number.</param>
        /// <returns></returns>
        User GetUser(string aNumber);

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        User GetUser(int? userId);

        /// <summary>
        /// Gets the name of the user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        string GetUserName(int userId);

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="userModel">The user model.</param>
        /// <returns></returns>
        int CreateUser(User userModel);

        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <returns></returns>
        IEnumerable<User> GetUsers();

        /// <summary>
        /// Gets the user identifier for filtering.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        int GetUserIdForFiltering(User user);

        /// <summary>
        /// Gets the users by role.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <returns></returns>
        IEnumerable<User> GetUsersByRole(string roleName);

        /// <summary>
        /// Resets the user cache.
        /// </summary>
        /// <param name="aNumber">a number.</param>
        void ResetUserCache(string aNumber);

        /// <summary>
        /// Gets the users by role and centre identifier.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <param name="centreId">The centre identifier.</param>
        /// <returns></returns>
        IEnumerable<User> GetUsersByRoleCentreId(string roleName, int centreId);

        /// <summary>
        /// Gets the users by centre identifier.
        /// </summary>
        /// <param name="centreId">The centre identifier.</param>
        /// <returns></returns>
        IEnumerable<User> GetUsersByCentreId(int centreId);

        /// <summary>
        /// Gets the users sub role
        /// </summary>
        /// <returns></returns>
        IEnumerable<RoleSubRole> GetRoleSubRole();

        /// <summary>
        /// Validates the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="roleName">Name of the role.</param>
        /// <returns></returns>
        IEnumerable<string> ValidateUser(User user, string roleName);

        /// <summary>
        /// Gets the account assistants for account executive.
        /// </summary>
        /// <param name="accountExecutiveUserId">The account executive user identifier.</param>
        /// <returns></returns>
        IEnumerable<User> GetAccountAssistantsForAccountExecutive(int accountExecutiveUserId);
    }
}
