using System;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// ConcessionCondition entity
    /// </summary>
    public class ConcessionCondition
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
        /// Gets or sets the IsActive.
        /// </summary>
        /// <value>
        /// The IsActive.
        /// </value>
        public bool IsActive { get; set; }
    }
}
