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
        /// <param name="requestorId">The requestor identifier.</param>
        /// <returns></returns>
        IEnumerable<ConcessionConditionView> ReadByPeriodIdPeriodTypeId(int periodId, int periodTypeId, int requestorId);

        /// <summary>
        /// Reads the concession counts.
        /// </summary>
        /// <param name="requestorId">The requestor identifier.</param>
        /// <returns></returns>
        IEnumerable<ConcessionCount> ReadConcessionCounts(int requestorId);

        /// <summary>
        /// Reads for renewing ongoing conditions.
        /// </summary>
        /// <returns></returns>
        IEnumerable<ConcessionConditionView> ReadForRenewingOngoingConditions();
    }
}
