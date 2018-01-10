namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// AccountExecutiveAssistant entity
    /// </summary>
    public class AccountExecutiveAssistant : IAuditable
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        /// <value>
        /// The Id.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the AccountAssistantUserId.
        /// </summary>
        /// <value>
        /// The AccountAssistantUserId.
        /// </value>
        public int AccountAssistantUserId { get; set; }

        /// <summary>
        /// Gets or sets the AccountExecutiveUserId.
        /// </summary>
        /// <value>
        /// The AccountExecutiveUserId.
        /// </value>
        public int AccountExecutiveUserId { get; set; }

        /// <summary>
        /// Gets or sets the IsActive.
        /// </summary>
        /// <value>
        /// The IsActive.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or the name of the table.
        /// </summary>
        /// <value>
        /// The name of the table.
        /// </value>
        public string TableName => "tblAccountExecutiveAssistant";

        /// <summary>
        /// Gets the name of the primary key column.
        /// </summary>
        /// <value>
        /// The name of the primary key column.
        /// </value>
        public string PrimaryKeyColumnName => "pkAccountExecutiveAssistantId";

        /// <summary>
        /// Gets the primary key value.
        /// </summary>
        /// <value>
        /// The primary key value.
        /// </value>
        public object PrimaryKeyValue => Id;
    }
}
