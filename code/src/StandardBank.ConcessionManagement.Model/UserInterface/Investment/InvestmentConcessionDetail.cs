namespace StandardBank.ConcessionManagement.Model.UserInterface.Investment
{
    /// <summary>
    /// Cash concession detail entity
    /// </summary>
    public class InvestmentConcessionDetail : BaseConcessionDetail
    {
        public int InvestmentConcessionDetailId { get; set; }
        public string LegalEntity { get; set; }
        public string InvestmentProduct { get; set; }
        public string InvestmentProductType { get; set; } 
        public string ApprovedRate { get; set; }
        public string LoadedRate { get; set; }
        public int? fkConcessionId { get; set; }       
        public int? fkConcessionDetailId { get; set; }
        public string Communication { get; set; }
        public double EstablishmentFee { get; set; }
        public double FlatFee { get; set; }
        public string GBBNumber { get; set; }
        public double max { get; set; }
        public double min { get; set; }
        public int term { get; set; }        

        public int? fkInvestmentProductId { get; set; }
       
        public int? fkLegalEntityAccountId { get; set; }

        public int? fkLegalEntityGBBNumber { get; set; }

        public int? fkLegalEntityId { get; set; }

        public decimal? AdValorem { get; set; }

        public string Currency { get; set; }





    }
}
