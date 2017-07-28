using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.Interface.Repository
{
    /// <summary>
    /// ConcessionAccount repository interface
    /// </summary>
    public interface IConcessionAccountRepository
    {
        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        ConcessionAccount Create(ConcessionAccount model);

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        ConcessionAccount ReadById(int id);

        /// <summary>
        /// Reads by the concession id and is active flag
        /// </summary>
        /// <param name="concessionId"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        ConcessionAccount ReadByConcessionIdIsActive(int concessionId, bool isActive);

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        IEnumerable<ConcessionAccount> ReadAll();

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        void Update(ConcessionAccount model);

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        void Delete(ConcessionAccount model);
    }
}