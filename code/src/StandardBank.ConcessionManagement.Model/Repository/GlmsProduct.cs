namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// TransactionType entity
    /// </summary>
    public class GlmsProduct
    {

        public int pkProductGlmsId { get; set; }

        public int fkRiskGroupId { get; set; }
  
        public int fkLegalEntityId { get; set; }
    
        public int fkGroupId { get; set; }

        public string GroupType { get; set; }

        public int fkInterestTypeId { get; set; }

        public int fkSlabTypeId { get; set; }

        public int fkInterestPricingCategoryId { get; set; }

        public int TierFrom { get; set; }

        public int TierTo { get; set; }

        public int RateType { get; set; }

        public int fkBaseRateCodeId { get; set; }

        public decimal Spread { get; set; }


    }
}
