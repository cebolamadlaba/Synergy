using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.Interface.Repository
{
    /// <summary>
    /// ConcessionTrade repository interface
    /// </summary>
    public interface IConcessionTradeRepository
    {
        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        ConcessionTrade Create(ConcessionTrade model);

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        ConcessionTrade ReadById(int id);

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        IEnumerable<ConcessionTrade> ReadAll();

        IEnumerable<TradeProduct> GetTradeProducts();

        IEnumerable<TradeProductType> GetTradeProductTypes();

        TradeProductType GetTradeProductTypeByTradeProductId(int tradeProductId);

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        void Update(ConcessionTrade model);

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        void Delete(ConcessionTrade model);

        IEnumerable<LegalEntityGBBNumber> GetLegalEntityGBBNumbers(int riskGroupNumber);

        IEnumerable<LegalEntityGBBNumber> GetLegalEntityGBBNumbersBySAPBPID(int sapbpid);
    }
}