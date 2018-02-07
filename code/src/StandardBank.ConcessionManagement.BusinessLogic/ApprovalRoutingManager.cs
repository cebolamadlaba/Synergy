using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using System.Collections.Generic;
using System.Linq;
using StandardBank.ConcessionManagement.Model.Repository;
using static StandardBank.ConcessionManagement.Model.BusinessLogic.Constants;
using StandardBank.ConcessionManagement.Interface.Repository;
using AutoMapper;

namespace StandardBank.ConcessionManagement.BusinessLogic
{
    /// <summary>
    /// Approval routing manager
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.BusinessLogic.IApprovalRoutingManager" />
    public class ApprovalRoutingManager : IApprovalRoutingManager
    {
        /// <summary>
        /// The role repository
        /// </summary>
        private readonly IRoleRepository _roleRepository;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// The approval workflow repository
        /// </summary>
        private readonly IApprovalWorkflowRepository _approvalWorkflowRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApprovalRoutingManager"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="roleRepository">The role repository.</param>
        /// <param name="approvalWorkflowRepository">The approval workflow repository.</param>
        public ApprovalRoutingManager(IMapper mapper, IRoleRepository roleRepository,
            IApprovalWorkflowRepository approvalWorkflowRepository)
        {
            _roleRepository = roleRepository;
            _approvalWorkflowRepository = approvalWorkflowRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the approvers by role.
        /// </summary>
        /// <param name="centerId">The center identifier.</param>
        /// <param name="approvalStep">The approval step.</param>
        /// <returns></returns>
        public IEnumerable<User> GetApproversByRole(int centerId, ApprovalStep approvalStep)
        {
            switch (approvalStep)
            {
                case ApprovalStep.RequestorApproval:
                    var roles = _roleRepository.ReadAll().Where(x => x.RoleName.Trim() == Roles.Requestor)
                        .Select(x => x.Id);
                    var users = _approvalWorkflowRepository.GetApproversByRoles(centerId, roles);
                    return _mapper.Map<IEnumerable<User>>(users);

                case ApprovalStep.BCMApproval:
                    roles = _roleRepository.ReadAll()
                        .Where(x => x.RoleName.Trim() == Roles.BCM).Select(x => x.Id);
                    users = _approvalWorkflowRepository.GetApproversByRoles(centerId, roles);
                    return _mapper.Map<IEnumerable<User>>(users);

                case ApprovalStep.PCMApproval:
                    roles = _roleRepository.ReadAll()
                        .Where(x => x.RoleName.Trim() == Roles.PCM ||
                                    x.RoleName.Trim() == Roles.HeadOffice).Select(x => x.Id);
                    users = _approvalWorkflowRepository.GetApproversByRoles(centerId, roles);
                    return _mapper.Map<IEnumerable<User>>(users);

                default:
                    return null;
            }
        }
    }
}