using StandardBank.ConcessionManagement.Model.UserInterface;
using System.Collections.Generic;

namespace StandardBank.ConcessionManagement.Interface.BusinessLogic
{
    /// <summary>
    /// Provinces manager interface
    /// </summary>
    public interface IProvinceManager
    {
        /// <summary>
        /// Gets all provinces
        /// </summary>
        /// <returns></returns>
        IEnumerable<Province> GetProvinces();
        
    }
}
