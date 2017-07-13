using System;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// ConcessionRemovalRequest entity
    /// </summary>
    public class ConcessionRemovalRequest
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
        /// Gets or sets the RequestorId.
        /// </summary>
        /// <value>
        /// The RequestorId.
        /// </value>
        public int RequestorId { get; set; }

        /// <summary>
        /// Gets or sets the BCMUserId.
        /// </summary>
        /// <value>
        /// The BCMUserId.
        /// </value>
        public int? BCMUserId { get; set; }

        /// <summary>
        /// Gets or sets the PCMUserId.
        /// </summary>
        /// <value>
        /// The PCMUserId.
        /// </value>
        public int? PCMUserId { get; set; }

        /// <summary>
        /// Gets or sets the HOUserId.
        /// </summary>
        /// <value>
        /// The HOUserId.
        /// </value>
        public int? HOUserId { get; set; }

        /// <summary>
        /// Gets or sets the SubStatusId.
        /// </summary>
        /// <value>
        /// The SubStatusId.
        /// </value>
        public int? SubStatusId { get; set; }

        /// <summary>
        /// Gets or sets the SystemDate.
        /// </summary>
        /// <value>
        /// The SystemDate.
        /// </value>
        public DateTime? SystemDate { get; set; }

        /// <summary>
        /// Gets or sets the DateApproved.
        /// </summary>
        /// <value>
        /// The DateApproved.
        /// </value>
        public DateTime? DateApproved { get; set; }
    }
}
