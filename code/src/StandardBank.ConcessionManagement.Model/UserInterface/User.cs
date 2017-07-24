using System.Collections.Generic;

namespace StandardBank.ConcessionManagement.Model.UserInterface
{
    /// <summary>
    /// User entity
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        /// <value>
        /// The Id.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ANumber.
        /// </summary>
        /// <value>
        /// The ANumber.
        /// </value>
        public string ANumber { get; set; }

        /// <summary>
        /// Gets or sets the EmailAddress.
        /// </summary>
        /// <value>
        /// The EmailAddress.
        /// </value>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the FirstName.
        /// </summary>
        /// <value>
        /// The FirstName.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the Surname.
        /// </summary>
        /// <value>
        /// The Surname.
        /// </value>
        public string Surname { get; set; }

        /// <summary>
        /// Gets or sets the IsActive.
        /// </summary>
        /// <value>
        /// The IsActive.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the user roles.
        /// </summary>
        /// <value>
        /// The user roles.
        /// </value>
        public IEnumerable<Role> UserRoles { get; set; }

        /// <summary>
        /// Gets or sets the user regions
        /// </summary>
        public IEnumerable<Region> UserRegions { get; set; }

        /// <summary>
        /// Gets or sets the user centres
        /// </summary>
        public IEnumerable<Centre> UserCentres { get; set; }
    }
}