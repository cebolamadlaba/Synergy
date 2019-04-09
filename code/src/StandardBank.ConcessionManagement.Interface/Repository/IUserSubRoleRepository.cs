using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.UserInterface;

namespace StandardBank.ConcessionManagement.Interface.Repository
{
    /// <summary>
    /// UserSubRole repository interface
    /// </summary>
    public interface IUserSubRoleRepository
    {
        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        UserSubRole Create(UserSubRole model);

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        UserSubRole ReadById(int id);

            /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        IEnumerable<UserSubRole> ReadAll();

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        void Update(UserSubRole model);

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        void Delete(UserSubRole model);
    }
}