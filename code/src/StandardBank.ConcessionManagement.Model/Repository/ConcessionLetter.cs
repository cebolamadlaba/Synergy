namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// ConcessionLending entity
    /// </summary>
    public class ConcessionLetter
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        /// <value>
        /// The Id.
        /// </value>
        public int pkConcessionLetter { get; set; }

        /// <summary>
        /// Gets or sets the ProductTypeId.
        /// </summary>
        /// <value>
        /// The ProductTypeId.
        /// </value>
        public int fkConcessionDetailId { get; set; }

        /// <summary>
        /// Gets or sets the ReviewFeeTypeId.
        /// </summary>
        /// <value>
        /// The ReviewFeeTypeId.
        /// </value>
        public string Location { get; set; }


        /// <summary>
        /// Gets or the name of the table.
        /// </summary>
        /// <value>
        /// The name of the table.
        /// </value>
        public string TableName => "tblConcessionLetter";

        /// <summary>
        /// Gets the name of the primary key column.
        /// </summary>
        /// <value>
        /// The name of the primary key column.
        /// </value>
        public string PrimaryKeyColumnName => "pkConcessionLetter";

        /// <summary>
        /// Gets the primary key value.
        /// </summary>
        /// <value>
        /// The primary key value.
        /// </value>
        public object PrimaryKeyValue => pkConcessionLetter;
    }
}
