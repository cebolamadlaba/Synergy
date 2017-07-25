namespace StandardBank.ConcessionManagement.Model.UserInterface.Pricing
{
    /// <summary>
    /// Legal entity
    /// </summary>
    public class LegalEntity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the risk group identifier.
        /// </summary>
        /// <value>
        /// The risk group identifier.
        /// </value>
        public int RiskGroupId { get; set; }

        /// <summary>
        /// Gets or sets the risk group number.
        /// </summary>
        /// <value>
        /// The risk group number.
        /// </value>
        public int RiskGroupNumber { get; set; }

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
        /// Gets or sets the customer number.
        /// </summary>
        /// <value>
        /// The customer number.
        /// </value>
        public string CustomerNumber { get; set; }

        /// <summary>
        /// Gets or sets the market segment identifier.
        /// </summary>
        /// <value>
        /// The market segment identifier.
        /// </value>
        public int MarketSegmentId { get; set; }

        /// <summary>
        /// Gets or sets the market segment description.
        /// </summary>
        /// <value>
        /// The market segment description.
        /// </value>
        public string MarketSegmentDescription { get; set; }
    }
}
