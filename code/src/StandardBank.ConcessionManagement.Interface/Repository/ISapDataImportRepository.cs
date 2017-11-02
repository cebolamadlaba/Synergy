using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.Interface.Repository
{
    /// <summary>
    /// SapDataImport repository interface
    /// </summary>
    public interface ISapDataImportRepository
    {
        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        SapDataImport Create(SapDataImport model);

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        SapDataImport ReadById(int id);

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns></returns>
        IEnumerable<SapDataImport> ReadAll();

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        void Update(SapDataImport model);

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        void Delete(SapDataImport model);

        /// <summary>
        /// Updates the prices and mismatches.
        /// </summary>
        void UpdatePricesAndMismatches();

        /// <summary>
        /// Generates the sap export.
        /// </summary>
        /// <returns></returns>
        IEnumerable<SapDataImport> GenerateSapExport();
    }
}