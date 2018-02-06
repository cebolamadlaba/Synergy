using System.Collections.Generic;

namespace StandardBank.ConcessionManagement.Model.UserInterface.Administration
{
    /// <summary>
    /// Account executive model
    /// </summary>
    public class AccountExecutive
    {
        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public User User { get; set; }

        /// <summary>
        /// Gets or sets the account assistants.
        /// </summary>
        /// <value>
        /// The account assistants.
        /// </value>
        public IEnumerable<User> AccountAssistants { get; set; }
    }
}
