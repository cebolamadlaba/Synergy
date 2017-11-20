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
        /// Gets or sets the name of the risk group.
        /// </summary>
        /// <value>
        /// The name of the risk group.
        /// </value>
        public string RiskGroupName { get; set; }

        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        /// <value>
        /// The product.
        /// </value>
        public string Product { get; set; }

        /// <summary>
        /// Gets or sets the date of request.
        /// </summary>
        /// <value>
        /// The date of request.
        /// </value>
        public string DateOfRequest { get; set; }

        /// <summary>
        /// Gets or sets the approval step.
        /// </summary>
        /// <value>
        /// The approval step.
        /// </value>
        public Constants.ApprovalStep ApprovalStep { get; set; }
    }
}
