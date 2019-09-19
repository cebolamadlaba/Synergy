using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.BusinessLogic.ScheduledJobs;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandardBank.ConcessionManagement.BusinessLogic.ScheduledJobs
{
    public class MismatchedScheduledJob : IDailyScheduledJob
    {
        private readonly IConcessionInboxViewRepository _concessionInboxViewRepository;
        private readonly IMarketSegmentEnablementTeamUserManager _marketSegmentEnablementTeamUserManager;

        public MismatchedScheduledJob(IConcessionInboxViewRepository concessionInboxViewRepository,
            IMarketSegmentEnablementTeamUserManager marketSegmentEnablementTeamUserManager)
        {
            this._concessionInboxViewRepository = concessionInboxViewRepository;
            this._marketSegmentEnablementTeamUserManager = marketSegmentEnablementTeamUserManager;
        }

        public string Name => "Mismatched Scheduled Job";

        public int HourToRun => 6;

        public int MinuteToRun => 0;

        public string type => "";

        public async Task Run()
        {
            // find all Mismatched Concession from tblConcessionDetail.IsMismatched
            IEnumerable<ConcessionInboxView> concessions = this._concessionInboxViewRepository.GetMisMatchedConcession();

            // get a list of market segment enablement team users and their email addresses.
            IEnumerable<MarketSegmentEnablementTeamUser> enablementTeamUsers = this._marketSegmentEnablementTeamUserManager.GetMarketSegmentEnablementTeamUsers(x => x.IsActive);

            IEnumerable<ConcessionTypeMismatchEscalation> concessionTypeMismatchEscalation = this._marketSegmentEnablementTeamUserManager.GetConcessionTypeMismatchEscalation();

            // when was last 24 hours escalation email sent?
            bool canSend24HourEscalationEmail = concessionTypeMismatchEscalation
                .FirstOrDefault(x => x.ConcessionType.ToLower() == Constants.ConcessionType.Lending.ToLower())
                .LastEscalationSentDateTime.Date < DateTime.Now.Date;

            // when was last 30 day escalation email sent?
            DateTime next30DayEscalationSendDate = concessionTypeMismatchEscalation
                .FirstOrDefault(x => x.ConcessionType.ToLower() == Constants.ConcessionType.BusinessOnlineDesc.ToLower())
                .LastEscalationSentDateTime.AddDays(30);

            bool canSend30DayEscalationEmail = next30DayEscalationSendDate.Date == DateTime.Now.Date;

            // return if neither of the escalations can be sent.
            if (!canSend24HourEscalationEmail && !canSend30DayEscalationEmail)
                return;

            // get list of market segments
            IEnumerable<MarketSegment> marketSegments = this._marketSegmentEnablementTeamUserManager.GetMarketSegments(x => x.IsActive);
            MarketSegment businessMarketSegment = marketSegments.FirstOrDefault(x => x.Description == Constants.MarketSegment.Business);
            MarketSegment commercialMarketSegment = marketSegments.FirstOrDefault(x => x.Description == Constants.MarketSegment.Business);
            MarketSegment smallEnterpriseMarketSegment = marketSegments.FirstOrDefault(x => x.Description == Constants.MarketSegment.Business);

            List<ConcessionInboxView> businessConcessions = new List<ConcessionInboxView>();
            List<ConcessionInboxView> commercialConcessions = new List<ConcessionInboxView>();
            List<ConcessionInboxView> smallEnterpriseConcessions = new List<ConcessionInboxView>();

            // get and loop through distinct list of market segment ids
            foreach (int marketSegmentId in enablementTeamUsers.Select(x => x.MarketSegmentId).Distinct())
            {
                // build grouped lists of concessions products which can be sent
                foreach (ConcessionInboxView concession in concessions.Where(x => x.MarketSegmentId == marketSegmentId))
                {
                    switch (concession.ConcessionType)
                    {
                        // must be escalated every 24 hours via email
                        case Constants.ConcessionType.Lending:
                        case Constants.ConcessionType.Cash:
                        case Constants.ConcessionType.Transactional:
                        case Constants.ConcessionType.Trade:

                            break;

                        // must be escalated every 30 days via email
                        case Constants.ConcessionType.BusinessOnline:
                        case Constants.ConcessionType.BusinessOnlineDesc:
                            break;
                    }
                }
            }




        }

    }
}
