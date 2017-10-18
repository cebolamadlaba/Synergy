namespace StandardBank.ConcessionManagement.Model.BusinessLogic.LetterGenerator
{
    /// <summary>
    /// Cash concession letter
    /// </summary>
    public class CashConcessionLetter : BaseConcessionLetter
    {
        /// <summary>
        /// Gets or sets the account number.
        /// </summary>
        /// <value>
        /// The account number.
        /// </value>
        public string AccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the type of the channel.
        /// </summary>
        /// <value>
        /// The type of the channel.
        /// </value>
        public string ChannelType { get; set; }

        /// <summary>
        /// Gets or sets the base rate ad valorem.
        /// </summary>
        /// <value>
        /// The base rate ad valorem.
        /// </value>
        public string BaseRateAdValorem { get; set; }

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
