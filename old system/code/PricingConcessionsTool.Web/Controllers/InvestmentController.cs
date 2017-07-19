using PricingConcessionsTool.DTO;
using PricingConcessionsTool.DTO.Enums;
using PricingConcessionsTool.Services.Interfaces;
using System.Web.Http;
using System;
using PricingConcessionsTool.Utils;

namespace PricingConcessionsTool.API.Controllers
{
    public class InvestmentController : ApiController
    {
        private IPricingWorkflow _pricingWorkflow;

        private IConcessionService _concessionService;
        public InvestmentController(IPricingWorkflow pricingWorkflow, IConcessionService concessionService)
        {
            _pricingWorkflow = pricingWorkflow;
            _concessionService = concessionService;
        }


        [HttpPost]
        public Result Save(ConcessionInvestment concessionInvestment)
        {
            if (concessionInvestment.ConcessionId == 0)
            {
                concessionInvestment.Status = ConcessionStatuses.Pending;

                concessionInvestment.ConcessionDate = DateTime.Now;

                concessionInvestment.SubStatus = ConcessionSubStatuses.BCMPending;

                concessionInvestment.DatesentForApproval = DateTime.Now;
            }

            concessionInvestment.Type = Types.New;


            concessionInvestment.ConcessionType = ConcessionTypes.Investment;

            return _pricingWorkflow.Save(concessionInvestment);
        }

        public FinancialInfo GetFinancialInfo(int customerId)
        {
            return _concessionService.GetFinancialInfo(customerId, ConcessionTypes.Investment);
        }        

        public Result Edit(ConcessionInvestment concession)
        {
            concession.Type = Types.Existing;

            concession.ConcessionType = ConcessionTypes.Investment;

            return _pricingWorkflow.Edit(concession);
        }

        [HttpPost]
        public Result ApproveWithChanges(ConcessionInvestment concession)
        {
            concession.UserName = Util.GetUserName(this);
            return _pricingWorkflow.ApproveWithChanges(concession);
        }

        [HttpPost]
        public Result RemoveConcession(ConcessionInvestment concession)
        {
            concession.Type = Types.Removal;
            concession.UserName = Util.GetUserName(this);
            return _pricingWorkflow.RemoveConcession(concession);
        }
    }
}
