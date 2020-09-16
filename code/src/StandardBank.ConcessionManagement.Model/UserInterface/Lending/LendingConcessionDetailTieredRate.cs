namespace StandardBank.ConcessionManagement.Model.UserInterface.Lending
{

    public class LendingConcessionDetailTieredRate
    {

        public int Id { get; set; }

        public int ConcessionLendingId { get; set; }

        public decimal? Limit { get; set; }

        public decimal? MarginToPrime { get; set; }

        public decimal? ApprovedMap { get; set; }
    }
}
