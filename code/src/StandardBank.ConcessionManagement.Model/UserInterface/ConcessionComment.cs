using System;

namespace StandardBank.ConcessionManagement.Model.UserInterface
{
    /// <summary>
    /// Concession comment entity
    /// </summary>
    public class ConcessionComment
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the concession identifier.
        /// </summary>
        /// <value>
        /// The concession identifier.
        /// </value>
        public int ConcessionId { get; set; }

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
        /// Gets or sets the concession sub status identifier.
        /// </summary>
        /// <value>
        /// The concession sub status identifier.
        /// </value>
        public int ConcessionSubStatusId { get; set; }

        /// <summary>
        /// Gets or sets the concession sub status description.
        /// </summary>
        /// <value>
        /// The concession sub status description.
        /// </value>
        public string ConcessionSubStatusDescription { get; set; }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the system date.
        /// </summary>
        /// <value>
        /// The system date.
        /// </value>
        public DateTime SystemDate { get; set; }
    }
}
