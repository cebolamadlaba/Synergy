namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// ConcessionCash entity
    /// </summary>
    public class ConcessionCash : ConcessionDetail, IAuditable
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        /// <value>
        /// The Id.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ChannelTypeId.
        /// </summary>
        /// <value>
        /// The ChannelTypeId.
        /// </value>
        public int ChannelTypeId { get; set; }

        /// <summary>
        /// Gets or sets the AccrualTypeId.
        /// </summary>
        /// <value>
        /// The AccrualTypeId.
        /// </value>
        public int AccrualTypeId { get; set; }

        /// <summary>
        /// Gets or sets the TableNumberId.
        /// </summary>
        /// <value>
        /// The TableNumberId.
        /// </value>
        public int TableNumberId { get; set; }

        /// <summary>
        /// Gets or sets the ApprovedTableNumberId.
        /// </summary>
        /// <value>
        /// The ApprovedTableNumberId.
        /// </value>
        public int? ApprovedTableNumberId { get; set; }

        /// <summary>
        /// Gets or sets the LoadedTableNumberId.
        /// </summary>
        /// <value>
        /// The LoadedTableNumberId.
        /// </value>
        public int? LoadedTableNumberId { get; set; }

        /// <summary>
        /// Gets or sets the AdValorem.
        /// </summary>
        /// <value>
        /// The AdValorem.
        /// </value>
        public decimal? AdValorem { get; set; }

        /// <summary>
        /// Gets or sets the BaseRate.
        /// </summary>
        /// <value>
        /// The BaseRate.
        /// </value>
        public decimal? BaseRate { get; set; }

        /// <summary>
        /// Gets or the name of the table.
        /// </summary>
        /// <value>
        /// The name of the table.
        /// </value>
        public string TableName => "tblConcessionCash";

        /// <summary>
        /// Gets the name of the primary key column.
        /// </summary>
        /// <value>
        /// The name of the primary key column.
        /// </value>
        public string PrimaryKeyColumnName => "pkConcessionCashId";

        /// <summary>
        /// Gets the primary key value.
        /// </summary>
        /// <value>
        /// The primary key value.
        /// </value>
        public object PrimaryKeyValue => Id;
    }
}
