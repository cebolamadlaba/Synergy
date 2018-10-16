namespace StandardBank.ConcessionManagement.Model.BusinessLogic.EmailTemplates
{
    /// <summary>
    /// Approved concession email entity
    /// </summary>
    public class ApprovedConcessionEmail
    {
        /// <summary>
        /// Gets or sets the EmailAddress.
        /// </summary>
        /// <value>
        /// The EmailAddress.
        /// </value>
        public string EmailAddress { get; set; }

        public string ServerURL { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the concession identifier.
        /// </summary>
        /// <value>
        /// The concession identifier.
        /// </value>
        public string ConcessionId { get; set; }

        /// <summary>
        /// Gets or sets the date of request.
        /// </summary>
        /// <value>
        /// The date of request.
        /// </value>
        public string DateOfRequest { get; set; }

        /// <summary>
        /// Gets or sets the date actioned.
        /// </summary>
        /// <value>
        /// The date actioned.
        /// </value>
        public string DateActioned { get; set; }

        /// <summary>
        /// Gets or sets the name of the risk group.
        /// </summary>
        /// <value>
        /// The name of the risk group.
        /// </value>
        public string RiskGroupName { get; set; }

        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        /// <value>
        /// The product.
        /// </value>
        public string Product { get; set; }
    }
}
