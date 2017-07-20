namespace StandardBank.ConcessionManagement.Model.UserInterface.Inbox
{
    /// <summary>
    /// Concessions summary entity
    /// </summary>
    public class ConcessionsSummary
    {
        /// <summary>
        /// Gets or sets the pending concessions count
        /// </summary>
        public int PendingConcessions { get; set; }

        /// <summary>
        /// Gets or sets the due for expiry concessions count
        /// </summary>
        public int DueForExpiryConcessions { get; set; }

        /// <summary>
        /// Gets or sets the expired concessions count
        /// </summary>
        public int ExpiredConcessions { get; set; }

        /// <summary>
        /// Gets or sets the mismatched concessions count
        /// </summary>
        public int MismatchedConcessions { get; set; }

        /// <summary>
        /// Gets or sets the declinced concessions count
        /// </summary>
        public int DeclinedConcessions { get; set; }
    }
}
