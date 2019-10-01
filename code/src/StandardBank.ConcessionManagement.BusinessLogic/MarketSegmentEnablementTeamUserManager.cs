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
    public class MarketSegmentEnablementTeamUserManager : IMarketSegmentEnablementTeamUserManager
    {
        private readonly IMarketSegmentEnablementTeamUserRepository _marketSegmentEnablementTeamRepository;
        private readonly IMarketSegmentRepository _marketSegmentRepository;

        public MarketSegmentEnablementTeamUserManager(IMarketSegmentEnablementTeamUserRepository marketSegmentEnablementTeamRepository,
            IMarketSegmentRepository marketSegmentRepository)
        {
            this._marketSegmentEnablementTeamRepository = marketSegmentEnablementTeamRepository;
            this._marketSegmentRepository = marketSegmentRepository;
        }

        public IEnumerable<MarketSegmentEnablementTeamUser> GetMarketSegmentEnablementTeamUsers(Func<MarketSegmentEnablementTeamUser, bool> predicate)
        {
            return this._marketSegmentEnablementTeamRepository.ReadAll().Where(predicate);
        }

        public IEnumerable<ConcessionTypeMismatchEscalation> GetConcessionTypeMismatchEscalation()
        {
            return this._marketSegmentEnablementTeamRepository.GetConcessionTypeMismatchEscalation();
        }

        public void Update(ConcessionTypeMismatchEscalation model)
        {
            this._marketSegmentEnablementTeamRepository.Update(model);
        }

        public IEnumerable<MarketSegment> GetMarketSegments(Func<MarketSegment, bool> predicate)
        {
            return this._marketSegmentRepository.ReadAll().Where(predicate);
        }
    }
}
