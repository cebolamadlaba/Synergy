using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace StandardBank.ConcessionManagement.BusinessLogic
{
    public class MarketSegmentEnablementTeamManager : IMarketSegmentEnablementTeamManager
    {
        private readonly IMarketSegmentEnablementTeamRepository _marketSegmentEnablementTeamRepository;

        public MarketSegmentEnablementTeamManager(IMarketSegmentEnablementTeamRepository marketSegmentEnablementTeamRepository)
        {
            this._marketSegmentEnablementTeamRepository = marketSegmentEnablementTeamRepository;
        }

        public IEnumerable<MarketSegmentEnablementTeam> GetMarketSegmentEnablementTeams(Func<MarketSegmentEnablementTeam, bool> predicate)
        {
            return this._marketSegmentEnablementTeamRepository.ReadAll().Where(predicate);
        }
    }
}
