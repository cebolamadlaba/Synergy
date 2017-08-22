using System.Collections.Generic;

namespace StandardBank.ConcessionManagement.Model.UserInterface.Cash
{
    /// <summary>
    /// Cash concession entity
    /// </summary>
    public class CashConcession
    {
        /// <summary>
        /// Gets or sets the concession.
        /// </summary>
        /// <value>
        /// The concession.
        /// </value>
        public Concession Concession { get; set; }

        /// <summary>
        /// Gets or sets the cash concession details.
        /// </summary>
        /// <value>
        /// The cash concession details.
        /// </value>
        public IEnumerable<CashConcessionDetail> CashConcessionDetails { get; set; }

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
