namespace StandardBank.ConcessionManagement.Model.UserInterface.Bol
{
    /// <summary>
    /// Cash concession detail entity
    /// </summary>
    public class BolConcessionDetail : BaseConcessionDetail
    {
        public int BolConcessionDetailId { get; set; }

        public string LegalEntity { get; set; }

        public string BolUserID{ get; set; }

        public string ChargeCodeDesc { get; set; }

        public string ChargeCode { get; set; }

        public int ChargeLength { get; set; }

        public string ApprovedRate { get; set; }

        public string LoadedRate { get; set; }
    }
}
