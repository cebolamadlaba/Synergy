using System;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// SapDataImportConfiguration entity
    /// </summary>
    public class SapDataImportConfiguration
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        /// <value>
        /// The Id.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the FileImportLocation.
        /// </summary>
        /// <value>
        /// The FileImportLocation.
        /// </value>
        public string FileImportLocation { get; set; }

        /// <summary>
        /// Gets or sets the FileExportLocation.
        /// </summary>
        /// <value>
        /// The FileExportLocation.
        /// </value>
        public string FileExportLocation { get; set; }

        /// <summary>
        /// Gets or sets the SupportEmailAddress.
        /// </summary>
        /// <value>
        /// The SupportEmailAddress.
        /// </value>
        public string SupportEmailAddress { get; set; }
    }
}
