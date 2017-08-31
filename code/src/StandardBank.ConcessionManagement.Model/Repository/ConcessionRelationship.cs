using System;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// ConcessionRelationship entity
    /// </summary>
    public class ConcessionRelationship
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
    }
}
