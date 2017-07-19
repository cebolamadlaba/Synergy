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
    public class TransactionalController : ApiController
    {
        private IPricingWorkflow _pricingWorkflow;

        private IConcessionService _concessionService;
        public TransactionalController(IPricingWorkflow pricingWorkflow, IConcessionService concessionService)
        {
            _pricingWorkflow = pricingWorkflow;
            _concessionService = concessionService;
        }


        [HttpPost]
        public Result Save(ConcessionTransactional concessionTransactional)
        {
            if (concessionTransactional.ConcessionId == 0)
            {
                concessionTransactional.Status = ConcessionStatuses.Pending;

                concessionTransactional.ConcessionDate = DateTime.Now;

                concessionTransactional.SubStatus = ConcessionSubStatuses.BCMPending;

                concessionTransactional.DatesentForApproval = DateTime.Now;
            }

            concessionTransactional.Type = Types.New;


            concessionTransactional.ConcessionType = ConcessionTypes.Transactional;

            return _pricingWorkflow.Save(concessionTransactional);
        }

        public FinancialInfo GetFinancialInfo(int customerId)
        {
            return _concessionService.GetFinancialInfo(customerId, ConcessionTypes.Transactional);
        }

        public Result Edit(ConcessionTransactional concessionTransactional)
        {
            concessionTransactional.Type = Types.Existing;

            concessionTransactional.ConcessionType = ConcessionTypes.Transactional;

            return _pricingWorkflow.Edit(concessionTransactional);
        }

        [HttpPost]
        public Result ApproveWithChanges(ConcessionTransactional concession)
        {
            concession.UserName = Util.GetUserName(this);
            return _pricingWorkflow.ApproveWithChanges(concession);
        }

        [HttpPost]
        public Result RemoveConcession(ConcessionTransactional concession)
        {
            concession.Type = Types.Removal;
            concession.UserName = Util.GetUserName(this);
            return _pricingWorkflow.RemoveConcession(concession);
        }
    }
}
