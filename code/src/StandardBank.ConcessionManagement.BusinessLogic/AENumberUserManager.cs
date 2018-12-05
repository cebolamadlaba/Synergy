using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StandardBank.ConcessionManagement.BusinessLogic
{
    public class AENumberUserManager : IAENumberUserManager
    {
        private readonly IAENumberUserRepository _aeNumberUserRepository;
        private readonly IAccountExecutiveAssistantRepository _accountExecutiveAssistantRepository;
        private readonly IUserRepository _userRepository;

        public AENumberUserManager(IAENumberUserRepository aeNumberUserRepository, IAccountExecutiveAssistantRepository accountExecutiveAssistantRepository,
            IUserRepository userRepository)
        {
            this._aeNumberUserRepository = aeNumberUserRepository;
            this._accountExecutiveAssistantRepository = accountExecutiveAssistantRepository;
            this._userRepository = userRepository;
        }

        public AENumberUser GetAENumberUser(int AENumberUserId)
        {
            return this._aeNumberUserRepository.ReadById(AENumberUserId);
        }

        public int? GetCurrentAccountExecutiveUserId(int? aeNumberUserId)
        {
            if (aeNumberUserId == null)
                return null;

            AENumberUser aeNumberUser = this.GetAENumberUser(aeNumberUserId.Value);

            if (aeNumberUser == null)
                return null;

            return aeNumberUser.UserId;
        }

        public int?[] GetAccountAssistantIds(int? aeNumberUserId)
        {
            int? accountExecutiveUserId = this.GetCurrentAccountExecutiveUserId(aeNumberUserId);

            if (accountExecutiveUserId == null)
                return null;

            IEnumerable<AccountExecutiveAssistant> accountAssistants = this._accountExecutiveAssistantRepository.ReadByAccountExecutiveUserId(accountExecutiveUserId.Value);

            if (accountAssistants != null && accountAssistants.Count() > 0)
                return accountAssistants.Select(a => (int?)a.AccountAssistantUserId).ToArray();

            return null;
        }

        public User GetAccountExecutiveUser(int accountExecutiveUserId)
        {
            return this._userRepository.ReadById(accountExecutiveUserId);
        }
    }
}
