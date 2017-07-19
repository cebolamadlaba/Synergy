using PricingConcessionsTool.DTO;
using PricingConcessionsTool.DTO.Enums;
using PricingConcessionsTool.Services.Interfaces;
using System.Web.Http;
using System;
using PricingConcessionsTool.API.Utils;

namespace PricingConcessionsTool.API.Controllers
{
    public class LendingController : ApiController
    {
        private IPricingWorkflow _pricingWorkflow;

        private IConcessionService _concessionService;
        public LendingController(IPricingWorkflow pricingWorkflow, IConcessionService concessionService)
        {
            _pricingWorkflow = pricingWorkflow;
            _concessionService = concessionService;
        }


        [HttpPost]
        public Result Save(ConcessionLending concessionLending)
        {
            if (concessionLending.ConcessionId == 0)
            {
                concessionLending.Status = ConcessionStatuses.Pending;

                concessionLending.SubStatus = ConcessionSubStatuses.BCMPending;

                concessionLending.ConcessionDate = DateTime.Now;

                concessionLending.Type = Types.New;


                concessionLending.DatesentForApproval = DateTime.Now;
            }

            concessionLending.ConcessionType = ConcessionTypes.Lending;

            return _pricingWorkflow.Save(concessionLending);
        }

        public FinancialInfo GetFinancialInfo(int customerId)
        {
            return _concessionService.GetFinancialInfo(customerId, ConcessionTypes.Lending);
        }        

        public Result Edit(ConcessionLending concessionLending)
        {
            concessionLending.Type = Types.Existing;

            concessionLending.ConcessionType = ConcessionTypes.Lending;

            return _pricingWorkflow.Edit(concessionLending);

        }

        [HttpPost]
        public Result ApproveWithChanges(ConcessionLending concession)
        {
            concession.UserName = Util.GetUserName(this);
            return _pricingWorkflow.ApproveWithChanges(concession);
        }

        [HttpPost]
        public Result RemoveConcession(ConcessionLending concession)
        {
            concession.Type = Types.Removal;
            concession.UserName = Util.GetUserName(this);
            return _pricingWorkflow.RemoveConcession(concession);
        }
    }
}
