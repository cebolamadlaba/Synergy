using System;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// ConcessionCondition entity
    /// </summary>
    public class ConcessionCondition : IAuditable
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        /// <value>
        /// The Id.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ConcessionId.
        /// </summary>
        /// <value>
        /// The ConcessionId.
        /// </value>
        public int ConcessionId { get; set; }

        /// <summary>
        /// Gets or sets the ConditionTypeId.
        /// </summary>
        /// <value>
        /// The ConditionTypeId.
        /// </value>
        public int ConditionTypeId { get; set; }

        /// <summary>
        /// Gets or sets the ConditionProductId.
        /// </summary>
        /// <value>
        /// The ConditionProductId.
        /// </value>
        public int ConditionProductId { get; set; }

        /// <summary>
        /// Gets or sets the PeriodTypeId.
        /// </summary>
        /// <value>
        /// The PeriodTypeId.
        /// </value>
        public int? PeriodTypeId { get; set; }

        /// <summary>
        /// Gets or sets the PeriodId.
        /// </summary>
        /// <value>
        /// The PeriodId.
        /// </value>
        public int? PeriodId { get; set; }

        /// <summary>
        /// Gets or sets the InterestRate.
        /// </summary>
        /// <value>
        /// The InterestRate.
        /// </value>
        public decimal? InterestRate { get; set; }

        /// <summary>
        /// Gets or sets the Volume.
        /// </summary>
        /// <value>
        /// The Volume.
        /// </value>
        public int? Volume { get; set; }

        /// <summary>
        /// Gets or sets the Value.
        /// </summary>
        /// <value>
        /// The Value.
        /// </value>
        public decimal? Value { get; set; }

        /// <summary>
        /// Gets or sets the ConditionMet.
        /// </summary>
        /// <value>
        /// The ConditionMet.
        /// </value>
        public bool? ConditionMet { get; set; }

        /// <summary>
        /// Gets or sets the ExpectedTurnoverValue.
        /// </summary>
        /// <value>
        /// The ExpectedTurnoverValue.
        /// </value>
        public decimal? ExpectedTurnoverValue { get; set; }

        /// <summary>
        /// Gets or sets the ExpiryDate.
        /// </summary>
        /// <value>
        /// The ExpiryDate.
        /// </value>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// Gets or sets the date approved.
        /// </summary>
        /// <value>
        /// The date approved.
        /// </value>
        public DateTime? DateApproved { get; set; }

        /// <summary>
        /// Gets or sets the IsActive.
        /// </summary>
        /// <value>
        /// The IsActive.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or the name of the table.
        /// </summary>
        /// <value>
        /// The name of the table.
        /// </value>
        public string TableName => "tblConcessionCondition";

        /// <summary>
        /// Gets the name of the primary key column.
        /// </summary>
        /// <value>
        /// The name of the primary key column.
        /// </value>
        public string PrimaryKeyColumnName => "pkConcessionConditionId";

        /// <summary>
        /// Gets the primary key value.
        /// </summary>
        /// <value>
        /// The primary key value.
        /// </value>
        public object PrimaryKeyValue => Id;
    }
}
