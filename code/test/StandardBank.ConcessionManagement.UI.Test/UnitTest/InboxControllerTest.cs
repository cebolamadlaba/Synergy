using Microsoft.AspNetCore.Mvc;
using Moq;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.Model.UserInterface.Inbox;
using StandardBank.ConcessionManagement.UI.Controllers;
using Xunit;
using static StandardBank.ConcessionManagement.Test.Helpers.MockedDependencies;

namespace StandardBank.ConcessionManagement.UI.Test.UnitTest
{
    /// <summary>
    /// Inbox controller tests
    /// </summary>
    public class InboxControllerTest
    {
        /// <summary>
        /// The inbox controller
        /// </summary>
        private readonly InboxController _inboxController;

        /// <summary>
        /// Inbox controller tests
        /// </summary>
        public InboxControllerTest()
        {
            _inboxController = new InboxController(MockConcessionManager.Object, MockSiteHelper.Object);
        }

        /// <summary>
        /// Tests that UserConcessions executes positive
        /// </summary>
        [Fact]
        public void UserConcessions_Executes_Positive()
        {
            MockConcessionManager.Setup(_ => _.GetUserConcessions(It.IsAny<User>())).Returns(new UserConcessions());

            var result = _inboxController.UserConcessions();
            var apiResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(apiResult.Value);
        }
    }
}