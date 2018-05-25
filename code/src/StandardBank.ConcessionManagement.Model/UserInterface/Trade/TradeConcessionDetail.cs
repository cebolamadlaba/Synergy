namespace StandardBank.ConcessionManagement.Model.UserInterface.Trade
{
    /// <summary>
    /// Cash concession detail entity
    /// </summary>
    public class TradeConcessionDetail : BaseConcessionDetail
    {
        public int TradeConcessionDetailId { get; set; }
        public string LegalEntity { get; set; }
        public string TradeProduct { get; set; }
        public string TradeProductType { get; set; } 
        public string ApprovedRate { get; set; }
        public string LoadedRate { get; set; }
        public int? fkConcessionId { get; set; }       
        public int? fkConcessionDetailId { get; set; }
        public string Communication { get; set; }
        public double EstablishmentFee { get; set; }
        public double FlatFee { get; set; }
        public string GBBNumber { get; set; }
        public double Max { get; set; }
        public double Min { get; set; }
        public int Term { get; set; }

    }
}
