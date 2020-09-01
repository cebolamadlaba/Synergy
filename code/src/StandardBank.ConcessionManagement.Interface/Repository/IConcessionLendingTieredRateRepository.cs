using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.Interface.Repository
{
    /// <summary>
    /// ConcessionLendingTieredRate repository interface
    /// </summary>
    public interface IConcessionLendingTieredRateRepository
    {
        ConcessionLendingTieredRate Create(ConcessionLendingTieredRate model);

        ConcessionLendingTieredRate ReadById(int id);

        IEnumerable<ConcessionLendingTieredRate> ReadByConcessionLendingId(int concessionLendingId);

        IEnumerable<ConcessionLendingTieredRate> ReadAll();

        void Update(ConcessionLendingTieredRate model);

        void Delete(int concessionLendingTieredRateId);
    }
}