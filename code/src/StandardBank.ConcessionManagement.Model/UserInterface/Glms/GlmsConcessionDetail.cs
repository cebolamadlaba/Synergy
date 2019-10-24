using System.Collections.Generic;

namespace StandardBank.ConcessionManagement.Model.UserInterface.Glms
{
    /// <summary>
    /// Glms concession detail entity
    /// </summary>
    public class GlmsConcessionDetail : BaseConcessionDetail
    {
        public int GlmsConcessionDetailId { get; set; }
        public string LegalEntity { get; set; }
        public string GlmsProduct { get; set; }
        public int interestPricingCategoryId { get; set; }
        public int? GroupNumber { get; set; }
        public int? fkGlmsProductId { get; set; }    
        public int? fkLegalEntityAccountId { get; set; }
        public int? fkLegalEntityId { get; set; }
        public int InterestTypeId { get; set; }
        public int SlabTypeId { get; set; }
        public int GlmsGroupId { get; set; }
        public string InterestPricingCategory { get; set; }
 
        public IEnumerable<GlmsTierData> GlmsTierData { get; set; }

        public IEnumerable<GlmsTierDataView> GlmsTierDataView { get; set; }


    }
}
