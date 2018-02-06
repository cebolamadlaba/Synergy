using System.Collections.Generic;
using System.Linq;
using MediatR;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.Administration
{
    /// <summary>
    /// Creates or updates the account executive
    /// </summary>
    /// <seealso cref="CreateOrUpdateAccountExecutives" />
    public class CreateOrUpdateAccountExecutivesHandler : IRequestHandler<CreateOrUpdateAccountExecutives>
    {
        private readonly IAccountExecutiveAssistantRepository _accountExecutiveAssistantRepository;

        public CreateOrUpdateAccountExecutivesHandler(IAccountExecutiveAssistantRepository accountExecutiveAssistantRepository)
        {
            _accountExecutiveAssistantRepository = accountExecutiveAssistantRepository;
        }

        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Handle(CreateOrUpdateAccountExecutives message)
        {
            var auditRecords = new List<AuditRecord>();

            var accountAssistants =
                _accountExecutiveAssistantRepository.ReadByAccountExecutiveUserId(message.AccountExecutive.User.Id);

            if (accountAssistants != null && accountAssistants.Any())
            {
                //if there account assistants in the database but not on the model passed in they must be removed
                if (message.AccountExecutive.AccountAssistants == null ||
                    !message.AccountExecutive.AccountAssistants.Any())
                {
                    foreach (var accountAssistant in accountAssistants)
                    {
                        _accountExecutiveAssistantRepository.Delete(accountAssistant);
                        auditRecords.Add(new AuditRecord(accountAssistant, message.CurrentUser, AuditType.Delete));
                    }
                }
            }

            //if there are records, insert the ones that are not there
            if (message.AccountExecutive.AccountAssistants != null && message.AccountExecutive.AccountAssistants.Any())
            {
                foreach (var accountAssistant in message.AccountExecutive.AccountAssistants)
                {
                    
                }
            }

            message.AuditRecords = auditRecords;
        }
    }
}
