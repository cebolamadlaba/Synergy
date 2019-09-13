using StandardBank.ConcessionManagement.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace StandardBank.ConcessionManagement.Interface.BusinessLogic
{
    public interface IMarketSegmentEnablementTeamManager
    {
        IEnumerable<MarketSegmentEnablementTeam> GetMarketSegmentEnablementTeams(Func<MarketSegmentEnablementTeam, bool> predicate);
    }
}
