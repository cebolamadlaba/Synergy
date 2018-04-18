using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.UI.Controllers;
using Xunit;
using static StandardBank.ConcessionManagement.Test.Helpers.MockedDependencies;

namespace StandardBank.ConcessionManagement.UI.Test.UnitTest
{
    /// <summary>
    /// Application controller tests
    /// </summary>
    public class ApplicationControllerTest
    {
        /// <summary>
        /// The application controller
        /// </summary>
        private readonly ApplicationController _applicationController;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly Mock<ILogger<ApplicationController>> _logger = new Mock<ILogger<ApplicationController>>();

        /// <summary>
        /// Initializes the class
        /// </summary>
        public ApplicationControllerTest()
        {
            _applicationController = new ApplicationController(MockExceptionLogRepository.Object, _logger.Object,
                MockSiteHelper.Object, new[] {MockDailyScheduledJob.Object}, null);
        }

        /// <summary>
        /// Tests that ExceptionLogTest executes positive
        /// </summary>
        [Fact]
        public void ExceptionLogTest_Executes_Positive()
        {
            MockExceptionLogRepository.Setup(_ => _.Create(It.IsAny<ExceptionLog>())).Returns(new ExceptionLog { ExceptionLogId = 1 });

            var result = _applicationController.ExceptionLogTest();
            var apiResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal(apiResult.Value, "Success");
        }

        /// <summary>
        /// Tests that LoggedInUser executes positive
        /// </summary>
        [Fact]
        public void LoggedInUser_Executes_Positive()
        {
            MockSiteHelper.Setup(_ => _.LoggedInUser(It.IsAny<Controller>())).Returns(new ConcessionManagement.Model.UserInterface.User());

            var result = _applicationController.LoggedInUser();
            var apiResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(apiResult.Value);
        }

        /// <summary>
        /// Tests that LoggerTest executes positive
        /// </summary>
        [Fact]
        public void LoggerTest_Executes_Positive()
        {
            var result = _applicationController.LoggerTest();

            Assert.NotNull(result);
            var apiResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(apiResult.Value, "Success");
        }

        /// <summary>
        /// Tests that UserIdentity executes positive
        /// </summary>
        [Fact]
        public void UserIdentity_Executes_Positive()
        {
            MockSiteHelper.Setup(_ => _.UserIdentity(It.IsAny<Controller>())).Returns("Unit Test User");

            var result = _applicationController.UserIdentity();

            Assert.NotNull(result);
            var apiResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("User Identity: Unit Test User", apiResult.Value);
        }
    }
}