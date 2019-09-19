using StandardBank.ConcessionManagement.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace StandardBank.ConcessionManagement.Interface.BusinessLogic
{
    public interface IMarketSegmentEnablementTeamUserManager
    {
        IEnumerable<MarketSegmentEnablementTeamUser> GetMarketSegmentEnablementTeamUsers(Func<MarketSegmentEnablementTeamUser, bool> predicate);

        IEnumerable<ConcessionTypeMismatchEscalation> GetConcessionTypeMismatchEscalation();

        IEnumerable<MarketSegment> GetMarketSegments(Func<MarketSegment, bool> predicate);
    }
}
