namespace StandardBank.ConcessionManagement.Model.UserInterface.Trade
{
    /// <summary>
    /// Cash financial entity
    /// </summary>
    public class TradeFinancial
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }      

        public int RiskGroupId { get; set; }

        public decimal TotalAccounts { get; set; }

        public decimal AvgFee { get; set; }

        public decimal OverallForexMargin { get; set; }
    }
}
