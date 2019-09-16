using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.Interface.Repository
{
    /// <summary>
    /// User repository interface
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        User Create(User model);

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        User ReadById(int id);

        /// <summary>
        /// Reads by the a number
        /// </summary>
        /// <param name="aNumber"></param>
        /// <returns></returns>
        User ReadByANumber(string aNumber);

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        IEnumerable<User> ReadAll();

        /// <summary>
        /// Reads the by role.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <returns></returns>
        IEnumerable<User> ReadByRole(string roleName);

        /// <summary>
        /// Reads the by role centre identifier.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <param name="centreId">The centre identifier.</param>
        /// <returns></returns>
        IEnumerable<User> ReadByRoleCentreId(string roleName, int centreId);

        /// <summary>
        /// Reads the by centre identifier.
        /// </summary>
        /// <param name="centreId">The centre identifier.</param>
        /// <returns></returns>
        IEnumerable<User> ReadByCentreId(int centreId);

        /// <summary>
        /// Reads the by risk group identifier.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group identifier.</param>
        /// <returns></returns>
        User ReadByRiskGroupNumber(int riskGroupNumber);

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        void Update(User model);

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        void Delete(User model);

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        int CreateUser(User user);

        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <param name="user">The user.</param>
        void UpdateUser(User user);
    }
}