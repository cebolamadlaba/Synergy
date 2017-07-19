using PricingConcessionsTool.API.Utils;
using PricingConcessionsTool.DTO;
using PricingConcessionsTool.DTO.Enums;
using PricingConcessionsTool.Services.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;

namespace PricingConcessionsTool.API.Controllers
{
    public class ConcessionController : ApiController
    {
        private IConcessionService _concessionService = null;

        private IPricingWorkflow _pricingWorkflow = null;

        public ConcessionController(IPricingWorkflow pricingWorkflow, IConcessionService concessionService)
        {
            _concessionService = concessionService;
            _pricingWorkflow = pricingWorkflow;
        }

        [HttpPost]
        public string GenerateLetters(List<Concession> concessionIds)
        {   //pdfBytes is a valid set of...pdfBytes, how it's generated is irrelevant
            return _pricingWorkflow.GenerateLetters(concessionIds.Select(c => c.ConcessionId).ToList());
        }

        public List<Concession> GetConcessions(string username, int roleId, bool pending)
        {
            return _concessionService.GetConcessions(username, roleId, pending);
        }

        public List<Concession> GetBCMConcessions(string username, bool pending)
        {
            return _concessionService.GetBCMConcessions(username, pending);
        }

        public RiskGroup GetRiskGroup(int riskGroupNumber)
        {
            return _concessionService.GetRiskGroup(riskGroupNumber);
        }

        public List<Concession> GetCustomerConcessions(int customerId,string concessionType, bool pending)
        {
            return _concessionService.GetCustomerConcessions(customerId, concessionType, pending, Util.GetUserName(this));
        }

        public List<Concession> GetCustomerProducts(int customerId, string concessionType)
        {
            return _concessionService.GetCustomerProducts(customerId, concessionType);
        }

        public Concession GetConcession(int concessionId)
        {
            return _concessionService.GetConcession(concessionId);
        }

        [HttpPost]
        public Result Forward(Concession concession)
        {
            concession.UserName = Util.GetUserName(this);
            return _pricingWorkflow.Forward(concession);
        }

        [HttpPost]
        public Result Decline(Concession concession)
        {
            concession.UserName = Util.GetUserName(this);
            return _pricingWorkflow.Decline(concession);
        }

        [HttpPost]
        public Result Approve(Concession concession)
        {
            concession.UserName = Util.GetUserName(this);
            return _pricingWorkflow.Approve(concession);
        }

        [HttpPost]
        public Result AcceptChanges(Concession concession)
        {
            concession.UserName = Util.GetUserName(this);
            return _pricingWorkflow.AcceptChanges(concession);
        }


        [HttpPost]
        public Result DeclineChanges(Concession concession)
        {
            concession.UserName = Util.GetUserName(this);
            return _pricingWorkflow.DeclineChanges(concession);
        }

        
    }
}
