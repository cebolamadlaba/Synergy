using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.Interface.Repository
{
    /// <summary>
    /// Concession condition view repository
    /// </summary>
    public interface IConcessionConditionViewRepository
    {
        /// <summary>
        /// Reads the by period identifier period type identifier.
        /// </summary>
        /// <param name="periodId">The period identifier.</param>
        /// <param name="periodTypeId">The period type identifier.</param>
        /// <returns></returns>
        IEnumerable<ConcessionConditionView> ReadByPeriodIdPeriodTypeId(int periodId, int periodTypeId);

        /// <summary>
        /// Reads the concession counts.
        /// </summary>
        /// <returns></returns>
        IEnumerable<ConcessionCount> ReadConcessionCounts();
    }
}
