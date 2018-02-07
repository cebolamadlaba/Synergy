using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;

namespace StandardBank.ConcessionManagement.Interface.Repository
{
    /// <summary>
    /// Approval workflow repository
    /// </summary>
    public interface IApprovalWorkflowRepository
    {
        /// <summary>
        /// Gets the approvers by roles.
        /// </summary>
        /// <param name="centerId">The center identifier.</param>
        /// <param name="roles">The roles.</param>
        /// <returns></returns>
        IEnumerable<User> GetApproversByRoles(int centerId, IEnumerable<int> roles);
    }
}
