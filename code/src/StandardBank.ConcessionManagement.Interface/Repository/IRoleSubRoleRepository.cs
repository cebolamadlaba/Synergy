using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.UserInterface;

namespace StandardBank.ConcessionManagement.Interface.Repository
{
    /// <summary>
    /// RoleSubRole repository interface
    /// </summary>
    public interface IRoleSubRoleRepository
    {      
        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        RoleSubRole ReadById(int id);

            /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        IEnumerable<RoleSubRole> ReadAll();

    }
}