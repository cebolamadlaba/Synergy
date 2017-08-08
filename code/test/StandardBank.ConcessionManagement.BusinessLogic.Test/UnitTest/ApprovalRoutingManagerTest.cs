using Moq;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Test.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace StandardBank.ConcessionManagement.BusinessLogic.Test.UnitTest
{
    public class ApprovalRoutingManagerTest
    {
        private ApprovalRoutingManager sut;
        Mock<IRoleRepository> roleRepository= new Mock<IRoleRepository>();
        Mock<IApprovalWorkflowRepository> approvalWorkflowRepository = new Mock<IApprovalWorkflowRepository>();
        public ApprovalRoutingManagerTest()
        {
          
          
            sut = new ApprovalRoutingManager(InstantiatedDependencies.Mapper,roleRepository.Object, approvalWorkflowRepository.Object);
        }

        [Fact]
        public void GetApproversByRole_For_BCM_Role_Returns_BCM_Users()
        {
            var roles = new List<Role> { new Role { Id = 1 } };
            var users = new List<User> { new User { ANumber = "A1" } , new User { ANumber ="A2" } };
            roleRepository.Setup(x => x.ReadAll()).Returns(roles);
            approvalWorkflowRepository.Setup(_ => _.GetApproversByRoles(It.IsAny<int>(), It.IsAny<IEnumerable<int>>())).Returns(users);
            var approvers = sut.GetApproversByRole(1, Model.Constants.ApprovalStep.BCMApproval).ToList();
            Assert.True(users.Count == approvers.Count);
            roleRepository.Verify(_ => _.ReadAll(), Times.Once());
            approvalWorkflowRepository.Verify(_ => _.GetApproversByRoles(It.IsAny<int>(), It.IsAny<IEnumerable<int>>()), Times.Once());

        }

        [Fact]
        public void GetApproversByRole_For_PCM_Role_Returns_PCM_Users()
        {
            var roles = new List<Role> { new Role { Id = 1 } };
            var users = new List<User> { new User { ANumber = "A1" }, new User { ANumber = "A2" } };
            roleRepository.Setup(x => x.ReadAll()).Returns(roles);
            approvalWorkflowRepository.Setup(_ => _.GetApproversByRoles(It.IsAny<int>(), It.IsAny<IEnumerable<int>>())).Returns(users);
            var approvers = sut.GetApproversByRole(1, Model.Constants.ApprovalStep.PCMApproval).ToList();
            Assert.True(users.Count == approvers.Count);
            roleRepository.Verify(_ => _.ReadAll(), Times.Once());
            approvalWorkflowRepository.Verify(_ => _.GetApproversByRoles(It.IsAny<int>(), It.IsAny<IEnumerable<int>>()), Times.Once());
        }
    }
}
