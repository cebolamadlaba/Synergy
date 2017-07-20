using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.UI.Helpers.Interface;

namespace StandardBank.ConcessionManagement.Test.Helpers
{
    /// <summary>
    /// Fake site helper
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.UI.Helpers.Interface.ISiteHelper" />
    public class FakeSiteHelper : ISiteHelper
    {
        /// <summary>
        /// Gets the logged in user.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <returns></returns>
        public User LoggedInUser(Controller controller)
        {
            return new User
            {
                ANumber = "A1234567",
                Id = 1,
                IsActive = true,
                FirstName = "Unit",
                EmailAddress = "unit.test@standardbank.co.za",
                Surname = "Test",
                UserRoles = new[] {1, 2, 3, 4, 5}
            };
        }

        /// <summary>
        /// Gets the user identity.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <returns></returns>
        public string UserIdentity(Controller controller)
        {
            return "A1234567";
        }
    }
}
