using System;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// Concession condition view
    /// </summary>
    public class ConcessionConditionView
    {
        /// <summary>
        /// Gets or sets the concession condition identifier.
        /// </summary>
        /// <value>
        /// The concession condition identifier.
        /// </value>
        public int ConcessionConditionId { get; set; }

        /// <summary>
        /// Gets or sets the concession identifier.
        /// </summary>
        /// <value>
        /// The concession identifier.
        /// </value>
        public int ConcessionId { get; set; }

        /// <summary>
        /// Gets or sets the reference number.
        /// </summary>
        /// <value>
        /// The reference number.
        /// </value>
        public string ReferenceNumber { get; set; }

        /// <summary>
        /// Gets or sets the risk group identifier.
        /// </summary>
        /// <value>
        /// The risk group identifier.
        /// </value>
        public int RiskGroupId { get; set; }

        /// <summary>
        /// Gets or sets the risk group number.
        /// </summary>
        /// <value>
        /// The risk group number.
        /// </value>
        public int RiskGroupNumber { get; set; }

        /// <summary>
        /// Gets or sets the concession type identifier.
        /// </summary>
        /// <value>
        /// The concession type identifier.
        /// </value>
        public int ConcessionTypeId { get; set; }

        /// <summary>
        /// Gets or sets the type of the concession.
        /// </summary>
        /// <value>
        /// The type of the concession.
        /// </value>
        public string ConcessionType { get; set; }

        /// <summary>
        /// Gets or sets the name of the risk group.
        /// </summary>
        /// <value>
        /// The name of the risk group.
        /// </value>
        public string RiskGroupName { get; set; }

        /// <summary>
        /// Gets or sets the condition type identifier.
        /// </summary>
        /// <value>
        /// The condition type identifier.
        /// </value>
        public int ConditionTypeId { get; set; }

        /// <summary>
        /// Gets or sets the type of the condition.
        /// </summary>
        /// <value>
        /// The type of the condition.
        /// </value>
        public string ConditionType { get; set; }

        /// <summary>
        /// Gets or sets the condition product identifier.
        /// </summary>
        /// <value>
        /// The condition product identifier.
        /// </value>
        public int ConditionProductId { get; set; }

        /// <summary>
        /// Gets or sets the condition product.
        /// </summary>
        /// <value>
        /// The condition product.
        /// </value>
        public string ConditionProduct { get; set; }

        /// <summary>
        /// Gets or sets the period type identifier.
        /// </summary>
        /// <value>
        /// The period type identifier.
        /// </value>
        public int? PeriodTypeId { get; set; }

        /// <summary>
        /// Gets or sets the type of the period.
        /// </summary>
        /// <value>
        /// The type of the period.
        /// </value>
        public string PeriodType { get; set; }

        /// <summary>
        /// Gets or sets the period identifier.
        /// </summary>
        /// <value>
        /// The period identifier.
        /// </value>
        public int? PeriodId { get; set; }

        /// <summary>
        /// Gets or sets the period.
        /// </summary>
        /// <value>
        /// The period.
        /// </value>
        public string Period { get; set; }

        /// <summary>
        /// Gets or sets the interest rate.
        /// </summary>
        /// <value>
        /// The interest rate.
        /// </value>
        public decimal? InterestRate { get; set; }

        /// <summary>
        /// Gets or sets the volume.
        /// </summary>
        /// <value>
        /// The volume.
        /// </value>
        public int? Volume { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public decimal? Value { get; set; }

        /// <summary>
        /// Gets or sets the condition met.
        /// </summary>
        /// <value>
        /// The condition met.
        /// </value>
        public bool? ConditionMet { get; set; }

        /// <summary>
        /// Gets or sets the expected turnover value.
        /// </summary>
        /// <value>
        /// The expected turnover value.
        /// </value>
        public decimal? ExpectedTurnoverValue { get; set; }

        /// <summary>
        /// Gets or sets the date approved.
        /// </summary>
        /// <value>
        /// The date approved.
        /// </value>
        public DateTime? DateApproved { get; set; }

        /// <summary>
        /// Gets or sets the expiry date.
        /// </summary>
        /// <value>
        /// The expiry date.
        /// </value>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }
    }
}
