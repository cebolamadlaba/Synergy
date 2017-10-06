using System.Collections.Generic;

namespace StandardBank.ConcessionManagement.Model.UserInterface.Transactional
{
    /// <summary>
    /// Transactional view entity
    /// </summary>
    public class TransactionalView
    {
        /// <summary>
        /// Gets or sets the risk group.
        /// </summary>
        /// <value>
        /// The risk group.
        /// </value>
        public RiskGroup RiskGroup { get; set; }

        /// <summary>
        /// Gets or sets the transactional concessions.
        /// </summary>
        /// <value>
        /// The transactional concessions.
        /// </value>
        public IEnumerable<TransactionalConcession> TransactionalConcessions { get; set; }

        /// <summary>
        /// Gets or sets the transactional financial.
        /// </summary>
        /// <value>
        /// The transactional financial.
        /// </value>
        public TransactionalFinancial TransactionalFinancial { get; set; }

        /// <summary>
        /// Gets or sets the transactional products.
        /// </summary>
        /// <value>
        /// The transactional products.
        /// </value>
        public IEnumerable<TransactionalProduct> TransactionalProducts { get; set; }
    }
}
