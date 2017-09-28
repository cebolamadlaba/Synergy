using System.Collections.Generic;

namespace StandardBank.ConcessionManagement.Model.UserInterface
{
    /// <summary>
    /// Approved concession entity
    /// </summary>
    public class ApprovedConcession
    {
        /// <summary>
        /// Gets or sets the name of the risk group.
        /// </summary>
        /// <value>
        /// The name of the risk group.
        /// </value>
        public string RiskGroupName { get; set; }

        /// <summary>
        /// Gets or sets the risk group number.
        /// </summary>
        /// <value>
        /// The risk group number.
        /// </value>
        public int RiskGroupNumber { get; set; }

        /// <summary>
        /// Gets or sets the concession identifier.
        /// </summary>
        /// <value>
        /// The concession identifier.
        /// </value>
        public int ConcessionId { get; set; }

        /// <summary>
        /// Gets or sets the reference number.
        /// </summary>
        /// <value>
        /// The reference number.
        /// </value>
        public string ReferenceNumber { get; set; }

        /// <summary>
        /// Gets or sets the approved concession details.
        /// </summary>
        /// <value>
        /// The approved concession details.
        /// </value>
        public IEnumerable<ApprovedConcessionDetail> ApprovedConcessionDetails { get; set; }
    }
}
