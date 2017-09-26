using System;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// ConcessionMas entity
    /// </summary>
    public class ConcessionMas
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
        /// Gets or sets the TransactionTypeId.
        /// </summary>
        /// <value>
        /// The TransactionTypeId.
        /// </value>
        public int TransactionTypeId { get; set; }

        /// <summary>
        /// Gets or sets the MerchantNumber.
        /// </summary>
        /// <value>
        /// The MerchantNumber.
        /// </value>
        public string MerchantNumber { get; set; }

        /// <summary>
        /// Gets or sets the Turnover.
        /// </summary>
        /// <value>
        /// The Turnover.
        /// </value>
        public decimal? Turnover { get; set; }

        /// <summary>
        /// Gets or sets the CommissionRate.
        /// </summary>
        /// <value>
        /// The CommissionRate.
        /// </value>
        public decimal? CommissionRate { get; set; }
    }
}
