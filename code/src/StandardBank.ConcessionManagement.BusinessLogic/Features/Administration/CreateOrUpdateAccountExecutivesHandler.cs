using System.Collections.Generic;
using System.Linq;
using MediatR;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using User = StandardBank.ConcessionManagement.Model.UserInterface.User;

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
                DeleteRecords(message, accountAssistants, auditRecords);

            //if there are records, insert the ones that are not in the database already
            if (message.AccountExecutive.AccountAssistants != null && message.AccountExecutive.AccountAssistants.Any())
                foreach (var accountAssistant in message.AccountExecutive.AccountAssistants)
                    AddRecord(message, accountAssistants, accountAssistant, auditRecords);

            message.AuditRecords = auditRecords;
        }

        /// <summary>
        /// Deletes the records.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="accountAssistants">The account assistants.</param>
        /// <param name="auditRecords">The audit records.</param>
        private void DeleteRecords(CreateOrUpdateAccountExecutives message, IEnumerable<AccountExecutiveAssistant> accountAssistants, List<AuditRecord> auditRecords)
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
            else
            {
                //if there are accounts in the database that are not on the model they must be deleted
                foreach (var accountAssistant in accountAssistants)
                {
                    if (!message.AccountExecutive.AccountAssistants.Any(_ =>
                        _.Id == accountAssistant.AccountAssistantUserId))
                    {
                        _accountExecutiveAssistantRepository.Delete(accountAssistant);
                        auditRecords.Add(new AuditRecord(accountAssistant, message.CurrentUser, AuditType.Delete));
                    }
                }
            }
        }

        /// <summary>
        /// Adds the record.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="accountAssistants">The account assistants.</param>
        /// <param name="accountAssistant">The account assistant.</param>
        /// <param name="auditRecords">The audit records.</param>
        private void AddRecord(CreateOrUpdateAccountExecutives message, IEnumerable<AccountExecutiveAssistant> accountAssistants, User accountAssistant,
            List<AuditRecord> auditRecords)
        {
            var addRecord = accountAssistants == null || !accountAssistants.Any();

            if (accountAssistants != null &&
                !accountAssistants.Any(_ => _.AccountAssistantUserId == accountAssistant.Id))
            {
                addRecord = true;
            }

            if (addRecord)
            {
                RemoveExistingLinkIfPresent(message, accountAssistant, auditRecords);

                var result = _accountExecutiveAssistantRepository.Create(new AccountExecutiveAssistant
                {
                    AccountAssistantUserId = accountAssistant.Id,
                    AccountExecutiveUserId = message.AccountExecutive.User.Id,
                    IsActive = true
                });

                auditRecords.Add(new AuditRecord(result, message.CurrentUser, AuditType.Insert));
            }
        }

        /// <summary>
        /// Removes the existing link if present.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="accountAssistant">The account assistant.</param>
        /// <param name="auditRecords">The audit records.</param>
        private void RemoveExistingLinkIfPresent(CreateOrUpdateAccountExecutives message, User accountAssistant,
            List<AuditRecord> auditRecords)
        {
            var recordToRemove =
                _accountExecutiveAssistantRepository.ReadByAccountAssistantUserId(accountAssistant.Id);

            if (recordToRemove != null)
            {
                _accountExecutiveAssistantRepository.Delete(recordToRemove);
                auditRecords.Add(new AuditRecord(recordToRemove, message.CurrentUser, AuditType.Delete));
            }
        }
    }
}
