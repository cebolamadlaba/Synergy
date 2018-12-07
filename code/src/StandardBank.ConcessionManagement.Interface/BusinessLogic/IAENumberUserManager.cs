using StandardBank.ConcessionManagement.Model.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace StandardBank.ConcessionManagement.Interface.BusinessLogic
{
    public interface IAENumberUserManager
    {
        AENumberUser GetAENumberUser(int AENumberUserId);

        AENumberUser GetAENumberUserByAccountExecutiveUserId(int accountExecutiveUserId);

        int? GetCurrentAccountExecutiveUserId(int? aeNumberUserId);

        int?[] GetAccountAssistantIds(int? aeNumberUserId);

        User GetAccountExecutiveUser(int accountExecutiveUserId);
    }
}
