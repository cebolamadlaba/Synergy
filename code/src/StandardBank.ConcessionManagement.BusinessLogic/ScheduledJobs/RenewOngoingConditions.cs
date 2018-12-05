using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using StandardBank.ConcessionManagement.BusinessLogic.Features.ConcessionCondition;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Interface.BusinessLogic.ScheduledJobs;
using StandardBank.ConcessionManagement.Interface.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface;

namespace StandardBank.ConcessionManagement.BusinessLogic.ScheduledJobs
{
    /// <summary>
    /// Renew ongoing conditions
    /// </summary>
    /// <seealso cref="StandardBank.ConcessionManagement.Interface.BusinessLogic.ScheduledJobs.IDailyScheduledJob" />
    public class RenewOngoingConditions : IDailyScheduledJob
    {
        /// <summary>
        /// The concession condition view repository
        /// </summary>
        private readonly IConcessionConditionViewRepository _concessionConditionViewRepository;

        /// <summary>
        /// The concession manager
        /// </summary>
        private readonly IConcessionManager _concessionManager;

        /// <summary>
        /// The mediator
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="RenewOngoingConditions"/> class.
        /// </summary>
        /// <param name="concessionConditionViewRepository">The concession condition view repository.</param>
        /// <param name="concessionManager">The concession manager.</param>
        /// <param name="mediator">The mediator.</param>
        public RenewOngoingConditions(IConcessionConditionViewRepository concessionConditionViewRepository,
            IConcessionManager concessionManager, IMediator mediator)
        {
            _concessionConditionViewRepository = concessionConditionViewRepository;
            _concessionManager = concessionManager;
            _mediator = mediator;
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        /// <returns></returns>
        public async Task Run()
        {
            var expiringOngoingConditions = _concessionConditionViewRepository.ReadForRenewingOngoingConditions();

            foreach (var expiringOngoingCondition in expiringOngoingConditions)
            {
                var concession =
                    _concessionManager.GetConcessionForConcessionReferenceId(expiringOngoingCondition.ReferenceNumber, null);

                var concessionConditions = _concessionManager.GetConcessionConditions(concession.Id);

                var expiringConcessionCondition =
                    concessionConditions.First(_ => _.ConcessionConditionId == expiringOngoingCondition.ConcessionConditionId);

                expiringConcessionCondition.ExpiryDate =
                    CalculateNewExpiryDate(expiringConcessionCondition.ExpiryDate.Value,
                        expiringConcessionCondition.Period);

                expiringConcessionCondition.ConditionMet = null;

                await _mediator.Send(new AddOrUpdateConcessionCondition(expiringConcessionCondition,
                    new User { ANumber = "SYSTEM" }, concession));
            }
        }

        /// <summary>
        /// Calculates the new expiry date.
        /// </summary>
        /// <param name="expiryDateValue">The expiry date value.</param>
        /// <param name="period">The period.</param>
        /// <returns></returns>
        private DateTime CalculateNewExpiryDate(DateTime expiryDateValue, string period)
        {
            var periodNumber = Convert.ToInt32(period.Split(' ')[0]);

            return expiryDateValue.AddMonths(periodNumber);
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name => "Renew Ongoing Conditions";

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
        public int MinuteToRun => 5;

        public string type => "";
    }
}
