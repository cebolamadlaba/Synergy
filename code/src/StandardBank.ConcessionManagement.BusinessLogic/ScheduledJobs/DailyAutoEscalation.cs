using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using MediatR;
using StandardBank.ConcessionManagement.BusinessLogic.Features.BolConcession;
using StandardBank.ConcessionManagement.BusinessLogic.Features.Concession;
using StandardBank.ConcessionManagement.BusinessLogic.Features.ConcessionCondition;
using StandardBank.ConcessionManagement.BusinessLogic.Features.LendingConcession;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.BusinessLogic.ScheduledJobs;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic.EmailTemplates;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface.Bol;
using StandardBank.ConcessionManagement.Model.UserInterface.Lending;
using StandardBank.ConcessionManagement.Model.UserInterface.Transactional;

namespace StandardBank.ConcessionManagement.BusinessLogic.ScheduledJobs
{
    /// <summary>
    /// Due for expiry notification
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.BusinessLogic.ScheduledJobs.IDailyScheduledJob" />
    public class DailyAutoEscalation : IDailyScheduledJob
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

        private readonly ILendingManager _lendingManager;

        private readonly IBolManager _bolManager;

        private readonly ITransactionalManager _transactionalManager;

        private readonly ICashManager _cashManager;

        private readonly ILookupTableManager _lookupTableManager;

        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="DueForExpiryNotification"/> class.
        /// </summary>
        /// <param name="concessionInboxViewRepository">The concession inbox view repository.</param>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="emailManager">The email manager.</param>
        public DailyAutoEscalation(IConcessionInboxViewRepository concessionInboxViewRepository,
            IUserRepository userRepository, IEmailManager emailManager, ILendingManager lendingManager, ILookupTableManager lookupTableManager, IMediator mediator, IBolManager bolManager, ITransactionalManager transactionalManager, ICashManager cashManager)
        {
            _concessionInboxViewRepository = concessionInboxViewRepository;
            _userRepository = userRepository;
            _emailManager = emailManager;
            _lendingManager = lendingManager;
            _lookupTableManager = lookupTableManager;
            _mediator = mediator;
            _bolManager = bolManager;
            _transactionalManager = transactionalManager;
            _cashManager = cashManager;

        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        /// <returns></returns>
        public async Task Run()
        {

            //Only look for statusses in BcmPending
            var pendingStatusIds = _lookupTableManager.GetSubStatusId(Constants.ConcessionSubStatus.BcmPending);

            //get the concessions that are expiring in three months
            var dueconcessions = _concessionInboxViewRepository.ReadDueFor24HourEscaltion(new[] { pendingStatusIds });

            if (dueconcessions != null && dueconcessions.Any())
            {
                Model.UserInterface.User system = new Model.UserInterface.User();
                system.ANumber = "00000";
                system.FirstName = "System";
                system.Surname = "System";

                foreach (var dueconcession in dueconcessions)
                {
                    if (dueconcession.ConcessionType == Constants.ConcessionType.Lending)
                    {
                        LendingConcession lendingConcession = _lendingManager.GetLendingConcession(dueconcession.ConcessionRef, system);

                        if (lendingConcession.Concession.RequestorId.HasValue)
                        {
                            system.Id = lendingConcession.Concession.RequestorId.Value;
                            system.SelectedCentre = new Model.UserInterface.Centre() { Id = lendingConcession.Concession.CentreId };
                            lendingConcession.Concession.SubStatus = Constants.ConcessionSubStatus.PcmPending;
                            lendingConcession.Concession.Comments = "Auto(24H) forwarded by system";
                            lendingConcession.Concession.IsInProgressForwarding = true;

                            await _lendingManager.ForwardLendingConcession(lendingConcession, system);
                        }
                    }
                    else if (dueconcession.ConcessionType == Constants.ConcessionType.Cash)
                    {

                        var cashconsession = _cashManager.GetCashConcession(dueconcession.ConcessionRef, system);
                        if (cashconsession.Concession.RequestorId.HasValue)
                        {

                            system.Id = cashconsession.Concession.RequestorId.Value;
                            system.SelectedCentre = new Model.UserInterface.Centre() { Id = cashconsession.Concession.CentreId };
                            cashconsession.Concession.SubStatus = Constants.ConcessionSubStatus.PcmPending;
                            cashconsession.Concession.Comments = "Auto(24H) forwarded by system";
                            cashconsession.Concession.IsInProgressForwarding = true;                                                       

                            await _cashManager.ForwardCashConcession(cashconsession, system);
                        }

                    }
                    else if (dueconcession.ConcessionType == Constants.ConcessionType.Transactional)
                    {
                        TransactionalConcession transactionalConcession = _transactionalManager.GetTransactionalConcession(dueconcession.ConcessionRef, system);

                        if (transactionalConcession.Concession.RequestorId.HasValue)
                        {

                            system.Id = transactionalConcession.Concession.RequestorId.Value;
                            system.SelectedCentre = new Model.UserInterface.Centre() { Id = transactionalConcession.Concession.CentreId };
                            transactionalConcession.Concession.SubStatus = Constants.ConcessionSubStatus.PcmPending;
                            transactionalConcession.Concession.Comments = "Auto(24H) forwarded by system";
                            transactionalConcession.Concession.IsInProgressForwarding = true;

                           await _transactionalManager.ForwardTransactionalConcession(transactionalConcession, system);
                        }

                    }
                    else if (dueconcession.ConcessionType == "Business Online")
                    {
                        BolConcession bolConcession = _bolManager.GetBolConcession(dueconcession.ConcessionRef, system);

                        if (bolConcession.Concession.RequestorId.HasValue)
                        {
                            if (bolConcession.Concession.RequestorId.HasValue)
                            {
                                system.Id = bolConcession.Concession.RequestorId.Value;
                                system.SelectedCentre = new Model.UserInterface.Centre() { Id = bolConcession.Concession.CentreId };
                                bolConcession.Concession.SubStatus = Constants.ConcessionSubStatus.PcmPending;
                                bolConcession.Concession.Comments = "Auto(24H) forwarded by system";
                                bolConcession.Concession.IsInProgressForwarding = true; 

                                await _bolManager.ForwardBolConcession(bolConcession, system);
                            }
                        }
                    }
                }
            }

        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name => "24-hour automated escalation";

        /// <summary>
        /// Gets the hour to run.
        /// </summary>
        /// <value>
        /// The hour to run.
        /// </value>
        public int HourToRun => 2;

        /// <summary>
        /// Gets the minute to run.
        /// </summary>
        /// <value>
        /// The minute to run.
        /// </value>
        public int MinuteToRun => 0;

        public string type => "Recurring";

    }
}
