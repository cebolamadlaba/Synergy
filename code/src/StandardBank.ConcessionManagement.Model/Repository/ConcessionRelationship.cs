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
        /// Gets or sets the CreationDate.
        /// </summary>
        /// <value>
        /// The CreationDate.
        /// </value>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the UserId.
        /// </summary>
        /// <value>
        /// The UserId.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets the table name
        /// </summary>
        public string TableName => "tblConcessionRelationship";

        /// <summary>
        /// Gets the primary key column name
        /// </summary>
        public string PrimaryKeyColumnName => "pkConcessionRelationshipId";

        /// <summary>
        /// Gets the primary key value
        /// </summary>
        public object PrimaryKeyValue => Id;
    }
}
