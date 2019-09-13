using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.Interface.Repository
{
    /// <summary>
    /// MarketSegmentEnablementTeamRepository repository interface
    /// </summary>
    public interface IMarketSegmentEnablementTeamRepository
    {

        IEnumerable<MarketSegmentEnablementTeam> ReadAll();

    }
}