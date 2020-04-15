using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.Interface.Repository
{
    /// <summary>
    /// ChannelType repository interface
    /// </summary>
    public interface IGlmsGroupRepository
    {

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        GlmsGroup ReadById(int id);

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        IEnumerable<GlmsGroup> ReadAll();

        IEnumerable<GlmsGroup> ReadAllByRiskGroupAndOrSapBpId(int riskGroupNumber, int? sapBpId);
    }
}