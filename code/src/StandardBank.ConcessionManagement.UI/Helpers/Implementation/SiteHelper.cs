using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.Common;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.UI.Helpers.Interface;

namespace StandardBank.ConcessionManagement.UI.Helpers.Implementation
{
    /// <summary>
    /// Site helper implementation
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.UI.Helpers.Interface.ISiteHelper" />
    public class SiteHelper : ISiteHelper
    {
        /// <summary>
        /// The configuration data
        /// </summary>
        private readonly IConfigurationData _configurationData;

        /// <summary>
        /// The user manager
        /// </summary>
        private readonly IUserManager _userManager;

    

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteHelper"/> class.
        /// </summary>
        /// <param name="configurationData">The configuration data.</param>
        /// <param name="userManager"></param>
        public SiteHelper(IConfigurationData configurationData, IUserManager userManager)
        {
            _configurationData = configurationData;
            _userManager = userManager;
           
        }

        /// <summary>
        /// Gets the logged in user.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <returns></returns>
        public User LoggedInUser(Controller controller)
        {
            var aNumber = UserIdentity(controller);

            if (!string.IsNullOrWhiteSpace(aNumber))
            {
                var user = _userManager.GetUser(aNumber);

                if(user == null)
                {
                    return null;
                }
              
                if (user.IsActive)
                    return user;
            }  

            return null;
        }

        /// <summary>
        /// Gets the UAT Warning
        /// </summary>
        /// <returns></returns>
        public bool UATWarning()
        {
            return !string.IsNullOrWhiteSpace(_configurationData.ShowUatWarning)  &&
                _configurationData.ShowUatWarning.Equals("true");           
        }

        /// <summary>
        /// Gets the user identity.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <returns></returns>
        public string UserIdentity(Controller controller)
        {
            if (!string.IsNullOrWhiteSpace(_configurationData.OverrideLoggedInUser))
                return _configurationData.OverrideLoggedInUser;

            var aNumber = controller.ControllerContext?.HttpContext?.User?.Identity?.Name;

            if (!string.IsNullOrWhiteSpace(aNumber) && aNumber.Contains("\\"))
                aNumber = aNumber.Substring(aNumber.LastIndexOf("\\", StringComparison.Ordinal) + 1);

            return aNumber;
        }

        /// <summary>
        /// Gets the user identifier for filtering.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <returns></returns>
        public int GetUserIdForFiltering(Controller controller)
        {
            var user = LoggedInUser(controller);
            return _userManager.GetUserIdForFiltering(user);
        }

    }
}
