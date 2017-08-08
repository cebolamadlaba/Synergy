using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.UI.Helpers.Interface;

namespace StandardBank.ConcessionManagement.UI.Controllers
{
    /// <summary>
    /// Application controller
    /// </summary>
    [Produces("application/json")]
    [Route("api/Application")]
    public class ApplicationController : Controller
    {
        /// <summary>
        /// The exception log repository
        /// </summary>
        private readonly IExceptionLogRepository _exceptionLogRepository;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<ApplicationController> _logger;

        /// <summary>
        /// The site helper
        /// </summary>
        private readonly ISiteHelper _siteHelper;

        /// <summary>
        /// Initializes the controller
        /// </summary>
        /// <param name="exceptionLogRepository"></param>
        /// <param name="logger"></param>
        /// <param name="siteHelper"></param>
        public ApplicationController(IExceptionLogRepository exceptionLogRepository, ILogger<ApplicationController> logger, ISiteHelper siteHelper)
        {
            _exceptionLogRepository = exceptionLogRepository;
            _logger = logger;
            _siteHelper = siteHelper;
        }

        /// <summary>
        /// Exception log test
        /// </summary>
        /// <returns></returns>
        [Route("ExceptionLogTest")]
        public IActionResult ExceptionLogTest()
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

                return Ok(exceptionLogResult.ExceptionLogId != 0 ? "Success" : "Failed");
            }
            catch (Exception ex)
            {
                var errorMessage = $"Failed: {ex}";
                _logger.LogError(errorMessage);
                return BadRequest(errorMessage);
            }
        }

        /// <summary>
        /// Gets the logged in user
        /// </summary>
        /// <returns></returns>
        [Route("LoggedInUser")]
        public IActionResult LoggedInUser()
        {
            return Ok(_siteHelper.LoggedInUser(this));
        }

        /// <summary>
        /// Tests the logger
        /// </summary>
        /// <returns></returns>
        [Route("LoggerTest")]
        public IActionResult LoggerTest()
        {
            try
            {
                _logger.LogTrace("1. LogTrace");
                _logger.LogDebug("2. LogDebug");
                _logger.LogInformation("3. LogInformation");
                _logger.LogWarning("4. LogWarning");
                _logger.LogError("5. LogError");
                _logger.LogCritical("6. LogCritical");

                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed: {ex}");
            }
        }

        /// <summary>
        /// Gets the user identity
        /// </summary>
        /// <returns></returns>
        [Route("UserIdentity")]
        public IActionResult UserIdentity()
        {
            return Ok($"User Identity: {_siteHelper.UserIdentity(this)}");
        }
    }
}