using System;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// ChannelTypeBaseRate entity
    /// </summary>
    public class ChannelTypeBaseRate
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        /// <value>
        /// The Id.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ChannelTypeId.
        /// </summary>
        /// <value>
        /// The ChannelTypeId.
        /// </value>
        public int ChannelTypeId { get; set; }

        /// <summary>
        /// Gets or sets the BaseRateId.
        /// </summary>
        /// <value>
        /// The BaseRateId.
        /// </value>
        public int BaseRateId { get; set; }
    }
}
