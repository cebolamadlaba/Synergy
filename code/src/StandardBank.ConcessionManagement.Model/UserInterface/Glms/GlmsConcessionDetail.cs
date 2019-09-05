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
        public string GlmsProductType { get; set; } 
        public string ApprovedRate { get; set; }
        public string LoadedRate { get; set; }
        public int? fkConcessionId { get; set; }       
        public int? fkConcessionDetailId { get; set; }

        public int? ProductTypeId { get; set; }

        public int Term { get; set; }        

        public int? fkGlmsProductId { get; set; }
       
        public int? fkLegalEntityAccountId { get; set; }

        public int? fkLegalEntityGBBNumber { get; set; }

        public int? fkLegalEntityId { get; set; } 

        public double? Balance { get; set; }

    }
}
