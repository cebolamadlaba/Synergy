using System.Threading.Tasks;
using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.AddConcessionComment
{
    /// <summary>
    /// Add concession comment command handler
    /// </summary>
    /// <seealso cref="IAsyncRequestHandler{AddConcessionCommentCommand, ConcessionComment}" />
    public class AddConcessionCommentCommandHandler : IAsyncRequestHandler<AddConcessionCommentCommand, ConcessionComment>
    {
        /// <summary>
        /// The concession manager
        /// </summary>
        private readonly IConcessionManager _concessionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddConcessionCommentCommandHandler"/> class.
        /// </summary>
        /// <param name="concessionManager">The concession manager.</param>
        public AddConcessionCommentCommandHandler(IConcessionManager concessionManager)
        {
            _concessionManager = concessionManager;
        }

        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public async Task<ConcessionComment> Handle(AddConcessionCommentCommand message)
        {
            var result = _concessionManager.CreateConcessionComment(message.ConcessionComment);

            message.AuditRecord = new AuditRecord(result, message.User, AuditType.Insert);

            return message.ConcessionComment;
        }
    }
}
