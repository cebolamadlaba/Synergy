using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.BusinessLogic.Features.Concession;
using StandardBank.ConcessionManagement.BusinessLogic.Features.ConcessionCondition;
using StandardBank.ConcessionManagement.BusinessLogic.Features.TransactionalConcession;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.Model.UserInterface.Transactional;
using StandardBank.ConcessionManagement.UI.Helpers.Interface;
using StandardBank.ConcessionManagement.UI.Validation;

namespace StandardBank.ConcessionManagement.UI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/Transactional")]
    public class TransactionalController : Controller
    {
        /// <summary>
        /// The site helper
        /// </summary>
        private readonly ISiteHelper _siteHelper;

        /// <summary>
        /// The transactional manager
        /// </summary>
        private readonly ITransactionalManager _transactionalManager;

        /// <summary>
        /// The mediator
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionalController"/> class.
        /// </summary>
        /// <param name="siteHelper">The site helper.</param>
        /// <param name="transactionalManager">The transactional manager.</param>
        /// <param name="mediator">The mediator.</param>
        public TransactionalController(ISiteHelper siteHelper, ITransactionalManager transactionalManager,
            IMediator mediator)
        {
            _siteHelper = siteHelper;
            _transactionalManager = transactionalManager;
            _mediator = mediator;
        }

        /// <summary>
        /// Transactionals the view.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <returns></returns>
        [Route("TransactionalView/{riskGroupNumber}")]
        public IActionResult TransactionalView(int riskGroupNumber)
        {
            return Ok(_transactionalManager.GetTransactionalViewData(riskGroupNumber));
        }

        /// <summary>
        /// Gets the transactional concession data.
        /// </summary>
        /// <param name="concessionReferenceId">The concession reference identifier.</param>
        /// <returns></returns>
        [Route("TransactionalConcessionData/{concessionReferenceId}")]
        public IActionResult TransactionalConcessionData(string concessionReferenceId)
        {
            return Ok(_transactionalManager.GetTransactionalConcession(concessionReferenceId, _siteHelper.LoggedInUser(this)));
        }

        /// <summary>
        /// Creates a new transactional concession
        /// </summary>
        /// <param name="transactionalConcession">The transactional concession.</param>
        /// <returns></returns>
        [Route("ForwardTransactionalPCM")]
        [ValidateModel]
        public async Task<IActionResult> ForwardTransactionalPCM([FromBody] SearchConcessionDetail detail)
        {
            var user = _siteHelper.LoggedInUser(this);

            TransactionalConcession transactionalConcession = _transactionalManager.GetTransactionalConcession(detail.ReferenceNumber, user);

            transactionalConcession.Concession.SubStatus = Constants.ConcessionSubStatus.PcmPending;
            transactionalConcession.Concession.BcmUserId = user.Id;
            transactionalConcession.Concession.Comments  = "Manually forwarded by PCM";
            transactionalConcession.Concession.IsInProgressForwarding = true;

            await _transactionalManager.ForwardTransactionalConcession(transactionalConcession, user);

            return Ok(_transactionalManager.GetTransactionalConcession(detail.ReferenceNumber, user));
        }

        /// <summary>
        /// Updates the transactional concession.
        /// </summary>
        /// <param name="transactionalConcession">The transactional concession.</param>
        /// <returns></returns>
        [Route("UpdateTransactional")]
        [ValidateModel]
        public async Task<IActionResult> UpdateTransactional([FromBody] TransactionalConcession transactionalConcession)
        {
            var user = _siteHelper.LoggedInUser(this);

            await UpdateTransactionalConcession(transactionalConcession, user);

            return Ok(_transactionalManager.GetTransactionalConcession(transactionalConcession.Concession.ReferenceNumber, user));
        }
      

        /// <summary>
        /// Updates the transactional concession.
        /// </summary>
        /// <param name="transactionalConcession">The transactional concession.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        private async Task UpdateTransactionalConcession(TransactionalConcession transactionalConcession, User user)
        {
            var databaseTransactionalConcession =
                _transactionalManager.GetTransactionalConcession(transactionalConcession.Concession.ReferenceNumber,
                    user);

            //if there are any conditions that have been removed, delete them
            foreach (var condition in databaseTransactionalConcession.ConcessionConditions)
                if (transactionalConcession.ConcessionConditions.All(
                    _ => _.ConcessionConditionId != condition.ConcessionConditionId))
                    await _mediator.Send(new DeleteConcessionCondition(condition, user));

            //if there are any cash concession details that have been removed delete them
            foreach (var transactionalConcessionDetail in databaseTransactionalConcession
                .TransactionalConcessionDetails)
                if (transactionalConcession.TransactionalConcessionDetails.All(
                    _ => _.TransactionalConcessionDetailId !=
                         transactionalConcessionDetail.TransactionalConcessionDetailId))
                    await _mediator.Send(new DeleteTransactionalConcessionDetail(transactionalConcessionDetail, user));

            //update the concession
            var concession = await _mediator.Send(new UpdateConcession(transactionalConcession.Concession, user));

            //add all the new conditions and cash details and comments
            foreach (var transactionalConcessionDetail in transactionalConcession.TransactionalConcessionDetails)
                await _mediator.Send(
                    new AddOrUpdateTransactionalConcessionDetail(transactionalConcessionDetail, user, concession));

            if (transactionalConcession.ConcessionConditions != null &&
                transactionalConcession.ConcessionConditions.Any())
                foreach (var concessionCondition in transactionalConcession.ConcessionConditions)
                    await _mediator.Send(new AddOrUpdateConcessionCondition(concessionCondition, user, concession));

            if (!string.IsNullOrWhiteSpace(transactionalConcession.Concession.Comments))
                await _mediator.Send(new AddConcessionComment(concession.Id,
                    databaseTransactionalConcession.Concession.SubStatusId,
                    transactionalConcession.Concession.Comments, user));
        }

   

        /// <summary>
        /// Creates a new transactional concession
        /// </summary>
        /// <param name="transactionalConcession">The transactional concession.</param>
        /// <returns></returns>
        [Route("NewTransactional")]
        [ValidateModel]
        public async Task<IActionResult> NewTransactional([FromBody] TransactionalConcession transactionalConcession)
        {
            var user = _siteHelper.LoggedInUser(this);

            transactionalConcession.Concession.ConcessionType = Constants.ConcessionType.Transactional;
            transactionalConcession.Concession.Type = Constants.ReferenceType.New;

            var concession = await _mediator.Send(new AddConcession(transactionalConcession.Concession, user));

            foreach (var transactionalConcessionDetail in transactionalConcession.TransactionalConcessionDetails)
                await _mediator.Send(
                    new AddOrUpdateTransactionalConcessionDetail(transactionalConcessionDetail, user, concession));

            if (transactionalConcession.ConcessionConditions != null &&
                transactionalConcession.ConcessionConditions.Any())
                foreach (var concessionCondition in transactionalConcession.ConcessionConditions)
                    await _mediator.Send(new AddOrUpdateConcessionCondition(concessionCondition, user, concession));

            return Ok(transactionalConcession);
        }

        /// <summary>
        /// Extends the concession.
        /// </summary>
        /// <param name="concessionReferenceId">The concession reference identifier.</param>
        /// <returns></returns>
        [Route("ExtendConcession/{concessionReferenceId}")]
        public async Task<IActionResult> ExtendConcession(string concessionReferenceId)
        {
            var user = _siteHelper.LoggedInUser(this);

            //get the transactional concession details
            var transactionalConcession = _transactionalManager.GetTransactionalConcession(concessionReferenceId, user);

            var parentConcessionId = transactionalConcession.Concession.Id;

            //add a new concession using the old concession's details
            var newConcession = transactionalConcession.Concession;
            newConcession.Id = 0;
            newConcession.Status = Constants.ConcessionStatus.Pending;
            newConcession.BcmUserId = null;
            newConcession.DateOpened = DateTime.Now;
            newConcession.DateSentForApproval = DateTime.Now;
            newConcession.HoUserId = null;
            newConcession.PcmUserId = null;
            newConcession.ReferenceNumber = string.Empty;
            newConcession.SubStatus = Constants.ConcessionSubStatus.BcmPending;
            newConcession.Type = Constants.ReferenceType.Existing;

            var concession = await _mediator.Send(new AddConcession(newConcession, user));

            transactionalConcession.Concession = concession;

            //add all the new conditions and transactional details
            foreach (var transactionalConcessionDetail in transactionalConcession.TransactionalConcessionDetails)
            {
                transactionalConcessionDetail.DateApproved = null;
                transactionalConcessionDetail.TransactionalConcessionDetailId = 0;
                await _mediator.Send(new AddOrUpdateTransactionalConcessionDetail(transactionalConcessionDetail, user, concession));
            }

            if (transactionalConcession.ConcessionConditions != null && transactionalConcession.ConcessionConditions.Any())
            {
                foreach (var concessionCondition in transactionalConcession.ConcessionConditions)
                {
                    concessionCondition.ConcessionConditionId = 0;
                    await _mediator.Send(new AddOrUpdateConcessionCondition(concessionCondition, user, concession));
                }
            }

            //link the new concession to the old concession
            var concessionRelationship = new ConcessionRelationship
            {
                CreationDate = DateTime.Now,
                UserId = user.Id,
                RelationshipDescription = Constants.RelationshipType.Extension,
                ParentConcessionId = parentConcessionId,
                ChildConcessionId = concession.Id
            };

            await _mediator.Send(new AddConcessionRelationship(concessionRelationship, user));

            var returnConcession = _transactionalManager.GetTransactionalConcession(concessionReferenceId, user);
            returnConcession.Concession.ChildReferenceNumber = concession.ReferenceNumber;
            return Ok(returnConcession);
        }

        /// <summary>
        /// Renews the transactional.
        /// </summary>
        /// <param name="transactionalConcession">The transactional concession.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [Route("RenewTransactional")]
        [ValidateModel]
        public async Task<IActionResult> RenewTransactional([FromBody] TransactionalConcession transactionalConcession)
        {
            var user = _siteHelper.LoggedInUser(this);

            var returnConcession = await CreateChildConcession(transactionalConcession, user, Constants.RelationshipType.Renewal);

            return Ok(returnConcession);
        }

        /// <summary>
        /// Resubmits the transactional.
        /// </summary>
        /// <param name="transactionalConcession">The transactional concession.</param>
        /// <returns></returns>
        [Route("ResubmitTransactional")]
        [ValidateModel]
        public async Task<IActionResult> ResubmitTransactional([FromBody] TransactionalConcession transactionalConcession)
        {
            var user = _siteHelper.LoggedInUser(this);

            var returnConcession = await CreateChildConcession(transactionalConcession, user, Constants.RelationshipType.Resubmit);

            return Ok(returnConcession);
        }

        /// <summary>
        /// Updates the approved transactional.
        /// </summary>
        /// <param name="transactionalConcession">The transactional concession.</param>
        /// <returns></returns>
        [Route("UpdateApprovedTransactional")]
        [ValidateModel]
        public async Task<IActionResult> UpdateApprovedTransactional([FromBody] TransactionalConcession transactionalConcession)
        {
            var user = _siteHelper.LoggedInUser(this);

            var returnConcession = await CreateChildConcession(transactionalConcession, user, Constants.RelationshipType.Update);

            return Ok(returnConcession);
        }

        /// <summary>
        /// Creates the child concession.
        /// </summary>
        /// <param name="transactionalConcession">The transactional concession.</param>
        /// <param name="user">The user.</param>
        /// <param name="relationship">The relationship.</param>
        /// <returns></returns>
        private async Task<TransactionalConcession> CreateChildConcession(TransactionalConcession transactionalConcession, User user, string relationship)
        {
            //get the parent cash concession details
            var parentCashConcession =
                _transactionalManager.GetTransactionalConcession(transactionalConcession.Concession.ReferenceNumber,
                    user);

            var parentConcessionId = parentCashConcession.Concession.Id;

            transactionalConcession.Concession.ReferenceNumber = string.Empty;
            transactionalConcession.Concession.ConcessionType = Constants.ConcessionType.Transactional;
            transactionalConcession.Concession.Type = Constants.ReferenceType.Existing;

            var concession = await _mediator.Send(new AddConcession(transactionalConcession.Concession, user));

            foreach (var transactionalConcessionDetail in transactionalConcession.TransactionalConcessionDetails)
                await _mediator.Send(
                    new AddOrUpdateTransactionalConcessionDetail(transactionalConcessionDetail, user, concession));

            if (transactionalConcession.ConcessionConditions != null &&
                transactionalConcession.ConcessionConditions.Any())
                foreach (var concessionCondition in transactionalConcession.ConcessionConditions)
                    await _mediator.Send(new AddOrUpdateConcessionCondition(concessionCondition, user, concession));

            //link the new concession to the old concession
            var concessionRelationship = new ConcessionRelationship
            {
                CreationDate = DateTime.Now,
                UserId = user.Id,
                RelationshipDescription = relationship,
                ParentConcessionId = parentConcessionId,
                ChildConcessionId = concession.Id
            };

            await _mediator.Send(new AddConcessionRelationship(concessionRelationship, user));

            var returnConcession =
                _transactionalManager.GetTransactionalConcession(parentCashConcession.Concession.ReferenceNumber, user);
            returnConcession.Concession.ChildReferenceNumber = concession.ReferenceNumber;
            return returnConcession;
        }

        /// <summary>
        /// Updates the recalled transactional.
        /// </summary>
        /// <param name="transactionalConcession">The transactional concession.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [Route("UpdateRecalledTransactional")]
        [ValidateModel]
        public async Task<IActionResult> UpdateRecalledTransactional([FromBody] TransactionalConcession transactionalConcession)
        {
            var user = _siteHelper.LoggedInUser(this);

            //activate the concession after the recall disabled it
            await _mediator.Send(new ActivateConcession(transactionalConcession.Concession.ReferenceNumber, user));

            //update the concession accordingly
            await UpdateTransactionalConcession(transactionalConcession, user);

            return Ok(_transactionalManager.GetTransactionalConcession(transactionalConcession.Concession.ReferenceNumber, user));
        }

        /// <summary>
        /// Latests the CRS or MRS.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <returns></returns>
        [Route("LatestCrsOrMrs/{riskGroupNumber}")]
        public IActionResult LatestCrsOrMrs(int riskGroupNumber)
        {
            return Ok(_transactionalManager.GetLatestCrsOrMrs(riskGroupNumber));
        }

        /// <summary>
        /// Transactionals the financial.
        /// </summary>
        /// <param name="riskGroupNumber">The risk group number.</param>
        /// <returns></returns>
        [Route("TransactionalFinancial/{riskGroupNumber}")]
        public IActionResult TransactionalFinancial(int riskGroupNumber)
        {
            return Ok(_transactionalManager.GetTransactionalFinancialForRiskGroupNumber(riskGroupNumber));
        }
    }
}
