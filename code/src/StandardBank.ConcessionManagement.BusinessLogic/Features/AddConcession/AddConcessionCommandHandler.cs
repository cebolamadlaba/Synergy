using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.AddConcession
{
    /// <summary>
    /// Add concession command handler
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler{AddConcessionCommand, Concession}" />
    public class AddConcessionCommandHandler : IRequestHandler<AddConcessionCommand, Concession>
    {
        /// <summary>
        /// The concession manager
        /// </summary>
        private readonly IConcessionManager _concessionManager;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="AddConcessionCommandHandler"/> class.
        /// </summary>
        /// <param name="concessionManager"></param>
        public AddConcessionCommandHandler(IConcessionManager concessionManager)
        {
            _concessionManager = concessionManager;
        }

        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public Concession Handle(AddConcessionCommand message)
        {
            var result = _concessionManager.CreateConcession(message.Concession, message.User);

            message.RepositoryConcession = result;

            message.Concession.ReferenceNumber = result.ConcessionRef;
            message.Concession.Id = result.Id;

            return message.Concession;
        }
    }
}
