namespace StandardBank.ConcessionManagement.Model.UserInterface.Administration
{
    /// <summary>
    /// Business centre management model
    /// </summary>
    public class BusinessCentreManagementModel
    {
        /// <summary>
        /// Gets or sets the centre identifier.
        /// </summary>
        /// <value>
        /// The centre identifier.
        /// </value>
        public int CentreId { get; set; }

        /// <summary>
        /// Gets or sets the name of the centre.
        /// </summary>
        /// <value>
        /// The name of the centre.
        /// </value>
        public string CentreName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the business centre manager identifier.
        /// </summary>
        /// <value>
        /// The business centre manager identifier.
        /// </value>
        public int? BusinessCentreManagerId { get; set; }

        /// <summary>
        /// Gets or sets the business centre manager.
        /// </summary>
        /// <value>
        /// The business centre manager.
        /// </value>
        public string BusinessCentreManager { get; set; }

        /// <summary>
        /// Gets or sets the region identifier.
        /// </summary>
        /// <value>
        /// The region identifier.
        /// </value>
        public int RegionId { get; set; }

        /// <summary>
        /// Gets or sets the region.
        /// </summary>
        /// <value>
        /// The region.
        /// </value>
        public string Region { get; set; }

        /// <summary>
        /// Gets or sets the requestor count.
        /// </summary>
        /// <value>
        /// The requestor count.
        /// </value>
        public int RequestorCount { get; set; }
    }
}
