namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// ProductCash entity
    /// </summary>
    public class ProductCash
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
        /// Gets or sets the TableNumberId.
        /// </summary>
        /// <value>
        /// The TableNumberId.
        /// </value>
        public int TableNumberId { get; set; }

        /// <summary>
        /// Gets or sets the Channel.
        /// </summary>
        /// <value>
        /// The Channel.
        /// </value>
        public string Channel { get; set; }

        /// <summary>
        /// Gets or sets the BpId.
        /// </summary>
        /// <value>
        /// The BpId.
        /// </value>
        public int BpId { get; set; }

        /// <summary>
        /// Gets or sets the Volume.
        /// </summary>
        /// <value>
        /// The Volume.
        /// </value>
        public decimal Volume { get; set; }

        /// <summary>
        /// Gets or sets the Value.
        /// </summary>
        /// <value>
        /// The Value.
        /// </value>
        public decimal Value { get; set; }

        /// <summary>
        /// Gets or sets the LoadedPrice.
        /// </summary>
        /// <value>
        /// The LoadedPrice.
        /// </value>
        public decimal LoadedPrice { get; set; }
    }
}
