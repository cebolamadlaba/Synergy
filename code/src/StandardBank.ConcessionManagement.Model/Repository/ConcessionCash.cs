namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// ConcessionCash entity
    /// </summary>
    public class ConcessionCash : IAuditable
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
        /// Gets or sets the ChannelTypeId.
        /// </summary>
        /// <value>
        /// The ChannelTypeId.
        /// </value>
        public int ChannelTypeId { get; set; }

        /// <summary>
        /// Gets or sets the CashVolume.
        /// </summary>
        /// <value>
        /// The CashVolume.
        /// </value>
        public int? CashVolume { get; set; }

        /// <summary>
        /// Gets or sets the CashValue.
        /// </summary>
        /// <value>
        /// The CashValue.
        /// </value>
        public decimal? CashValue { get; set; }

        /// <summary>
        /// Gets or sets the BaseRateId.
        /// </summary>
        /// <value>
        /// The BaseRateId.
        /// </value>
        public int? BaseRateId { get; set; }

        /// <summary>
        /// Gets or sets the AdValorem.
        /// </summary>
        /// <value>
        /// The AdValorem.
        /// </value>
        public decimal? AdValorem { get; set; }

        /// <summary>
        /// Gets or sets the legal entity identifier.
        /// </summary>
        /// <value>
        /// The legal entity identifier.
        /// </value>
        public int LegalEntityId { get; set; }

        /// <summary>
        /// Gets or sets the legal entity account identifier.
        /// </summary>
        /// <value>
        /// The legal entity account identifier.
        /// </value>
        public int LegalEntityAccountId { get; set; }

        /// <summary>
        /// Gets or sets the base rate.
        /// </summary>
        /// <value>
        /// The base rate.
        /// </value>
        public decimal? BaseRate { get; set; }

        /// <summary>
        /// Gets or sets the accrual type identifier.
        /// </summary>
        /// <value>
        /// The accrual type identifier.
        /// </value>
        public int AccrualTypeId { get; set; } 

        /// <summary>
        /// Gets or the name of the table.
        /// </summary>
        /// <value>
        /// The name of the table.
        /// </value>
        public string TableName => "tblConcessionCash";

        /// <summary>
        /// Gets the primary key column name
        /// </summary>
        public string PrimaryKeyColumnName => "pkConcessionCashId";

        /// <summary>
        /// Gets the primary key value
        /// </summary>
        public object PrimaryKeyValue => Id;

        /// <summary>
        /// Gets or sets the table number identifier.
        /// </summary>
        /// <value>
        /// The table number identifier.
        /// </value>
        public int TableNumberId { get; set; }

        /// <summary>
        /// Gets or sets the approved table number identifier.
        /// </summary>
        /// <value>
        /// The approved table number identifier.
        /// </value>
        public int? ApprovedTableNumberId { get; set; }

        /// <summary>
        /// Gets or sets the loaded table number identifier.
        /// </summary>
        /// <value>
        /// The loaded table number identifier.
        /// </value>
        public int? LoadedTableNumberId { get; set; }
    }
}
