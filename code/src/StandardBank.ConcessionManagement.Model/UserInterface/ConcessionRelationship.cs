using System;

namespace StandardBank.ConcessionManagement.Model.UserInterface
{
    /// <summary>
    /// Concession relationship entity
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
        /// Gets or sets the creation date.
        /// </summary>
        /// <value>
        /// The creation date.
        /// </value>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the user description.
        /// </summary>
        /// <value>
        /// The user description.
        /// </value>
        public string UserDescription { get; set; }

        /// <summary>
        /// Gets or sets the RelationshipId.
        /// </summary>
        /// <value>
        /// The RelationshipId.
        /// </value>
        public int RelationshipId { get; set; }

        /// <summary>
        /// Gets or sets the relationship description.
        /// </summary>
        /// <value>
        /// The relationship description.
        /// </value>
        public string RelationshipDescription { get; set; }
    }
}
