using Microsoft.AspNetCore.Mvc;
using Moq;
using StandardBank.ConcessionManagement.Test.Helpers;
using StandardBank.ConcessionManagement.UI.Controllers.Application;
using Xunit;

namespace StandardBank.ConcessionManagement.UI.Test.UnitTest.Application
{
    /// <summary>
    /// User identity controller tests
    /// </summary>
    public class UserIdentityControllerTest
    {
        /// <summary>
        /// The user identity controller
        /// </summary>
        private readonly UserIdentityController _userIdentityController;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserIdentityControllerTest"/> class.
        /// </summary>
        public UserIdentityControllerTest()
        {
            _userIdentityController = new UserIdentityController(MockedDependencies.MockSiteHelper.Object);
        }

        /// <summary>
        /// Tests that Get executes positive.
        /// </summary>
        [Fact]
        public void Get_Executes_Positive()
        {
            MockedDependencies.MockSiteHelper.Setup(_ => _.UserIdentity(It.IsAny<Controller>()))
                .Returns("Unit Test User");

            var result = _userIdentityController.Get();

            Assert.NotNull(result);
            Assert.Equal("User Identity: Unit Test User", result);
        }
    }
}
