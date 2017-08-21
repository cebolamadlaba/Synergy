using System.Collections.Generic;

namespace StandardBank.ConcessionManagement.Model.UserInterface
{
    /// <summary>
    /// Approved concession
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
        /// Gets or sets the concession reference number.
        /// </summary>
        /// <value>
        /// The concession reference number.
        /// </value>
        public string ConcessionReferenceNumber { get; set; }

        /// <summary>
        /// Gets or sets the type of the concession.
        /// </summary>
        /// <value>
        /// The type of the concession.
        /// </value>
        public string ConcessionType { get; set; }

        /// <summary>
        /// Gets or sets the approved concession details.
        /// </summary>
        /// <value>
        /// The approved concession details.
        /// </value>
        public IEnumerable<ApprovedConcessionDetail> ApprovedConcessionDetails { get; set; }
    }
}
