﻿using System;
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

                //send notifications about the expiry to the requestors
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
            var expiringConcessions = new List<ExpiringConcession>();

            foreach (var concession in concessions)
            {
                var expiringConcession =
                    expiringConcessions.FirstOrDefault(_ => _.RequestorId == concession.RequestorId.Value);

                if (expiringConcession == null)
                {
                    var requestor = _userRepository.ReadById(concession.RequestorId.Value);

                    expiringConcession =
                        new ExpiringConcession
                        {
                            ExpiringConcessionDetails = new List<ExpiringConcessionDetail>(),
                            RequestorEmail = requestor.EmailAddress,
                            RequestorName = $"{requestor.FirstName} {requestor.Surname}",
                            RequestorId = requestor.Id
                        };

                    expiringConcessions.Add(expiringConcession);
                }

                var expiringConcessionDetails = new List<ExpiringConcessionDetail>();
                expiringConcessionDetails.AddRange(expiringConcession.ExpiringConcessionDetails);
                expiringConcessionDetails.Add(new ExpiringConcessionDetail
                {
                    ConcessionType = concession.ConcessionType,
                    ConcessionRef = concession.ConcessionRef,
                    CustomerName = concession.CustomerName,
                    ExpiryDate = concession.ExpiryDate.Value.ToString("yyyy-MM-dd"),
                    RiskGroupNumber = Convert.ToString(concession.RiskGroupNumber),
                    RiskGroupName = concession.RiskGroupName,
                    DateApproved = concession.DateApproved.Value.ToString("yyyy-MM-dd")
                });

                expiringConcession.ExpiringConcessionDetails = expiringConcessionDetails;
            }
            return expiringConcessions;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name => "Due For Expiry Notification";
    }
}