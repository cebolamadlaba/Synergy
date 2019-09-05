using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.Interface.Repository
{
    /// <summary>
    /// ConcessionInvestment repository interface
    /// </summary>
    public interface IConcessionGlmsRepository
    {
        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// 
        IEnumerable<GlmsProduct> GetGlmsProducts();

    }
}