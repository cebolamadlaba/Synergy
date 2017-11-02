using System;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// ChannelTypeImport entity
    /// </summary>
    public class ChannelTypeImport
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
        /// Gets or sets the ImportFileChannel.
        /// </summary>
        /// <value>
        /// The ImportFileChannel.
        /// </value>
        public string ImportFileChannel { get; set; }
    }
}
