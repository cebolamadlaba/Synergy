namespace StandardBank.ConcessionManagement.Model.BusinessLogic.EmailTemplates
{
    /// <summary>
    /// Expiring concession detail
    /// </summary>
    public class ExpiringConcessionDetail
    {
        /// <summary>
        /// Gets or sets the concession reference.
        /// </summary>
        /// <value>
        /// The concession reference.
        /// </value>
        public string ConcessionRef { get; set; }

        /// <summary>
        /// Gets or sets the type of the concession.
        /// </summary>
        /// <value>
        /// The type of the concession.
        /// </value>
        public string ConcessionType { get; set; }

        /// <summary>
        /// Gets or sets the risk group number.
        /// </summary>
        /// <value>
        /// The risk group number.
        /// </value>
        public string RiskGroupNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the risk group.
        /// </summary>
        /// <value>
        /// The name of the risk group.
        /// </value>
        public string RiskGroupName { get; set; }

        /// <summary>
        /// Gets or sets the name of the customer.
        /// </summary>
        /// <value>
        /// The name of the customer.
        /// </value>
        public string CustomerName { get; set; }
        
        /// <summary>
        /// Gets or sets the expiry date.
        /// </summary>
        /// <value>
        /// The expiry date.
        /// </value>
        public string ExpiryDate { get; set; }

        /// <summary>
        /// Gets or sets the date approved.
        /// </summary>
        /// <value>
        /// The date approved.
        /// </value>
        public string DateApproved { get; set; }
    }
}
