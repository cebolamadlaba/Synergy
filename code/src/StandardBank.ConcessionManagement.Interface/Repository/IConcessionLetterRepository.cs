using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.Interface.Repository
{
    /// <summary>
    /// FinancialTransactional repository interface
    /// </summary>
    public interface IConcessionLetterRepository
    {
      
        /// <summary>
        /// Reads by the risk group id
        /// </summary>
        /// <param name="riskGroupId"></param>
        /// <returns></returns>
        IEnumerable<ConcessionLetter> ReadByConcessionDetailId(int riskGroupId);

        ConcessionLetter Create(ConcessionLetter model);

        void Delete(ConcessionLetter model);

    }
}