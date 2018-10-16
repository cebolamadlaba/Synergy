namespace StandardBank.ConcessionManagement.Model.UserInterface.Lending
{
    /// <summary>
    /// Lending concession detail
    /// </summary>
    public class LendingConcessionDetail : BaseConcessionDetail
    {
        /// <summary>
        /// Gets or sets the lending concession detail identifier.
        /// </summary>
        /// <value>
        /// The lending concession detail identifier.
        /// </value>
        public int LendingConcessionDetailId { get; set; }

        /// <summary>
        /// Gets or sets the type of the product.
        /// </summary>
        /// <value>
        /// The type of the product.
        /// </value>
        public string ProductType { get; set; }

        /// <summary>
        /// Gets or sets the product type identifier.
        /// </summary>
        /// <value>
        /// The product type identifier.
        /// </value>
        public int? ProductTypeId { get; set; }

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
        public decimal? LoadedMap { get; set; }

        /// <summary>
        /// Gets or sets the approved map.
        /// </summary>
        /// <value>
        /// The approved map.
        /// </value>
        public decimal? ApprovedMap { get; set; }

        /// <summary>
        /// Gets or sets the margin against prime
        /// </summary>
        public decimal MarginAgainstPrime { get; set; }

        /// <summary>
        /// Gets or sets the intiation fee
        /// </summary>
        public decimal InitiationFee { get; set; }

        /// <summary>
        /// Gets or sets the review fee type
        /// </summary>
        public string ReviewFeeType { get; set; }

        /// <summary>
        /// Gets or sets the review fee type id
        /// </summary>
        public int? ReviewFeeTypeId { get; set; }

        /// <summary>
        /// Gets or sets the review fee
        /// </summary>
        public decimal ReviewFee { get; set; }

        /// <summary>
        /// Gets or sets the uff fee
        /// </summary>
        public decimal UffFee { get; set; }

        public decimal ServiceFee { get; set; }

        public string Frequency { get; set; }

      
    }
}
