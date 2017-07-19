using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace StandardBank.ConcessionManagement.UI.Controllers.Application
{
    /// <summary>
    /// Logger test controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/Application/[controller]")]
    public class LoggerTestController : Controller
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<LoggerTestController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggerTestController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public LoggerTestController(ILogger<LoggerTestController> logger)
        {
            _logger = logger;
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
                _logger.LogTrace("1. LogTrace");
                _logger.LogDebug("2. LogDebug");
                _logger.LogInformation("3. LogInformation");
                _logger.LogWarning("4. LogWarning");
                _logger.LogError("5. LogError");
                _logger.LogCritical("6. LogCritical");

                return "Success";
            }
            catch (Exception ex)
            {
                return $"Failed: {ex}";
            }
        }
    }
}
