namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// Auditable interface
    /// </summary>
    public interface IAuditable
    {
        /// <summary>
        /// Gets or the name of the table.
        /// </summary>
        /// <value>
        /// The name of the table.
        /// </value>
        string TableName { get; }

        /// <summary>
        /// Gets the name of the primary key column.
        /// </summary>
        /// <value>
        /// The name of the primary key column.
        /// </value>
        string PrimaryKeyColumnName { get; }

        /// <summary>
        /// Gets the primary key value.
        /// </summary>
        /// <value>
        /// The primary key value.
        /// </value>
        object PrimaryKeyValue { get; }
    }
}
