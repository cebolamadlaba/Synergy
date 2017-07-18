using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.Interface.Repository
{
    /// <summary>
    /// Authorizing user repository interface
    /// </summary>
    public interface IAuthorizingUserRepository
    {
        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        AuthorizingUser Create(AuthorizingUser model);

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        IEnumerable<AuthorizingUser> ReadAll();

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        void Delete(AuthorizingUser model);
    }
}
