using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.Interface.Repository
{
    /// <summary>
    /// LoadedPriceLending repository interface
    /// </summary>
    public interface ILoadedPriceLendingRepository
    {
        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        LoadedPriceLending Create(LoadedPriceLending model);

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        LoadedPriceLending ReadById(int id);

        /// <summary>
        /// Reads by the product type id and the legal entity account id
        /// </summary>
        /// <param name="productTypeId"></param>
        /// <param name="legalEntityAccountId"></param>
        /// <returns></returns>
        LoadedPriceLending ReadByProductTypeIdLegalEntityAccountId(int productTypeId, int legalEntityAccountId);

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        IEnumerable<LoadedPriceLending> ReadAll();

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        void Update(LoadedPriceLending model);

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        void Delete(LoadedPriceLending model);
    }
}