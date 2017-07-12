using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.Interface.Repository
{
    /// <summary>
    /// Concession count repository interface
    /// </summary>
    public interface IConcessionCountRepository
    {
        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        IEnumerable<ConcessionCount> ReadAll();
    }
}
