namespace StandardBank.ConcessionManagement.Model.BusinessLogic.LetterGenerator
{
    /// <summary>
    /// Condition concession letter
    /// </summary>
    public class ConditionConcessionLetter
    {
        /// <summary>
        /// Gets or sets the condition measure.
        /// </summary>
        /// <value>
        /// The condition measure.
        /// </value>
        public string ConditionMeasure { get; set; }

        /// <summary>
        /// Gets or sets the condition product.
        /// </summary>
        /// <value>
        /// The condition product.
        /// </value>
        public string ConditionProduct { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the deadline.
        /// </summary>
        /// <value>
        /// The deadline.
        /// </value>
        public string Deadline { get; set; }
    }
}
