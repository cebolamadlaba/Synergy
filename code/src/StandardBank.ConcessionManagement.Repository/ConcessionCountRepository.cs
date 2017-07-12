using System.Collections.Generic;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.Repository
{
    /// <summary>
    /// Concession count repository
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Repository.IConcessionCountRepository" />
    public class ConcessionCountRepository : IConcessionCountRepository
    {
        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ConcessionCount> ReadAll()
        {
            //TODO: Get this from the database
            return new[]
            {
                new ConcessionCount {Status = "Pending", Count = 16},
                new ConcessionCount {Status = "DueForExpiry", Count = 2},
                new ConcessionCount {Status = "Expired", Count = 10},
                new ConcessionCount {Status = "Declined", Count = 4}
            };
        }
    }
}
