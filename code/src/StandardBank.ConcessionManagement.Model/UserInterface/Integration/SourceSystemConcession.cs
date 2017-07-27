namespace StandardBank.ConcessionManagement.Model.UserInterface.Integration
{
    /// <summary>
    /// Source system concession
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Model.UserInterface.Integration.BaseSourceSystem" />
    public class SourceSystemConcession : BaseSourceSystem
    {
        /// <summary>
        /// Gets or sets the type of the product.
        /// </summary>
        /// <value>
        /// The type of the product.
        /// </value>
        public string ProductType { get; set; }

        /// <summary>
        /// Gets or sets the name of the customer.
        /// </summary>
        /// <value>
        /// The name of the customer.
        /// </value>
        public string CustomerName { get; set; }

        /// <summary>
        /// Gets or sets the account number.
        /// </summary>
        /// <value>
        /// The account number.
        /// </value>
        public int AccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the limit.
        /// </summary>
        /// <value>
        /// The limit.
        /// </value>
        public decimal Limit { get; set; }

        /// <summary>
        /// Gets or sets the average balance.
        /// </summary>
        /// <value>
        /// The average balance.
        /// </value>
        public decimal AverageBalance { get; set; }

        /// <summary>
        /// Gets or sets the term.
        /// </summary>
        /// <value>
        /// The term.
        /// </value>
        public int Term { get; set; }

        /// <summary>
        /// Gets or sets the loaded map.
        /// </summary>
        /// <value>
        /// The loaded map.
        /// </value>
        public decimal LoadedMap { get; set; }

        /// <summary>
        /// Gets or sets the approved map.
        /// </summary>
        /// <value>
        /// The approved map.
        /// </value>
        public decimal ApprovedMap { get; set; }

        /// <summary>
        /// Gets or sets the concession identifier.
        /// </summary>
        /// <value>
        /// The concession identifier.
        /// </value>
        public int ConcessionId { get; set; }
    }
}
