using System;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// ConcessionApproval entity
    /// </summary>
    public class ConcessionApproval
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        /// <value>
        /// The Id.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ConcessionId.
        /// </summary>
        /// <value>
        /// The ConcessionId.
        /// </value>
        public int ConcessionId { get; set; }

        /// <summary>
        /// Gets or sets the OldSubStatusId.
        /// </summary>
        /// <value>
        /// The OldSubStatusId.
        /// </value>
        public int? OldSubStatusId { get; set; }

        /// <summary>
        /// Gets or sets the NewSubStatusId.
        /// </summary>
        /// <value>
        /// The NewSubStatusId.
        /// </value>
        public int NewSubStatusId { get; set; }

        /// <summary>
        /// Gets or sets the UserId.
        /// </summary>
        /// <value>
        /// The UserId.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the SystemDate.
        /// </summary>
        /// <value>
        /// The SystemDate.
        /// </value>
        public DateTime SystemDate { get; set; }

        /// <summary>
        /// Gets or sets the IsActive.
        /// </summary>
        /// <value>
        /// The IsActive.
        /// </value>
        public bool IsActive { get; set; }
    }
}
