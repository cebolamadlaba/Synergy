namespace StandardBank.ConcessionManagement.Model.UserInterface.Cash
{
    /// <summary>
    /// Cash product entity
    /// </summary>
    public class CashProduct
    {
        /// <summary>
        /// Gets or sets the cash product identifier.
        /// </summary>
        /// <value>
        /// The cash product identifier.
        /// </value>
        public int CashProductId { get; set; }

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
        /// Gets or sets the tariff table.
        /// </summary>
        /// <value>
        /// The tariff table.
        /// </value>
        public int TariffTable { get; set; }

        /// <summary>
        /// Gets or sets the channel.
        /// </summary>
        /// <value>
        /// The channel.
        /// </value>
        public string Channel { get; set; }

        /// <summary>
        /// Gets or sets the bp identifier.
        /// </summary>
        /// <value>
        /// The bp identifier.
        /// </value>
        public int BpId { get; set; }

        /// <summary>
        /// Gets or sets the volume.
        /// </summary>
        /// <value>
        /// The volume.
        /// </value>
        public decimal Volume { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public decimal Value { get; set; }

        /// <summary>
        /// Gets or sets the loaded price.
        /// </summary>
        /// <value>
        /// The loaded price.
        /// </value>
        public decimal LoadedPrice { get; set; }
    }
}
