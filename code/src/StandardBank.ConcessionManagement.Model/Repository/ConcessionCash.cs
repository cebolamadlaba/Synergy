using System;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// ConcessionCash entity
    /// </summary>
    public class ConcessionCash
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
        /// Gets or sets the ChannelTypeId.
        /// </summary>
        /// <value>
        /// The ChannelTypeId.
        /// </value>
        public int ChannelTypeId { get; set; }

        /// <summary>
        /// Gets or sets the TableNumber.
        /// </summary>
        /// <value>
        /// The TableNumber.
        /// </value>
        public int? TableNumber { get; set; }

        /// <summary>
        /// Gets or sets the CashVolume.
        /// </summary>
        /// <value>
        /// The CashVolume.
        /// </value>
        public int? CashVolume { get; set; }

        /// <summary>
        /// Gets or sets the CashValue.
        /// </summary>
        /// <value>
        /// The CashValue.
        /// </value>
        public decimal? CashValue { get; set; }

        /// <summary>
        /// Gets or sets the BaseRateId.
        /// </summary>
        /// <value>
        /// The BaseRateId.
        /// </value>
        public int? BaseRateId { get; set; }

        /// <summary>
        /// Gets or sets the AdValorem.
        /// </summary>
        /// <value>
        /// The AdValorem.
        /// </value>
        public decimal? AdValorem { get; set; }
    }
}
