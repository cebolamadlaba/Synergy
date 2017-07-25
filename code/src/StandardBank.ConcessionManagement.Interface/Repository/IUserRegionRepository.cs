using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.Interface.Repository
{
    /// <summary>
    /// UserRegion repository interface
    /// </summary>
    public interface IUserRegionRepository
    {
        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        UserRegion Create(UserRegion model);

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        UserRegion ReadById(int id);

        /// <summary>
        /// Reads by the user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<UserRegion> ReadByUserId(int userId);

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        IEnumerable<UserRegion> ReadAll();

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        void Update(UserRegion model);

        /// <summary>
        /// Updates the selected region for the user id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="regionId"></param>
        void UpdateSelectedRegion(int userId, int regionId);

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        void Delete(UserRegion model);
    }
}