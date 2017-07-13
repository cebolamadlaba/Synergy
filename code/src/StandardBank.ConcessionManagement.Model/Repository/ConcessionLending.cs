using System;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// ConcessionLending entity
    /// </summary>
    public class ConcessionLending
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
        /// Gets or sets the ProductTypeId.
        /// </summary>
        /// <value>
        /// The ProductTypeId.
        /// </value>
        public int ProductTypeId { get; set; }

        /// <summary>
        /// Gets or sets the Limit.
        /// </summary>
        /// <value>
        /// The Limit.
        /// </value>
        public decimal? Limit { get; set; }

        /// <summary>
        /// Gets or sets the Term.
        /// </summary>
        /// <value>
        /// The Term.
        /// </value>
        public int? Term { get; set; }

        /// <summary>
        /// Gets or sets the MarginToPrime.
        /// </summary>
        /// <value>
        /// The MarginToPrime.
        /// </value>
        public decimal? MarginToPrime { get; set; }

        /// <summary>
        /// Gets or sets the InitiationFee.
        /// </summary>
        /// <value>
        /// The InitiationFee.
        /// </value>
        public decimal? InitiationFee { get; set; }

        /// <summary>
        /// Gets or sets the ReviewFee.
        /// </summary>
        /// <value>
        /// The ReviewFee.
        /// </value>
        public decimal? ReviewFee { get; set; }

        /// <summary>
        /// Gets or sets the UFFFee.
        /// </summary>
        /// <value>
        /// The UFFFee.
        /// </value>
        public decimal? UFFFee { get; set; }

        /// <summary>
        /// Gets or sets the ReviewFeeTypeId.
        /// </summary>
        /// <value>
        /// The ReviewFeeTypeId.
        /// </value>
        public int? ReviewFeeTypeId { get; set; }
    }
}
