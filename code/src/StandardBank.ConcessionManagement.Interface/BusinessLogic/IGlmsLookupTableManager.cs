using System.Collections.Generic;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.Model.UserInterface.Bol;
using StandardBank.ConcessionManagement.Model.UserInterface.Transactional;
using StandardBank.ConcessionManagement.Model.UserInterface.Trade;


namespace StandardBank.ConcessionManagement.Interface.BusinessLogic
{
    /// <summary>
    /// Glms Lookup table manager
    /// </summary>
    public interface IGlmsLookupTableManager
    {
     
        IEnumerable<GlmsGroup> GetGlmsGroups();

        IEnumerable<InterestPricingCategory> GetInterestPricingCategories();

        IEnumerable<InterestType> GetInterestTypes();

        IEnumerable<RateType> GetRateTypes();

        IEnumerable<SlabType> GetSlabTypes();

        IEnumerable<BaseRateCode> GetBaseRateCodes();
    }
}
