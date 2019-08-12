using StandardBank.ConcessionManagement.Model.Repository;
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
        /// Gets the full name.
        /// </summary>
        /// <value>
        /// The full name.
        /// </value>
        public string FullName => $"{FirstName} {Surname}";

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
        /// Gets or sets the user sub roles.
        /// </summary>
        /// <value>
        /// The user sub roles.
        /// </value>
        public RoleSubRole RoleSubRole { get; set; }

        /// <summary>
        /// Gets or sets the user centres
        /// </summary>
        public IEnumerable<Centre> UserCentres { get; set; }

        /// <summary>
        /// Gets or sets the selected centre
        /// </summary>
        public Centre SelectedCentre { get; set; }

        /// <summary>
        /// Gets or sets the centre identifier.
        /// </summary>
        /// <value>
        /// The centre identifier.
        /// </value>
        public int CentreId { get; set; }

        /// <summary>
        /// Gets or sets the role identifier.
        /// </summary>
        /// <value>
        /// The role identifier.
        /// </value>
        public int RoleId { get; set; }

        /// <summary>
        /// Gets or sets the role identifier.
        /// </summary>
        /// <value>
        /// The SubRole identifier.
        /// </value>
        public int? SubRoleId { get; set; }

        /// <summary>
        /// Gets or sets whether or not the user can request
        /// </summary>
        public bool CanRequest { get; set; }

        /// <summary>
        /// Gets or sets whether or not the user can bcm approve
        /// </summary>
        public bool CanBcmApprove { get; set; }

        /// <summary>
        /// Gets or sets whether or not the user can pcm approve
        /// </summary>
        public bool CanPcmApprove { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is requestor.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is requestor; otherwise, <c>false</c>.
        /// </value>
        public bool IsRequestor { get; set; }

        /// <summary>
        /// Gets or sets whether or not the user is a HO
        /// </summary>
        public bool IsHO { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is PCM.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is PCM; otherwise, <c>false</c>.
        /// </value>
        public bool IsPCM { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is BCM.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is BCM; otherwise, <c>false</c>.
        /// </value>
        public bool IsBCM { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is admin assistant.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is admin assistant; otherwise, <c>false</c>.
        /// </value>
        public bool IsAdminAssistant { get; set; }

        /// <summary>
        /// Gets or sets the contact number.
        /// </summary>
        /// <value>
        /// The contact number.
        /// </value>
        public string ContactNumber { get; set; }

        /// <summary>
        /// Gets or sets the account executive.
        /// </summary>
        /// <value>
        /// The account executive.
        /// </value>
        public User AccountExecutive { get; set; }

        public List<AccountExecutiveAssistant> AccountAssistants { get; set; }

        public List<AccountExecutiveAssistant> AccountExecutives { get; set; }

        /// <summary>
        /// Gets or sets the account executive user identifier.
        /// </summary>
        /// <value>
        /// The account executive user identifier.
        /// </value>
        public int? AccountExecutiveUserId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance can approve.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can approve; otherwise, <c>false</c>.
        /// </value>
        public bool CanApprove { get; set; }

        public bool Validated { get; set; }

        public string ErrorMessage { get; set; }
    }
}
