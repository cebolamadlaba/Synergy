namespace StandardBank.ConcessionManagement.Model.Repository
{
    /// <summary>
    /// FinancialCash entity
    /// </summary>
    public class FinancialBol
    {
       
        public int Id { get; set; }
       
        public int RiskGroupId { get; set; }
      
        public decimal TotalPayments { get; set; }
     
        public decimal TotalCollections { get; set; }
      
        public decimal TotalValueAdded { get; set; }
        
    }
}
