namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// ConcessionGlms entity
    /// </summary>
    public class ConcessionGlms : ConcessionDetail, IAuditable
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

        public int? InterestPricingCategoryId { get; set; }

        public int? GlmsGroupId { get; set; }

        public int? InterestTypeId { get; set; }

        public int? SlabTypeId { get; set; }

        /// <summary>
        /// Gets or the name of the table.
        /// </summary>
        /// <value>
        /// The name of the table.
        /// </value>
        public string TableName => "tblConcessionGlms";

        /// <summary>
        /// Gets the name of the primary key column.
        /// </summary>
        /// <value>
        /// The name of the primary key column.
        /// </value>
        public string PrimaryKeyColumnName => "pkConcessionGlmsId";

        /// <summary>
        /// Gets the primary key value.
        /// </summary>
        /// <value>
        /// The primary key value.
        /// </value>
        public object PrimaryKeyValue => Id;
    }
}
