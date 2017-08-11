using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.Repository;
using StandardBank.ConcessionManagement.Model.UserInterface.Lending;

namespace StandardBank.ConcessionManagement.BusinessLogic.Features.AddLendingConcessionDetail
{
    /// <summary>
    /// Add lending concession detail command handler
    /// </summary>
    /// <seealso cref="MediatR.IAsyncRequestHandler{AddLendingConcessionCommand, LendingConcessionDetail}" />
    public class AddLendingConcessionDetailCommandHandler : IAsyncRequestHandler<AddLendingConcessionDetailCommand, LendingConcessionDetail>
    {
        /// <summary>
        /// The lending manager
        /// </summary>
        private readonly ILendingManager _lendingManager;

        /// <summary>
        /// Initializes the class
        /// </summary>
        /// <param name="lendingManager"></param>
        public AddLendingConcessionDetailCommandHandler(ILendingManager lendingManager)
        {
            _lendingManager = lendingManager;
        }

        /// <summary>
        /// Handles the message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<LendingConcessionDetail> Handle(AddLendingConcessionDetailCommand message)
        {
            var result = _lendingManager.CreateConcessionLending(message.LendingConcessionDetail, message.Concession);

            message.AuditRecord = new AuditRecord(result, message.User, AuditType.Insert);

            return message.LendingConcessionDetail;
        }
    }
}
