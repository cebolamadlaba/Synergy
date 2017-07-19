using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PricingConcessionsTool.DTO.ReferenceData;

namespace PricingConcessionsTool.Services.Interfaces
{
    public interface IReferenceService
    {
        List<ProductType> GetProductTypes(int concessionTypeId);

        List<ConditionType> GetConditionTypes();

        List<ConditionProduct> GetConditionProducts(int conditionTypeId);
        List<Province> GetProvinces();
        List<MarketSegment> GetMarketSegments();

        List<ReviewFeeType> ReviewFeeTypes();
        List<TransactionType> GetTransactionTypes(int concessionTypeId);
        List<ChannelType> GetChannelTypes();
        List<BaseRate> GetBaseRates(int channelTypeId);
        List<TransactionGroup> GetTransactionGroups();
        List<BusinesOnlineTransactionType> GetBusinesOnlineTransactionTypes(int transactionGroupId);
        List<BusinesOnlineUser> GetBusinesOnlineUsers();
    }
}
