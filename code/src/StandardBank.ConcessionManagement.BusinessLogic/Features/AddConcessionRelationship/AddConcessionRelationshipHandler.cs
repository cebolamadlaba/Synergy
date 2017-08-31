using System.Threading.Tasks;
using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using ConcessionRelationship = StandardBank.ConcessionManagement.Model.UserInterface.ConcessionRelationship;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.AddConcessionRelationship
{
    /// <summary>
    /// Add concession relationship handler
    /// </summary>
    /// <seealso cref="MediatR.IAsyncRequestHandler{AddConcessionRelationship, ConcessionRelationship}" />
    public class AddConcessionRelationshipHandler : IAsyncRequestHandler<AddConcessionRelationship, ConcessionRelationship>
    {
        /// <summary>
        /// The concession manager
        /// </summary>
        private readonly IConcessionManager _concessionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddConcessionRelationshipHandler"/> class.
        /// </summary>
        /// <param name="concessionManager">The concession manager.</param>
        public AddConcessionRelationshipHandler(IConcessionManager concessionManager)
        {
            _concessionManager = concessionManager;
        }

        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public async Task<ConcessionRelationship> Handle(AddConcessionRelationship message)
        {
            var result = _concessionManager.CreateConcessionRelationship(message.ConcessionRelationship);

            message.AuditRecord = new AuditRecord(result, message.User, AuditType.Insert);

            return message.ConcessionRelationship;
        }
    }
}
