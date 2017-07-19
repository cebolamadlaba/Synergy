using PricingConcessionsTool.DTO.ReferenceData;
using System.Collections.Generic;

namespace PricingConcessionsTool.DTO
{
    public class ConcessionLending : Concession
    {
        public ProductType ProductType { get; set; }

        public ReviewFeeType ReviewFeeType { get; set; }

        public decimal ? Limit { get; set; }
        
        public int? Term { get; set; }

        public decimal? MarginAbovePrime { get; set; }

        public decimal? ApprovedMarginAbovePrime { get; set; }

        public decimal? InitiationFee { get; set; }

        public decimal? ReviewFee { get; set; }
        
        public decimal? UnutilizedFacilityFee { get; set; }

    }

    public class ConcessionDto
    {
        public List<ConcessionLending> Concessions { get; set; }

        public List<ConcessionCondition> ConditionList { get; set; }
    }
}
