using System;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// ProductInvestment entity
    /// </summary>
    public class ProductInvestment
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        /// <value>
        /// The Id.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the RiskGroupId.
        /// </summary>
        /// <value>
        /// The RiskGroupId.
        /// </value>
        public int RiskGroupId { get; set; }

        /// <summary>
        /// Gets or sets the LegalEntityId.
        /// </summary>
        /// <value>
        /// The LegalEntityId.
        /// </value>
        public int LegalEntityId { get; set; }

        /// <summary>
        /// Gets or sets the LegalEntityAccountId.
        /// </summary>
        /// <value>
        /// The LegalEntityAccountId.
        /// </value>
        public int LegalEntityAccountId { get; set; }

        /// <summary>
        /// Gets or sets the ProductId.
        /// </summary>
        /// <value>
        /// The ProductId.
        /// </value>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the AverageBalance.
        /// </summary>
        /// <value>
        /// The AverageBalance.
        /// </value>
        public decimal AverageBalance { get; set; }

        /// <summary>
        /// Gets or sets the LoadedCustomerRate.
        /// </summary>
        /// <value>
        /// The LoadedCustomerRate.
        /// </value>
        public decimal LoadedCustomerRate { get; set; }
    }
}
