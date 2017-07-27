namespace StandardBank.ConcessionManagement.Model.UserInterface.Integration
{
    /// <summary>
    /// Source system entity base
    /// </summary>
    public abstract class BaseSourceSystem
    {
        /// <summary>
        /// Gets or sets the source system name
        /// </summary>
        public string SourceSystemName { get; set; }

        /// <summary>
        /// Gets or sets the source system identifier
        /// </summary>
        public string SourceSystemIdentifier { get; set; }
    }
}
