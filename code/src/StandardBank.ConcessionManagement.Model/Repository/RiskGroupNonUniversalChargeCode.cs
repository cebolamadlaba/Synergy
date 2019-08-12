using System;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// UserRole entity
    /// </summary>
    public class RiskGroupNonUniversalChargeCode
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        /// <value>
        /// The Id.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the RiskGroupId.
        /// </summary>
        /// <value>
        /// The UserId.
        /// </value>
        public int RiskGroupId { get; set; }

        /// <summary>
        /// Gets or sets the ChargeCodeId.
        /// </summary>
        /// <value>
        /// The RoleId.
        /// </value>
        public int ChargeCodeId { get; set; }

        /// <summary>
        /// Gets or sets the IsActive.
        /// </summary>
        /// <value>
        /// The IsActive.
        /// </value>
        public bool IsActive { get; set; }

    }
}
