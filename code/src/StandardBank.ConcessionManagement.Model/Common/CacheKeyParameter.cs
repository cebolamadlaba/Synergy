using System;

namespace StandardBank.ConcessionManagement.Model.Common
{
    /// <summary>
    /// Cache key parameter
    /// </summary>
    public class CacheKeyParameter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CacheKeyParameter"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public CacheKeyParameter(string name, object value)
        {
            Name = name;
            Value = Convert.ToString(value);
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get; set; }
    }
}
