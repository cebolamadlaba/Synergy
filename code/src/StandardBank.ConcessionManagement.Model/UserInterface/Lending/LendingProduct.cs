using System.Collections.Generic;

namespace StandardBank.ConcessionManagement.Model.UserInterface.Lending
{
    /// <summary>
    /// Lending product entity
    /// </summary>
    public class LendingProduct
    {
        /// <summary>
        /// Gets or sets the lending product identifier.
        /// </summary>
        /// <value>
        /// The lending product identifier.
        /// </value>
        public int LendingProductId { get; set; }

        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        /// <value>
        /// The product.
        /// </value>
        public string Product { get; set; }

        /// <summary>
        /// Gets or sets the name of the risk group.
        /// </summary>
        /// <value>
        /// The name of the risk group.
        /// </value>
        public string RiskGroupName { get; set; }

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
        public string AccountNumber { get; set; }

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
        /// Gets or sets the loaded map.
        /// </summary>
        /// <value>
        /// The loaded map.
        /// </value>
        public decimal LoadedMap { get; set; }
    }

    public class LendingProductGroup
    {
       
        public string RiskGroupName { get; set; }

    
        public string CustomerName { get; set; }

       
        public List<LendingProduct> LendingProducts { get; set; }
    }
}
