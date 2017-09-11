namespace StandardBank.ConcessionManagement.Interface.Common
{
    /// <summary>
    /// Pdf utility
    /// </summary>
    public interface IPdfUtility
    {
        /// <summary>
        /// Generates the PDF from HTML.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <returns></returns>
        byte[] GeneratePdfFromHtml(string html);
    }
}
