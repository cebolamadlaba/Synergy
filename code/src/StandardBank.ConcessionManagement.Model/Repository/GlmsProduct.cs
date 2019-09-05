namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// TransactionType entity
    /// </summary>
    public class GlmsProduct
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        /// <value>
        /// The Id.
        /// </value>
        public int GlmsProductId { get; set; }

        /// <summary>
        /// Gets or sets the ConcessionTypeId.
        /// </summary>
        /// <value>
        /// The ConcessionTypeId.
        /// </value>
        public int? GlmsProductTypeId { get; set; }

        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        /// <value>
        /// The Description.
        /// </value>
        public string investmentProductName { get; set; }

      
    }
}
