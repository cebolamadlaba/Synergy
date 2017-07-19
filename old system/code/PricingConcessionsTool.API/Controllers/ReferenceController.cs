
using PricingConcessionsTool.DTO.Enums;
using PricingConcessionsTool.DTO.ReferenceData;
using PricingConcessionsTool.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Web.Http;

namespace PricingConcessionsTool.API.Controllers
{
    public class ReferenceController : ApiController
    {
        private IReferenceService _referenceService;

        public ReferenceController(IReferenceService referenceService)
        {
            _referenceService = referenceService;
        }


        public List<ProductType> GetProductTypes(string concessionType)
        {
            var cType = Enum.Parse(typeof(ConcessionTypes), concessionType);

            return _referenceService.GetProductTypes((int)cType);
        }

        public List<ConditionType> GetConditionTypes()
        {
            return _referenceService.GetConditionTypes();
        }

        public List<ConditionProduct> GetConditionProducts(int conditionTypeId)
        {
            return _referenceService.GetConditionProducts(conditionTypeId);
        }     

        public List<Province> GetProvinces()
        {
            return _referenceService.GetProvinces();
        }


        public List<MarketSegment> GetMarketSegments()
        {
            return _referenceService.GetMarketSegments();
        }

        public List<ReviewFeeType> GetReviewFeeTypes()
        {
            return _referenceService.ReviewFeeTypes();
        }

        public List<TransactionType> GetTransactionTypes(string concessionType)
        {
            var cType = Enum.Parse(typeof(ConcessionTypes), concessionType);

            return _referenceService.GetTransactionTypes((int)cType);
        }

        public List<ChannelType> GetChannelTypes()
        {
            return _referenceService.GetChannelTypes();
        }

        public List<BaseRate> GetBaseRates(int channelTypeId)
        {            
            return _referenceService.GetBaseRates(channelTypeId);
        }

        public List<TransactionGroup> GetTransactionGroups()
        {
            return _referenceService.GetTransactionGroups();
        }

        public List<BusinesOnlineTransactionType> GetBusinesOnlineTransactionTypes(int transactionGroupId)
        {
            return _referenceService.GetBusinesOnlineTransactionTypes(transactionGroupId);
        }

        public List<BusinesOnlineUser> GetBusinesOnlineUsers()
        {
            return _referenceService.GetBusinesOnlineUsers();
        }

    }
}
