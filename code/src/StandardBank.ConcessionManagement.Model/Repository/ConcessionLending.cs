using System.Collections.Generic;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// ConcessionLending entity
    /// </summary>
    public class ConcessionLending : ConcessionDetail, IAuditable
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        /// <value>
        /// The Id.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ProductTypeId.
        /// </summary>
        /// <value>
        /// The ProductTypeId.
        /// </value>
        public int ProductTypeId { get; set; }

        /// <summary>
        /// Gets or sets the ReviewFeeTypeId.
        /// </summary>
        /// <value>
        /// The ReviewFeeTypeId.
        /// </value>
        public int? ReviewFeeTypeId { get; set; }

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
        /// Gets or sets the ApprovedMarginToPrime.
        /// </summary>
        /// <value>
        /// The ApprovedMarginToPrime.
        /// </value>
        public decimal? ApprovedMarginToPrime { get; set; }

        /// <summary>
        /// Gets or sets the LoadedMarginToPrime.
        /// </summary>
        /// <value>
        /// The LoadedMarginToPrime.
        /// </value>
        public decimal? LoadedMarginToPrime { get; set; }

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
        /// Gets or sets the AverageBalance.
        /// </summary>
        /// <value>
        /// The AverageBalance.
        /// </value>
        public decimal? AverageBalance { get; set; }

        public string Frequency { get; set; }

        public decimal? ServiceFee { get; set; }

        public int MRS_ERI { get; set; }

        public IEnumerable<ConcessionLendingTieredRate> ConcessionLendingTieredRates { get; set; }

        /// <summary>
        /// Gets or the name of the table.
        /// </summary>
        /// <value>
        /// The name of the table.
        /// </value>
        public string TableName => "tblConcessionLending";

        /// <summary>
        /// Gets the name of the primary key column.
        /// </summary>
        /// <value>
        /// The name of the primary key column.
        /// </value>
        public string PrimaryKeyColumnName => "pkConcessionLendingId";

        /// <summary>
        /// Gets the primary key value.
        /// </summary>
        /// <value>
        /// The primary key value.
        /// </value>
        public object PrimaryKeyValue => Id;

    }
}
