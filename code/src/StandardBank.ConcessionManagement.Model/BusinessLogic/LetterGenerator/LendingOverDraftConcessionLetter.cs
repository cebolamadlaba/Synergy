namespace StandardBank.ConcessionManagement.Model.BusinessLogic.LetterGenerator
{
    /// <summary>
    /// Lending over draft concession letter
    /// </summary>
    public class LendingOverDraftConcessionLetter
    {
        /// <summary>
        /// Gets or sets the type of the product.
        /// </summary>
        /// <value>
        /// The type of the product.
        /// </value>
        public string ProductType { get; set; }

        /// <summary>
        /// Gets or sets the account number.
        /// </summary>
        /// <value>
        /// The account number.
        /// </value>
        public string AccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the initiation fee.
        /// </summary>
        /// <value>
        /// The initiation fee.
        /// </value>
        public string InitiationFee { get; set; }

        /// <summary>
        /// Gets or sets the type of the review fee.
        /// </summary>
        /// <value>
        /// The type of the review fee.
        /// </value>
        public string ReviewFeeType { get; set; }

        /// <summary>
        /// Gets or sets the review fee.
        /// </summary>
        /// <value>
        /// The review fee.
        /// </value>
        public string ReviewFee { get; set; }

        /// <summary>
        /// Gets or sets the margin to prime.
        /// </summary>
        /// <value>
        /// The margin to prime.
        /// </value>
        public string MarginToPrime { get; set; }

        /// <summary>
        /// Gets or sets the approved margin to prime.
        /// </summary>
        /// <value>
        /// The approved margin to prime.
        /// </value>
        public string ApprovedMarginToPrime { get; set; }

        /// <summary>
        /// Gets or sets the concession start date.
        /// </summary>
        /// <value>
        /// The concession start date.
        /// </value>
        public string ConcessionStartDate { get; set; }

        /// <summary>
        /// Gets or sets the concession end date.
        /// </summary>
        /// <value>
        /// The concession end date.
        /// </value>
        public string ConcessionEndDate { get; set; }
    }
}
