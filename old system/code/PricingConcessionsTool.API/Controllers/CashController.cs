using PricingConcessionsTool.API.Utils;
using PricingConcessionsTool.DTO;
using PricingConcessionsTool.DTO.Enums;
using PricingConcessionsTool.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PricingConcessionsTool.API.Controllers
{
    public class CashController : ApiController
    {

        private IPricingWorkflow _pricingWorkflow;

        private IConcessionService _concessionService;
        public CashController(IPricingWorkflow pricingWorkflow, IConcessionService concessionService)
        {
            _pricingWorkflow = pricingWorkflow;
            _concessionService = concessionService;
        }


        [HttpPost]
        public Result Save(ConcessionCash concessionCash)
        {
            if (concessionCash.ConcessionId == 0)
            {
                concessionCash.Status = ConcessionStatuses.Pending;

                concessionCash.ConcessionDate = DateTime.Now;

                concessionCash.SubStatus = ConcessionSubStatuses.BCMPending;

                concessionCash.DatesentForApproval = DateTime.Now;
            }

            concessionCash.Type = Types.New;


            concessionCash.ConcessionType = ConcessionTypes.Cash;

            return _pricingWorkflow.Save(concessionCash);
        }

        public FinancialInfo GetFinancialInfo(int customerId)
        {
            return _concessionService.GetFinancialInfo(customerId, ConcessionTypes.Cash);
        }

        public Result Edit(ConcessionCash concessionCash)
        {
            concessionCash.Type = Types.Existing;

            concessionCash.ConcessionType = ConcessionTypes.Cash;

            return _pricingWorkflow.Edit(concessionCash);
        }


        [HttpPost]
        public Result ApproveWithChanges(ConcessionCash concession)
        {
            concession.UserName = Util.GetUserName(this);
            return _pricingWorkflow.ApproveWithChanges(concession);
        }

        [HttpPost]
        public Result RemoveConcession(ConcessionCash concession)
        {
            concession.Type = Types.Removal;
            concession.UserName = Util.GetUserName(this);
            return _pricingWorkflow.RemoveConcession(concession);
        }
    }
}
