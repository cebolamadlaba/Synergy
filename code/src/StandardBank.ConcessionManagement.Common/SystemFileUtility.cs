using System.IO;
using StandardBank.ConcessionManagement.Interface.Common;

namespace StandardBank.ConcessionManagement.Common
{
    /// <summary>
    /// System file utility
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Common.IFileUtiltity" />
    public class SystemFileUtility : IFileUtiltity
    {
        /// <summary>
        /// Reads the file bytes.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="throwExceptionIfNotFound">if set to <c>true</c> [throw exception if not found].</param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public byte[] ReadFileBytes(string fileName, bool throwExceptionIfNotFound = false)
        {
            if (File.Exists(fileName))
                return File.ReadAllBytes(fileName);

            if (throwExceptionIfNotFound)
                throw new FileNotFoundException(fileName);

            return null;
        }

        /// <summary>
        /// Reads the file text.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="throwExceptionIfNotFound">if set to <c>true</c> [throw exception if not found].</param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public string ReadFileText(string fileName, bool throwExceptionIfNotFound = false)
        {
            if (File.Exists(fileName))
                return File.ReadAllText(fileName);

            if (throwExceptionIfNotFound)
                throw new FileNotFoundException(fileName);

            return null;
        }
    }
}
