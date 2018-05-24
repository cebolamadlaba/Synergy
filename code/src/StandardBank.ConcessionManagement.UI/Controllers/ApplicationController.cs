using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyAccess;
using StandardBank.ConcessionManagement.Interface.BusinessLogic.ScheduledJobs;
using StandardBank.ConcessionManagement.Interface.Common;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Common;
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
        /// The daily scheduled jobs
        /// </summary>
        private readonly IEnumerable<IDailyScheduledJob> _dailyScheduledJobs;

        private readonly ICacheManager _cacheManager;

        private readonly IConfigurationData _configurationData;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationController"/> class.
        /// </summary>
        /// <param name="exceptionLogRepository">The exception log repository.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="siteHelper">The site helper.</param>
        /// <param name="dailyScheduledJobs">The daily scheduled jobs.</param>
        public ApplicationController(IExceptionLogRepository exceptionLogRepository,
            ILogger<ApplicationController> logger, ISiteHelper siteHelper,
            IEnumerable<IDailyScheduledJob> dailyScheduledJobs, ICacheManager cacheManager, IConfigurationData configurationData)
        {
            _exceptionLogRepository = exceptionLogRepository;
            _logger = logger;
            _siteHelper = siteHelper;
            _dailyScheduledJobs = dailyScheduledJobs;
            _cacheManager = cacheManager;
            _configurationData = configurationData;
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


        [Route("ValidateUserMyAccess")]
        public IActionResult ValidateUserMyAccess()
        {
            var theuser = _siteHelper.LoggedInUser(this);

            if (!string.IsNullOrEmpty(_configurationData.EnforceMyAccess) && _configurationData.EnforceMyAccess == "false")
            {
                if (theuser == null)
                {
                    theuser = new Model.UserInterface.User();
                    theuser.Validated = true;
                    theuser.ErrorMessage = "";

                    return Ok(theuser);
                }
                else
                {
                    theuser.Validated = true;
                    theuser.ErrorMessage = "";

                    return Ok(theuser);
                }
            }

            //Func<Model.UserInterface.User> function = () =>
            //{
            string reason = "";

            if (theuser == null)
            {
                // for testing purposes..
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    theuser = new Model.UserInterface.User();
                    theuser.Validated = true;
                    theuser.ErrorMessage = "";

                    return Ok(theuser);
                }
                else
                {
                    theuser = new Model.UserInterface.User();
                    theuser.Validated = true;
                    theuser.ErrorMessage = "User not found in repository";

                    return Ok(theuser);

                }
            }

            bool validUserForApplication = this.AuthenticateUserForApplication(theuser.ANumber, "Concession Management Service", out reason);

            if (validUserForApplication)
            {
                theuser.Validated = true;
                theuser.ErrorMessage = "";

                return Ok(theuser);
            }
            else
            {
                //for testing purposes..
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    theuser.Validated = true;
                    theuser.ErrorMessage = "";

                    return Ok(theuser);
                }

                theuser.Validated = false;
                theuser.ErrorMessage = reason;

                return Ok(theuser);
            }
            // };


            //var applicableuser = _cacheManager.ReturnFromCache(function, 1, CacheKey.UserInterface.SiteHelper.UserAccess,new CacheKeyParameter(nameof(theuser.ANumber), theuser));

            // return Ok(applicableuser);

        }


        public bool AuthenticateUserForApplication(string aNumber, string applicationName, out string reason)
        {
            reason = "Not_Valid";
            try
            {
                MyAccess.AuditServiceClient client = new MyAccess.AuditServiceClient();
                AuthenticationResult result = client.AuthUserAsync(aNumber, applicationName).Result;

                if (!string.IsNullOrEmpty(result.Reason))
                    reason = result.Reason;

                return result.Result;
            }
            catch (Exception ex)
            {
                // Either service is offline, or the ANumber is not linked to the provided application.
                reason = "My Access error during WS call for " + aNumber;
            }

            return false;
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

        /// <summary>
        /// Runs the daily scheduled jobs.
        /// </summary>
        /// <returns></returns>
        [Route("RunDailyScheduledJobs")]
        public async Task<IActionResult> RunDailyScheduledJobs()
        {
            foreach (var dailyScheduledJob in _dailyScheduledJobs)
                await dailyScheduledJob.Run();

            return Ok("Done");
        }
    }
}
