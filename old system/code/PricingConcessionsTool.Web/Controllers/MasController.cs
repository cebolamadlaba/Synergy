using PricingConcessionsTool.DTO;
using PricingConcessionsTool.DTO.Enums;
using PricingConcessionsTool.Services.Interfaces;
using System.Web.Http;
using System;
using PricingConcessionsTool.Utils;

namespace PricingConcessionsTool.API.Controllers
{
    public class MasController : ApiController
    {
        private IPricingWorkflow _pricingWorkflow;

        private IConcessionService _concessionService;
        public MasController(IPricingWorkflow pricingWorkflow, IConcessionService concessionService)
        {
            _pricingWorkflow = pricingWorkflow;
            _concessionService = concessionService;
        }


        [HttpPost]
        public Result Save(ConcessionMas concessionMas)
        {
            if (concessionMas.ConcessionId == 0)
            {
                concessionMas.Status = ConcessionStatuses.Pending;

                concessionMas.ConcessionDate = DateTime.Now;

                concessionMas.SubStatus = ConcessionSubStatuses.BCMPending;

                concessionMas.DatesentForApproval = DateTime.Now;
            }

            concessionMas.Type = Types.New;

            concessionMas.ConcessionType = ConcessionTypes.Mas;

            return _pricingWorkflow.Save(concessionMas);
        }

        public FinancialInfo GetFinancialInfo(int customerId)
        {
            return _concessionService.GetFinancialInfo(customerId, ConcessionTypes.Mas);
        }        

        public Result Edit(ConcessionMas concession)
        {
            concession.Type = Types.Existing;

            concession.ConcessionType = ConcessionTypes.Mas;

            return _pricingWorkflow.Edit(concession);
        }

        [HttpPost]
        public Result ApproveWithChanges(ConcessionMas concession)
        {
            concession.UserName = Util.GetUserName(this);
            return _pricingWorkflow.ApproveWithChanges(concession);
        }

        [HttpPost]
        public Result RemoveConcession(ConcessionMas concession)
        {
            concession.Type = Types.Removal;
            concession.UserName = Util.GetUserName(this);
            return _pricingWorkflow.RemoveConcession(concession);
        }
    }
}
