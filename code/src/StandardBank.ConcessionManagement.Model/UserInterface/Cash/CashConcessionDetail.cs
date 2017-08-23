namespace StandardBank.ConcessionManagement.Model.UserInterface.Cash
{
    /// <summary>
    /// Cash concession detail entity
    /// </summary>
    public class CashConcessionDetail
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

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
        /// Gets or sets the legal entity identifier.
        /// </summary>
        /// <value>
        /// The legal entity identifier.
        /// </value>
        public int? LegalEntityId { get; set; }

        /// <summary>
        /// Gets or sets the legal entity account identifier.
        /// </summary>
        /// <value>
        /// The legal entity account identifier.
        /// </value>
        public int? LegalEntityAccountId { get; set; }

        /// <summary>
        /// Gets or sets the channel.
        /// </summary>
        /// <value>
        /// The channel.
        /// </value>
        public string Channel { get; set; }

        /// <summary>
        /// Gets or sets the channel type identifier.
        /// </summary>
        /// <value>
        /// The channel type identifier.
        /// </value>
        public int? ChannelTypeId { get; set; }

        /// <summary>
        /// Gets or sets the cash table number.
        /// </summary>
        /// <value>
        /// The cash table number.
        /// </value>
        public int? CashTableNumber { get; set; }

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

        /// <summary>
        /// Gets or sets the approved price.
        /// </summary>
        /// <value>
        /// The approved price.
        /// </value>
        public decimal ApprovedPrice { get; set; }

        /// <summary>
        /// Gets or sets the base rate.
        /// </summary>
        /// <value>
        /// The base rate.
        /// </value>
        public decimal BaseRate { get; set; }

        /// <summary>
        /// Gets or sets the ad valorem.
        /// </summary>
        /// <value>
        /// The ad valorem.
        /// </value>
        public decimal AdValorem { get; set; }

        /// <summary>
        /// Gets or sets the accrual type identifier.
        /// </summary>
        /// <value>
        /// The accrual type identifier.
        /// </value>
        public int? AccrualTypeId { get; set; }
    }
}
