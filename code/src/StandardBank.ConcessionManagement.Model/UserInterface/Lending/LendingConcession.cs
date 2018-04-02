using System.Collections.Generic;

namespace StandardBank.ConcessionManagement.Model.UserInterface.Lending
{
    /// <summary>
    /// Lending concession
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Model.UserInterface.Concession" />
    public class LendingConcession
    {
        /// <summary>
        /// Gets or sets the concession.
        /// </summary>
        /// <value>
        /// The concession.
        /// </value>
        public Concession Concession { get; set; }

        /// <summary>
        /// Gets or sets the lending concession details.
        /// </summary>
        /// <value>
        /// The lending concession details.
        /// </value>
        public IEnumerable<LendingConcessionDetail> LendingConcessionDetails { get; set; }

        /// <summary>
        /// Gets or sets the concession conditions
        /// </summary>
        public IEnumerable<ConcessionCondition> ConcessionConditions { get; set; }

        /// <summary>
        /// Gets or sets the current user
        /// </summary>
        public User CurrentUser { get; set; }

        public IEnumerable<string> PrimeRate { get; set; }
    }
}
