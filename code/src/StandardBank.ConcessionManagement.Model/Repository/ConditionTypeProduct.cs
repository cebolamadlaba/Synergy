using System;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// ConditionTypeProduct entity
    /// </summary>
    public class ConditionTypeProduct
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        /// <value>
        /// The Id.
        /// </value>
        public int Id { get; set; }

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
        /// Gets or sets the IsActive.
        /// </summary>
        /// <value>
        /// The IsActive.
        /// </value>
        public bool IsActive { get; set; }
    }
}
