using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using static StandardBank.ConcessionManagement.Model.Constants;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model;
using AutoMapper;

namespace StandardBank.ConcessionManagement.BusinessLogic
{
    public class ApprovalRoutingManager : IApprovalRoutingManager
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        private readonly IApprovalWorkflowRepository _approvalWorkflowRepository;
        public ApprovalRoutingManager(IMapper mapper,IRoleRepository roleRepository, IApprovalWorkflowRepository approvalWorkflowRepository)
        {
            _roleRepository = roleRepository;
            _approvalWorkflowRepository = approvalWorkflowRepository;
            _mapper = mapper;
        }
        public IEnumerable<User> GetApproversByRole(int centerId, ApprovalStep approvalStep)
        {
            switch (approvalStep)
            {
                case ApprovalStep.BCMApproval:
                    var roles = _roleRepository.ReadAll().Where(x => x.RoleName == Constants.Roles.BCM || x.RoleName == Constants.Roles.SuiteHead).Select(x => x.Id);
                    var users = _approvalWorkflowRepository.GetApproversByRoles(centerId, roles);
                    return _mapper.Map<IEnumerable<User>>(users);
                    
                case ApprovalStep.PCMApproval:
                     roles = _roleRepository.ReadAll().Where(x => x.RoleName == Constants.Roles.PCM || x.RoleName == Constants.Roles.HeadOffice).Select(x => x.Id);
                     users = _approvalWorkflowRepository.GetApproversByRoles(centerId, roles);
                    return _mapper.Map<IEnumerable<User>>(users);

                default:
                    return null;
            }
        }
    }
}
