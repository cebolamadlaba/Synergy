using System;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// UserRole entity
    /// </summary>
    public class UserRole
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        /// <value>
        /// The Id.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the UserId.
        /// </summary>
        /// <value>
        /// The UserId.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the RoleId.
        /// </summary>
        /// <value>
        /// The RoleId.
        /// </value>
        public int RoleId { get; set; }

        /// <summary>
        /// Gets or sets the IsActive.
        /// </summary>
        /// <value>
        /// The IsActive.
        /// </value>
        public bool IsActive { get; set; }
    }
}
