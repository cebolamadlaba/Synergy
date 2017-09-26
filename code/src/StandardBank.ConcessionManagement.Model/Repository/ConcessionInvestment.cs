using System;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// ConcessionInvestment entity
    /// </summary>
    public class ConcessionInvestment
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        /// <value>
        /// The Id.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ConcessionId.
        /// </summary>
        /// <value>
        /// The ConcessionId.
        /// </value>
        public int ConcessionId { get; set; }

        /// <summary>
        /// Gets or sets the ConcessionDetailId.
        /// </summary>
        /// <value>
        /// The ConcessionDetailId.
        /// </value>
        public int ConcessionDetailId { get; set; }

        /// <summary>
        /// Gets or sets the ProductTypeId.
        /// </summary>
        /// <value>
        /// The ProductTypeId.
        /// </value>
        public int ProductTypeId { get; set; }

        /// <summary>
        /// Gets or sets the Balance.
        /// </summary>
        /// <value>
        /// The Balance.
        /// </value>
        public decimal Balance { get; set; }

        /// <summary>
        /// Gets or sets the Term.
        /// </summary>
        /// <value>
        /// The Term.
        /// </value>
        public int Term { get; set; }

        /// <summary>
        /// Gets or sets the InterestToCustomer.
        /// </summary>
        /// <value>
        /// The InterestToCustomer.
        /// </value>
        public decimal InterestToCustomer { get; set; }
    }
}
