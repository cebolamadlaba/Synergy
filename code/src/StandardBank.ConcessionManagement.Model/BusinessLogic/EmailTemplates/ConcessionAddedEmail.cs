namespace StandardBank.ConcessionManagement.Model.BusinessLogic.EmailTemplates
{
    /// <summary>
    /// Concession added email entity
    /// </summary>
    public class ConcessionAddedEmail
    {
        /// <summary>
        /// Gets or sets the EmailAddress.
        /// </summary>
        /// <value>
        /// The EmailAddress.
        /// </value>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the FirstName.
        /// </summary>
        /// <value>
        /// The FirstName.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the consession identifier.
        /// </summary>
        /// <value>
        /// The consession identifier.
        /// </value>
        public string ConsessionId { get; set; }
    }
}
