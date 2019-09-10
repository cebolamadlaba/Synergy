using StandardBank.ConcessionManagement.Interface.BusinessLogic.ScheduledJobs;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StandardBank.ConcessionManagement.BusinessLogic.ScheduledJobs
{
    public class MismatchedScheduledJob : IDailyScheduledJob
    {
        private readonly IConcessionInboxViewRepository _concessionInboxViewRepository;

        public MismatchedScheduledJob(IConcessionInboxViewRepository concessionInboxViewRepository)
        {
            this._concessionInboxViewRepository = concessionInboxViewRepository;
        }

        public string Name => "Mismatched Scheduled Job";

        public int HourToRun => 6;

        public int MinuteToRun => 0;

        public string type => "";

        public async Task Run()
        {
            // find all Mismatched Concession from tblConcessionDetail.IsMismatched
            IEnumerable<ConcessionInboxView> concessions = this._concessionInboxViewRepository.GetMisMatchedConcession();


            foreach (ConcessionInboxView concession in concessions)
            {
                switch (concession.ConcessionType)
                {
                    // must be escalated every 24 hours via email
                    case Constants.ConcessionType.Lending:
                        break;
                    case Constants.ConcessionType.Cash:
                        break;
                    case Constants.ConcessionType.Transactional:
                        break;
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
