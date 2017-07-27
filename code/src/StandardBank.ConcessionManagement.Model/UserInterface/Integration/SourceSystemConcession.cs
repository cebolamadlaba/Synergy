using System.Collections.Generic;

namespace StandardBank.ConcessionManagement.Model.UserInterface.Integration
{
    /// <summary>
    /// Source system concession
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Model.UserInterface.Integration.BaseSourceSystem" />
    public class SourceSystemConcession : BaseSourceSystem
    {
        /// <summary>
        /// Gets or sets the concession identifier.
        /// </summary>
        /// <value>
        /// The concession identifier.
        /// </value>
        public string ConcessionId { get; set; }

        /// <summary>
        /// Gets or sets the concessions.
        /// </summary>
        /// <value>
        /// The concessions.
        /// </value>
        public IEnumerable<SourceSystemCustomerConcession> Concessions { get; set; }
    }
}
