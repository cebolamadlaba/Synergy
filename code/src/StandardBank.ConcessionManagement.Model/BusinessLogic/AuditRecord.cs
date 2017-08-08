using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.Model.BusinessLogic
{
    /// <summary>
    /// Audit record entity
    /// </summary>
    public class AuditRecord
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuditRecord"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="user">The user.</param>
        /// <param name="auditType">Type of the audit.</param>
        public AuditRecord(IAuditable entity, UserInterface.User user, AuditType auditType)
        {
            Entity = entity;
            User = user;
            AuditType = auditType;
        }

        /// <summary>
        /// Gets or sets the entity.
        /// </summary>
        /// <value>
        /// The entity.
        /// </value>
        public IAuditable Entity { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public UserInterface.User User { get; set; }

        /// <summary>
        /// Gets or sets the type of the audit.
        /// </summary>
        /// <value>
        /// The type of the audit.
        /// </value>
        public AuditType AuditType { get; set; }
    }
}
