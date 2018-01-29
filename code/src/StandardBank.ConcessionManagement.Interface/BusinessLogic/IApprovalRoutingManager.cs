using StandardBank.ConcessionManagement.Model.Repository;
using System.Collections.Generic;
using static StandardBank.ConcessionManagement.Model.BusinessLogic.Constants;

namespace StandardBank.ConcessionManagement.Interface.BusinessLogic
{
    /// <summary>
    /// Approval routing manager
    /// </summary>
    public interface IApprovalRoutingManager
    {
        /// <summary>
        /// Gets the approvers by role.
        /// </summary>
        /// <param name="centerId">The center identifier.</param>
        /// <param name="approvalStep">The approval step.</param>
        /// <returns></returns>
        IEnumerable<User> GetApproversByRole(int centerId, ApprovalStep approvalStep);
    }
}
