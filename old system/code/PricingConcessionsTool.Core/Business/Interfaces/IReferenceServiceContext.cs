using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PricingConcessionsTool.DTO.ReferenceData;

namespace PricingConcessionsTool.Core.Business.Interfaces
{
    public interface IReferenceServiceContext
    {
        List<ConditionType> GetConditionTypes();

        List<ConditionProduct> GetConditionProducts(int conditionTypeId);
        List<MarketSegment> GetMarketSegments();

        List<ProductType> GetProductTypes(int concessionTypeId);

        List<ReviewFeeType> ReviewFeeTypes();
        List<TransactionType> GetTransactionTypes(int concessionTypeId);
        List<ChannelType> GetChannelTypes();
        List<BaseRate> GetBaseRates(int channelTypeId);
        List<BusinesOnlineTransactionType> GetBusinesOnlineTransactionTypes(int transactionGroupId);
        List<TransactionGroup> GetTransactionGroups();
        List<BusinesOnlineUser> GetBusinesOnlineUsers();
    }
}
