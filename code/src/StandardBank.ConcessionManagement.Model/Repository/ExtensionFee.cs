using System;
using System.Collections.Generic;
using System.Text;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    public class ExtensionFee
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        /// <value>
        /// The Id.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ExtensionFee.
        /// </summary>
        /// <value>
        /// The ExtensionFee.
        /// </value>
        public decimal extensionFee { get; set; }

        /// <summary>
        /// Gets or sets the IsActive.
        /// </summary>
        /// <value>
        /// The IsActive.
        /// </value>
        public bool IsActive { get; set; }
    }
}
