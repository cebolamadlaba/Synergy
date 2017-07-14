using System;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// Centre entity
    /// </summary>
    public class Centre
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        /// <value>
        /// The Id.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ProvinceId.
        /// </summary>
        /// <value>
        /// The ProvinceId.
        /// </value>
        public int ProvinceId { get; set; }

        /// <summary>
        /// Gets or sets the CentreName.
        /// </summary>
        /// <value>
        /// The CentreName.
        /// </value>
        public string CentreName { get; set; }

        /// <summary>
        /// Gets or sets the IsActive.
        /// </summary>
        /// <value>
        /// The IsActive.
        /// </value>
        public bool IsActive { get; set; }
    }
}