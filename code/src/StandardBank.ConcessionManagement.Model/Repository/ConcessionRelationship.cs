using System;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// ConcessionRelationship entity
    /// </summary>
    public class ConcessionRelationship : IAuditable
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        /// <value>
        /// The Id.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ParentConcessionId.
        /// </summary>
        /// <value>
        /// The ParentConcessionId.
        /// </value>
        public int ParentConcessionId { get; set; }

        /// <summary>
        /// Gets or sets the ChildConcessionId.
        /// </summary>
        /// <value>
        /// The ChildConcessionId.
        /// </value>
        public int ChildConcessionId { get; set; }

        /// <summary>
        /// Gets or sets the RelationshipId.
        /// </summary>
        /// <value>
        /// The RelationshipId.
        /// </value>
        public int RelationshipId { get; set; }

        /// <summary>
        /// Gets or sets the UserId.
        /// </summary>
        /// <value>
        /// The UserId.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the CreationDate.
        /// </summary>
        /// <value>
        /// The CreationDate.
        /// </value>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Gets or the name of the table.
        /// </summary>
        /// <value>
        /// The name of the table.
        /// </value>
        public string TableName => "tblConcessionRelationship";

        /// <summary>
        /// Gets the name of the primary key column.
        /// </summary>
        /// <value>
        /// The name of the primary key column.
        /// </value>
        public string PrimaryKeyColumnName => "pkConcessionRelationshipId";

        /// <summary>
        /// Gets the primary key value.
        /// </summary>
        /// <value>
        /// The primary key value.
        /// </value>
        public object PrimaryKeyValue => Id;
    }
}
