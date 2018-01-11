using StandardBank.ConcessionManagement.Model.UserInterface;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        /// Sets the user's selected region
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="regionId"></param>
        void SetUserSelectedRegion(int userId, int regionId);

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
    }
}
