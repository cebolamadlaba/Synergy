using System;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// Role entity
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        /// <value>
        /// The Id.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the RoleName.
        /// </summary>
        /// <value>
        /// The RoleName.
        /// </value>
        public string RoleName { get; set; }

        /// <summary>
        /// Gets or sets the RoleDescription.
        /// </summary>
        /// <value>
        /// The RoleDescription.
        /// </value>
        public string RoleDescription { get; set; }

        /// <summary>
        /// Gets or sets the IsActive.
        /// </summary>
        /// <value>
        /// The IsActive.
        /// </value>
        public bool IsActive { get; set; }
    }
}
