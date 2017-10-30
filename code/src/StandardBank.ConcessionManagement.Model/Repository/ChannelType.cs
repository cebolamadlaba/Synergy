using System;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// ChannelType entity
    /// </summary>
    public class ChannelType
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        /// <value>
        /// The Id.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        /// <value>
        /// The Description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the IsActive.
        /// </summary>
        /// <value>
        /// The IsActive.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the import file product identifier.
        /// </summary>
        /// <value>
        /// The import file product identifier.
        /// </value>
        public string ImportFileProductId { get; set; }
    }
}
