using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.Interface.Repository
{
    /// <summary>
    /// GlmsTierData repository interface
    /// </summary>
    public interface IGlmsTierDataRepository
    {

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        GlmsTierData ReadById(int id);

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        IEnumerable<GlmsTierData> ReadAll();

        GlmsTierData Create(GlmsTierData model);

        GlmsTierData Update(GlmsTierData model);

    }
}