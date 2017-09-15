using System;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// TableNumber entity
    /// </summary>
    public class TableNumber
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        /// <value>
        /// The Id.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the TariffTable.
        /// </summary>
        /// <value>
        /// The TariffTable.
        /// </value>
        public int TariffTable { get; set; }

        /// <summary>
        /// Gets or sets the AdValorem.
        /// </summary>
        /// <value>
        /// The AdValorem.
        /// </value>
        public decimal? AdValorem { get; set; }

        /// <summary>
        /// Gets or sets the BaseRate.
        /// </summary>
        /// <value>
        /// The BaseRate.
        /// </value>
        public decimal? BaseRate { get; set; }

        /// <summary>
        /// Gets or sets the IsActive.
        /// </summary>
        /// <value>
        /// The IsActive.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the concession type identifier.
        /// </summary>
        /// <value>
        /// The concession type identifier.
        /// </value>
        public int ConcessionTypeId { get; set; }
    }
}
