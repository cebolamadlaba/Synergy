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
        /// Gets the primary key column name
        /// </summary>
        string PrimaryKeyColumnName { get; }

        /// <summary>
        /// Gets the primary key value
        /// </summary>
        object PrimaryKeyValue { get; }
    }
}
