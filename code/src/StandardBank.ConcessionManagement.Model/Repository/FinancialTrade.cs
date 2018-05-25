namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// FinancialCash entity
    /// </summary>
    public class FinancialTrade
    {
       
        public int Id { get; set; }
       
        public int RiskGroupId { get; set; }
      
        public decimal TotalAccounts { get; set; }
     
        public decimal AvgFee { get; set; }
      
        public decimal OverallForexMargin { get; set; }
        
    }
}
