using System.Collections.Generic;

namespace StandardBank.ConcessionManagement.Model.UserInterface
{
    /// <summary>
    /// Condition type entity
    /// </summary>
    public class ConditionType
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the condition products
        /// </summary>
        public IEnumerable<ConditionProduct> ConditionProducts { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [enable interest rate].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [enable interest rate]; otherwise, <c>false</c>.
        /// </value>
        public bool EnableInterestRate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [enable condition volume].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [enable condition volume]; otherwise, <c>false</c>.
        /// </value>
        public bool EnableConditionVolume { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [enable condition value].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [enable condition value]; otherwise, <c>false</c>.
        /// </value>
        public bool EnableConditionValue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [enable expected turnover value].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [enable expected turnover value]; otherwise, <c>false</c>.
        /// </value>
        public bool EnableExpectedTurnoverValue { get; set; }
    }
}
