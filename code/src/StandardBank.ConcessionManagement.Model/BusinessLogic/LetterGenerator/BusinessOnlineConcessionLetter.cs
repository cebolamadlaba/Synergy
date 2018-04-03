namespace StandardBank.ConcessionManagement.Model.BusinessLogic.LetterGenerator
{
    /// <summary>
    /// Transactional concession letter
    /// </summary>
    public class BusinessOnlineConcessionLetter : BaseConcessionLetter
    {
        /// <summary>
        /// Gets or sets the account number.
        /// </summary>
        /// <value>
        /// The account number.
        /// </value>
        public string BOLuserID { get; set; }

        /// <summary>
        /// Gets or sets the type of the channel or fee.
        /// </summary>
        /// <value>
        /// The type of the channel or fee.
        /// </value>
        public string TransactionType { get; set; }

        /// <summary>
        /// Gets or sets the fee or rate.
        /// </summary>
        /// <value>
        /// The fee or rate.
        /// </value>
        public string UnitRate { get; set; }

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
