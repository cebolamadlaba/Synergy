namespace StandardBank.ConcessionManagement.Model.UserInterface
{
    /// <summary>
    /// Concession condition
    /// </summary>
    public class ConcessionCondition
    {
        /// <summary>
        /// Gets or sets the id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the concession id
        /// </summary>
        public int ConcessionId { get; set; }

        /// <summary>
        /// Gets or sets the condition type
        /// </summary>
        public string ConditionType { get; set; }

        /// <summary>
        /// Gets or sets the condition type id
        /// </summary>
        public int? ConditionTypeId { get; set; }

        /// <summary>
        /// Gets or sets the product type
        /// </summary>
        public string ProductType {get; set; }

        /// <summary>
        /// Gets or sets the condition product id
        /// </summary>
        public int? ConditionProductId { get; set; }

        /// <summary>
        /// Gets or sets the interest rate
        /// </summary>
        public decimal InterestRate { get; set; }

        /// <summary>
        /// Gets or sets the condition volume
        /// </summary>
        public int ConditionVolume { get; set; }

        /// <summary>
        /// Gets or sets the condition value
        /// </summary>
        public decimal ConditionValue { get; set; }

        /// <summary>
        /// Gets or sets the period type
        /// </summary>
        public string PeriodType { get; set; }

        /// <summary>
        /// Gets or sets the period type id
        /// </summary>
        public int PeriodTypeId { get; set; }

        /// <summary>
        /// Gets or sets the period
        /// </summary>
        public string Period { get; set; }

        /// <summary>
        /// Gets or sets the period id
        /// </summary>
        public int? PeriodId { get; set; }
    }
}
