namespace StandardBank.ConcessionManagement.Model.BusinessLogic.LetterGenerator
{
    public class GlmsConcessionLetter : BaseConcessionLetter
    {
        public string GroupNumber { get; set; }
        public string InterestPricingCategory { get; set; }
        public string InterestType { get; set; }
        public string SlabType { get; set; }
        public string RateType { get; set; }
        public string TierFrom { get; set; }
        public string BaseRate { get; set; }
        public string Spread { get; set; }
        public string Value { get; set; }
        public string ConcessionStartDate { get; set; }
        public string ConcessionEndDate { get; set; }
    }
}