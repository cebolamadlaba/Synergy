using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.Model.UserInterface;

namespace StandardBank.ConcessionManagement.UI.Helpers.Interface
{
    /// <summary>
    /// Site helper interface
    /// </summary>
    public interface ISiteHelper
    {
        /// <summary>
        /// Gets the logged in user.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <returns></returns>
        User LoggedInUser(Controller controller);

        /// <summary>
        /// Gets the user identity.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <returns></returns>
        string UserIdentity(Controller controller);

        /// <summary>
        /// Gets the user identifier for filtering.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <returns></returns>
        int GetUserIdForFiltering(Controller controller);
    }
}
