using System;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// ExceptionLog entity
    /// </summary>
    public class ExceptionLog
    {
        /// <summary>
        /// Gets or sets the ExceptionLogId.
        /// </summary>
        /// <value>
        /// The ExceptionLogId.
        /// </value>
        public int ExceptionLogId { get; set; }

        /// <summary>
        /// Gets or sets the ExceptionMessage.
        /// </summary>
        /// <value>
        /// The ExceptionMessage.
        /// </value>
        public string ExceptionMessage { get; set; }

        /// <summary>
        /// Gets or sets the ExceptionType.
        /// </summary>
        /// <value>
        /// The ExceptionType.
        /// </value>
        public string ExceptionType { get; set; }

        /// <summary>
        /// Gets or sets the ExceptionSource.
        /// </summary>
        /// <value>
        /// The ExceptionSource.
        /// </value>
        public string ExceptionSource { get; set; }

        /// <summary>
        /// Gets or sets the ExceptionData.
        /// </summary>
        /// <value>
        /// The ExceptionData.
        /// </value>
        public string ExceptionData { get; set; }

        /// <summary>
        /// Gets or sets the Logdate.
        /// </summary>
        /// <value>
        /// The Logdate.
        /// </value>
        public DateTime? Logdate { get; set; }
    }
}
