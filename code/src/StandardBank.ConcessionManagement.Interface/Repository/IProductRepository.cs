using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.Interface.Repository
{
    /// <summary>
    /// Product repository interface
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Product Create(Product model);

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Product ReadById(int id);

        /// <summary>
        /// Reads by the concession type id and the is active flag
        /// </summary>
        /// <param name="concessionTypeId"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        IEnumerable<Product> ReadByConcessionTypeIdIsActive(int concessionTypeId, bool isActive);

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Product> ReadAll();

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        void Update(Product model);

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        void Delete(Product model);
    }
}