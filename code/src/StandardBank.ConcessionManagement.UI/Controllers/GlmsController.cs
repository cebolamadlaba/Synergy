using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.BusinessLogic.Features.InvestmentConcession;
using StandardBank.ConcessionManagement.BusinessLogic.Features.Concession;
using StandardBank.ConcessionManagement.BusinessLogic.Features.ConcessionCondition;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface.Investment;
using StandardBank.ConcessionManagement.UI.Helpers.Interface;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.UI.Validation;
using StandardBank.ConcessionManagement.Model.UserInterface.Glms;
using StandardBank.ConcessionManagement.BusinessLogic.Features.GlmsConcession;

namespace StandardBank.ConcessionManagement.UI.Controllers
{
    [Produces("application/json")]
    [Route("api/Glms")]
    public class GlmsController : Controller
    {
        private readonly ISiteHelper _siteHelper;

        private readonly IGlmsManager _glmsManager;

        private readonly IMediator _mediator;

        private readonly ILookupTableManager _lookupTableManager;

        private readonly IBusinessCentreManager _bcmManager;

        public GlmsController(ISiteHelper siteHelper, IGlmsManager glmsManager, IMediator mediator,ILookupTableManager lookupTableManager,
            IBusinessCentreManager bcmManager)
        {
            _siteHelper = siteHelper;
            _glmsManager = glmsManager;
            _mediator = mediator;
            _lookupTableManager = lookupTableManager;
            _bcmManager = bcmManager;
        }

        /// <returns></returns>
        [Route("GlmsView/{riskGroupNumber}/{sapbpid}")]
        public IActionResult GlmsView(int riskGroupNumber, int sapbpid)
        {
            var user = _siteHelper.LoggedInUser(this);

            return Ok(_glmsManager.GetGlmsViewData(riskGroupNumber, sapbpid, user));
        }

        [Route("NewGlms")]
         [ValidateModel]
        public async Task<IActionResult> NewGlms([FromBody] GlmsConcession glmsConcession)
        {
            var user = _siteHelper.LoggedInUser(this);

            glmsConcession.Concession.ConcessionType = Constants.ConcessionType.Glms;
            glmsConcession.Concession.Type = Constants.ReferenceType.New;

            var concession = await _mediator.Send(new AddConcession(glmsConcession.Concession, user));

            foreach (var glmsConcessionDetail in glmsConcession.GlmsConcessionDetails)
                try
                {
                    await _mediator.Send(new BusinessLogic.Features.GlmsConcession.AddOrUpdateGlmsConcessionDetail(glmsConcessionDetail, user, concession));
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
               
        

            if (glmsConcession.ConcessionConditions != null && glmsConcession.ConcessionConditions.Any())
                foreach (var concessionCondition in glmsConcession.ConcessionConditions)
                    await _mediator.Send(new AddOrUpdateConcessionCondition(concessionCondition, user, concession));

            var bcmPendingStatusId = _lookupTableManager.GetSubStatusId(Constants.ConcessionSubStatus.NewSubmission);

            if (!string.IsNullOrWhiteSpace(glmsConcession.Concession.Comments))
                await _mediator.Send(new AddConcessionComment(concession.Id, bcmPendingStatusId,
                    glmsConcession.Concession.Comments, user));

            return Ok(glmsConcession);

        }

        /// <summary>
        /// Gets the glms concession data for the concession reference id specified
        /// </summary>
        /// <param name="concessionReferenceId"></param>
        /// <returns></returns>
        [Route("GlmsConcessionData/{concessionReferenceId}")]
        public IActionResult GlmsConcessionData(string concessionReferenceId)
        {
            return Ok(_glmsManager.GetGlmsConcession(concessionReferenceId, _siteHelper.LoggedInUser(this)));
        }

        [Route("UpdateGlms")]
        [ValidateModel]
        public async Task<IActionResult> UpdateGlms([FromBody] GlmsConcession glmsConcession)
        {
            var user = _siteHelper.LoggedInUser(this);

            await UpdateGlmsConcession(glmsConcession, user);

            return Ok(_glmsManager.GetGlmsConcession(glmsConcession.Concession.ReferenceNumber, user));
        }

        private async Task UpdateGlmsConcession(GlmsConcession glmsConcession, User user)
        {
            var databaseGlmsConcession =
                _glmsManager.GetGlmsConcession(glmsConcession.Concession.ReferenceNumber, user);

            //if there are any conditions that have been removed, delete them
            foreach (var condition in databaseGlmsConcession.ConcessionConditions)
                if (glmsConcession.ConcessionConditions.All(_ => _.ConcessionConditionId != condition.ConcessionConditionId))
                    await _mediator.Send(new DeleteConcessionCondition(condition, user));

            //if there are any concession details that have been removed delete them
            foreach (var glmsConcessionDetail in databaseGlmsConcession.GlmsConcessionDetails)
                if (glmsConcession.GlmsConcessionDetails.All(_ => _.GlmsConcessionDetailId !=
                                                                  glmsConcessionDetail.GlmsConcessionDetailId))
                    await _mediator.Send(new DeleteGlmsConcessionDetail(glmsConcessionDetail, user));

            if (!glmsConcession.Concession.AENumberUserId.HasValue)
                glmsConcession.Concession.AENumberUserId = databaseGlmsConcession.Concession.AENumberUserId;

            //update the concession
            var concession = await _mediator.Send(new UpdateConcession(glmsConcession.Concession, user));

            //add all the new conditions and details and comments
            foreach (var glmsConcessionDetail in glmsConcession.GlmsConcessionDetails)
                await _mediator.Send(new AddOrUpdateGlmsConcessionDetail(glmsConcessionDetail, user, concession));

            if (glmsConcession.ConcessionConditions != null && glmsConcession.ConcessionConditions.Any())
                foreach (var concessionCondition in glmsConcession.ConcessionConditions)
                    await _mediator.Send(new AddOrUpdateConcessionCondition(concessionCondition, user, concession));

            if (!string.IsNullOrWhiteSpace(glmsConcession.Concession.Comments))
            {
                await _mediator.Send(new AddConcessionComment(concession.Id, databaseGlmsConcession.Concession.SubStatusId,
                    glmsConcession.Concession.Comments, user));

            }

            if ((glmsConcession.Concession.SubStatus == Constants.ConcessionSubStatus.PcmApprovedWithChanges || glmsConcession.Concession.SubStatus == Constants.ConcessionSubStatus.HoApprovedWithChanges) && glmsConcession.Concession.ConcessionComments != null)
            {
                if (glmsConcession.Concession.ConcessionComments.Count() > 0 && glmsConcession.Concession.ConcessionComments.First().UserDescription == "LogChanges")
                {
                    await _mediator.Send(new AddConcessionComment(concession.Id, databaseGlmsConcession.Concession.SubStatusId, "LogChanges:" + glmsConcession.Concession.ConcessionComments.First().Comment, user));

                }
            }
        }

        [Route("UpdateRecalledGlms")]
        [ValidateModel]
        public async Task<IActionResult> UpdateRecalledGlms([FromBody] GlmsConcession glmsConcession)
        {
            var user = _siteHelper.LoggedInUser(this);

            //activate the concession after the recall disabled it
             await _mediator.Send(new ActivateConcession(glmsConcession.Concession.ReferenceNumber, user));
                 
   
            //update the concession accordingly
            await UpdateGlmsConcession(glmsConcession, user);
 

            return Ok(_glmsManager.GetGlmsConcession(glmsConcession.Concession.ReferenceNumber, user));
        }

        [Route("ExtendConcession/{concessionReferenceId}")]
        public async Task<IActionResult> ExtendConcession(string concessionReferenceId)
        {
            var user = _siteHelper.LoggedInUser(this);

            //get the concession details
            var glmsConcession =
                _glmsManager.GetGlmsConcession(concessionReferenceId, user);

            var parentConcessionId = glmsConcession.Concession.Id;

            //add a new concession using the old concession's details
            var newConcession = glmsConcession.Concession;
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

            glmsConcession.Concession = concession;

            //add all the new conditions and details
            foreach (var glmsConcessionDetail in glmsConcession.GlmsConcessionDetails)
            {
                glmsConcessionDetail.DateApproved = null;
                glmsConcessionDetail.GlmsConcessionDetailId = 0;
                try
                {
                    await _mediator.Send(new AddOrUpdateGlmsConcessionDetail(glmsConcessionDetail, user, concession));
                }
                catch(Exception ex)
                {
                    ex.ToString();
                }
              
            }

            if (glmsConcession.ConcessionConditions != null && glmsConcession.ConcessionConditions.Any())
            {
                foreach (var concessionCondition in glmsConcession.ConcessionConditions)
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

            var returnConcession = _glmsManager.GetGlmsConcession(concessionReferenceId, user);
            returnConcession.Concession.ChildReferenceNumber = concession.ReferenceNumber;
            return Ok(returnConcession);
        }
   
        [Route("ForwardGlmsPCM")]
        [ValidateModel]
        public async Task<IActionResult> ForwardGlmsPCM([FromBody] SearchConcessionDetail detail)
        {
            var user = _siteHelper.LoggedInUser(this);

            GlmsConcession glmsConcession = _glmsManager.GetGlmsConcession(detail.ReferenceNumber, user);

            glmsConcession.Concession.SubStatus = Constants.ConcessionSubStatus.PcmPending;
            glmsConcession.Concession.BcmUserId = _bcmManager.GetBusinessCentreManager(glmsConcession.Concession.CentreId).BusinessCentreManagerId;
            glmsConcession.Concession.Comments = "Manually forwarded by PCM";
            glmsConcession.Concession.IsInProgressForwarding = true;

           await _glmsManager.ForwardGlmsConcession(glmsConcession, user);

            return Ok(_glmsManager.GetGlmsConcession(detail.ReferenceNumber, user));
        }

        [Route("RenewGlms")]
        [ValidateModel]
        public async Task<IActionResult> RenewGlms([FromBody] GlmsConcession glmsConcession)
        {
            var user = _siteHelper.LoggedInUser(this);

            var returnConcession = await CreateChildConcession(glmsConcession, user, Constants.RelationshipType.Renewal);

            return Ok(returnConcession);
        }

        [Route("ResubmitGlms")]
        [ValidateModel]
        public async Task<IActionResult> ResubmitGlms([FromBody] GlmsConcession lendingConcession)
        {
            var user = _siteHelper.LoggedInUser(this);

            var returnConcession = await CreateChildConcession(lendingConcession, user, Constants.RelationshipType.Resubmit);

            return Ok(returnConcession);
        }

        [Route("UpdateApprovedGlms")]
        [ValidateModel]
        public async Task<IActionResult> UpdateApprovedGlms([FromBody] GlmsConcession lendingConcession)
        {
            var user = _siteHelper.LoggedInUser(this);

            var returnConcession = await CreateChildConcession(lendingConcession, user, Constants.RelationshipType.Update);

            return Ok(returnConcession);
        }
       
        private async Task<GlmsConcession> CreateChildConcession(GlmsConcession glmsConcession, User user, string relationship)
        {
    
            var parentGlmsConcession =
                _glmsManager.GetGlmsConcession(glmsConcession.Concession.ReferenceNumber, user);

            var parentConcessionId = parentGlmsConcession.Concession.Id;

            glmsConcession.Concession.ReferenceNumber = string.Empty;
            glmsConcession.Concession.ConcessionType = Constants.ConcessionType.Glms;
            glmsConcession.Concession.Type = Constants.ReferenceType.Existing;

            var concession = await _mediator.Send(new AddConcession(glmsConcession.Concession, user));

            foreach (var glmsConcessionDetail in glmsConcession.GlmsConcessionDetails)
            {
                if (relationship != Constants.RelationshipType.Extension)
                    glmsConcessionDetail.ExpiryDate = null;
                try
                {
                    await _mediator.Send(new AddOrUpdateGlmsConcessionDetail(glmsConcessionDetail, user, concession));
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
               
            }

            if (glmsConcession.ConcessionConditions != null && glmsConcession.ConcessionConditions.Any())
                foreach (var concessionCondition in glmsConcession.ConcessionConditions)
                    try { 
                    await _mediator.Send(new AddOrUpdateConcessionCondition(concessionCondition, user, concession));
        }
                catch (Exception ex)
                {
                    ex.ToString();
                }

    //link the new concession to the old concession
    var concessionRelationship = new ConcessionRelationship
            {
                CreationDate = DateTime.Now,
                UserId = user.Id,
                RelationshipDescription = relationship,
                ParentConcessionId = parentConcessionId,
                ChildConcessionId = concession.Id
            };
                try { 
                  await _mediator.Send(new AddConcessionRelationship(concessionRelationship, user));
                 }
                catch (Exception ex)
                {
                    ex.ToString();
                }

    var returnConcession =
                _glmsManager.GetGlmsConcession(parentGlmsConcession.Concession.ReferenceNumber, user);

            returnConcession.Concession.ChildReferenceNumber = concession.ReferenceNumber;
            return returnConcession;
        }
    }
}
