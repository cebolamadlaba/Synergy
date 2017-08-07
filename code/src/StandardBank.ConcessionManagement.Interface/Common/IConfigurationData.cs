﻿using StandardBank.ConcessionManagement.Model.Common;

namespace StandardBank.ConcessionManagement.Interface.Common
{
    /// <summary>
    /// Configuration data interface
    /// </summary>
    public interface IConfigurationData
    {
        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <value>
        /// The connection string.
        /// </value>
        string ConnectionString { get; set; }

        /// <summary>
        /// Gets the database type
        /// </summary>
        DatabaseType DatabaseType  { get; set; }

        /// <summary>
        /// Gets the override logged in user
        /// </summary>
        string OverrideLoggedInUser  { get; set; }
        string SmtpServer  { get; set; }
        int SmtpPort  { get; set; }
        string SmtpServerUserName  { get; set; }
        string SmtpServerPassword  { get; set; }
        string DefaultEmail  { get; set; }
        string TemplatePath  { get; set; }
    }
}
