using Moq;
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
        public ApprovalRoutingManagerTest()
        {

            sut = new ApprovalRoutingManager(InstantiatedDependencies.Mapper, MockedDependencies.MockRoleRepository.Object, MockedDependencies.MockApprovalWorkflowRepository.Object);
        }

        [Fact]
        public void GetApproversByRole_For_BCM_Role_Returns_BCM_Users()
        {
            var roles = new List<Role> { new Role { Id = 1 } };
            var users = new List<User> { new User { ANumber = "A1" } , new User { ANumber ="A2" } };
            MockedDependencies.MockRoleRepository.Setup(x => x.ReadAll()).Returns(roles);
            MockedDependencies.MockApprovalWorkflowRepository.Setup(_ => _.GetApproversByRoles(It.IsAny<int>(), It.IsAny<IEnumerable<int>>())).Returns(users);
            var approvers = sut.GetApproversByRole(1, Model.Constants.ApprovalStep.BCMApproval).ToList();
            Assert.True(users.Count == approvers.Count);
            MockedDependencies.MockRoleRepository.Verify(_ => _.ReadAll(), Times.Once());
            MockedDependencies.MockApprovalWorkflowRepository.Verify(_ => _.GetApproversByRoles(It.IsAny<int>(), It.IsAny<IEnumerable<int>>()), Times.AtLeastOnce());

        }

        [Fact]
        public void GetApproversByRole_For_PCM_Role_Returns_PCM_Users()
        {
            var roles = new List<Role> { new Role { Id = 1 } };
            var users = new List<User> { new User { ANumber = "A1" }, new User { ANumber = "A2" } };
            MockedDependencies.MockRoleRepository.Setup(x => x.ReadAll()).Returns(roles);
            MockedDependencies.MockApprovalWorkflowRepository.Setup(_ => _.GetApproversByRoles(It.IsAny<int>(), It.IsAny<IEnumerable<int>>())).Returns(users);
            var approvers = sut.GetApproversByRole(1, Model.Constants.ApprovalStep.PCMApproval).ToList();
            Assert.True(users.Count == approvers.Count);
            MockedDependencies.MockRoleRepository.Verify(_ => _.ReadAll(),Times.Once());
            MockedDependencies.MockApprovalWorkflowRepository.Verify(_ => _.GetApproversByRoles(It.IsAny<int>(), It.IsAny<IEnumerable<int>>()), Times.AtLeastOnce());
        }
    }
}
