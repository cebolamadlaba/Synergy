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

        /// <summary>
        /// Gets or the name of the table.
        /// </summary>
        /// <value>
        /// The name of the table.
        /// </value>
        public string TableName => "tblConcessionComment";

        /// <summary>
        /// Gets the name of the primary key column.
        /// </summary>
        /// <value>
        /// The name of the primary key column.
        /// </value>
        public string PrimaryKeyColumnName => "pkConcessionCommentId";

        /// <summary>
        /// Gets the primary key value.
        /// </summary>
        /// <value>
        /// The primary key value.
        /// </value>
        public object PrimaryKeyValue => Id;
    }
}
