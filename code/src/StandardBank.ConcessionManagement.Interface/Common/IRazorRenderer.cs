namespace StandardBank.ConcessionManagement.Interface.Common
{
    /// <summary>
    /// Razor renderer interface
    /// </summary>
    public interface IRazorRenderer
    {
        /// <summary>
        /// Parses the specified template.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="template">The template.</param>
        /// <param name="model">The model.</param>
        /// <param name="isHtml">if set to <c>true</c> [is HTML].</param>
        /// <returns></returns>
        string Parse<T>(string template, T model, bool isHtml = true);
    }
}
