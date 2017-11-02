using System.Collections.Generic;

namespace StandardBank.ConcessionManagement.Interface.Common
{
    /// <summary>
    /// File utility interface
    /// </summary>
    public interface IFileUtiltity
    {
        /// <summary>
        /// Reads the file bytes.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="throwExceptionIfNotFound">if set to <c>true</c> [throw exception if not found].</param>
        /// <returns></returns>
        byte[] ReadFileBytes(string fileName, bool throwExceptionIfNotFound = false);

        /// <summary>
        /// Reads the file text.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="throwExceptionIfNotFound">if set to <c>true</c> [throw exception if not found].</param>
        /// <returns></returns>
        string ReadFileText(string fileName, bool throwExceptionIfNotFound = false);

        /// <summary>
        /// Gets the files in directory.
        /// </summary>
        /// <param name="directoryName">Name of the directory.</param>
        /// <param name="throwExceptionIfNotFound">if set to <c>true</c> [throw exception if not found].</param>
        /// <returns></returns>
        IEnumerable<string> GetFilesInDirectory(string directoryName, bool throwExceptionIfNotFound = false);

        /// <summary>
        /// Reads the file lines.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="throwExceptionIfNotFound">if set to <c>true</c> [throw exception if not found].</param>
        /// <returns></returns>
        IEnumerable<string> ReadFileLines(string fileName, bool throwExceptionIfNotFound = false);

        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="throwExceptionIfNotFound">if set to <c>true</c> [throw exception if not found].</param>
        void DeleteFile(string fileName, bool throwExceptionIfNotFound = false);

        /// <summary>
        /// Writes the file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="fileContents">The file contents.</param>
        /// <param name="deleteIfExists">if set to <c>true</c> [delete if exists].</param>
        void WriteFile(string fileName, string fileContents, bool deleteIfExists);
    }
}
