using System;

namespace StandardBank.ConcessionManagement.Model.UserInterface
{
    /// <summary>
    /// Concession condition
    /// </summary>
    public class ConcessionCondition
    {
        /// <summary>
        /// Gets or sets the concession condition id
        /// </summary>
        public int ConcessionConditionId { get; set; }

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
        public string ProductType { get; set; }

        /// <summary>
        /// Gets or sets the condition product id
        /// </summary>
        public int? ConditionProductId { get; set; }

        /// <summary>
        /// Gets or sets the interest rate
        /// </summary>
        public decimal? InterestRate { get; set; }

        /// <summary>
        /// Gets or sets the condition volume
        /// </summary>
        public int? ConditionVolume { get; set; }

        /// <summary>
        /// Gets or sets the condition value
        /// </summary>
        public decimal? ConditionValue { get; set; }

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

        /// <summary>
        /// Gets or sets the expiry date.
        /// </summary>
        /// <value>
        /// The expiry date.
        /// </value>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// Gets or sets the name of the risk group.
        /// </summary>
        /// <value>
        /// The name of the risk group.
        /// </value>
        public string RiskGroupName { get; set; }

        /// <summary>
        /// Gets or sets the risk group number.
        /// </summary>
        /// <value>
        /// The risk group number.
        /// </value>
        public int RiskGroupNumber { get; set; }

        /// <summary>
        /// Gets or sets the concession reference number.
        /// </summary>
        /// <value>
        /// The concession reference number.
        /// </value>
        public string ConcessionReferenceNumber { get; set; }

        /// <summary>
        /// Gets or sets the rag status.
        /// </summary>
        /// <value>
        /// The rag status.
        /// </value>
        public string RagStatus { get; set; }

        /// <summary>
        /// Gets or sets the approved date.
        /// </summary>
        /// <value>
        /// The approved date.
        /// </value>
        public DateTime? ApprovedDate { get; set; }

        /// <summary>
        /// Gets or sets the type of the concession.
        /// </summary>
        /// <value>
        /// The type of the concession.
        /// </value>
        public string ConcessionType { get; set; }

        /// <summary>
        /// Gets or sets the ConditionMet.
        /// </summary>
        /// <value>
        /// The ConditionMet.
        /// </value>
        public bool? ConditionMet { get; set; }

        public string ActualVolume { get; set; }
        public string ActualValue { get; set; }
        public string ActualTurnover { get; set; }

        public string ConditionComment { get; set; }
    }
}
