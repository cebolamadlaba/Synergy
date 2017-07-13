using System;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// AdValorem entity
    /// </summary>
    public class AdValorem
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        /// <value>
        /// The Id.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Amount.
        /// </summary>
        /// <value>
        /// The Amount.
        /// </value>
        public decimal? Amount { get; set; }

        /// <summary>
        /// Gets or sets the IsActive.
        /// </summary>
        /// <value>
        /// The IsActive.
        /// </value>
        public bool IsActive { get; set; }
    }
}
