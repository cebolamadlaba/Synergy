using System.Threading.Tasks;
using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.Concession
{
    /// <summary>
    /// Add concession comment command handler
    /// </summary>
    /// <seealso cref="IAsyncRequestHandler{AddConcessionCommentCommand, ConcessionComment}" />
    public class AddConcessionCommentHandler : IAsyncRequestHandler<AddConcessionComment, ConcessionComment>
    {
        /// <summary>
        /// The concession manager
        /// </summary>
        private readonly IConcessionManager _concessionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddConcessionCommentHandler"/> class.
        /// </summary>
        /// <param name="concessionManager">The concession manager.</param>
        public AddConcessionCommentHandler(IConcessionManager concessionManager)
        {
            _concessionManager = concessionManager;
        }

        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public async Task<ConcessionComment> Handle(AddConcessionComment message)
        {
            var result = _concessionManager.CreateConcessionComment(message.ConcessionComment);

            message.AuditRecord = new AuditRecord(result, message.User, AuditType.Insert);

            return message.ConcessionComment;
        }
    }
}
