using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.Interface.Repository
{
    /// <summary>
    /// SapDataImportConfiguration repository interface
    /// </summary>
    public interface ISapDataImportConfigurationRepository
    {
        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        SapDataImportConfiguration Create(SapDataImportConfiguration model);

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        SapDataImportConfiguration ReadById(int id);

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        IEnumerable<SapDataImportConfiguration> ReadAll();

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        void Update(SapDataImportConfiguration model);

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        void Delete(SapDataImportConfiguration model);
    }
}