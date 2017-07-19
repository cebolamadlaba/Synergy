using PricingConcessionsTool.DTO;
using PricingConcessionsTool.DTO.Enums;
using PricingConcessionsTool.Services.Interfaces;
using System.Web.Http;
using System;
using PricingConcessionsTool.Utils;

namespace PricingConcessionsTool.API.Controllers
{
    public class TradeController : ApiController
    {
        private IPricingWorkflow _pricingWorkflow;

        private IConcessionService _concessionService;
        public TradeController(IPricingWorkflow pricingWorkflow, IConcessionService concessionService)
        {
            _pricingWorkflow = pricingWorkflow;
            _concessionService = concessionService;
        }


        [HttpPost]
        public Result Save(ConcessionTrade concessionTrade)
        {
            if (concessionTrade.ConcessionId == 0)
            {
                concessionTrade.Status = ConcessionStatuses.Pending;

                concessionTrade.ConcessionDate = DateTime.Now;

                concessionTrade.SubStatus = ConcessionSubStatuses.BCMPending;

                concessionTrade.DatesentForApproval = DateTime.Now;
            }
            concessionTrade.Type = Types.New;

            concessionTrade.ConcessionType = ConcessionTypes.Trade;

            return _pricingWorkflow.Save(concessionTrade);
        }

        public FinancialInfo GetFinancialInfo(int customerId)
        {
            return _concessionService.GetFinancialInfo(customerId, ConcessionTypes.Trade);
        }        

        public Result Edit(ConcessionTrade concessionTrade)
        {
            concessionTrade.Type = Types.Existing;

            concessionTrade.ConcessionType = ConcessionTypes.Trade;

            return _pricingWorkflow.Edit(concessionTrade);
        }

        [HttpPost]
        public Result ApproveWithChanges(ConcessionTrade concession)
        {
            concession.UserName = Util.GetUserName(this);
            return _pricingWorkflow.ApproveWithChanges(concession);
        }

        [HttpPost]
        public Result RemoveConcession(ConcessionTrade concession)
        {
            concession.Type = Types.Removal;
            concession.UserName = Util.GetUserName(this);
            return _pricingWorkflow.RemoveConcession(concession);
        }
    }
}
