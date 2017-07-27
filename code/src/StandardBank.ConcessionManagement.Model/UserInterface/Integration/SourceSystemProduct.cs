namespace StandardBank.ConcessionManagement.Model.UserInterface.Integration
{
    /// <summary>
    /// Source system product entity
    /// </summary>
    public class SourceSystemProduct : BaseSourceSystem
    {
        /// <summary>
        /// Gets or sets the product name
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Gets or sets the risk group number
        /// </summary>
        public int RiskGroupNumber { get; set; }

        /// <summary>
        /// Gets or sets the customer name
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Gets or sets the account number
        /// </summary>
        public int AccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the limit
        /// </summary>
        public decimal Limit { get; set; }

        /// <summary>
        /// Gets or sets the average balance
        /// </summary>
        public decimal AverageBalance { get; set; }

        /// <summary>
        /// Gets or sets the loaded MAP
        /// </summary>
        public decimal LoadedMap { get; set; }
    }
}
