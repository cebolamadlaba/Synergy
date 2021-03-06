using System;

namespace StandardBank.ConcessionManagement.Model.UserInterface
{
    /// <summary>
    /// RoleSubRole entity
    /// </summary>
    public class RoleSubRole
    {
        /// <summary>
        /// Gets or sets the SubRoleId.
        /// </summary>
        /// <value>
        /// The subRoleId.
        /// </value>
        public int SubRoleId { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        /// <value>
        /// The name
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Active.
        /// </summary>
        /// <value>
        /// The Active.
        /// </value>
        public bool Active { get; set; }

        public int? RoleId { get; set; }
    }
}
