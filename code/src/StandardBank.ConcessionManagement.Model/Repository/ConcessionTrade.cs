namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// ConcessionTrade entity
    /// </summary>
    public class ConcessionTrade : ConcessionDetail, IAuditable
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        /// <value>
        /// The Id.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the TransactionTypeId.
        /// </summary>
        /// <value>
        /// The TransactionTypeId.
        /// </value>
        public int? fkTradeProductId { get; set; }
     
        public int? fkLegalEntityAccountId { get; set; }     

        public int? fkLegalEntityGBBNumber { get; set; }

        public int? fkLegalEntityId { get; set; }
      
        public decimal? LoadedRate { get; set; }

        public decimal? ApprovedRate { get; set; }

        public string GBBNumber { get; set; }

        public decimal? min { get; set; }

        public decimal? max { get; set; }

        public int? term { get; set; }

        public string Communication { get; set; }

        public decimal? FlatFee { get; set; }

        public decimal? EstablishmentFee { get; set; }
       
        public decimal? AdValorem { get; set; }

        public string Currency { get; set; }

        /// <summary>
        /// Gets or the name of the table.
        /// </summary>
        /// <value>
        /// The name of the table.
        /// </value>
        public string TableName => "tblConcessionTrade";

        /// <summary>
        /// Gets the name of the primary key column.
        /// </summary>
        /// <value>
        /// The name of the primary key column.
        /// </value>
        public string PrimaryKeyColumnName => "pkConcessionTradeId";

        /// <summary>
        /// Gets the primary key value.
        /// </summary>
        /// <value>
        /// The primary key value.
        /// </value>
        public object PrimaryKeyValue => Id;
    }
}
