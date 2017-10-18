namespace StandardBank.ConcessionManagement.Model.BusinessLogic.LetterGenerator
{
    /// <summary>
    /// Lending concession letter
    /// </summary>
    public class LendingConcessionLetter : BaseConcessionLetter
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
        /// Gets or sets the type of the channel or fee.
        /// </summary>
        /// <value>
        /// The type of the channel or fee.
        /// </value>
        public string ChannelOrFeeType { get; set; }

        /// <summary>
        /// Gets or sets the fee or margin above prime.
        /// </summary>
        /// <value>
        /// The fee or margin above prime.
        /// </value>
        public string FeeOrMarginAbovePrime { get; set; }

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
