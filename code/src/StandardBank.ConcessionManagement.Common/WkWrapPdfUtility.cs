using System.IO;
using StandardBank.ConcessionManagement.Interface.Common;
using WkWrap.Core;

namespace StandardBank.ConcessionManagement.Common
{
    /// <summary>
    /// Wk Wrap PDF utility
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.Common.IPdfUtility" />
    public class WkWrapPdfUtility : IPdfUtility
    {
        /// <summary>
        /// The configuration data
        /// </summary>
        private readonly IConfigurationData _configurationData;

        /// <summary>
        /// Initializes a new instance of the <see cref="WkWrapPdfUtility"/> class.
        /// </summary>
        /// <param name="configurationData">The configuration data.</param>
        public WkWrapPdfUtility(IConfigurationData configurationData)
        {
            _configurationData = configurationData;
        }

        /// <summary>
        /// Generates the PDF from HTML.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <returns></returns>
        public byte[] GeneratePdfFromHtml(string html)
        {
            var wkhtmltopdf = new FileInfo(_configurationData.WKhtmlToPDFExecutable);
            var converter = new HtmlToPdfConverter(wkhtmltopdf);
            return converter.ConvertToPdf(html);
        }
    }
}
