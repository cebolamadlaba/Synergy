using MediatR;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.Administration
{
    /// <summary>
    /// Creates the business centre management model handler
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler{CreateBusinessCentreManagementModel}" />
    public class CreateBusinessCentreManagementModelHandler : IRequestHandler<CreateBusinessCentreManagementModel>
    {
        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Handle(CreateBusinessCentreManagementModel message)
        {
            //throw new NotImplementedException();
        }
    }
}
