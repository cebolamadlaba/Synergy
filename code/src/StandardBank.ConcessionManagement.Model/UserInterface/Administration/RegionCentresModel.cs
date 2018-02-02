using System.Collections.Generic;

namespace StandardBank.ConcessionManagement.Model.UserInterface.Administration
{
    /// <summary>
    /// Region centres model
    /// </summary>
    public class RegionCentresModel
    {
        /// <summary>
        /// Gets or sets the region identifier.
        /// </summary>
        /// <value>
        /// The region identifier.
        /// </value>
        public int RegionId { get; set; }

        /// <summary>
        /// Gets or sets the name of the region.
        /// </summary>
        /// <value>
        /// The name of the region.
        /// </value>
        public string RegionName { get; set; }

        /// <summary>
        /// Gets or sets the centres.
        /// </summary>
        /// <value>
        /// The centres.
        /// </value>
        public IEnumerable<Centre> Centres { get; set; }
    }
}
