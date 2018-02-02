using System.Collections.Generic;

namespace StandardBank.ConcessionManagement.Model.UserInterface.Administration
{
    /// <summary>
    /// User admin lookup model
    /// </summary>
    public class UserAdminLookupModel
    {
        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        public IEnumerable<Role> Roles { get; set; }

        /// <summary>
        /// Gets or sets the centres.
        /// </summary>
        /// <value>
        /// The centres.
        /// </value>
        public IEnumerable<Centre> Centres { get; set; }
    }
}
