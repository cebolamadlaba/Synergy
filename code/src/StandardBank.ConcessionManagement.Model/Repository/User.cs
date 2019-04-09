using System.Collections.Generic;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// User entity
    /// </summary>
    public class User : IAuditable
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
        /// Gets or sets the centre ids.
        /// </summary>
        /// <value>
        /// The centre ids.
        /// </value>
        public IEnumerable<int> CentreIds { get; set; }

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
        /// Gets or sets a value indicating whether this instance can approve.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can approve; otherwise, <c>false</c>.
        /// </value>
        public bool CanApprove { get; set; }

        /// <summary>
        /// Gets or the name of the table.
        /// </summary>
        /// <value>
        /// The name of the table.
        /// </value>
        public string TableName => "tblUser";

        /// <summary>
        /// Gets the primary key column name
        /// </summary>
        public string PrimaryKeyColumnName => "pkUserId";

        /// <summary>
        /// Gets the primary key value
        /// </summary>
        public object PrimaryKeyValue => Id;

        /// <summary>
        /// Gets or sets the contact number.
        /// </summary>
        /// <value>
        /// The contact number.
        /// </value>
        public string ContactNumber { get; set; }
    }
}
