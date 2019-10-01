using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.Interface.Repository
{
    /// <summary>
    /// MarketSegmentEnablementTeamUserRepository repository interface
    /// </summary>
    public interface IMarketSegmentEnablementTeamUserRepository
    {

        IEnumerable<MarketSegmentEnablementTeamUser> ReadAll();

        IEnumerable<ConcessionTypeMismatchEscalation> GetConcessionTypeMismatchEscalation();

        void Update(ConcessionTypeMismatchEscalation model);
    }
}