namespace RepoGenerator.Model
{
    /// <summary>
    /// CSV record entity
    /// </summary>
    public class CsvRecord
    {
        /// <summary>
        /// Gets or sets the table schema.
        /// </summary>
        /// <value>
        /// The table schema.
        /// </value>
        public string TABLE_SCHEMA { get; set; }

        /// <summary>
        /// Gets or sets the name of the table.
        /// </summary>
        /// <value>
        /// The name of the table.
        /// </value>
        public string TABLE_NAME { get; set; }

        /// <summary>
        /// Gets or sets the name of the column.
        /// </summary>
        /// <value>
        /// The name of the column.
        /// </value>
        public string COLUMN_NAME { get; set; }

        /// <summary>
        /// Gets or sets the is nullable.
        /// </summary>
        /// <value>
        /// The is nullable.
        /// </value>
        public string IS_NULLABLE { get; set; }

        /// <summary>
        /// Gets or sets the type of the data.
        /// </summary>
        /// <value>
        /// The type of the data.
        /// </value>
        public string DATA_TYPE { get; set; }
    }
}
