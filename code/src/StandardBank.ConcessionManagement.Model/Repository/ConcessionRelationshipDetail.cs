using System;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// Concession relationship detail
    /// </summary>
    public class ConcessionRelationshipDetail
    {
        /// <summary>
        /// Gets or sets the type of the relationship.
        /// </summary>
        /// <value>
        /// The type of the relationship.
        /// </value>
        public string RelationshipType { get; set; }

        /// <summary>
        /// Gets or sets the parent concession identifier.
        /// </summary>
        /// <value>
        /// The parent concession identifier.
        /// </value>
        public int ParentConcessionId { get; set; }

        /// <summary>
        /// Gets or sets the parent concession reference.
        /// </summary>
        /// <value>
        /// The parent concession reference.
        /// </value>
        public string ParentConcessionReference { get; set; }

        /// <summary>
        /// Gets or sets the parent concession.
        /// </summary>
        /// <value>
        /// The parent concession.
        /// </value>
        public string ParentConcession { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [parent is active].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [parent is active]; otherwise, <c>false</c>.
        /// </value>
        public bool ParentIsActive { get; set; }

        /// <summary>
        /// Gets or sets the relationship.
        /// </summary>
        /// <value>
        /// The relationship.
        /// </value>
        public string Relationship { get; set; }

        /// <summary>
        /// Gets or sets the child concession identifier.
        /// </summary>
        /// <value>
        /// The child concession identifier.
        /// </value>
        public int ChildConcessionId { get; set; }

        /// <summary>
        /// Gets or sets the child concession reference.
        /// </summary>
        /// <value>
        /// The child concession reference.
        /// </value>
        public string ChildConcessionReference { get; set; }

        /// <summary>
        /// Gets or sets the child concession.
        /// </summary>
        /// <value>
        /// The child concession.
        /// </value>
        public string ChildConcession { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [child is active].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [child is active]; otherwise, <c>false</c>.
        /// </value>
        public bool ChildIsActive { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public string User { get; set; }
    }
}
