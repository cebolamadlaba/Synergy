using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.BusinessLogic.Features.BolConcession;
using StandardBank.ConcessionManagement.BusinessLogic.Features.Concession;
using StandardBank.ConcessionManagement.BusinessLogic.Features.ConcessionCondition;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface.Bol;
using StandardBank.ConcessionManagement.UI.Helpers.Interface;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.UI.Validation;

namespace StandardBank.ConcessionManagement.UI.Controllers
{
 
    [Produces("application/json")]
    [Route("api/Bol")]
    public class BolController : Controller
    {
        /// <summary>
        /// The site helper
        /// </summary>
        private readonly ISiteHelper _siteHelper;

        /// <summary>
        /// The bol manager
        /// </summary>
        private readonly IBolManager _bolManager;

        /// <summary>
        /// The mediator
        /// </summary>
        private readonly IMediator _mediator;

        private readonly IBusinessCentreManager _bcmManager;
        private readonly ILookupTableManager _lookupTableManager;

     
        public BolController(ISiteHelper siteHelper, IBolManager bolManager, IMediator mediator,  IBusinessCentreManager businessCentreManager, ILookupTableManager lookupTableManager)
        {
            _siteHelper = siteHelper;
            _bolManager = bolManager;
            _mediator = mediator;
            _bcmManager = businessCentreManager;
            _lookupTableManager = lookupTableManager;
        }
     
        /// <returns></returns>
        [Route("BolView/{riskGroupNumber}")]
        public IActionResult BolView(int riskGroupNumber)
        {
            return Ok(_bolManager.GetBolViewData(riskGroupNumber));
        }

     
        [Route("NewBol")]
        [ValidateModel]
        public async Task<IActionResult> NewBol([FromBody] BolConcession bolConcession)
        {
            var user = _siteHelper.LoggedInUser(this);

            bolConcession.Concession.ConcessionType = Constants.ConcessionType.BusinessOnline;
            bolConcession.Concession.Type = Constants.ReferenceType.New;

            var concession = await _mediator.Send(new AddConcession(bolConcession.Concession, user));

            foreach (var bolConcessionDetail in bolConcession.BolConcessionDetails)
                await _mediator.Send(new BusinessLogic.Features.BolConcession.AddOrUpdateBolConcessionDetail(bolConcessionDetail, user, concession));

            if (bolConcession.ConcessionConditions != null && bolConcession.ConcessionConditions.Any())
                foreach (var concessionCondition in bolConcession.ConcessionConditions)
                    await _mediator.Send(new AddOrUpdateConcessionCondition(concessionCondition, user, concession));


            var bcmPendingStatusId = _lookupTableManager.GetSubStatusId(Constants.ConcessionSubStatus.NewSubmission);

            if (!string.IsNullOrWhiteSpace(bolConcession.Concession.Comments))
                await _mediator.Send(new AddConcessionComment(concession.Id, bcmPendingStatusId,
                    bolConcession.Concession.Comments, user));

            return Ok(bolConcession);
        }



        [Route("createupdateBOLChargeCode")]
        [ValidateModel]
        public async Task<IActionResult> CreateUpdateBOLChargeCode([FromBody]BOLChargeCode bolChargecode)
        {
            var user = _siteHelper.LoggedInUser(this);

            var returned =_bolManager.CreateUpdateBOLChargeCode(bolChargecode);        

            return Ok(returned);
        }

        [Route("NewBolChargeCodeType")]
        [ValidateModel]
        public async Task<IActionResult> NewBolChargeCodeType([FromBody]BOLChargeCodeType bolChargecodeType)
        {
            var user = _siteHelper.LoggedInUser(this);

            var returned = _bolManager.CreateBOLChargeCodeType(bolChargecodeType);

            return Ok(returned);
        }


        [Route("UpdateBol")]
        [ValidateModel]
        public async Task<IActionResult> UpdateBol([FromBody] BolConcession bolConcession)
        {
            var user = _siteHelper.LoggedInUser(this);

            await UpdateBolConcession(bolConcession, user);

            return Ok(_bolManager.GetBolConcession(bolConcession.Concession.ReferenceNumber, user));
        }

        [Route("UpdateApprovedBol")]
        [ValidateModel]
        public async Task<IActionResult> UpdateApprovedBol([FromBody] BolConcession bolConcession)
        {
            var user = _siteHelper.LoggedInUser(this);

            var returnConcession = await CreateChildConcession(bolConcession, user, Constants.RelationshipType.Update);

            return Ok(returnConcession);
        }

        [Route("BolConcessionData/{concessionReferenceId}")]
        public IActionResult BolConcessionData(string concessionReferenceId)
        {
            return Ok(_bolManager.GetBolConcession(concessionReferenceId, _siteHelper.LoggedInUser(this)));
        }


        private async Task UpdateBolConcession(BolConcession bolConcession, User user)
        {
            var databaseBolConcession =
                _bolManager.GetBolConcession(bolConcession.Concession.ReferenceNumber, user);

            //if there are any conditions that have been removed, delete them
            foreach (var condition in databaseBolConcession.ConcessionConditions)
                if (bolConcession.ConcessionConditions.All(_ => _.ConcessionConditionId != condition.ConcessionConditionId))
                    await _mediator.Send(new DeleteConcessionCondition(condition, user));

            //if there are any bol concession details that have been removed delete them
            foreach (var bolConcessionDetail in databaseBolConcession.BolConcessionDetails)
                if (bolConcession.BolConcessionDetails.All(_ => _.BolConcessionDetailId !=
                                                                  bolConcessionDetail.BolConcessionDetailId))
                    await _mediator.Send(new DeleteBolConcessionDetail(bolConcessionDetail, user));

            //update the concession
            var concession = await _mediator.Send(new UpdateConcession(bolConcession.Concession, user));

            //add all the new conditions and bol details and comments
            foreach (var bolConcessionDetail in bolConcession.BolConcessionDetails)
                await _mediator.Send(new AddOrUpdateBolConcessionDetail(bolConcessionDetail, user, concession));

            if (bolConcession.ConcessionConditions != null && bolConcession.ConcessionConditions.Any())
                foreach (var concessionCondition in bolConcession.ConcessionConditions)
                    await _mediator.Send(new AddOrUpdateConcessionCondition(concessionCondition, user, concession));

            if (!string.IsNullOrWhiteSpace(bolConcession.Concession.Comments))
                await _mediator.Send(new AddConcessionComment(concession.Id, databaseBolConcession.Concession.SubStatusId,
                    bolConcession.Concession.Comments, user));


            if (bolConcession.Concession.Comments == "Approved With Changes" && bolConcession.Concession.ConcessionComments != null)
            {
                if (bolConcession.Concession.ConcessionComments.Count() > 0 && bolConcession.Concession.ConcessionComments.First().UserDescription == "LogChanges")
                {
                    await _mediator.Send(new AddConcessionComment(concession.Id, databaseBolConcession.Concession.SubStatusId, "LogChanges:" + bolConcession.Concession.ConcessionComments.First().Comment, user));


                }
            }
        }


        [Route("UpdateRecalledBol")]
        [ValidateModel]
        public async Task<IActionResult> UpdateRecalledBol([FromBody] BolConcession bolConcession)
        {
            var user = _siteHelper.LoggedInUser(this);

            //activate the concession after the recall disabled it
            await _mediator.Send(new ActivateConcession(bolConcession.Concession.ReferenceNumber, user));

            //update the concession accordingly
            await UpdateBolConcession(bolConcession, user);

            return Ok(_bolManager.GetBolConcession(bolConcession.Concession.ReferenceNumber, user));
        }

        [Route("ExtendConcession/{concessionReferenceId}")]
        public async Task<IActionResult> ExtendConcession(string concessionReferenceId)
        {
            var user = _siteHelper.LoggedInUser(this);

            //get the bol concession details
            var bolConcession =
                _bolManager.GetBolConcession(concessionReferenceId, user);

            var parentConcessionId = bolConcession.Concession.Id;

            //add a new concession using the old concession's details
            var newConcession = bolConcession.Concession;
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

            bolConcession.Concession = concession;

            //add all the new conditions and bol details
            foreach (var bolConcessionDetail in bolConcession.BolConcessionDetails)
            {
                bolConcessionDetail.DateApproved = null;
                bolConcessionDetail.BolConcessionDetailId = 0;
                await _mediator.Send(new AddOrUpdateBolConcessionDetail(bolConcessionDetail, user, concession));
            }

            if (bolConcession.ConcessionConditions != null && bolConcession.ConcessionConditions.Any())
            {
                foreach (var concessionCondition in bolConcession.ConcessionConditions)
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

            var returnConcession = _bolManager.GetBolConcession(concessionReferenceId, user);
            returnConcession.Concession.ChildReferenceNumber = concession.ReferenceNumber;
            return Ok(returnConcession);
        } 
     
    
        private async Task<BolConcession> CreateChildConcession(BolConcession bolConcession, User user, string relationship)
        {
            //get the parent bol concession details
            var parentBolConcession = _bolManager.GetBolConcession(bolConcession.Concession.ReferenceNumber, user);

            var parentConcessionId = parentBolConcession.Concession.Id;

            bolConcession.Concession.ReferenceNumber = string.Empty;
            bolConcession.Concession.ConcessionType = Constants.ConcessionType.BusinessOnline;
            bolConcession.Concession.Type = Constants.ReferenceType.Existing;

            var concession = await _mediator.Send(new AddConcession(bolConcession.Concession, user));

            foreach (var bolConcessionDetail in bolConcession.BolConcessionDetails)
                await _mediator.Send(new AddOrUpdateBolConcessionDetail(bolConcessionDetail, user, concession));

            if (bolConcession.ConcessionConditions != null && bolConcession.ConcessionConditions.Any())
                foreach (var concessionCondition in bolConcession.ConcessionConditions)
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

            var returnConcession = _bolManager.GetBolConcession(parentBolConcession.Concession.ReferenceNumber, user);
            returnConcession.Concession.ChildReferenceNumber = concession.ReferenceNumber;
            return returnConcession;
        }

        [Route("ForwardBolPCM")]
        [ValidateModel]
        public async Task<IActionResult> ForwardBolPCM([FromBody] SearchConcessionDetail detail)
        {
            var user = _siteHelper.LoggedInUser(this);

            var bolconsession = _bolManager.GetBolConcession(detail.ReferenceNumber, user);

            bolconsession.Concession.SubStatus = Constants.ConcessionSubStatus.PcmPending;
            bolconsession.Concession.BcmUserId = _bcmManager.GetBusinessCentreManager(bolconsession.Concession.CentreId).BusinessCentreManagerId;

            bolconsession.Concession.Comments = "Manually forwarded by PCM";
            bolconsession.Concession.IsInProgressForwarding = true;

           await _bolManager.ForwardBolConcession(bolconsession, user);

            return Ok(_bolManager.GetBolConcession(detail.ReferenceNumber, user));
        }

    


    }
}
