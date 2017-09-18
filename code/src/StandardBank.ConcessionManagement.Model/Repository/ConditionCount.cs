namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// Condition count entity
    /// </summary>
    public class ConditionCount
    {
        /// <summary>
        /// Gets or sets the type of the period.
        /// </summary>
        /// <value>
        /// The type of the period.
        /// </value>
        public string PeriodType { get; set; }

        /// <summary>
        /// Gets or sets the record count.
        /// </summary>
        /// <value>
        /// The record count.
        /// </value>
        public int RecordCount { get; set; }
    }
}
