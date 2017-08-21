using System.Collections.Generic;

namespace StandardBank.ConcessionManagement.Model.UserInterface.Inbox
{
    /// <summary>
    /// User concessions entity
    /// </summary>
    public class UserConcessions
    {
        /// <summary>
        /// Gets or sets the pending concessions count
        /// </summary>
        public int PendingConcessionsCount { get; set; }

        /// <summary>
        /// Gets or sets the pending concessions
        /// </summary>
        public IEnumerable<Concession> PendingConcessions { get; set; }

        /// <summary>
        /// Gets or sets the due for expiry concessions count
        /// </summary>
        public int DueForExpiryConcessionsCount { get; set; }

        /// <summary>
        /// Gets or sets the pending concessions
        /// </summary>
        public IEnumerable<Concession> DueForExpiryConcessions { get; set; }

        /// <summary>
        /// Gets or sets the expired concessions count
        /// </summary>
        public int ExpiredConcessionsCount { get; set; }

        /// <summary>
        /// Gets or sets the expired concessions
        /// </summary>
        public IEnumerable<Concession> ExpiredConcessions { get; set; }

        /// <summary>
        /// Gets or sets the mismatched concessions count
        /// </summary>
        public int MismatchedConcessionsCount { get; set; }

        /// <summary>
        /// Gets or sets the mismatched concessions
        /// </summary>
        public IEnumerable<Concession> MismatchedConcessions { get; set; }

        /// <summary>
        /// Gets or sets the declinced concessions count
        /// </summary>
        public int DeclinedConcessionsCount { get; set; }

        /// <summary>
        /// Gets or sets the declinced concessions
        /// </summary>
        public IEnumerable<Concession> DeclinedConcessions { get; set; }
    }
}
