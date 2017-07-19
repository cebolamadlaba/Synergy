using Microsoft.Extensions.Logging;
using Moq;
using StandardBank.ConcessionManagement.UI.Controllers.Application;
using Xunit;

namespace StandardBank.ConcessionManagement.UI.Test.UnitTest.Application
{
    /// <summary>
    /// Logger test controller tests
    /// </summary>
    public class LoggerTestControllerTest
    {
        /// <summary>
        /// The concessions summary controller
        /// </summary>
        private readonly LoggerTestController _loggerTestController;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly Mock<ILogger<LoggerTestController>> _logger = new Mock<ILogger<LoggerTestController>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggerTestControllerTest"/> class.
        /// </summary>
        public LoggerTestControllerTest()
        {
            _loggerTestController = new LoggerTestController(_logger.Object);
        }

        /// <summary>
        /// Tests that Get executes positive.
        /// </summary>
        [Fact]
        public void Get_Executes_Positive()
        {
            var result = _loggerTestController.Get();

            Assert.NotNull(result);
            Assert.Equal(result, "Success");
        }
    }
}
