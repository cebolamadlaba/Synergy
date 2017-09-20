using System;
using System.Threading.Tasks;
using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using ConcessionCondition = StandardBank.ConcessionManagement.Model.UserInterface.ConcessionCondition;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.AddOrUpdateConcessionCondition
{
    /// <summary>
    /// Add concession condition command handler
    /// </summary>
    /// <seealso cref="MediatR.IAsyncRequestHandler{AddConcessionConditionCommand, ConcessionCondition}" />
    public class AddOrUpdateConcessionConditionHandler : IAsyncRequestHandler<AddOrUpdateConcessionCondition, ConcessionCondition>
    {
        /// <summary>
        /// The concession manager
        /// </summary>
        private readonly IConcessionManager _concessionManager;

        /// <summary>
        /// The lookup table manager
        /// </summary>
        private readonly ILookupTableManager _lookupTableManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddOrUpdateConcessionConditionHandler"/> class.
        /// </summary>
        /// <param name="concessionManager">The concession manager.</param>
        /// <param name="lookupTableManager">The lookup table manager.</param>
        public AddOrUpdateConcessionConditionHandler(IConcessionManager concessionManager, ILookupTableManager lookupTableManager)
        {
            _concessionManager = concessionManager;
            _lookupTableManager = lookupTableManager;
        }

        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public async Task<ConcessionCondition> Handle(AddOrUpdateConcessionCondition message)
        {
            if (message.ConcessionCondition.ConcessionConditionId == 0)
            {
                var result =
                    _concessionManager.CreateConcessionCondition(message.ConcessionCondition, message.Concession);

                message.AuditRecord = new AuditRecord(result, message.User, AuditType.Insert);

                message.ConcessionCondition.ConcessionConditionId = result.Id;
            }
            else
            {
                if (message.Concession.DateApproved.HasValue && message.ConcessionCondition.PeriodId.HasValue &&
                    (message.Concession.Status == "Approved" || message.Concession.Status == "Approved With Changes"))
                {
                    var period = _lookupTableManager.GetPeriodName(message.ConcessionCondition.PeriodId.Value);
                    var months = Convert.ToInt32(period.Split(' ')[0]);
                    message.ConcessionCondition.ExpiryDate = message.Concession.DateApproved.Value.AddMonths(months);
                }

                var result =
                    _concessionManager.UpdateConcessionCondition(message.ConcessionCondition, message.Concession);

                message.AuditRecord = new AuditRecord(result, message.User, AuditType.Update);
            }

            return message.ConcessionCondition;
        }
    }
}
