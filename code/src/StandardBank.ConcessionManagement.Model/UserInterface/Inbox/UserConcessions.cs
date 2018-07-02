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
        public IEnumerable<InboxConcession> PendingConcessions { get; set; }

        /// <summary>
        /// Gets or sets whether or not to show the pending concessions view
        /// </summary>
        public bool ShowPendingConcessions { get; set; }

        public bool IsElevatedUser { get; set; }

        /// <summary>
        /// Gets or sets the due for expiry concessions count
        /// </summary>
        public int DueForExpiryConcessionsCount { get; set; }

        /// <summary>
        /// Gets or sets the pending concessions
        /// </summary>
        public IEnumerable<InboxConcession> DueForExpiryConcessions { get; set; }

        /// <summary>
        /// Gets or sets whether or not to show the DueForExpiryConcessions view
        /// </summary>
        public bool ShowDueForExpiryConcessions { get; set; }

        /// <summary>
        /// Gets or sets the expired concessions count
        /// </summary>
        public int ExpiredConcessionsCount { get; set; }

        /// <summary>
        /// Gets or sets the expired concessions
        /// </summary>
        public IEnumerable<InboxConcession> ExpiredConcessions { get; set; }

        /// <summary>
        /// Gets or sets whether or not to show the ExpiredConcessions view
        /// </summary>
        public bool ShowExpiredConcessions { get; set; }

        /// <summary>
        /// Gets or sets the mismatched concessions count
        /// </summary>
        public int MismatchedConcessionsCount { get; set; }

        /// <summary>
        /// Gets or sets the mismatched concessions
        /// </summary>
        public IEnumerable<InboxConcession> MismatchedConcessions { get; set; }

        /// <summary>
        /// Gets or sets whether or not to show the MismatchedConcessions view
        /// </summary>
        public bool ShowMismatchedConcessions { get; set; }

        /// <summary>
        /// Gets or sets the declinced concessions count
        /// </summary>
        public int DeclinedConcessionsCount { get; set; }

        /// <summary>
        /// Gets or sets the declinced concessions
        /// </summary>
        public IEnumerable<InboxConcession> DeclinedConcessions { get; set; }

        /// <summary>
        /// Gets or sets whether or not to show the DeclinedConcessions view
        /// </summary>
        public bool ShowDeclinedConcessions { get; set; }

        /// <summary>
        /// Gets or sets the actioned concessions count.
        /// </summary>
        /// <value>
        /// The actioned concessions count.
        /// </value>
        public int ActionedConcessionsCount { get; set; }

        /// <summary>
        /// Gets or sets the actioned concessions.
        /// </summary>
        /// <value>
        /// The actioned concessions.
        /// </value>
        public IEnumerable<InboxConcession> ActionedConcessions { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show actioned concessions].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show actioned concessions]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowActionedConcessions { get; set; }
    }
}
