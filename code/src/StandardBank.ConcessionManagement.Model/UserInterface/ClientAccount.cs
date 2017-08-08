namespace StandardBank.ConcessionManagement.Model.UserInterface
{
    /// <summary>
    /// Client account entity
    /// </summary>
    public class ClientAccount
    {
        /// <summary>
        /// Gets or sets the legal entity identifier.
        /// </summary>
        /// <value>
        /// The legal entity identifier.
        /// </value>
        public int LegalEntityId { get; set; }

        /// <summary>
        /// Gets or sets the legal entity account identifier.
        /// </summary>
        /// <value>
        /// The legal entity account identifier.
        /// </value>
        public int LegalEntityAccountId { get; set; }

        /// <summary>
        /// Gets or sets the risk group identifier.
        /// </summary>
        /// <value>
        /// The risk group identifier.
        /// </value>
        public int RiskGroupId { get; set; }

        /// <summary>
        /// Gets or sets the account number.
        /// </summary>
        /// <value>
        /// The account number.
        /// </value>
        public string AccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the customer.
        /// </summary>
        /// <value>
        /// The name of the customer.
        /// </value>
        public string CustomerName { get; set; }
    }
}
