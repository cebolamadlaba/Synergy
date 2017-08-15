namespace StandardBank.ConcessionManagement.Model.UserInterface.Lending
{
    /// <summary>
    /// Lending concession detail
    /// </summary>
    public class LendingConcessionDetail
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the type of the product.
        /// </summary>
        /// <value>
        /// The type of the product.
        /// </value>
        public string ProductType { get; set; }

        /// <summary>
        /// Gets or sets the product type id
        /// </summary>
        public int? ProductTypeId { get; set; }

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
        /// Gets or sets the legal entity id
        /// </summary>
        public int? LegalEntityId { get; set; }

        /// <summary>
        /// Gets or sets the legal entity account id
        /// </summary>
        public int? LegalEntityAccountId { get; set; }

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
        public decimal LoadedMap { get; set; }

        /// <summary>
        /// Gets or sets the approved map.
        /// </summary>
        /// <value>
        /// The approved map.
        /// </value>
        public decimal ApprovedMap { get; set; }

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
    }
}
