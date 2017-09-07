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
        int CreateUser(UserModel userModel);
        IEnumerable<UserModel> GetUsers();
    }
}
