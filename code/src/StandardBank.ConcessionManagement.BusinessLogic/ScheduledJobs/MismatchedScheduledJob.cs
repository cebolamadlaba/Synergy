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
            IEnumerable<ConcessionMismatchEscalationView> concessions = this._concessionInboxViewRepository.GetMisMatchedConcession();

            this.CanSendEscalation(out bool canSend24HourEscalationEmail, out bool canSend30DayEscalationEmail);

            // return if neither of the escalations can be sent.
            if (!canSend24HourEscalationEmail && !canSend30DayEscalationEmail)
                return;

            var concession24HourGroup = new List<ConcessionMismatchEscalationView>();
            var concession30DayGroup = new List<ConcessionMismatchEscalationView>();

            // add concessions to relevant groups.
            this.AddConcessionsForMismatchEscalationtoGroup(
                concessions,
                concession24HourGroup,
                concession30DayGroup,
                canSend24HourEscalationEmail,
                canSend30DayEscalationEmail);

            // group and schedule emails.
            this.GroupAndScheduleMarketSegmentConcessionsForEmail(concession24HourGroup, concession30DayGroup);
        }

        private void CanSendEscalation(out bool canSend24HourEscalationEmail, out bool canSend30DayEscalationEmail)
        {
            IEnumerable<ConcessionTypeMismatchEscalation> concessionTypeMismatchEscalation = this._marketSegmentEnablementTeamUserManager.GetConcessionTypeMismatchEscalation();

            // is the last escalation date the day before today?
            canSend24HourEscalationEmail = concessionTypeMismatchEscalation
                .FirstOrDefault(x => x.ConcessionType.ToLower() == Constants.ConcessionType.Lending.ToLower())
                .LastEscalationSentDateTime.Date < DateTime.Now.Date;

            // when was last 30 day escalation email sent?
            DateTime next30DayEscalationSendDate = concessionTypeMismatchEscalation
                .FirstOrDefault(x => x.ConcessionType.ToLower() == Constants.ConcessionType.BusinessOnlineDesc.ToLower())
                .LastEscalationSentDateTime.AddDays(30);

            canSend30DayEscalationEmail = next30DayEscalationSendDate.Date >= DateTime.Now.Date;
        }

        private void AddConcessionsForMismatchEscalationtoGroup(
            IEnumerable<ConcessionMismatchEscalationView> concessions,
            List<ConcessionMismatchEscalationView> concession24HourGroup,
            List<ConcessionMismatchEscalationView> concession30DayGroup,
            bool canSend24HourEscalationEmail,
            bool canSend30DayEscalationEmail)
        {
            // get and loop through distinct list of market segment ids
            foreach (int marketSegmentId in concessions.Select(x => x.MarketSegmentId).Distinct())
            {
                // build grouped lists of concessions products which can be sent
                foreach (ConcessionMismatchEscalationView concession in concessions.Where(x => x.MarketSegmentId == marketSegmentId))
                {
                    switch (concession.ConcessionType)
                    {
                        // must be escalated every 24 hours via email
                        case Constants.ConcessionType.Lending:
                        case Constants.ConcessionType.Cash:
                        case Constants.ConcessionType.Transactional:
                        case Constants.ConcessionType.Trade:
                            if (canSend24HourEscalationEmail)
                                concession24HourGroup.Add(concession);
                            break;

                        // must be escalated every 30 days via email
                        case Constants.ConcessionType.BusinessOnline:
                        case Constants.ConcessionType.BusinessOnlineDesc:
                            if (canSend30DayEscalationEmail)
                                concession30DayGroup.Add(concession);
                            break;
                    }
                }
            }
        }

        private void GroupAndScheduleMarketSegmentConcessionsForEmail(
            List<ConcessionMismatchEscalationView> concession24HourGroup,
            List<ConcessionMismatchEscalationView> concession30DayGroup
            )
        {
            // send email for 24 hour group
            if (concession24HourGroup.Count > 0)
            {
                this.ScheduleMarketSegmentEmails(concession24HourGroup, Constants.MarketSegment.Business);
                this.ScheduleMarketSegmentEmails(concession24HourGroup, Constants.MarketSegment.Commercial);
                this.ScheduleMarketSegmentEmails(concession24HourGroup, Constants.MarketSegment.SmallEnterprise);
            }

            // send email for 30 day group
            if (concession30DayGroup.Count > 0)
            {
                this.ScheduleMarketSegmentEmails(concession30DayGroup, Constants.MarketSegment.Business);
                this.ScheduleMarketSegmentEmails(concession30DayGroup, Constants.MarketSegment.Commercial);
                this.ScheduleMarketSegmentEmails(concession30DayGroup, Constants.MarketSegment.SmallEnterprise);
            }
        }

        private void ScheduleMarketSegmentEmails(
            IEnumerable<ConcessionMismatchEscalationView> marketSegmentConcessions,
            string marketSegment)
        {
            // group by market segment
            var groupedMarketSegmentConcessions = marketSegmentConcessions.Where(a => a.MarketSegment == marketSegment);

            // get market segment enablement team user emailaddresses
            var marketSegmentEnablementTeamEmailAddresses = groupedMarketSegmentConcessions.Select(a => a.EnablementTeamUserEmailAddress).Distinct();

            // To Do:
            // 1. create email template
            // 2. populate email template with above information.
            
        }

    }
}
