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
        public string ApprovedRate { get; set; }
        public string TierTo { get; set; }
        public int? TierFrom { get; set; }
        public int? GroupNumber { get; set; }
        public int? fkGlmsProductId { get; set; }    
        public int? fkLegalEntityAccountId { get; set; }
        public int? fkLegalEntityId { get; set; } 
        public double? BaseRate { get; set; }

    }
}
