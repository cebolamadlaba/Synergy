using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.Interface.Repository
{
    /// <summary>
    /// LoadedPriceCash repository interface
    /// </summary>
    public interface ILoadedPriceCashRepository
    {
        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        LoadedPriceCash Create(LoadedPriceCash model);

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        LoadedPriceCash ReadById(int id);

        /// <summary>
        /// Reads by the channel type id and the legal entity account id
        /// </summary>
        /// <param name="channelTypeId"></param>
        /// <param name="legalEntityAccountId"></param>
        /// <returns></returns>
        LoadedPriceCash ReadByChannelTypeIdLegalEntityAccountId(int channelTypeId, int legalEntityAccountId);

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        IEnumerable<LoadedPriceCash> ReadAll();

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        void Update(LoadedPriceCash model);

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        void Delete(LoadedPriceCash model);
    }
}