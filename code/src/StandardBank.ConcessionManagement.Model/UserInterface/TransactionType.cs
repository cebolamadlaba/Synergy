using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.UserInterface.Transactional;

namespace StandardBank.ConcessionManagement.Model.UserInterface
{
    /// <summary>
    /// Transaction type entity
    /// </summary>
    public class TransactionType
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the concession type identifier.
        /// </summary>
        /// <value>
        /// The concession type identifier.
        /// </value>
        public int ConcessionTypeId { get; set; }

        /// <summary>
        /// Gets or sets the type of the concession.
        /// </summary>
        /// <value>
        /// The type of the concession.
        /// </value>
        public string ConcessionType { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the transaction table numbers.
        /// </summary>
        /// <value>
        /// The transaction table numbers.
        /// </value>
        public IEnumerable<TransactionTableNumber> TransactionTableNumbers { get; set; }
    }
}
