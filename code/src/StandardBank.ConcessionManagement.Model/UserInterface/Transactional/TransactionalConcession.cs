using System.Collections.Generic;

namespace StandardBank.ConcessionManagement.Model.UserInterface.Transactional
{
    /// <summary>
    /// Transactional concession
    /// </summary>
    public class TransactionalConcession
    {
        /// <summary>
        /// Gets or sets the concession.
        /// </summary>
        /// <value>
        /// The concession.
        /// </value>
        public Concession Concession { get; set; }

        /// <summary>
        /// Gets or sets the transactional concession details.
        /// </summary>
        /// <value>
        /// The transactional concession details.
        /// </value>
        public IEnumerable<TransactionalConcessionDetail> TransactionalConcessionDetails { get; set; }

        /// <summary>
        /// Gets or sets the concession conditions.
        /// </summary>
        /// <value>
        /// The concession conditions.
        /// </value>
        public IEnumerable<ConcessionCondition> ConcessionConditions { get; set; }

        /// <summary>
        /// Gets or sets the current user.
        /// </summary>
        /// <value>
        /// The current user.
        /// </value>
        public User CurrentUser { get; set; }
    }
}
