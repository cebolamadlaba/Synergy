using StandardBank.ConcessionManagement.Model.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using static StandardBank.ConcessionManagement.Model.Constants;

namespace StandardBank.ConcessionManagement.Interface.BusinessLogic
{
    public interface IApprovalRoutingManager
    {
        IEnumerable<User> GetApproversByRole(int centerId, ApprovalStep approvalStep);
    }
}
