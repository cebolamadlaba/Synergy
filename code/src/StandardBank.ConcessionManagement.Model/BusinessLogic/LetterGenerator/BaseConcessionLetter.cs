namespace StandardBank.ConcessionManagement.Model.BusinessLogic.LetterGenerator
{
    /// <summary>
    /// Base concession letter entity
    /// </summary>
    public abstract class BaseConcessionLetter
    {
        /// <summary>
        /// Gets or sets the legal entity identifier.
        /// </summary>
        /// <value>
        /// The legal entity identifier.
        /// </value>
        public int? LegalEntityId { get; set; }
    }
}
