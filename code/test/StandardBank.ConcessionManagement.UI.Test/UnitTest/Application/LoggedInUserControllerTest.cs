using Microsoft.AspNetCore.Mvc;
using Moq;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.Test.Helpers;
using StandardBank.ConcessionManagement.UI.Controllers.Application;
using Xunit;

namespace StandardBank.ConcessionManagement.UI.Test.UnitTest.Application
{
    /// <summary>
    /// Logged in user controller tests
    /// </summary>
    public class LoggedInUserControllerTest
    {
        /// <summary>
        /// The logged in user controller
        /// </summary>
        private readonly LoggedInUserController _loggedInUserController;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggedInUserControllerTest"/> class.
        /// </summary>
        public LoggedInUserControllerTest()
        {
            _loggedInUserController = new LoggedInUserController(MockedDependencies.MockSiteHelper.Object);
        }

        /// <summary>
        /// Tests that Get executes positive.
        /// </summary>
        [Fact]
        public void Get_Executes_Positive()
        {
            MockedDependencies.MockSiteHelper.Setup(_ => _.LoggedInUser(It.IsAny<Controller>()))
                .Returns(new User());

            var result = _loggedInUserController.Get();

            Assert.NotNull(result);
        }
    }
}
