using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.BusinessLogic.ScheduledJobs;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.BusinessLogic.EmailTemplates;
using StandardBank.ConcessionManagement.Model.Repository;

namespace StandardBank.ConcessionManagement.BusinessLogic.ScheduledJobs
{
    /// <summary>
    /// Due for expiry notification
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.BusinessLogic.ScheduledJobs.IDailyScheduledJob" />
    public class DueForExpiryNotification : IDailyScheduledJob
    {
        /// <summary>
        /// The concession inbox view repository
        /// </summary>
        private readonly IConcessionInboxViewRepository _concessionInboxViewRepository;

        /// <summary>
        /// The user repository
        /// </summary>
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// The email manager
        /// </summary>
        private readonly IEmailManager _emailManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="DueForExpiryNotification"/> class.
        /// </summary>
        /// <param name="concessionInboxViewRepository">The concession inbox view repository.</param>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="emailManager">The email manager.</param>
        public DueForExpiryNotification(IConcessionInboxViewRepository concessionInboxViewRepository,
            IUserRepository userRepository, IEmailManager emailManager)
        {
            _concessionInboxViewRepository = concessionInboxViewRepository;
            _userRepository = userRepository;
            _emailManager = emailManager;
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        /// <returns></returns>
        public async Task Run()
        {
            //get the concessions that are expiring in three months
            var concessions = _concessionInboxViewRepository.ReadForDueForExpiryNotification();

            if (concessions != null && concessions.Any())
            {
                var expiringConcessions = GetExpiringConcessions(concessions);

                //send notifications about the expiry to the recipients
                foreach (var expiringConcession in expiringConcessions)
                    BackgroundJob.Schedule(() => _emailManager.SendExpiringConcessionEmail(expiringConcession),
                        DateTime.Now);
            }
        }

        /// <summary>
        /// Gets the expiring concessions.
        /// </summary>
        /// <param name="concessions">The concessions.</param>
        /// <returns></returns>
        private IEnumerable<ExpiringConcession> GetExpiringConcessions(IEnumerable<ConcessionInboxView> concessions)
        {
            var expiringConcessionList = new List<ExpiringConcession>();

            foreach (var concession in concessions)
            {
                //Get expire before month
                int month = this.IsIntervalOfMonthBeforeExpiryDate(concession);

                //don't send notification if expiry time has not yet been a month
                if (month == 0) continue;

                //Create new detail to be sent out
                var concessionDetail = new ExpiringConcessionDetail
                {
                    ConcessionType = concession.ConcessionType,
                    ConcessionRef = concession.ConcessionRef,
                    CustomerName = concession.CustomerName,
                    ExpiryDate = concession.ExpiryDate.Value.ToString("yyyy-MM-dd"),
                    RiskGroupNumber = Convert.ToString(concession.RiskGroupNumber),
                    RiskGroupName = concession.RiskGroupName,
                    DateApproved = concession.DateApproved.Value.ToString("yyyy-MM-dd"),
                    ResponsibleAA = concession.AAUserFullName ?? "(-) NULL",
                    ResponsibleAE = concession.AEUserFullName
                };

                //Send notification to requestor
                if (concession.RequestorId.HasValue)
                    AddExpiringConcessionForUser(concession.RequestorId.Value, ref expiringConcessionList, concessionDetail);
                
                //Add BCM user if months are less then 3
                if(month < 3 && concession.BCMUserId.HasValue)
                    AddExpiringConcessionForUser(concession.BCMUserId.Value, ref expiringConcessionList, concessionDetail);
                
                //Add PCM user if there is a month left
                if (month == 1 && concession.PCMUserId.HasValue)
                    AddExpiringConcessionForUser(concession.PCMUserId.Value, ref expiringConcessionList, concessionDetail);


                //Add HO user if there is a month left
                if (month == 1 && concession.HOUserId.HasValue)
                    AddExpiringConcessionForUser(concession.HOUserId.Value, ref expiringConcessionList, concessionDetail);   
            }

            return expiringConcessionList;
        }


        /// <summary>
        /// Gets a expiring concession noti for a user 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="expiringConcessions"></param>
        /// <returns></returns>
        private void AddExpiringConcessionForUser(int userId ,ref List<ExpiringConcession> expiringConcessions, ExpiringConcessionDetail concessionDetail)
        {
            //checks if there currently is a recipient that matches the userid
            var expiringConcession =
                   expiringConcessions.FirstOrDefault(_ => _.RecipientId == userId);


            if (expiringConcession == null)
            {
                //Get thee recipient from the user table
                var recipient = _userRepository.ReadById(userId);

                expiringConcession =
                    new ExpiringConcession
                    {
                        ExpiringConcessionDetails = new List<ExpiringConcessionDetail>(),
                        RecipientEmail = recipient.EmailAddress,
                        RecipientName = $"{recipient.FirstName} {recipient.Surname}",
                        RecipientId = recipient.Id
                    };

                expiringConcessions.Add(expiringConcession);
            }

            //Only add the concession if it has not already been added to the notification list
            if (!expiringConcession.ExpiringConcessionDetails.Any(x=> x.ConcessionRef == concessionDetail.ConcessionRef))
                expiringConcession.ExpiringConcessionDetails.Add(concessionDetail);
        }


        private int IsIntervalOfMonthBeforeExpiryDate(ConcessionInboxView concessionInboxView)
        {
            // Is todays date 3 months before expiry date?
            // Is todays date 2 months before expiry date?
            // Is todays date 1 months before expiry date?

            if (!concessionInboxView.ExpiryDate.HasValue)
                return 0;

            DateTime monthValue;

            for (int i = 3; i > 0; i--)
            {
                monthValue = concessionInboxView.ExpiryDate.Value.Date.AddMonths(-i);
                if (DateTime.Today.Date == monthValue)
                    return i;

            }

            return 0;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name => "Due For Expiry Notification";

        /// <summary>
        /// Gets the hour to run.
        /// </summary>
        /// <value>
        /// The hour to run.
        /// </value>
        public int HourToRun => 6;

        /// <summary>
        /// Gets the minute to run.
        /// </summary>
        /// <value>
        /// The minute to run.
        /// </value>
        public int MinuteToRun => 0;

        public string type => "";
    }
}
