using System;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// CentreUser entity
    /// </summary>
    public class CentreUser
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        /// <value>
        /// The Id.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the CentreId.
        /// </summary>
        /// <value>
        /// The CentreId.
        /// </value>
        public int CentreId { get; set; }

        /// <summary>
        /// Gets or sets the UserId.
        /// </summary>
        /// <value>
        /// The UserId.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the IsActive.
        /// </summary>
        /// <value>
        /// The IsActive.
        /// </value>
        public bool IsActive { get; set; }
    }
}
