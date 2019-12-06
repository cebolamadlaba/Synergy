using System;

namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// GlmsTierData entity
    /// </summary>
    public class GlmsTierData
    {  
        public int Id { get; set; }
  
        public int GlmsConcessionId { get; set; }

        public decimal TierFrom { get; set; }

        public decimal TierTo { get; set; }

        public int RateTypeId { get; set; }

        public int BaseRateId { get; set; }

        public decimal Spread { get; set; }

        public decimal Value { get; set; }
    }
}
