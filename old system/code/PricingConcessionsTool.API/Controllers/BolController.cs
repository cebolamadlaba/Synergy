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
    public class BolController : ApiController
    {
        private IPricingWorkflow _pricingWorkflow;

        private IConcessionService _concessionService;
        public BolController(IPricingWorkflow pricingWorkflow, IConcessionService concessionService)
        {
            _pricingWorkflow = pricingWorkflow;
            _concessionService = concessionService;
        }

        [HttpPost]
        public Result Save(ConcessionBol concessionBol)
        {
            if (concessionBol.ConcessionId == 0)
            {
                concessionBol.Type = Types.New;

                concessionBol.Status = ConcessionStatuses.Pending;

                concessionBol.ConcessionDate = DateTime.Now;

                concessionBol.SubStatus = ConcessionSubStatuses.BCMPending;

                concessionBol.DatesentForApproval = DateTime.Now;
            }
            concessionBol.ConcessionType = ConcessionTypes.Bol;

            return _pricingWorkflow.Save(concessionBol);
        }

        public FinancialInfo GetFinancialInfo(int customerId)
        {
            return _concessionService.GetFinancialInfo(customerId, ConcessionTypes.Bol);
        }

        public Result Edit(ConcessionBol concessionBol)
        {
            concessionBol.Type = Types.Existing;

            concessionBol.ConcessionType = ConcessionTypes.Bol;

            return _pricingWorkflow.Edit(concessionBol);
        }


        [HttpPost]
        public Result RemoveConcession(ConcessionBol concession)
        {
            concession.Type = Types.Removal;
            concession.UserName = Util.GetUserName(this);
            return _pricingWorkflow.RemoveConcession(concession);
        }

        [HttpPost]
        public Result ApproveWithChanges(ConcessionBol concession)
        {
            concession.UserName = Util.GetUserName(this);
            return _pricingWorkflow.ApproveWithChanges(concession);
        }
    }
}
