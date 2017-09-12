using FluentEmail.Razor;
using StandardBank.ConcessionManagement.Interface.Common;

namespace StandardBank.ConcessionManagement.Common
{
    /// <summary>
    /// Fluent razor rendered
    /// </summary>
    public class FluentRazorRenderer : IRazorRenderer
    {
        /// <summary>
        /// Parses the specified template.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="template">The template.</param>
        /// <param name="model">The model.</param>
        /// <param name="isHtml">if set to <c>true</c> [is HTML].</param>
        /// <returns></returns>
        public string Parse<T>(string template, T model, bool isHtml = true)
        {
            var razorRenderer = new RazorRenderer();
            return razorRenderer.Parse(template, model, isHtml);
        }
    }
}
