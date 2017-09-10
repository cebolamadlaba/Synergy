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
    }
}
