using System;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// ScenarioManagerToolDeal entity
    /// </summary>
    public class ScenarioManagerToolDeal
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        /// <value>
        /// The Id.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the DealNumber.
        /// </summary>
        /// <value>
        /// The DealNumber.
        /// </value>
        public string DealNumber { get; set; }

        /// <summary>
        /// Gets or sets the IsActive.
        /// </summary>
        /// <value>
        /// The IsActive.
        /// </value>
        public bool IsActive { get; set; }
    }
}
