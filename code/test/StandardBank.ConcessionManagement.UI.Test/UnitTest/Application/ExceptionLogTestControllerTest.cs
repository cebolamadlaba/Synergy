using Microsoft.Extensions.Logging;
using Moq;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.UI.Controllers.Application;
using Xunit;
using static StandardBank.ConcessionManagement.Test.Helpers.MockedDependencies;

namespace StandardBank.ConcessionManagement.UI.Test.UnitTest.Application
{
    /// <summary>
    /// Exception log test controller tests
    /// </summary>
    public class ExceptionLogTestControllerTest
    {
        /// <summary>
        /// The exception log test controller
        /// </summary>
        private readonly ExceptionLogTestController _exceptionLogTestController;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly Mock<ILogger<ExceptionLogTestController>> _logger = new Mock<ILogger<ExceptionLogTestController>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionLogTestControllerTest"/> class.
        /// </summary>
        public ExceptionLogTestControllerTest()
        {
            _exceptionLogTestController =
                new ExceptionLogTestController(_logger.Object, MockExceptionLogRepository.Object);
        }

        /// <summary>
        /// Tests that Get executes positive.
        /// </summary>
        [Fact]
        public void Get_Executes_Positive()
        {
            MockExceptionLogRepository.Setup(_ => _.Create(It.IsAny<ExceptionLog>())).Returns(new ExceptionLog {ExceptionLogId = 1});

            var result = _exceptionLogTestController.Get();

            Assert.NotNull(result);
            Assert.Equal(result, "Success");
        }
    }
}
