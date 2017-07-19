
using System;
using System.Collections.Generic;
using PricingConcessionsTool.DTO.ReferenceData;
using PricingConcessionsTool.Services.Interfaces;
using PricingConcessionsTool.Core.Business.Interfaces;
using PricingConcessionsTool.Core.Business.Classes;

namespace PricingConcessionsTool.Services.Services
{
    public class ReferenceDataService : IReferenceService
    {
        private readonly IReferenceServiceContext _referenceServiceContext;

        private readonly IlogContext _log;

        public ReferenceDataService()
        {
            _referenceServiceContext = new ReferenceServiceContext();

            _log = new LogContext();
        }
        public List<ProductType> GetProductTypes(int concessionTypeId)
        {
            try
            {
                return _referenceServiceContext.GetProductTypes(concessionTypeId);
            }
            catch (Exception ex)
            {
                _log.LogException(ex);

                throw new Exception("Failed while trying process your request");
            }
        }


        public List<Province> GetProvinces()
        {
            return new List<Province>()
            {
                new Province { ProvinceId = 1, Description ="Gauteng" },
                 new Province { ProvinceId = 2, Description ="KZN" }
            };
        }

        public List<ConditionType> GetConditionTypes()
        {
            try
            { 
            return _referenceServiceContext.GetConditionTypes();
            }
            catch (Exception ex)
            {
                _log.LogException(ex);

                throw new Exception("Failed while trying process your request");
            }

        }

        public List<ConditionProduct> GetConditionProducts(int conditionTypeId)
        {
            try { 
            return _referenceServiceContext.GetConditionProducts(conditionTypeId);
            }
            catch (Exception ex)
            {
                _log.LogException(ex);

                throw new Exception("Failed while trying process your request");
            }

        }

        public List<MarketSegment> GetMarketSegments()
        {
            try
            {
            return _referenceServiceContext.GetMarketSegments();
            }
            catch (Exception ex)
            {
                _log.LogException(ex);

                throw new Exception("Failed while trying process your request");
            }

        }

        public List<ReviewFeeType> ReviewFeeTypes()
        {
            try
            {
                return _referenceServiceContext.ReviewFeeTypes();
            }
            catch (Exception ex)
            {
                _log.LogException(ex);

                throw new Exception("Failed while trying process your request");
            }
        }

        public List<TransactionType> GetTransactionTypes(int concessionTypeId)
        {
            try
            {
                return _referenceServiceContext.GetTransactionTypes(concessionTypeId);
            }
            catch (Exception ex)
            {
                _log.LogException(ex);

                throw new Exception("Failed while trying process your request");
            }
        }

        public List<ChannelType> GetChannelTypes()
        {
            try
            {
                return _referenceServiceContext.GetChannelTypes();
            }
            catch (Exception ex)
            {
                _log.LogException(ex);

                throw new Exception("Failed while trying process your request");
            }
        }

        public List<BaseRate> GetBaseRates(int channelTypeId)
        {
            try
            {
                return _referenceServiceContext.GetBaseRates(channelTypeId);
            }
            catch (Exception ex)
            {
                _log.LogException(ex);

                throw new Exception("Failed while trying process your request");
            }
        }

        public List<TransactionGroup> GetTransactionGroups()
        {
            try
            {
                return _referenceServiceContext.GetTransactionGroups();
            }
            catch (Exception ex)
            {
                _log.LogException(ex);

                throw new Exception("Failed while trying process your request");
            }
        }

        public List<BusinesOnlineTransactionType> GetBusinesOnlineTransactionTypes(int transactionGroupId)
        {
            try
            {
                return _referenceServiceContext.GetBusinesOnlineTransactionTypes(transactionGroupId);
            }
            catch (Exception ex)
            {
                _log.LogException(ex);

                throw new Exception("Failed while trying process your request");
            }
        }

        public List<BusinesOnlineUser> GetBusinesOnlineUsers()
        {
            try
            {
                return _referenceServiceContext.GetBusinesOnlineUsers();
            }
            catch (Exception ex)
            {
                _log.LogException(ex);

                throw new Exception("Failed while trying process your request");
            }
        }
    }
}
