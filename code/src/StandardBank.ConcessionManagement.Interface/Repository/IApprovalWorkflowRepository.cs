using StandardBank.ConcessionManagement.Model.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace StandardBank.ConcessionManagement.Interface.Repository
{
    public interface IApprovalWorkflowRepository
    {
        IEnumerable<User> GetApproversByRoles(int centerId, IEnumerable<int> roles);
    }
}
