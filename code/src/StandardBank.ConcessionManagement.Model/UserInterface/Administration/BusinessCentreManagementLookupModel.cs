using System.Collections.Generic;

namespace StandardBank.ConcessionManagement.Model.UserInterface.Administration
{
    /// <summary>
    /// Business centre management lookup model
    /// </summary>
    public class BusinessCentreManagementLookupModel
    {
        /// <summary>
        /// Gets or sets the business centre managers.
        /// </summary>
        /// <value>
        /// The business centre managers.
        /// </value>
        public IEnumerable<User> BusinessCentreManagers { get; set; }

        /// <summary>
        /// Gets or sets the account executives.
        /// </summary>
        /// <value>
        /// The account executives.
        /// </value>
        public IEnumerable<User> AccountExecutives { get; set; }

        /// <summary>
        /// Gets or sets the regions.
        /// </summary>
        /// <value>
        /// The regions.
        /// </value>
        public IEnumerable<Region> Regions { get; set; }

        /// <summary>
        /// Gets or sets the current user.
        /// </summary>
        /// <value>
        /// The current user.
        /// </value>
        public User CurrentUser { get; set; }
    }
}
