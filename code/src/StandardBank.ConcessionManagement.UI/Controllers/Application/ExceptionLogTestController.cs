using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.UI.Controllers.Application
{
    /// <summary>
    /// Exception log test controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/Application/[controller]")]
    public class ExceptionLogTestController : Controller
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<ExceptionLogTestController> _logger;

        /// <summary>
        /// The exception log repository
        /// </summary>
        private readonly IExceptionLogRepository _exceptionLogRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionLogTestController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="exceptionLogRepository">The exception log repository.</param>
        public ExceptionLogTestController(ILogger<ExceptionLogTestController> logger, IExceptionLogRepository exceptionLogRepository)
        {
            _logger = logger;
            _exceptionLogRepository = exceptionLogRepository;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Get()
        {
            try
            {
                var exceptionLog = new ExceptionLog
                {
                    ExceptionMessage = "Exception test from the application",
                    ExceptionData = "Exception test",
                    ExceptionSource = "ExceptionLogTestController",
                    ExceptionType = "ExceptionTest",
                    Logdate = DateTime.Now
                };

                var exceptionLogResult = _exceptionLogRepository.Create(exceptionLog);

                return exceptionLogResult.ExceptionLogId != 0 ? "Success" : "Failed";
            }
            catch (Exception ex)
            {
                var errorMessage = $"Failed: {ex}";
                _logger.LogError(errorMessage);
                return errorMessage;
            }
        }
    }
}
