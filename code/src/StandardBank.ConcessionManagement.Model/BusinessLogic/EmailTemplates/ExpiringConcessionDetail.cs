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

        /// <summary>
        /// Gets or sets the Responsible AE/RM/BM.
        /// </summary>
        /// <value>
        /// This column must display the Name and Surname of the AE that is responsible for the RiskGroup.
        /// </value>
        public string ResponsibleAE { get; set; }

        /// <summary>
        /// Gets or sets the Responsible AA.
        /// </summary>
        /// <value>
        /// This column must display the name and surname of the AA who logged the concession on behalf of the responsible AE. Display "-" if the concession has no responsible AA.
        /// </value>
        public string ResponsibleAA { get; set; } = "(-) NULL";

        public int MonthBeforeExpiry { get; set; }
    }
}
