using System;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// ConcessionComment entity
    /// </summary>
    public class ConcessionComment : IAuditable
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
        /// Gets or sets the UserId.
        /// </summary>
        /// <value>
        /// The UserId.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the ConcessionSubStatusId.
        /// </summary>
        /// <value>
        /// The ConcessionSubStatusId.
        /// </value>
        public int ConcessionSubStatusId { get; set; }

        /// <summary>
        /// Gets or sets the Comment.
        /// </summary>
        /// <value>
        /// The Comment.
        /// </value>
        public string Comment { get; set; }

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

        public string TableName => "tblConcessionComment";

        public string PrimaryKeyColumnName => "pkConcessionCommentId";

        public object PrimaryKeyValue => Id;
    }
}
