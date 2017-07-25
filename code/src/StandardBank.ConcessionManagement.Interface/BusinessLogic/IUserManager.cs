using StandardBank.ConcessionManagement.Model.UserInterface;

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
        /// Sets the user's selected region
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="regionId"></param>
        void SetUserSelectedRegion(int userId, int regionId);
    }
}
