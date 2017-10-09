using MediatR;
using StandardBank.ConcessionManagement.Model.BusinessLogic;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.Concession
{
    /// <summary>
    /// Concession added feature
    /// </summary>
    /// <seealso cref="MediatR.INotification" />
    public class ConcessionAdded : INotification
    {
        /// <summary>
        /// Gets or sets the consession identifier.
        /// </summary>
        /// <value>
        /// The consession identifier.
        /// </value>
        public string ConsessionId { get; set; }

        /// <summary>
        /// Gets or sets the center identifier.
        /// </summary>
        /// <value>
        /// The center identifier.
        /// </value>
        public int CenterId { get; set; }

        /// <summary>
        /// Gets or sets the approval step.
        /// </summary>
        /// <value>
        /// The approval step.
        /// </value>
        public Constants.ApprovalStep ApprovalStep { get; set; }
    }
}
